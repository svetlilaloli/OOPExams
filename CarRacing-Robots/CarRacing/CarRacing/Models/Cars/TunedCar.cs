namespace CarRacing.Models.Cars
{
    using CarRacing.Models.Cars.Contracts;

    public class TunedCar : Car, ICar
    {
        private const double AvailableFuel = 65;
        private const double FuelConsumption = 7.5;
        public TunedCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, AvailableFuel, FuelConsumption)
        {
        }
    }
}
