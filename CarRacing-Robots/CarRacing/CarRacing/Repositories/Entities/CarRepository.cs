namespace CarRacing.Repositories.Entities
{
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Repositories.Contracts;
    using CarRacing.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;
        public CarRepository()
        {
            cars = new List<ICar>();
        }
        public IReadOnlyCollection<ICar> Models => cars.AsReadOnly();

        public void Add(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            cars.Add(car);
        }

        public ICar FindBy(string vin)
        {
            return cars.Find(x => x.VIN == vin);
        }

        public bool Remove(ICar car)
        {
            return cars.Remove(car);
        }
    }
}
