using EasterRaces.Models.Cars.Entities;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<Car>
    {
        private readonly List<Car> cars;
        public CarRepository()
        {
            cars = new List<Car>();
        }
        public void Add(Car car)
        {
            cars.Add(car);
        }

        public IReadOnlyCollection<Car> GetAll()
        {
            return cars.AsReadOnly();
        }

        public Car GetByName(string model)
        {
            return cars.Find(x => x.Model == model);
        }

        public bool Remove(Car car)
        {
            return cars.Remove(car);
        }
    }
}
