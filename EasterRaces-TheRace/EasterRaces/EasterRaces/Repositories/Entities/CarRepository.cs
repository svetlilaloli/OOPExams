using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;
        public CarRepository()
        {
            cars = new List<ICar>();
        }
        public void Add(ICar car)
        {
            cars.Add(car);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return cars.AsReadOnly();
        }

        public ICar GetByName(string model)
        {
            return cars.Find(x => x.Model == model);
        }

        public bool Remove(ICar car)
        {
            return cars.Remove(car);
        }
    }
}
