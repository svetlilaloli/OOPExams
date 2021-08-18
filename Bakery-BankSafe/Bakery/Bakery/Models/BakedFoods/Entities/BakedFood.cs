using Bakery.Models.BakedFoods.Contracts;
using Bakery.Utilities.Messages;
using System;

namespace Bakery.Models.BakedFoods.Entities
{
    public abstract class BakedFood : IBakedFood
    {
        private string name;
        private int portion;
        private decimal price;
        public BakedFood(string name, int portion, decimal price)
        {
            Name = name;
            Portion = portion;
            Price = price;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidName);
                }
                name = value;
            }
        }
        public int Portion
        {
            get => portion;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPortion);
                }
                portion = value;
            }
        }
        public decimal Price
        {
            get => price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);
                }
                price = value;
            }
        }
        public override string ToString()
        {
            var currentFood = this.GetType();
            return $"{currentFood.Name}: {currentFood.GetProperty("Portion").GetValue(this)}g - {currentFood.GetProperty("Price").GetValue(this):f2}";
        }
    }
}
