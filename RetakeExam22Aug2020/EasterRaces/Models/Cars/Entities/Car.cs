using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private readonly int minHorsePower;
        private readonly int maxHorsePower;
        private int horsePower;

        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    string message = string.Format(ExceptionMessages.InvalidModel, value, 4);
                    throw new ArgumentException(message);
                }

                this.model = value;
            }
        }
        public double CubicCentimeters { get; private set; }

        public int HorsePower
        {
            get => this.horsePower;
            private set
            {
                if (minHorsePower > value || value > maxHorsePower)
                {
                    string message = string.Format(ExceptionMessages.InvalidHorsePower, value);
                    throw new ArgumentException(message);
                }

                this.horsePower = value;
            }

        }

        //TODO check logic !!!
        public double CalculateRacePoints(int laps)
        {
            return this.CubicCentimeters / this.HorsePower * laps;
        }
    }
}
