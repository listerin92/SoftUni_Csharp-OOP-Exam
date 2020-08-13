using System.Collections.Generic;
using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void Initialize()
        {
            this.robot = new Robot("Pesho", 100);
            this.robotManager = new RobotManager(2);
        }

        [Test]
        public void TestRobotConstructorAndNameGetProperty()
        {
            string expectedName = "Pesho";
            Assert.AreEqual(expectedName, this.robot.Name);
        }

        [Test]
        public void TestRobotConstructorAndBatteryGetProperty()
        {
            int expectedMaxBattery = 100;
            Assert.AreEqual(expectedMaxBattery, this.robot.MaximumBattery);
        }

        [Test]
        public void TestRobotNameSetProperty()
        {
            string expectedName = "Pesho123";
            this.robot.Name = expectedName;
            Assert.AreEqual(expectedName, this.robot.Name);
        }

        [Test]
        public void TestRobotBatterySetProperty()
        {
            int expectedBattery = 50;
            this.robot.Battery = expectedBattery;
            Assert.AreEqual(expectedBattery, this.robot.Battery);
        }

        [Test]
        public void TestRobotMangerConstructorAncCapacity()
        {
            int expectedCapacity = 101;
            RobotManager robot1 = new RobotManager(101);
            Assert.AreEqual(expectedCapacity, robot1.Capacity);
        }

        [Test]
        public void TestCapacityExceptionMessage()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager robot1 = new RobotManager(-101);
            }, "Invalid capacity!");

        }

        [Test]
        public void TestAddWithCountCorrect()
        {
            robotManager.Add(this.robot);
            Robot robot2 = new Robot("Ivan", 2);
            robotManager.Add(robot2);
            int expectedCount = 2;
            Assert.AreEqual(expectedCount, robotManager.Count);
        }


        [Test]
        public void TestRobotAddSameNameException()
        {
            robotManager.Add(this.robot);
            Robot robot2 = new Robot("Pesho", 2);
            var message = string.Format($"There is already a robot with name {robot.Name}!");
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot2);
            }, message);
        }

        [Test]
        public void TestNotEnoughCountException()
        {
            robotManager.Add(this.robot);
            Robot robot2 = new Robot("Ivan", 2);
            robotManager.Add(robot2);
            Robot robot3 = new Robot("Petkan", 3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot3);
            }, "Not enough capacity!");

        }

        [Test]
        public void TestRemoveCorrect()
        {
            int expectedCount = 1;
            robotManager.Add(robot);
            Robot robot2 = new Robot("Ivan", 2);
            robotManager.Add(robot2);
            robotManager.Remove("Ivan");

            Assert.AreEqual(expectedCount, robotManager.Count);
        }

        [Test]
        public void TestRemoveNotFoundException()
        {
            robotManager.Add(this.robot);
            Robot robot2 = new Robot("Ivan", 2);
            robotManager.Add(robot2);
            Robot robot3 = new Robot("Petkan", 3);
            string nameToRemove = "Dragan";
            string message = string.Format($"Robot with the name {nameToRemove} doesn't exist!");
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove(nameToRemove);
            }, message);
        }

        [Test]
        public void TestWorkCorrect()
        {
            this.robotManager.Add(robot);
            this.robotManager.Work("Pesho", "raboti", 50);
            int expectedBattery = 50;
            Assert.AreEqual(expectedBattery, this.robot.Battery);
        }

        [Test]
        public void TestWorkNotFoundException()
        {

            string nameToWork = "PeshoMesho";
            this.robotManager.Add(robot);
            string message = string.Format($"Robot with the name {nameToWork} doesn't exist!");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Work("PeshoMesho", "raboti", 50);

            }, message);
        }
        [Test]
        public void TestWorkNotEnoughBatteryException()
        {

            string nameToWork = "Pesho";

            string message = string.Format($"{this.robot.Name} doesn't have enough battery!");
            this.robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Work("Pesho", "raboti", 250);

            }, message);
        }

        [Test]
        public void TestChargeCorrectly()
        {
            this.robotManager.Add(robot);
            this.robotManager.Work("Pesho", "raboti", 50);
            this.robotManager.Charge("Pesho");
            Assert.AreEqual(100, robot.Battery);
        }
        [Test]
        public void TestChargeNotFoundException()
        {

            string nameToWork = "PeshoMesho";
            this.robotManager.Add(robot);
            string message = string.Format($"Robot with the name {nameToWork} doesn't exist!");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Charge("PeshoMesho");

            }, message);
        }
    }
}