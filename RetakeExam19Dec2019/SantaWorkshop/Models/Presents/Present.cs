using System;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Utilities.Messages;

namespace SantaWorkshop.Models.Presents
{
    public class Present : IPresent
    {
        private string name;
        private int energyRequired;

        public Present(string name, int energyRequired)
        {
            this.Name = name;
            this.EnergyRequired = energyRequired;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPresentName);
                }
                this.name = value;
            }
        }

        public int EnergyRequired
        {
            get => this.energyRequired;
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.energyRequired = value;
            }
        }
        public void GetCrafted()
        {
            this.EnergyRequired -= 10; //TODO Should I check again for below zero ?
        }

        public bool IsDone() => this.EnergyRequired == 0;
    }
}