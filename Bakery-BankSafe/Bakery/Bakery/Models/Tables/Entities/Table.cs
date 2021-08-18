using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace Bakery.Models.Tables.Entities
{
    public abstract class Table : ITable
    {
        private readonly List<IBakedFood> foodOrders;
        private readonly List<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;
        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }
        public int TableNumber { get; }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get => numberOfPeople;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; }

        public bool IsReserved { get; private set; }

        public decimal Price => NumberOfPeople * PricePerPerson;

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();
            numberOfPeople = 0;
            IsReserved = false;
        }

        public decimal GetBill()
        {
            decimal result = Price;
            foreach (var food in foodOrders)
            {
                result += food.Price;
            }
            foreach (var drink in drinkOrders)
            {
                result += drink.Price;
            }
            return result;
        }

        public string GetFreeTableInfo()
        {
            return $"Table: {TableNumber}\nType: {GetType().Name}\nCapacity: {Capacity}\nPrice per Person: {PricePerPerson:f2}";
        }

        public void OrderDrink(IDrink drink)
        {
            if (drink != null)
            {
                drinkOrders.Add(drink);
            }
        }

        public void OrderFood(IBakedFood food)
        {
            if (food != null)
            {
                foodOrders.Add(food);
            }
        }

        public void Reserve(int numberOfPeople)
        {
            NumberOfPeople = numberOfPeople;
            IsReserved = true;
        }
    }
}
