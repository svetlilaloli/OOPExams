using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System.Collections.Generic;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> astronauts;
        public AstronautRepository()
        {
            astronauts = new List<IAstronaut>();
        }
        public IReadOnlyCollection<IAstronaut> Models => astronauts.AsReadOnly();

        public void Add(IAstronaut astronaut)
        {
            var existingAstronaut = astronauts.Find(x => x.Name == astronaut.Name);
            if (existingAstronaut == null)
            {
                astronauts.Add(astronaut);
            }
        }

        public IAstronaut FindByName(string name)
        {
            return astronauts.Find(x => x.Name == name);
        }

        public bool Remove(IAstronaut astronaut)
        {
            return astronauts.Remove(astronaut);
        }
    }
}
