﻿namespace OnlineShop.Models.Products.Components
{
    public class Motherboard : Component, IComponent
    {
        private const double multiplier = 1.25;
        public Motherboard(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * multiplier, generation)
        {   
        }
    }
}
