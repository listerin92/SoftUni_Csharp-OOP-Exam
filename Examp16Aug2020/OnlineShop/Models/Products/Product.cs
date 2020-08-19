using System;
using OnlineShop.Common.Constants;

namespace OnlineShop.Models.Products
{
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        protected decimal price;
        protected double overallPerformance;

        protected Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            this.Id = id;
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Price = price;
            this.OverallPerformance = overallPerformance;
        }
        public int Id
        {
            get => this.id;
            private set => this.id = value > 0 ? value :
                    throw new ArgumentException
                        (ExceptionMessages.InvalidProductId);

        }

        public string Manufacturer
        {
            get => this.manufacturer;
            private set => this.manufacturer = !string.IsNullOrWhiteSpace(value)
                    ? value
                    : throw new AggregateException
                        (ExceptionMessages.InvalidManufacturer);
        }

        public string Model
        {
            get => this.model;
            private set => this.model = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException
                    (ExceptionMessages.InvalidModel);
        }

        public virtual decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);
                }

                this.price = value;
            }

        }

        public virtual double OverallPerformance
        {
            get => this.overallPerformance;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException
                           (ExceptionMessages.InvalidOverallPerformance);
                }
                this.overallPerformance = value;
            }
        }

        
        public override string ToString()
        {
            return $"Overall Performance: {this.OverallPerformance:F2}. Price: {this.Price:F2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id})";
        }
    }
}