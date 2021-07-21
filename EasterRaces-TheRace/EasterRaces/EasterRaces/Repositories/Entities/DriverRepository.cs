using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly List<Driver> drivers;
        public DriverRepository()
        {
            drivers = new List<Driver>();
        }
        public void Add(Driver driver)
        {
            drivers.Add(driver);
        }
        public IReadOnlyCollection<Driver> GetAll()
        {
            return drivers.AsReadOnly();
        }
        public Driver GetByName(string name)
        {
            return drivers.Find(x => x.Name == name);
        }
        public bool Remove(Driver driver)
        {
            return drivers.Remove(driver);
        }
    }
}
