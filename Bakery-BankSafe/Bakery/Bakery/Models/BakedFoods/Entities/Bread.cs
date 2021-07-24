using Bakery.Models.BakedFoods.Contracts;

namespace Bakery.Models.BakedFoods.Entities
{
    public class Bread : Food, IBakedFood
    {
        private const int InitialBreadPortion = 200;
        public Bread(string name, decimal price) : base(name, InitialBreadPortion, price)
        {
        }
    }
}
