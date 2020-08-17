using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Computers.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computerOne = new Computer(manufacturer, model, price);
        }

        [Test]
        public void TestComputer()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            Assert.AreEqual(manufacturer, computer.Manufacturer);
        }
        [Test]
        public void TestComputerSet()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computer.Manufacturer = "Asus2";
            Assert.AreEqual("Asus2", computer.Manufacturer);
        }
        [Test]
        public void TestComputerSetModel()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computer.Model = "asdf2";
            Assert.AreEqual("asdf2", computer.Model);
        }
        [Test]
        public void TestComputerSetPrice()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computer.Price = 12.4m;
            Assert.AreEqual(12.4m, computer.Price);
        }

        [Test]
        public void TestComputerManager()
        {
            ComputerManager computerManager = new ComputerManager();
        }

        [Test]
        public void TestComputerManagerAdd()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            Assert.AreEqual(1, computerManager.Count);
        }
        [Test]
        public void TestComputerManagerAddException()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            Assert.Throws<ArgumentException>(() =>
            {
                computerManager.AddComputer(computer);

            });
        }

        [Test]
        public void Remove()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            computerManager.RemoveComputer(manufacturer, model);

        }
        [Test]
        public void RemoveEx()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            Assert.Throws<ArgumentException>(() =>
            {
                computerManager.RemoveComputer(manufacturer, "a");
            });
        }

        [Test]
        public void GetBy()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);

            var comp = computerManager.GetComputersByManufacturer(manufacturer);
            var compFirst = comp.First();
            Assert.AreEqual(manufacturer, compFirst.Manufacturer);
        }
        [Test]
        public void GetByThrow()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var comp = computerManager.GetComputersByManufacturer(null);
            });
        }

        [Test]
        public void TestCollection()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);

            var computers = computerManager.Computers;
        }
    }
}