namespace CarRacing.Models.Cars.Entities
{
    using CarRacing.Models.Cars.Contracts;

    public class SuperCar : Car, ICar
    {
        private const double AvailableFuel = 80;
        private const double FuelConsumption = 10;
        public SuperCar(string make, string model, string vin, int horsePower)
            : base (make, model, vin, horsePower, AvailableFuel, FuelConsumption)
        {
        }
    }
}
