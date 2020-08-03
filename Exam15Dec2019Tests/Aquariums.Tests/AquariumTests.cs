using System.Collections.Generic;
using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;

    public class AquariumTests
    {
        private Aquarium aquarium;

        [SetUp]
        public void Initialize()
        {
            this.aquarium = new Aquarium("See", 3);
        }
        [Test]
        public void ConstructorInitializationTest()
        {

            int expectedCount = 3;
            Assert.AreEqual(expectedCount, this.aquarium.Capacity);
        }
        [Test]
        public void NameGetPropertyTest()
        {

            string expectedName = "Gupata";
            Aquarium aquarium = new Aquarium("Gupata", 3);
            Assert.AreEqual(expectedName, aquarium.Name);
        }
        [Test]
        public void NameEmptyStringPropertyTest()
        {
            string name = "";
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(name, -1);

            }, name + "Invalid aquarium name!");
        }
        [Test]
        public void NameNullPropertyTest()
        {
            string name = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(name, -1);

            }, name + "Invalid aquarium name!");
        }
        [Test]
        public void CapacityNegativeValueTest()
        {

            Assert.That(() =>
            {
                Aquarium aquarium = new Aquarium("See", -1);

            }, Throws.ArgumentException
                .With.Message
                .EqualTo("Invalid aquarium capacity!"));
        }
        [Test]
        public void CapacityValueTest()
        {
            int expectedValue = 5;
            Aquarium aquarium = new Aquarium("See", 5);
            Assert.AreEqual(expectedValue, aquarium.Capacity);

        }
        [Test]
        public void AddTest()
        {
            int expectedCount = 2;
            Fish fish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            Assert.AreEqual(expectedCount, aquarium.Count);
        }
        [Test]
        public void AddTestFullException()
        {
            Fish fish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            Fish fish3 = new Fish("Peshko");
            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            this.aquarium.Add(fish3);

            Assert.That(() =>
            {
                this.aquarium.Add(fish3);
            }, Throws.InvalidOperationException
                .With.Message
                .EqualTo("Aquarium is full!"));
        }
        [Test]
        public void RemoveTestPassed()
        {
            string fishNameToRemove = "Goshko";
            Fish fish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            this.aquarium.RemoveFish(fishNameToRemove);
        }
        [Test]
        public void RemoveTestException()
        {
            string fishNameToRemove = "Ivan";
            Fish fish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            Assert.That(() =>
            {
                this.aquarium.RemoveFish(fishNameToRemove);
            }, Throws.InvalidOperationException
                .With.Message
                .EqualTo($"Fish with the name {fishNameToRemove} doesn't exist!"));
        }

        [Test]
        public void SellFishTestPassed()
        {
            Fish expectedFish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(expectedFish);
            this.aquarium.Add(fish2);
            Fish actualRequestedFish = aquarium.SellFish("Goshko");
            Assert.AreEqual(expectedFish.Name, actualRequestedFish.Name);
        }
        [Test]
        public void SellFishTestBoolPassed()
        {
            Fish expectedFish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(expectedFish);
            this.aquarium.Add(fish2);
            Fish actualRequestedFish = aquarium.SellFish("Goshko");
            Assert.AreEqual(false, actualRequestedFish.Available);
        }
        [Test]
        public void SellFishTestException()
        {
            string fishNameToRemove = "Ivan";
            Fish expectedFish = new Fish("Goshko");
            Fish fish2 = new Fish("Peshko");
            this.aquarium.Add(expectedFish);
            this.aquarium.Add(fish2);
            Assert.That(() =>
            {
                Fish actualRequestedFish = aquarium.SellFish(fishNameToRemove);

            }, Throws.InvalidOperationException
                .With.Message
                .EqualTo($"Fish with the name {fishNameToRemove} doesn't exist!"));
        }
        [Test]
        public void ReportTest()
        {
            this.aquarium.Add(new Fish("Ivan"));
            this.aquarium.Add(new Fish("Dragan"));
            string expectedReport = $"Fish available at {this.aquarium.Name}: Ivan, Dragan";
            string actualReport = aquarium.Report();
            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}