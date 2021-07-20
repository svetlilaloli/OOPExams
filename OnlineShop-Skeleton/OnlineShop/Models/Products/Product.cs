using System;
using System.Reflection;

namespace OnlineShop.Models.Products
{
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerformance;
        public Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id can not be less or equal than 0.");
                }
                id = value;
            }
        }
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Manufacturer can not be empty.");
                }
                manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model can not be empty.");
                }
                model = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price can not be less or equal than 0.");
                }
                price = value;
            }
        }

        public double OverallPerformance
        {
            get
            {
                return overallPerformance;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Overall Performance can not be less or equal than 0.");
                }
                overallPerformance = value;
            }
        }
        public override string ToString()
        {
            var type = GetType();
            return $"Overall Performance: {type.GetProperty("OverallPerformance").GetValue(this, null):f2}. Price: {type.GetProperty("Price").GetValue(this, null):f2} - {type.Name}: {Manufacturer} {Model} (Id: {Id})";
        }
    }
}
