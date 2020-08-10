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
using RobotService.Utilities;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly Garage garage;
        private IRobot robot;
        private readonly Dictionary<ProcedureType, IProcedure> collectionWithProcedures;
        public Controller()
        {
            this.garage = new Garage();
            this.collectionWithProcedures = new Dictionary<ProcedureType, IProcedure>();
            this.InitCollection();

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

            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.Chip,
                OutputMessages.ChipProcedure);
            return message;
        }


        public string TechCheck(string robotName, int procedureTime)
        {
            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.TechCheck,
                OutputMessages.TechCheckProcedure);
            return message;
        }

        public string Rest(string robotName, int procedureTime)
        {
            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.Rest,
                OutputMessages.RestProcedure);
            return message;
        }

        public string Work(string robotName, int procedureTime)
        {
            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.Work,
                OutputMessages.WorkProcedure);
            return message;
        }

        public string Charge(string robotName, int procedureTime)
        {
            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.Work,
                OutputMessages.ChargeProcedure);
            return message;
        }

        public string Polish(string robotName, int procedureTime)
        {
            string message = DoServiceProcedure(
                robotName,
                procedureTime,
                ProcedureType.Polish,
                OutputMessages.PolishProcedure);
            return message;
        }

        public string Sell(string robotName, string ownerName)
        {
            IRobot robotForSell = this.garage.Robots.FirstOrDefault(n => n.Key == robotName).Value;
            this.garage.Sell(robotName, ownerName);

            return string.Format(robotForSell.IsChipped
                ? OutputMessages.SellChippedRobot
                : OutputMessages.SellNotChippedRobot
                , ownerName);
        }

        public string History(string procedureType)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var collectionWithProcedure in collectionWithProcedures
                .Where(x => x.Key.ToString() == procedureType))
            {
                sb.AppendLine(collectionWithProcedure.Value.History());
            }

            return sb.ToString().TrimEnd();
        }

        private string DoServiceProcedure(string robotName, int procedureTime, ProcedureType procedureType, string outputMessage)
        {
            string outputMsg;

            IRobot robot = GetRobotByName(robotName);
            IProcedure procedure = this.collectionWithProcedures[procedureType];
            procedure.DoService(robot, procedureTime);

            if (procedureType.ToString() == nameof(Work))
            {
                outputMsg = string.Format(outputMessage, robotName, procedureTime);
                return outputMsg;
            }
            outputMsg = string.Format(outputMessage, robotName);
            return outputMsg;
        }

        private IRobot GetRobotByName(string robotName)
        {
            robot = this.garage.Robots.FirstOrDefault(n => n.Key == robotName).Value;
            if (robot == null)
            {
                string message = string.Format(ExceptionMessages.InexistingRobot, robotName);
                throw new ArgumentException(message);
            }

            return robot;
        }
        private void InitCollection()
        {
            this.collectionWithProcedures.Add(ProcedureType.Chip, new Chip());
            this.collectionWithProcedures.Add(ProcedureType.TechCheck, new TechCheck());
            this.collectionWithProcedures.Add(ProcedureType.Rest, new Rest());
            this.collectionWithProcedures.Add(ProcedureType.Work, new Work());
            this.collectionWithProcedures.Add(ProcedureType.Charge, new Charge());
            this.collectionWithProcedures.Add(ProcedureType.Polish, new Polish());
        }
    }
}