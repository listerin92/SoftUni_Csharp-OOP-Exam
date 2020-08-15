using SantaWorkshop.Models.Instruments.Contracts;

namespace SantaWorkshop.Models.Instruments
{
    public class Instrument : IInstrument
    {
        private const int USE_POWER_DECR = 10;
        private int power;
        public Instrument(int power)
        {
            this.Power = power;
        }
        public int Power
        {
            get => this.power;
            private set 
                => this.power = value > 0 ? value : 0;
        }
        public void Use()
        {
            this.Power -= USE_POWER_DECR;
        }

        public bool IsBroken() => this.Power == 0;
    }
}
