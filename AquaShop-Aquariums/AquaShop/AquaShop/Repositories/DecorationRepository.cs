using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System.Collections.Generic;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> decorations;
        public DecorationRepository()
        {
            decorations = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => decorations.AsReadOnly();

        public void Add(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public IDecoration FindByType(string type)
        {
            return decorations.Find(x => x.GetType().Name == type);
        }

        public bool Remove(IDecoration decoration)
        {
            return decorations.Remove(decoration);
        }
    }
}
