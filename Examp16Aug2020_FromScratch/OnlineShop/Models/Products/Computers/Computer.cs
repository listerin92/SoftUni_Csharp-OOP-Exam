using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private readonly IReadOnlyCollection<IComponent> _components;
        private readonly IReadOnlyCollection<IPeripheral> _peripherals;
        private double _overallPerformance;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this._components = new List<IComponent>();
            this._peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => _components;

        public IReadOnlyCollection<IPeripheral> Peripherals => _peripherals;

        public override double OverallPerformance
        {
            get
            {
                if (!this.Components.Any())
                {
                    return this._overallPerformance;
                }
                //TODO check logic!!!
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
            throw new System.NotImplementedException();
        }

        public IComponent RemoveComponent(string componentType)
        {
            throw new System.NotImplementedException();
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            throw new System.NotImplementedException();
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            throw new System.NotImplementedException();
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

            var averageOverallPeripherals = this._peripherals.Average(x => x.OverallPerformance);
            sb.AppendLine(
                $" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({averageOverallPeripherals}):");
            foreach (var peripheral in this._peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
