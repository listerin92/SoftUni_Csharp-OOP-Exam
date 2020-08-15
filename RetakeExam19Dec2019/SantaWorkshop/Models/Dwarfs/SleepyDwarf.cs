namespace SantaWorkshop.Models.Dwarfs
{
    public class SleepyDwarf : Dwarf
    {
        private const int INITIAL_ENERGY = 50;
        private const int ADDITIONAL_WORK_ENERGY_DECR = 5;

        public SleepyDwarf(string name)
            : base(name, INITIAL_ENERGY)
        {
        }

        public override void Work()
        {
            this.Energy -= ADDITIONAL_WORK_ENERGY_DECR;
            base.Work();
        }
    }
}