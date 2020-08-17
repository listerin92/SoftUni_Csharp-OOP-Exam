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
        private ICollection<IComputer> computers;
        private ICollection<IPeripheral> peripherals;
        private ICollection<IComponent> components;
        public Controller()
        {
            this.computers = new List<IComputer>();
            this.peripherals = new List<IPeripheral>();
            this.components = new List<IComponent>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            Computer computer;
            if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);

            }
            else
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            this.computers.Add(computer);
            string message = string.Format(SuccessMessages.AddedComputer, computer.Id);
            return message;
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            CheckExistingComputer(computerId);
            CheckExistingComponent(id);
            IComponent component = null;
            if (componentType == nameof(CentralProcessingUnit))
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(Motherboard))
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);

            }
            else if (componentType == nameof(PowerSupply))
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(RandomAccessMemory))
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(SolidStateDrive))
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(VideoCard))
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }
            IComputer computer = this.computers.FirstOrDefault(x => x.Id == computerId);
            computer.AddComponent(component);
            this.components.Add(component);
            

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }


        public string RemoveComponent(string componentType, int computerId)
        {
            var computerWithID = this.computers.First(x => x.Id == computerId);
            var component = this.components.First(x => x.GetType().Name == componentType);

                return $"Successfully removed {componentType} with id {computerId}.";
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            CheckExistingComputer(computerId);
            CheckExistingPeripheral(id);
            IPeripheral peripheral = null;
            if (peripheralType == nameof(Headset))
            {
                peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Keyboard))
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Monitor))
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Mouse))
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }
            IComputer computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            computer.AddPeripheral(peripheral);
            this.peripherals.Add((Peripheral)peripheral);
            //this.computers.Add(computer);

            return $"Peripheral {peripheral.GetType().Name} with id {peripheral.Id} added successfully in computer with id {computer.Id}.";
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {

            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);
            IPeripheral peripheral = computer.Peripherals.First(x => x.GetType().Name == peripheralType);
            computer.RemovePeripheral(peripheral.GetType().Name);

            this.peripherals.Remove(peripheral);
            return $"Successfully removed {peripheral.GetType().Name} with id {peripheral.Id}.";
        }

        public string BuyComputer(int id)
        {
            var computer = this.computers.FirstOrDefault(x => x.Id == id);
            this.computers.Remove(computer);
            return computer.ToString();
        }
        public string BuyBest(decimal budget)
        {
            var computer = this.computers.Where(price=>price.Price <= budget).OrderByDescending(x=>x.OverallPerformance).First();
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
            CheckExistingComputer(id);
            var computer = this.computers.FirstOrDefault(x => x.Id == id);
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
        private void CheckExistingPeripheral(int peripheralsId)
        {
            var existingPeripheral = this.peripherals.FirstOrDefault(x => x.Id == peripheralsId);
            if (existingPeripheral != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }
        }
        private void CheckExistingComponent(int componentsId)
        {
            var existingCompoments = this.components.FirstOrDefault(x => x.Id == componentsId);
            if (existingCompoments != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }
        }
    }
}