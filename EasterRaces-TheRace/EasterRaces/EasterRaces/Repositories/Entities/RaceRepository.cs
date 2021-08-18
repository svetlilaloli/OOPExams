using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;
        public RaceRepository()
        {
            races = new List<IRace>();
        }
        public void Add(IRace race)
        {
            races.Add(race);
        }
        public IReadOnlyCollection<IRace> GetAll()
        {
            return races.AsReadOnly();
        }
        public IRace GetByName(string name)
        {
            return races.Find(x => x.Name == name);
        }
        public bool Remove(IRace race)
        {
            return races.Remove(race);
        }
    }
}
