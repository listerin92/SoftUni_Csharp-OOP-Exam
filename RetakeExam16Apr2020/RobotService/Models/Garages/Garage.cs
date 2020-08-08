using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models.Garages
{
    public class Garage :  IGarage
    {
        private const int Capacity = 10; //TODO test capacity later set Capacity default to 10
        private readonly Dictionary<string, IRobot> robots;
        public Garage()
        {
            this.robots = new Dictionary<string, IRobot>();
        }

        public IReadOnlyDictionary<string, IRobot> Robots => this.robots;
        public void Manufacture(IRobot robot)
        {
            if (this.robots.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            if (this.robots.ContainsKey(robot.Name))
            {
                string message = string.Format(ExceptionMessages.ExistingRobot, robot.Name);
                throw new ArgumentException(message);
            }
            this.robots.Add(robot.Name, robot);
        }

        public void Sell(string robotName, string ownerName)
        {
            if (!robots.ContainsKey(robotName))
            {
                string message = string.Format(ExceptionMessages.InexistingRobot, robotName);
                throw new ArgumentException(message);
            }

            this.robots[robotName].Owner = ownerName;
            this.robots[robotName].IsBought = false;
            this.robots.Remove(robotName);
        }
    }
}
