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
            // loop through instruments
            while (dwarf.Energy > 0 && dwarf.Instruments.Any())
            {
                IInstrument instrument = dwarf.Instruments.First();

                while (!present.IsDone() && dwarf.Energy > 0 && !instrument.IsBroken())
                {

                    dwarf.Work();
                    present.GetCrafted();
                    instrument.Use();
                }

                if (instrument.IsBroken())
                {
                    dwarf.Instruments.Remove(instrument);
                }

                if (present.IsDone())
                {
                    break;
                }
            }
        }
    }
}