using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish, IFish
    {
        public SaltwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
        }
        public new int Size { get; private set; } = 5;
        public override void Eat()
        {
            Size += 2;
        }
    }
}
