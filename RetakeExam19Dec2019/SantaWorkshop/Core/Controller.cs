using System;
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
            if (dwarfType == nameof(HappyDwarf))
            {
                IDwarf happyDwarf = new HappyDwarf(dwarfName);
                this.dwarfs.Add(happyDwarf);
            }
            else if (dwarfType == nameof(SleepyDwarf))
            {
                IDwarf sleepyDwarf = new SleepyDwarf(dwarfName);
                this.dwarfs.Add(sleepyDwarf);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }

            string message = string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
            return message;
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            var dwarf = dwarfs.FindByName(dwarfName);
            if (dwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            IInstrument instrument = new Instrument(power);
            dwarf.AddInstrument(instrument);
            string message = string.Format(OutputMessages.InstrumentAdded, power, dwarf.Name);
            return message;
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            Present present = new Present(presentName, energyRequired);
            this.presents.Add(present);
            string message = string.Format(OutputMessages.PresentAdded, presentName);
            return message;
        }

        public string CraftPresent(string presentName)
        {
            var present = this.presents.FindByName(presentName);
            var dwarf = TakeDwarf();
            if (dwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }

            IWorkshop workshop = new Workshop();
            while (dwarf.Instruments.Any(x => !x.IsBroken()))
            {
                workshop.Craft(present, dwarf);
                if (present.IsDone())
                {
                    break;
                }
                if (dwarf.Energy == 0)
                {
                    this.dwarfs.Remove(dwarf);
                    dwarf = TakeDwarf();
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
        private IDwarf TakeDwarf()
        {
            var dwarf = this.dwarfs.Models.OrderByDescending(x => x.Energy).FirstOrDefault(x => x.Energy >= 50);
            return dwarf;
        }

    }
}
