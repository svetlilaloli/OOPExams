using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly List<IEgg> eggs;
        public EggRepository()
        {
            eggs = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models => eggs.AsReadOnly();

        public void Add(IEgg egg)
        {
            var existingEgg = eggs.Find(x => x.Name == egg.Name);
            if (existingEgg == null)
            {
                eggs.Add(egg);
            }
        }

        public IEgg FindByName(string name)
        {
            return eggs.Find(x => x.Name == name);
        }

        public bool Remove(IEgg egg)
        {
            return eggs.Remove(egg);
        }
    }
}
