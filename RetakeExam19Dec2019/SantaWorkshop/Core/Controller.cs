using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SantaWorkshop.Core.Contracts;
using SantaWorkshop.Models.Dwarfs;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Models.Presents;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops;
using SantaWorkshop.Models.Workshops.Contracts;
using SantaWorkshop.Repositories;
using SantaWorkshop.Repositories.Contracts;
using SantaWorkshop.Utilities.Messages;

namespace SantaWorkshop.Core
{
    public class Controller : IController
    {
        private IRepository<IDwarf> dwarfs;
        private IRepository<IPresent> presents;

        public Controller()
        {
            this.dwarfs = new DwarfRepository();
            this.presents = new PresentRepository();
        }
        public string AddDwarf(string dwarfType, string dwarfName)
        {
            IDwarf dwarf;

            if (dwarfType == nameof(HappyDwarf))
            {
                dwarf = new HappyDwarf(dwarfName);
            }
            else if (dwarfType == nameof(SleepyDwarf))
            {
                dwarf = new SleepyDwarf(dwarfName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }
            this.dwarfs.Add(dwarf);

            string message = string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
            return message;
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            IDwarf dwarf = dwarfs.FindByName(dwarfName);
            if (dwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            IInstrument instrument = new Instrument(power);
            dwarf.AddInstrument(instrument);

            string message = string.Format(OutputMessages.InstrumentAdded, power, dwarfName);
            return message;
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            IPresent present = new Present(presentName, energyRequired);
            this.presents.Add(present);
            var message = string.Format(OutputMessages.PresentAdded, presentName);
            return message;
        }

        public string CraftPresent(string presentName)
        {
            var present = this.presents.FindByName(presentName);
             var workingDwarves = TakeDwarf();


            IWorkshop workshop = new Workshop();

            while (workingDwarves.Any())
            {
                IDwarf currentDwarf = workingDwarves.First();
                workshop.Craft(present, currentDwarf);

                if (!currentDwarf.Instruments.Any())
                {
                    workingDwarves.Remove(currentDwarf);
                }
                if (currentDwarf.Energy == 0)
                {
                    workingDwarves.Remove(currentDwarf);
                    this.dwarfs.Remove(currentDwarf);
                }

                if (present.IsDone())
                {
                    break;
                }
            }

            string message = string.Format(present.IsDone()
                ? OutputMessages.PresentIsDone
                : OutputMessages.PresentIsNotDone, presentName);

            return message;
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            var countCraftedPresents = this.presents.Models.Count(x => x.EnergyRequired == 0);
            sb.AppendLine($"{countCraftedPresents} presents are done!");
            sb.AppendLine("Dwarfs info:");
            foreach (var dwarf in this.dwarfs.Models)
            {
                sb.AppendLine($"Name: {dwarf.Name}");
                sb.AppendLine($"Energy: {dwarf.Energy}");
                sb.AppendLine($"Instruments: {dwarf.Instruments.Count(x => !x.IsBroken())} not broken left");
            }


            return sb.ToString().TrimEnd();
        }
        private ICollection<IDwarf> TakeDwarf()
        {
            var currentDwarfs = this.dwarfs.Models
                .Where(x => x.Energy >= 50)
                .OrderByDescending(dw => dw.Energy).ToList();
            if (!currentDwarfs.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }
            return currentDwarfs;
        }

    }
}
