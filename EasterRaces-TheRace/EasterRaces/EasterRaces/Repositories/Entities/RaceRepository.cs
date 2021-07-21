using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<Race>
    {
        private readonly List<Race> races;
        public RaceRepository()
        {
            races = new List<Race>();
        }
        public void Add(Race race)
        {
            races.Add(race);
        }
        public IReadOnlyCollection<Race> GetAll()
        {
            return races.AsReadOnly();
        }
        public Race GetByName(string name)
        {
            return races.Find(x => x.Name == name);
        }
        public bool Remove(Race race)
        {
            return races.Remove(race);
        }
    }
}
