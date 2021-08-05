using AquaShop.Models.Aquariums.Contracts;

namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium, IAquarium
    {
        private const int capacity = 50;
        public FreshwaterAquarium(string name) : base(name, capacity)
        {
        }
    }
}
