using System.Collections.Generic;
using System.Linq;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {

            var terrorists = players.Where(x => x.GetType() == typeof(Terrorist)).ToList();
            var counterTerrorists = players.Where(x => x.GetType() == typeof(CounterTerrorist)).ToList();

            while (terrorists.Any(x => x.IsAlive)
            && counterTerrorists.Any(x => x.IsAlive))
            {

                foreach (var terro in terrorists)
                {
                    if (!terro.IsAlive)
                    {
                        continue;
                    }
                    foreach (var counterTerro in counterTerrorists)
                    {
                        if (!counterTerro.IsAlive)
                        {
                            continue;
                        }

                        counterTerro.TakeDamage(terro.Gun.Fire());
                    }

                }

                foreach (var counterTerro in counterTerrorists)
                {
                    if (!counterTerro.IsAlive)
                    {
                        continue;
                    }

                    foreach (var terro in terrorists)
                    {

                        if (!terro.IsAlive)
                        {
                            continue;
                        }
                        terro.TakeDamage(counterTerro.Gun.Fire());
                    }
                }

            }
            return terrorists.Any(x => x.IsAlive) 
                ? "Terrorist wins!" 
                : "Counter Terrorist wins!";
        }
    }
}