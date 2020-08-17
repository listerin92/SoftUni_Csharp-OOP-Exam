using OnlineShop.Common.Enums;

namespace OnlineShop.Models.Products.Components
{
    public class PowerSupply : Component
    {
        private const double PERF_MULTIPLIER = 1.05;
        public PowerSupply(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance, generation)
        {
            this.OverallPerformance *= PERF_MULTIPLIER;
        }
        public override ComponentType ComponentType => ComponentType.PowerSupply;

    }
}