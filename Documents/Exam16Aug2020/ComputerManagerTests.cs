using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Computers.Tests
{
    [TestFixture]
    public class Tests
    {
        private ComputerManager _compManager;
        private Computer _computer;


        [SetUp]
        public void Setup()
        {
            this._compManager = new ComputerManager();
            this._computer = new Computer("Intel", "2800x", 1000);
        }

        [Test]
        public void ComputerInitialization()
        {
            Assert.AreEqual(this._computer.Price, 1000);
            Assert.AreEqual(this._computer.Manufacturer, "Intel");
            Assert.AreEqual(this._computer.Model, "2800x");

        }

        [Test]
        public void ConstructorNormalOperation()
        {
            Assert.AreEqual(this._compManager.Count, 0);
        }

        [Test]
        public void AddComputerNormalOperation()
        {
            this._compManager.AddComputer(this._computer);

            Assert.AreEqual(this._compManager.Count, 1);
        }

        [Test]
        public void AddComputerNullValue()
        {


            Assert.That(() =>
            {
                this._compManager.AddComputer(null);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Can not be null! (Parameter 'computer')"));
        }

        [Test]
        public void AddComputerAlreadyExists()
        {
            this._compManager.AddComputer(this._computer);

            Assert.That(() =>
            {
                this._compManager.AddComputer(this._computer);
            }, Throws.ArgumentException.With.Message.EqualTo("This computer already exists."));
        }

        [Test]
        public void RemoveComputerNormalOperation()
        {
            this._compManager.AddComputer(this._computer);
            var returnValue = this._compManager.RemoveComputer("Intel", "2800x");

            Assert.AreSame(this._computer, returnValue);
            Assert.AreEqual(this._compManager.Count, 0);
        }

        [Test]
        public void GetComputerNormalOperation()
        {
            this._compManager.AddComputer(this._computer);
            var returnValue = this._compManager.GetComputer("Intel", "2800x");

            Assert.AreSame(this._computer, returnValue);
        }

        [Test]
        public void GetComputerNullManufacturer()
        {

            Assert.That(() =>
            {
                this._compManager.GetComputer(null, "2800x");
            }, Throws.ArgumentNullException.With.Message.EqualTo("Can not be null! (Parameter 'manufacturer')"));
        }

        [Test]
        public void GetComputerNullModel()
        {

            Assert.That(() =>
            {
                this._compManager.GetComputer("Intel", null);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Can not be null! (Parameter 'model')"));
        }

        [Test]
        public void GetComputerNonExistent()
        {

            this._compManager.AddComputer(this._computer);

            Assert.That(() =>
            {
                this._compManager.GetComputer("Intel", "WorkWorkWork");
            }, Throws.ArgumentException.With.Message.EqualTo("There is no computer with this manufacturer and model."));
        }

        [Test]
        public void GetComputerByManufacturerIsNull()
        {

            this._compManager.AddComputer(this._computer);

            Assert.That(() =>
            {
                this._compManager.GetComputersByManufacturer(null);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Can not be null! (Parameter 'manufacturer')"));
        }

        [Test]
        public void GetComputerByManufacturerNormalOperation()
        {

            var pc2 = new Computer("Intel", "s200", 100);
            var pc3 = new Computer("Radeon", "k200", 1000);


            this._compManager.AddComputer(this._computer);
            this._compManager.AddComputer(pc2);
            this._compManager.AddComputer(pc3);

            var expectedResult = new Collection<Computer>()
            {
                this._computer,pc2
            };

            var returnValue = this._compManager.GetComputersByManufacturer("Intel");

            Assert.That(expectedResult, Is.EquivalentTo(returnValue));
        }


        [Test]
        public void GetComputerByManufacturerNormalOperationWithManyManufactureres()
        {

            var pc2 = new Computer("Intel", "s200", 100);
            var pc3 = new Computer("Radeon", "k200", 1000);
            var pc4 = new Computer("NVIDIA", "k200", 1000);
            var pc5 = new Computer("Intel", "k200", 1000);
            var pc6 = new Computer("MaxPC", "k200", 1000);


            this._compManager.AddComputer(this._computer);
            this._compManager.AddComputer(pc2);
            this._compManager.AddComputer(pc3);
            this._compManager.AddComputer(pc4);
            this._compManager.AddComputer(pc5);
            this._compManager.AddComputer(pc6);


            var expectedResult = new Collection<Computer>()
            {
                this._computer,pc2, pc5
            };

            var returnValue = this._compManager.GetComputersByManufacturer("Intel");

            Assert.That(expectedResult, Is.EquivalentTo(returnValue));
        }

        [Test]
        public void GetComputerByManufacturerNormalOperationManyPC()
        {

            var pc2 = new Computer("Intel", "s200", 100);
            var pc3 = new Computer("Radeon", "k200", 1000);


            this._compManager.AddComputer(this._computer);
            this._compManager.AddComputer(pc2);
            this._compManager.AddComputer(pc3);


            var returnValue = this._compManager.GetComputer("Radeon", "k200");

            Assert.AreSame(pc3, returnValue);
        }

        [Test]
        public void CountNormalOperation()
        {
            var pc2 = new Computer("Intel", "s200", 100);
            var pc3 = new Computer("Radeon", "k200", 1000);
            var pc4 = new Computer("NVIDIA", "k200", 1000);
            var pc5 = new Computer("Intel", "k200", 1000);
            var pc6 = new Computer("MaxPC", "k200", 1000);


            this._compManager.AddComputer(this._computer);
            this._compManager.AddComputer(pc2);
            this._compManager.AddComputer(pc3);
            this._compManager.AddComputer(pc4);
            this._compManager.AddComputer(pc5);
            this._compManager.AddComputer(pc6);

            Assert.AreEqual(this._compManager.Count, 6);
        }

        [Test]
        public void RemoveComputerIsNonExisten()
        {
            var pc6 = new Computer("MaxPC", "k200", 1000);

            this._compManager.AddComputer(this._computer);
            this._compManager.AddComputer(pc6);

            Assert.That(() =>
            {
                this._compManager.RemoveComputer("Ivan", "Ivanov");
            },Throws.ArgumentException.With.Message.EqualTo("There is no computer with this manufacturer and model."));
        }
    }
}