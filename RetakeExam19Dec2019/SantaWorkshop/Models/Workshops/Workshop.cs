using System.Linq;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops.Contracts;

namespace SantaWorkshop.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        //TODO Should check sequence of operations
        public void Craft(IPresent present, IDwarf dwarf)
        {
            IInstrument instrument = TakeNewInstrument(dwarf);
            while (true)
            {
                if (present.IsDone())
                {
                    break;
                }
                if (instrument == null)
                {
                    break;
                }
                if (instrument.IsBroken())
                {
                    instrument = TakeNewInstrument(dwarf);
                    if (instrument == null)
                    {
                        break;
                    }
                }

                if (dwarf.Energy == 0)
                {
                    break;
                }


                instrument.Use();
                present.GetCrafted();
                dwarf.Work();
            }
        }

        private static IInstrument TakeNewInstrument(IDwarf dwarf)
        {
            var instrument = dwarf.Instruments.FirstOrDefault(x => !x.IsBroken());
            return instrument;
        }
    }
}