using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using OnlineShop.Common.Constants;
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
            if (this.computers.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }
            IComputer computer = computerType switch
            {
                nameof(DesktopComputer) => new DesktopComputer(id, manufacturer, model, price),
                nameof(Laptop) => new Laptop(id, manufacturer, model, price),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };

            this.computers.Add(computer);

            string message = string.Format(SuccessMessages.AddedComputer, id);
            return message;
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
            CheckComputerIdExistInComputers(computerId);

            if (this.components.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComponent component = componentType switch
            {
                nameof(CentralProcessingUnit) => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                nameof(Motherboard) => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                nameof(PowerSupply) => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                nameof(RandomAccessMemory) => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                nameof(SolidStateDrive) => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                nameof(VideoCard) => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };

            var computer = this.computers.First(x => x.Id == computerId);
            computer.AddComponent(component);

            this.components.Add(component);
            string message = string.Format(SuccessMessages.AddedComponent, component.GetType().Name, component.Id,
                computer.Id);
            return message;
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            CheckComputerIdExistInComputers(computerId);

            var computer = this.computers.First(x => x.Id == computerId);
            var removedComponent = computer.RemoveComponent(componentType);
            
            IComponent component = this.components.FirstOrDefault(x => x.GetType().Name == componentType);
            this.components.Remove(component);

            string message = string.Format(SuccessMessages.RemovedComponent, componentType, removedComponent.Id);
            return message;
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            CheckComputerIdExistInComputers(computerId);

            if (this.peripherals.Any(x => x.Id == id))
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
            var computer = this.computers.First(x => x.Id == computerId);

            computer.AddPeripheral(peripheral);
            this.peripherals.Add(peripheral);

            string message = string.Format(SuccessMessages.AddedPeripheral, peripheral.GetType().Name, peripheral.Id, computer.Id);

            return message;
        }


        public string RemovePeripheral(string peripheralType, int computerId)
        {
            CheckComputerIdExistInComputers(computerId);

            var computer = this.computers.First(x => x.Id == computerId);
            var removedPeripheral= computer.RemovePeripheral(peripheralType);

            IPeripheral peripheral = this.peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheral);

            string message = string.Format(SuccessMessages.RemovedPeripheral, peripheralType, removedPeripheral.Id);
            return message;
        }


        public string BuyComputer(int id)
        {
            CheckComputerIdExistInComputers(id);

            IComputer computer = this.computers.First(x => x.Id == id);
            
            this.computers.Remove(computer);

            return computer.ToString();
        }

        public string BuyBest(decimal budget)
        {
            IComputer bestComputer = this.computers.Where(x => x.Price <= budget)
                .OrderByDescending(x => x.OverallPerformance).First();
            if (bestComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.CanNotBuyComputer);
            }

            this.computers.Remove(bestComputer);
            return bestComputer.ToString();
        }

        public string GetComputerData(int id)
        {
            CheckComputerIdExistInComputers(id);

            IComputer computer = this.computers.First(x => x.Id == id);
            return computer.ToString();
        }

        private void CheckComputerIdExistInComputers(int computerId)
        {
            if (this.computers.All(x => x.Id != computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
        }
    }
}
