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
        private double peripheralsAverage;
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;
        private decimal price;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();

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
                var sumAllComponentPrice = this.Components.Any()
                ? this.Components.Sum(s => s.Price)
                    : 0;
                var sumAllPeripheralPrice = this.Peripherals.Any()
                ? this.Peripherals.Sum(s => s.Price)
                    : 0;
                return sumAllComponentPrice + sumAllPeripheralPrice + base.price;
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
            var peripheralType = peripheral.GetType().Name;
            var productExists = ProductExists(peripheralType, this.Peripherals);
            if (productExists)
            {
                string message = string.Format(ExceptionMessages.ExistingPeripheral, peripheral.Model, this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this.peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {

            var productExists = ProductExists(peripheralType, (IReadOnlyCollection<IPeripheral>)this.peripherals);

            if (!productExists)
            {
                string message = string.Format(ExceptionMessages.NotExistingComponent, peripheralType,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            var peripheral = this.peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheral);
            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({this.Components.Count}):");

            foreach (var component in this.Components)
            {
                sb.AppendLine($"  {component}");
            }

            this.peripheralsAverage = this.Peripherals.Any()
                ? this.Peripherals.Average(x => x.OverallPerformance)
                : 0;

            sb.AppendLine(
                $" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({peripheralsAverage:F2}):");

            foreach (var peripheral in this.Peripherals)
            {
                sb.AppendLine($"  {peripheral.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }

        private bool ProductExists<T>(string componentType, IReadOnlyCollection<T> collection)
        {
            return collection.Any(c => c.GetType().Name == componentType);
        }
    }
}
