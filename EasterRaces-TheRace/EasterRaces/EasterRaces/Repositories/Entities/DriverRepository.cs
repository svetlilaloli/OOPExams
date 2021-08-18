using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> drivers;
        public DriverRepository()
        {
            drivers = new List<IDriver>();
        }
        public void Add(IDriver driver)
        {
            drivers.Add(driver);
        }
        public IReadOnlyCollection<IDriver> GetAll()
        {
            return drivers.AsReadOnly();
        }
        public IDriver GetByName(string name)
        {
            return drivers.Find(x => x.Name == name);
        }
        public bool Remove(IDriver driver)
        {
            return drivers.Remove(driver);
        }
    }
}
