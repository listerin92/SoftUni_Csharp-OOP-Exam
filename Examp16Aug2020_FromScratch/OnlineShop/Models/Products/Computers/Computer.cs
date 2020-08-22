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
        private readonly List<IComponent> _components;
        private readonly List<IPeripheral> _peripherals;
        private readonly double _overallPerformance;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this._components = new List<IComponent>();
            this._peripherals = new List<IPeripheral>();
            this._overallPerformance = overallPerformance;
        }

        public IReadOnlyCollection<IComponent> Components => _components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => _peripherals.AsReadOnly();

        public override double OverallPerformance
        {
            get
            {
                if (!this.Components.Any())
                {
                    return this._overallPerformance;
                }
                
                return base.OverallPerformance + this._components.Average(x => x.OverallPerformance);
            }
        }

        public override decimal Price
        {
            get
            {
                var componentPrice = this._components.Sum(x => x.Price);
                var peripheralPrice = this._peripherals.Sum(x => x.Price);

                return base.Price + componentPrice + peripheralPrice;
            }
        }

        public void AddComponent(IComponent component)
        {
            if (this._components.Any(x => x.GetType().Name == component.GetType().Name))
            {
                string message = string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this._components.Add(component);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!this._components.Any() || this._components.All(x => x.GetType().Name != componentType))
            {
                string message = string.Format(ExceptionMessages.NotExistingComponent,
                    componentType,
                    this.GetType().Name,
                    this.Id);
                throw new ArgumentException(message);
            }
            IComponent componentToRemove = this._components
                .First(x => x.GetType().Name == componentType);

            this._components.Remove(componentToRemove);
            return componentToRemove;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this._peripherals.Any(x =>
                x.GetType().Name == peripheral.GetType().Name))
            {
                string message = string.Format(ExceptionMessages.ExistingPeripheral,
                    peripheral.GetType().Name,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            this._peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!this._peripherals.Any() ||
                this._peripherals.All(x =>
                    x.GetType().Name != peripheralType))
            {
                string message = string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType,
                    this.GetType().Name, this.Id);
                throw new ArgumentException(message);
            }
            IPeripheral peripheralToRemove = this._peripherals
                .First(x => x.GetType().Name == peripheralType);

            this._peripherals.Remove(peripheralToRemove);
            return peripheralToRemove;
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

            var averageOverallPeripherals = 0.0d;
            if (this._peripherals.Any())
            {

                averageOverallPeripherals = this._peripherals.Average(x => x.OverallPerformance);
            }

            sb.AppendLine($" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({averageOverallPeripherals:F2}):");
            if (this._peripherals.Any())
            {
                foreach (var peripheral in this._peripherals)
                {
                    sb.AppendLine($"  {peripheral}");
                }

            }

            return sb.ToString().TrimEnd();
        }
    }
}
