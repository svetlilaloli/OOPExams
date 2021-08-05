using AquaShop.Models.Decorations.Contracts;

namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration, IDecoration
    {
        private const int comfort = 5;
        private const decimal price = 10;
        public Plant() : base(comfort, price)
        {
        }
    }
}
