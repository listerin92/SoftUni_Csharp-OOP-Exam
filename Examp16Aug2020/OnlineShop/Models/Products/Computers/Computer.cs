using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        //private double overallPerformance;
        private double peripheralsAverageOverallPerformance;
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;
        private decimal price;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
            this.OverallPerformance = overallPerformance;
            this.price = price;
        }

        public IReadOnlyCollection<IComponent> Components
            => (IReadOnlyCollection<IComponent>)this.components;

        public IReadOnlyCollection<IPeripheral> Peripherals
            => (IReadOnlyCollection<IPeripheral>)this.peripherals;

        public override double OverallPerformance
        {
            get
            {
                if (!this.Components.Any())
                {
                    return base.overallPerformance;
                }

                return base.overallPerformance + this.Components.Average(a => a.OverallPerformance);
            }
        }

        public override decimal Price
        {
            get
            {
                var sumAllComponentPrice = this.Components.Sum(s => s.Price);
                var sumAllPeripheralPrice = this.Peripherals.Sum(s => s.Price);
                return sumAllComponentPrice + sumAllPeripheralPrice + this.price;
            }

        }

        public void AddComponent(IComponent component)
        {

            var existComponentType = this.Components.FirstOrDefault(c => c.GetType().Name == component.GetType().Name);
            if (existComponentType != null)
            {
                string message = string.Format(ExceptionMessages.ExistingComponent, component.Model,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }

            this.components.Add(component);
        }

        public IComponent RemoveComponent(string componentType)
        {
            var component = this.Components.FirstOrDefault(co => co.GetType().Name == componentType);
            if (component == null)
            {
                string message = string.Format(ExceptionMessages.NotExistingComponent, componentType,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this.components.Remove(component);
            return component;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            var peripheralToAdd = this.Peripherals.FirstOrDefault(p => p.GetType().Name == peripheral.GetType().Name);
            if (peripheralToAdd != null)
            {
                string message = string.Format(ExceptionMessages.ExistingPeripheral, peripheral.Model,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this.peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var peripheral = this.Peripherals.FirstOrDefault(co => co.GetType().Name == peripheralType);
            if (peripheral == null)
            {
                string message = string.Format(ExceptionMessages.NotExistingComponent, peripheralType,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this.peripherals.Remove(peripheral);
            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Overall Performance: {this.OverallPerformance:f2}. Price: {this.Price:f2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id})");
            sb.AppendLine($" Components ({this.Components.Count}):");
            foreach (var component in this.Components)
            {
                sb.AppendLine($"  {component}");
            }

            this.peripheralsAverageOverallPerformance = this.Peripherals.Sum(x => x.OverallPerformance) / this.Peripherals.Count;
            if (this.Peripherals.Count == 0)
            {
                this.peripheralsAverageOverallPerformance = 0.00;
            }

            sb.AppendLine($" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({this.peripheralsAverageOverallPerformance:f2}):");
            foreach (var peripheral in this.Peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
