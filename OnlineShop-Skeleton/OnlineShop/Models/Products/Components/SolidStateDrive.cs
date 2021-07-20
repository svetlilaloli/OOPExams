﻿namespace OnlineShop.Models.Products.Components
{
    public class SolidStateDrive : Component, IComponent
    {
        private const double multiplier = 1.20;
        public SolidStateDrive(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base (id, manufacturer, model, price, overallPerformance * multiplier, generation)
        {
        }
    }
}
