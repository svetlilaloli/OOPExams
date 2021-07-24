using Bakery.Models.Drinks.Contracts;

namespace Bakery.Models.Drinks.Entities
{
    public class Tea : Drink, IDrink
    {
        private const decimal TeaPrice = 2.5M;
        public Tea(string name, int portion, string brand) : base(name, portion, TeaPrice, brand)
        {
        }
    }
}
