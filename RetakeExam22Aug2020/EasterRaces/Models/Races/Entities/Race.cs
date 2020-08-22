using System;
using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;

        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers = new List<IDriver>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = string.Format(ExceptionMessages.InvalidName, value, 5);
                    throw new ArgumentException(message);
                }

                this.name = value;
            }
        }

        public int Laps
        {
            get => this.laps;
            private set
            {
                if (value < 1)
                {
                    string message = string.Format(ExceptionMessages.InvalidNumberOfLaps, 1);
                    throw new ArgumentException(message);
                }

                this.laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers.AsReadOnly();
        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                string message = string.Format(ExceptionMessages.DriverInvalid);
                throw new ArgumentNullException(message);
            }

            if (!driver.CanParticipate)
            {
                string message = string.Format(ExceptionMessages.DriverNotParticipate, driver.Name);
                throw new ArgumentException(message);
            }

            if (drivers.Any(x=>x.Name == driver.Name))
            {
                string message = string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name);
                throw new ArgumentException(message);
            }
            this.drivers.Add(driver);
        }
    }
}
