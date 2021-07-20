﻿namespace OnlineShop.Models.Products.Components
{
    public class RandomAccessMemory : Component, IComponent
    {
        private const double multiplier = 1.20;
        public RandomAccessMemory(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base (id, manufacturer, model, price, overallPerformance * multiplier, generation)
        {
        }
    }
}
