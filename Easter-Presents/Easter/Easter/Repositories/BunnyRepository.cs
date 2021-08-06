using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> bunnies;
        public BunnyRepository()
        {
            bunnies = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models => bunnies.AsReadOnly();

        public void Add(IBunny bunny)
        {
            var existingBunny = bunnies.Find(x => x.Name == bunny.Name);
            if (existingBunny == null)
            {
                bunnies.Add(bunny);
            }
        }

        public IBunny FindByName(string name)
        {
            return bunnies.Find(x => x.Name == name);
        }

        public bool Remove(IBunny bunny)
        {
            return bunnies.Remove(bunny);
        }
    }
}
