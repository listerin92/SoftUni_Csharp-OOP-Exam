using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly ICollection<IComputer> computers;
        private readonly ICollection<IPeripheral> peripherals;
        private readonly ICollection<IComponent> components;
        public Controller()
        {
            this.computers = new List<IComputer>();
            this.peripherals = new List<IPeripheral>();
            this.components = new List<IComponent>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            var existiongComputer = this.computers.FirstOrDefault(c => c.Id == id);
            if (existiongComputer!= null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }
            Computer computer = computerType switch
            {
                nameof(DesktopComputer) => new DesktopComputer(id, manufacturer, model, price),
                nameof(Laptop) => new Laptop(id, manufacturer, model, price),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };
            this.computers.Add(computer);
            string message = string.Format(SuccessMessages.AddedComputer, computer.Id);
            return message;
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            CheckExistingComputer(computerId);
            CheckExistingComponent(id);

            IComponent component = componentType switch
            {
                nameof(CentralProcessingUnit) => new CentralProcessingUnit(id, manufacturer, model, price,
                    overallPerformance, generation),
                nameof(Motherboard) => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                nameof(PowerSupply) => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                nameof(RandomAccessMemory) => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance,
                    generation),
                nameof(SolidStateDrive) => new SolidStateDrive(id, manufacturer, model, price, overallPerformance,
                    generation),
                nameof(VideoCard) => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };
            IComputer computer = this.computers.FirstOrDefault(x => x.Id == computerId);
            computer.AddComponent(component);
            this.components.Add(component);


            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }


        public string RemoveComponent(string componentType, int computerId)
        {
            var computerWithID = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (computerWithID == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            var component = computerWithID.RemoveComponent(componentType);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            CheckExistingComputer(computerId);
            if (this.peripherals.Any(p=>p.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }
            IPeripheral peripheral = peripheralType switch
            {
                nameof(Headset) => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Keyboard) => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Monitor) => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Mouse) => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };
            IComputer computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            this.peripherals.Add(peripheral);
            computer.AddPeripheral(peripheral);


            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {

            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            var peripheral =  computer.RemovePeripheral(peripheralType);

            this.peripherals.Remove(peripheral);
            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string BuyComputer(int id)
        {
            var computer = this.computers.FirstOrDefault(x => x.Id == id);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            this.computers.Remove(computer);
            return computer.ToString();
        }
        public string BuyBest(decimal budget)
        {
            var computer = this.computers
                .Where(price => price.Price <= budget)
                .OrderByDescending(x => x.OverallPerformance)
                .FirstOrDefault();

            if (computer == null)
            {
                string message = string.Format(ExceptionMessages.CanNotBuyComputer, budget);
                throw new ArgumentException(message);
            }
            this.computers.Remove(computer);
            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            var computer = this.computers.FirstOrDefault(x => x.Id == id);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            return computer.ToString();
        }

        private void CheckExistingComputer(int computerId)
        {
            var existingComputer = this.computers.FirstOrDefault(x => x.Id == computerId);
            if (existingComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
        }
        private void CheckExistingComponent(int componentsId)
        {
            var existingComponents = this.components.Any(x => x.Id == componentsId);
            if (existingComponents)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }
        }
    }
}