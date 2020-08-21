using System.Collections.Generic;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            throw new System.NotImplementedException();
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            throw new System.NotImplementedException();
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            throw new System.NotImplementedException();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
            throw new System.NotImplementedException();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            throw new System.NotImplementedException();
        }

        public string BuyComputer(int id)
        {
            throw new System.NotImplementedException();
        }

        public string BuyBest(decimal budget)
        {
            throw new System.NotImplementedException();
        }

        public string GetComputerData(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
