using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double cubicCentimetersConst = 5000;
        private const int minHorsePowerConst = 400;
        private const int maxHorsePowerConst = 600;

        public MuscleCar(string model, int horsePower) 
            : base(model, horsePower, cubicCentimetersConst, minHorsePowerConst, maxHorsePowerConst)
        {
        }
    }
}