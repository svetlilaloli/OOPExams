using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish, IFish
    {
        public FreshwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
        }
        public new int Size { get; private set; } = 3;
        public override void Eat()
        {
            Size += 3;
        }
    }
}
