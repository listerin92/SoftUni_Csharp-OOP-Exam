using System;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computerOne;
        [SetUp]
        public void Setup()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            this.computerOne = new Computer(manufacturer, model, price);
        }

        [Test]
        public void TestComputerGetManufacturer()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            Assert.AreEqual(manufacturer, computer.Manufacturer);
        }
        [Test]
        public void TestComputerGetModel()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(model, model, price);
            Assert.AreEqual(model, computer.Model);
        }
        [Test]
        public void TestComputerGetPrice()
        {
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            var actualPrice = computer.Price;
            Assert.AreEqual(price, actualPrice);
        }
        [Test]
        public void TestComputerSetManufacturer()
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
        public void ConstructorNormal()
        {
            ComputerManager computerManager = new ComputerManager();
            Assert.AreEqual(computerManager.Count, 0);
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
        public void TestComputerManagerAddAlreadyExists()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);

            string message = "This computer already exists.";

            Assert.That(() =>
            {
                computerManager.AddComputer(computer);
            }, Throws.ArgumentException.With.Message.EqualTo(message));
        }

        [Test]
        public void AddComputerNullValue()
        {
            ComputerManager computerManager = new ComputerManager();
            computerManager.AddComputer(computerOne);

            Assert.That(() =>
            {
                computerManager.AddComputer(null);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Can not be null! (Parameter 'computer')"));
        }
        [Test]
        public void RemoveTestManufacturer()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            var removedComputer = computerManager.RemoveComputer(manufacturer, model);
            Assert.AreEqual(manufacturer, removedComputer.Manufacturer);

        }
        [Test]
        public void RemoveTestModel()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            var removedComputer = computerManager.RemoveComputer(manufacturer, model);
            Assert.AreEqual(model, removedComputer.Model);

        }
        [Test]
        public void RemoveTestObject()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);
            var removedComputer = computerManager.RemoveComputer(manufacturer, model);
            Assert.AreSame(computer, removedComputer);
            Assert.AreEqual(computerManager.Count, 0);

        }
        [Test]
        public void RemoveComputerIsNonExisten()
        {
            ComputerManager computerManager = new ComputerManager();

            var pc6 = new Computer("MaxPC", "k200", 1000);

            computerManager.AddComputer(computerOne);
            computerManager.AddComputer(pc6);

            Assert.That(() =>
            {
                computerManager.RemoveComputer("Ivan", "Ivanov");
            }, Throws.ArgumentException.With.Message.EqualTo("There is no computer with this manufacturer and model."));
        }
        [Test]
        public void GetComputerManufacturerNormal()
        {
            ComputerManager computerManager = new ComputerManager();
            
            computerManager.AddComputer(computerOne);
            string manufacturer = "Asus";
            string model = "asdf";

            var gettedComputer = computerManager.GetComputer(manufacturer, model);
            Assert.AreEqual(manufacturer, gettedComputer.Manufacturer);
        }

        [Test]
        public void GetComputerObjectNormal()
        {
            ComputerManager computerManager = new ComputerManager();

            computerManager.AddComputer(computerOne);
            string manufacturer = "Asus";
            string model = "asdf";

            var gettedComputer = computerManager.GetComputer(manufacturer, model);
            Assert.AreSame(computerOne, gettedComputer);
        }
        [Test]
        public void GetComputerModelNormal()
        {
            ComputerManager computerManager = new ComputerManager();

            computerManager.AddComputer(computerOne);
            string manufacturer = "Asus";
            string model = "asdf";

            var gettedComputer = computerManager.GetComputer(manufacturer, model);
            Assert.AreSame(computerOne, gettedComputer);
        }
        [Test]
        public void GetComputerModelThrowNonExist()
        {
            ComputerManager computerManager = new ComputerManager();

            computerManager.AddComputer(computerOne);
            string wrongManufacturer = "NoAsus";
            string wrongModel = "Wrong";
            string message = "There is no computer with this manufacturer and model.";

            Assert.That(() =>
            {
                computerManager.GetComputer(wrongManufacturer, wrongModel);
            }, Throws.ArgumentException
                .With.Message
                .EqualTo(message));
        }


        [Test]
        public void GetComputerValidateNullManufacturerThrow()
        {
            ComputerManager computerManager = new ComputerManager();

            computerManager.AddComputer(computerOne);
            //string wrongManufacturer = null;
            string okModel = "asdf";
            string message = "Can not be null!";

            Assert.That(() =>
            {
                computerManager.GetComputer(null, okModel);
            }, Throws.ArgumentNullException
                .With.Message.EqualTo($"{message} (Parameter 'manufacturer')"));
        }

        [Test]
        public void GetComputerValidateNullModelThrow()
        {
            ComputerManager computerManager = new ComputerManager();

            computerManager.AddComputer(computerOne);
            string manufacturer = "Asus";
            //string wrongModel = null;
            string message = "Can not be null!";

            Assert.That(() =>
            {
                computerManager.GetComputer(manufacturer, null);
            }, Throws.ArgumentNullException
                .With.Message.EqualTo($"{message} (Parameter 'model')"));
        }
        [Test]
        public void GetByManufacturer()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf2";
            decimal price = 12.4m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computerOne);
            computerManager.AddComputer(computer);

            var expectedResult = new Collection<Computer>()
            {
                this.computerOne,computer
            };

            var returnValue = computerManager.GetComputersByManufacturer(manufacturer);
            Assert.That(expectedResult, Is.EquivalentTo(returnValue));
        }

        [Test]
        public void GetComputersByManufacturerThrow()
        {
            ComputerManager computerManager = new ComputerManager();
            string manufacturer = "Asus";
            string model = "asdf";
            decimal price = 12.3m;
            Computer computer = new Computer(manufacturer, model, price);
            computerManager.AddComputer(computer);

            string message = "Can not be null!";

            Assert.That(() =>
            {
                computerManager.GetComputersByManufacturer(null);
            }, Throws.ArgumentNullException.With.Message.EqualTo($"{message} (Parameter 'manufacturer')")
                );
        }

        [Test]
        public void TestCollection()
        {
            ComputerManager computerManager = new ComputerManager();

            var computerTwo = new Computer("Asus", "A0001", 1000);
            var computerTree = new Computer("Intel2", "A0002", 12000);
            var computerFour = new Computer("Asus", "A0003", 500);
            var computerFive = new Computer("Intel4", "A0004", 100);
            var computerSix = new Computer("Asus", "A0005", 10);
            computerManager.AddComputer(computerOne);
            computerManager.AddComputer(computerTwo);
            computerManager.AddComputer(computerTree);
            computerManager.AddComputer(computerFour);
            computerManager.AddComputer(computerFive);
            computerManager.AddComputer(computerSix);

            var expected = new Collection<Computer>()
            {
                this.computerOne, computerTwo, computerFour, computerSix
            };

            var returnValue = computerManager.GetComputersByManufacturer("Asus");
            Assert.That(expected, Is.EquivalentTo(returnValue));

        }


    }
}