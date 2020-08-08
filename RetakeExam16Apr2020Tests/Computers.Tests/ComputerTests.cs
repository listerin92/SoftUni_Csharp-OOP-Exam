using System;
using System.Linq;

namespace Computers.Tests
{
    using NUnit.Framework;

    public class ComputerTests
    {
        private Part part;
        private Computer computer;
        [SetUp]
        public void Setup()
        {
            this.part = new Part("Ivan", 10.0m);
            this.computer = new Computer("Mashinata");
        }

        [Test]
        public void TestPartClassConstructor()
        {

            string expectedName = "Ivan";
            decimal expectedPrice = 10.0m;
            Assert.AreEqual(expectedName, this.part.Name);
            Assert.AreEqual(expectedPrice, this.part.Price);
        }
        [Test]
        public void TestPartClassSetter()
        {

            string expectedName = "IvanNew";
            decimal expectedPrice = 20.0m;

            this.part.Name = "IvanNew";
            this.part.Price = 20.0m;

            Assert.AreEqual(expectedName, this.part.Name);
            Assert.AreEqual(expectedPrice, this.part.Price);
        }

        [Test]
        public void TestComputerNameCorrect()
        {
            string expectedName = "Mashinata";
            Assert.AreEqual(expectedName, this.computer.Name);
        }
        [Test]
        public void TestComputerNameThrowWhenNull()
        {
            Computer computer1;
            string name = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                computer1 = new Computer(name);

            });
        }

        [Test]
        public void TestAddPartsCorrect()
        {
            Part part2 = new Part("Mashina2", 20.0m);
            int expectedPartCount = 2;
            this.computer.AddPart(part);
            this.computer.AddPart(part2);
            Assert.AreEqual(expectedPartCount, this.computer.Parts.Count);
        }
        [Test]
        public void TestAddPartsThrowsWhenNull()
        {
            Part part2 = null;

            Assert.That(() =>
            {
                this.computer.AddPart(part2);

            }, Throws.InvalidOperationException
                .With.Message
                .EqualTo("Cannot add null!"));
        }

        [Test]
        public void TestSum()
        {
            var expectedTotalPrice = 30.0m;
            this.computer.AddPart(part);
            this.computer.AddPart(part);
            this.computer.AddPart(part);
            var actualTotalPrice = this.computer.TotalPrice;
            Assert.AreEqual(expectedTotalPrice, actualTotalPrice);
        }

        [Test]
        public void TestRemovePartsCount()
        {
            Part part2 = new Part("Mashina2", 20.0m);
            int expectedPartCount = 1;
            this.computer.AddPart(part);
            this.computer.AddPart(part2);
            this.computer.RemovePart(part2);
            Assert.AreEqual(expectedPartCount, this.computer.Parts.Count);
        }

        [Test]
        public void TestRemovePartsPartName()
        {
            Part part2 = new Part("Mashina2", 20.0m);
            string expectedPartName = "Ivan";
            this.computer.AddPart(part);
            this.computer.AddPart(part2);
            this.computer.RemovePart(part2);
            var actualName = this.computer.Parts.FirstOrDefault(n => n.Name == expectedPartName);
            Assert.AreEqual(expectedPartName, actualName.Name);
        }

        [Test]
        public void TestGetPart()
        {
            this.computer.AddPart(part);

            Part gettedPart = this.computer.GetPart("Ivan");
            Assert.AreEqual(part.Name, gettedPart.Name);
        }
    }
}