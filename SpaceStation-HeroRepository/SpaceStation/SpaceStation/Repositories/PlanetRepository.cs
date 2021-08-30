using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System.Collections.Generic;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets;
        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();

        public void Add(IPlanet planet)
        {
            var existingPlanet = planets.Find(x => x.Name == planet.Name);
            if (existingPlanet == null)
            {
                planets.Add(planet);
            }
        }

        public IPlanet FindByName(string name)
        {
            return planets.Find(x => x.Name == name);
        }

        public bool Remove(IPlanet planet)
        {
            return planets.Remove(planet);
        }
    }
}
