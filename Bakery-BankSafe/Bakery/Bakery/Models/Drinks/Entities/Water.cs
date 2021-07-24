using Bakery.Models.Drinks.Contracts;

namespace Bakery.Models.Drinks.Entities
{
    public class Water : Drink, IDrink
    {
        private const decimal WaterPrice = 1.5M;
        public Water(string name, int portion, string brand) : base(name, portion, WaterPrice, brand)
        {
        }
    }
}
