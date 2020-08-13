using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Repositories.Contracts;
using CounterStrike.Utilities.Messages;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IGun> guns;
        private readonly IRepository<IPlayer> players;
        private readonly IMap map;

        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun;
            if (type == nameof(Pistol))
            {
                gun = new Pistol(name, bulletsCount);
            }
            else if (type == nameof(Rifle))
            {
                gun = new Rifle(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }
            this.guns.Add(gun);

            string message = string.Format(OutputMessages.SuccessfullyAddedGun, name);
            return message;
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gun = guns.FindByName(gunName);

            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            IPlayer player;

            if (type == nameof(CounterTerrorist))
            {
                player = new CounterTerrorist(username, health, armor, gun);
            }
            else if (type == nameof(Terrorist))
            {
                player = new Terrorist(username, health, armor, gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            this.players.Add(player);

            string message = string.Format(OutputMessages.SuccessfullyAddedPlayer, username);
            return message;
        }

        public string StartGame()
        {
            var allAlivePlayers = this.players.Models.Where(x => x.IsAlive).ToList();
            return map.Start(allAlivePlayers);
        }

        public string Report()
        {
            var playersToReport = this.players.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Username)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var player in playersToReport)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
