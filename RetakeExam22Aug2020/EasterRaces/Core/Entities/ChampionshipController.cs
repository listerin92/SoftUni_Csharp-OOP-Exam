using System;
using System.Linq;
using System.Text;
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController

    {
        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private RaceRepository raceRepository;
        public ChampionshipController()
        {
            this.driverRepository = new DriverRepository();
            this.carRepository = new CarRepository();
            this.raceRepository = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);
            if (this.driverRepository.GetAll().Any(x=>x.Name == driverName))
            {
                string message = string.Format(ExceptionMessages.DriversExists, driverName);
                throw new ArgumentException(message);
            }

            this.driverRepository.Add(driver);
            string messageOut = string.Format(OutputMessages.DriverCreated, driverName);
            return messageOut;
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = null;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);

            }

            if (this.carRepository.GetAll().Any(x=>x.Model == model))
            {
                string message = string.Format(ExceptionMessages.CarExists, model);
                throw new ArgumentException(message);
            }
            this.carRepository.Add(car);

            string messageOut = string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
            return messageOut;
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = this.driverRepository.GetByName(driverName);
            if (driver == null)
            {
                string message = string.Format(ExceptionMessages.DriverNotFound, driverName);
                throw new InvalidOperationException(message);
            }
            ICar car = this.carRepository.GetByName(carModel);
            if (car == null)
            {
                string message = string.Format(ExceptionMessages.CarNotFound, carModel);
                throw new InvalidOperationException(message);
            }
            driver.AddCar(car);

            string messageOut = string.Format(OutputMessages.CarAdded, driver.Name, car.Model);
            return messageOut;

        }
        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race == null)
            {
                string message = string.Format(ExceptionMessages.RaceNotFound, raceName);
                throw new InvalidOperationException(message);
            }

            IDriver driver = driverRepository.GetByName(driverName);
            if (driver == null)
            {
                string message = string.Format(ExceptionMessages.DriverNotFound, driverName);
                throw new InvalidOperationException(message);
            }
            race.AddDriver(driver);

            string messageOut = string.Format(OutputMessages.DriverAdded, driver.Name, race.Name);
            return messageOut;
        }

        public string CreateRace(string name, int laps)
        {
            var existingRace = this.raceRepository.GetByName(name);
            if (existingRace != null)
            {
                string message = string.Format(ExceptionMessages.RaceExists, name);
                throw new InvalidOperationException(message);
            }
            IRace race = new Race(name, laps);

            this.raceRepository.Add(race);

            string messageOut = string.Format(OutputMessages.RaceCreated, race.Name);
            return messageOut;
        }



        public string StartRace(string raceName)
        {
            IRace race = this.raceRepository.GetByName(raceName);
            if (race == null)
            {
                string message = string.Format(ExceptionMessages.RaceNotFound, raceName);
                throw new InvalidOperationException(message);
            }

            var sortedDrivers = this.driverRepository
                .GetAll()
                .OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps))
                .ToList();

            if (sortedDrivers.Count < 3)
            {
                string message = string.Format(ExceptionMessages.RaceInvalid, raceName, 3);
                throw new InvalidOperationException(message);
            }

            this.raceRepository.Remove(race);
            StringBuilder sb = new StringBuilder();

            
                sb.AppendLine($"Driver {sortedDrivers[0].Name} wins {race.Name} race.");
                sb.AppendLine($"Driver {sortedDrivers[1].Name} is second in {race.Name} race.");
                sb.AppendLine($"Driver {sortedDrivers[2].Name} is third in {race.Name} race.");

                return sb.ToString().TrimEnd();
        }
    }
}
