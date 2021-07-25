using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.BakedFoods.Entities;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Drinks.Entities;
using Bakery.Models.Tables.Contracts;
using Bakery.Models.Tables.Entities;
using Bakery.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private readonly List<IBakedFood> bakedFoods;
        private readonly List<IDrink> drinks;
        private readonly List<ITable> tables;
        private decimal totalIncome;
        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
            totalIncome = 0;
        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink;

            if (type == "Water")
            {
                drink = new Water(name, portion, brand);
            }
            else
            {
                drink = new Tea(name, portion, brand);
            }
            drinks.Add(drink);
            return string.Format(OutputMessages.DrinkAdded, drink.Name, drink.Brand);
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food;
            if (type == "Bread")
            {
                food = new Bread(name, price);
            }
            else
            {
                food = new Cake(name, price);
            }
            bakedFoods.Add(food);
            return string.Format(OutputMessages.FoodAdded, food.Name, food.GetType().Name);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table;
            if (type == "InsideTable")
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else
            {
                table = new OutsideTable(tableNumber, capacity);
            }
            tables.Add(table);
            return string.Format(OutputMessages.TableAdded, table.TableNumber);
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder result = new StringBuilder();

            foreach (var table in tables.Where(x => x.IsReserved == false))
            {
                result.AppendLine($"{table.GetFreeTableInfo()}");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public string GetTotalIncome()
        {
            return string.Format(OutputMessages.TotalIncome, totalIncome);
        }

        public string LeaveTable(int tableNumber)
        {
            var table = tables.Find(x => x.TableNumber == tableNumber);
            decimal bill = table.GetBill();
            string result = $"Table: {table.TableNumber}\nBill: {bill:f2}";
            totalIncome += bill;
            table.Clear();
            return result;
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.Find(x => x.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            var drink = drinks.Find(x => x.Name == drinkName);
            if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            table.OrderDrink(drink);
            return string.Format(OutputMessages.FoodOrderSuccessful, table.TableNumber, drink.Name + ' ' + drink.Brand);
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.Find(x => x.TableNumber == tableNumber);
            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            var food = bakedFoods.Find(x => x.Name == foodName);
            if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return string.Format(OutputMessages.FoodOrderSuccessful, table.TableNumber, food.Name);
        }

        public string ReserveTable(int numberOfPeople)
        {
            var notReservedTables = tables.FindAll(x => x.IsReserved == false);
            var table = notReservedTables.Find(x => x.Capacity >= numberOfPeople);
            if (table == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }
            else
            {
                table.Reserve(numberOfPeople);
                return string.Format(OutputMessages.TableReserved, table.TableNumber, table.NumberOfPeople);
            }
        }
    }
}
