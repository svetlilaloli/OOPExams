using EasterRaces.Models.Cars.Contracts;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car, ICar
    {
        private const double cubicCentimeters = 3000;
        private const int minHorsePower = 250;
        private const int maxHorsePower = 450;
        public SportsCar(string model, int horsePower)
            : base(model, horsePower, cubicCentimeters, minHorsePower, maxHorsePower)
        {
        }
    }
}
