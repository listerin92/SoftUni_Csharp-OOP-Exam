namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double cubicCentimetersConst = 3000;
        private const int minHorsePowerConst = 250;
        private const int maxHorsePowerConst = 450;
        public SportsCar(string model, int horsePower) 
            : base(model, horsePower, cubicCentimetersConst, minHorsePowerConst, maxHorsePowerConst)
        {
        }
    }
}
