using NUnit.Framework;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private UnitCar car;
        private UnitDriver driver;
        private RaceEntry raceEntry;
        [SetUp]
        public void Setup()
        {
            car = new UnitCar("Opel", 300, 1500);
            driver = new UnitDriver("Ivan", car);
            raceEntry = new RaceEntry();
        }

        [Test]
        public void UnitCarTestModel()
        {
            Assert.AreEqual("Opel", car.Model);
        }
        [Test]
        public void UnitCarTestHorsePower()
        {
            Assert.AreEqual(300, car.HorsePower);
        }
        [Test]
        public void UnitCarTestCubicCentimeters()
        {
            Assert.AreEqual(1500, car.CubicCentimeters);
        }

        [Test]
        public void UnitDriverTestName()
        {
            Assert.AreEqual("Ivan", this.driver.Name);
        }

        [Test]
        public void UnitDriverTestNameNull()
        {

            Assert.That(() =>
            {
                UnitDriver nullDriver = new UnitDriver(null, this.car);
            }, Throws.ArgumentNullException.With.Message.EqualTo("Name cannot be null! (Parameter 'Name')"));
        }

        [Test]
        public void UnitDriverTestCar()
        {
            Assert.AreEqual("Opel", this.driver.Car.Model);
            Assert.AreEqual(300, this.driver.Car.HorsePower);
            Assert.AreEqual(1500, this.driver.Car.CubicCentimeters);
        }

        [Test]
        public void RaceEntryCount()
        {
            Assert.AreEqual(0, raceEntry.Counter);
        }

        [Test]
        public void RaceEntryAddDriver()
        {
            var actualMsg = this.raceEntry.AddDriver(this.driver);
            string expectedMsg = $"Driver {this.driver.Name} added in race.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [Test]
        public void RaceEntryAddDriverNullDriver()
        {

            Assert.That(() =>
            {
                this.raceEntry.AddDriver(null);
            }, Throws.InvalidOperationException.With.Message.EqualTo("Driver cannot be null."));
        }
        [Test]
        public void RaceEntryAddDriverExistingDriverException()
        {
            this.raceEntry.AddDriver(driver);

            Assert.That(() =>
            {
                this.raceEntry.AddDriver(driver);
            }, Throws.InvalidOperationException
                .With
                .Message
                .EqualTo($"Driver {this.driver.Name} is already added."));
        }

        [Test]
        public void RaceEntryCalculateAverageHorsePower()
        {
            UnitCar carTwo = new UnitCar("VW", 200, 2000);
            UnitDriver driverTwo = new UnitDriver("Gosho", carTwo);
            this.raceEntry.AddDriver(driver);
            this.raceEntry.AddDriver(driverTwo);
            var actualResult = this.raceEntry.CalculateAverageHorsePower();
            Assert.AreEqual(250, actualResult);
        }

        [Test]
        public void RaceEntryCalculateAverageHorsePowerMinParticipantsThrow()
        {

            this.raceEntry.AddDriver(driver);
            Assert.That(() =>
            {
                var actualResult = this.raceEntry.CalculateAverageHorsePower();
            }, Throws.InvalidOperationException
                .With
                .Message
                .EqualTo($"The race cannot start with less than {2} participants."));
        }

        [Test]
        public void RaceEntryCountLarger()
        {
            UnitCar carTwo = new UnitCar("VW", 200, 2000);
            UnitDriver driverTwo = new UnitDriver("Gosho", carTwo);
            this.raceEntry.AddDriver(driver);
            this.raceEntry.AddDriver(driverTwo);
            Assert.AreEqual(2, raceEntry.Counter);
        }
    }
}