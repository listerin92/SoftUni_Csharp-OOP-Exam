using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using RobotService.Core.Contracts;
using RobotService.Models.Garages;
using RobotService.Models.Procedures;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly Garage garage;
        private IRobot robot;
        private IProcedure procedure;
        private readonly Dictionary<string, IList<IRobot>> collectionWithProcedures = new Dictionary<string, IList<IRobot>>();
        public Controller()
        {
            this.garage = new Garage();
            
        }
        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {
            if (robotType == nameof(HouseholdRobot))
            {
                robot = new HouseholdRobot(name, energy, happiness, procedureTime);
            }
            else if (robotType == nameof(PetRobot))
            {
                robot = new PetRobot(name, energy, happiness, procedureTime);

            }
            else if (robotType == nameof(WalkerRobot))
            {
                robot = new WalkerRobot(name, energy, happiness, procedureTime);

            }
            else
            {
                string message = string.Format(ExceptionMessages.InvalidRobotType, robotType);
                throw new ArgumentException(message);
            }

            this.garage.Manufacture(robot);
            return $"Robot {robot.Name} registered successfully";
        }

        public string Chip(string robotName, int procedureTime)
        {

            procedure = new Chip();

            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.ChipProcedure, robotName);
            return message;
        }


        public string TechCheck(string robotName, int procedureTime)
        {
            procedure = new TechCheck();

            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.TechCheckProcedure, robotName);
            return message;
        }

        public string Rest(string robotName, int procedureTime)
        {
            procedure = new Rest();

            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.RestProcedure, robotName);
            return message;
        }

        public string Work(string robotName, int procedureTime)
        {
            procedure = new Work();

            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.WorkProcedure, robotName, procedureTime);
            return message;
        }

        public string Charge(string robotName, int procedureTime)
        {
            procedure = new Charge();
            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.ChargeProcedure, robotName);
            return message;
        }

        public string Polish(string robotName, int procedureTime)
        {
            procedure = new Polish();
            DoServiceProcedure(robotName, procedureTime);
            string message = string.Format(OutputMessages.PolishProcedure, robotName);
            return message;
        }

        public string Sell(string robotName, string ownerName)
        {
            IRobot robotForSell = this.garage.Robots.FirstOrDefault(n => n.Key == robotName).Value;
            this.garage.Sell(robotName, ownerName);
            
            if (robotForSell.IsChipped)
            {
                string message = string.Format(OutputMessages.SellChippedRobot, ownerName);
                return message;
            }
            else
            {
                string message = string.Format(OutputMessages.SellNotChippedRobot, ownerName);
                return message;
            }
        }

        public string History(string procedureType)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var collectionWithProcedure in collectionWithProcedures
                .Where(x=>x.Key == procedureType))
            {
                sb.AppendLine(collectionWithProcedure.Key);
                foreach (var robotToShow in collectionWithProcedure.Value)
                {
                    sb.AppendLine(robotToShow.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }

        private void DoServiceProcedure(string robotName, int procedureTime)
        {
            var procedureName = this.procedure.GetType().Name;
            if (!collectionWithProcedures.ContainsKey(procedureName))
            {
                collectionWithProcedures[procedureName] = new List<IRobot>();
                
            }
            this.robot = this.garage.Robots.FirstOrDefault(n => n.Key == robotName).Value;
            if (robot == null)
            {
                string message = string.Format(ExceptionMessages.InexistingRobot, robotName);
                throw new ArgumentException(message);
            }
            procedure.DoService(robot, procedureTime);
            collectionWithProcedures[procedureName].Add(robot);
        }
    }
}