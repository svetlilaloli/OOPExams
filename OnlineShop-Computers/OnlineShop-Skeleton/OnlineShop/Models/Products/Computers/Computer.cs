using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        public Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }
        public new double OverallPerformance
        {
            get
            {
                if (components.Count > 0)
                {
                    double totalPerformance = 0;
                    foreach (var component in components)
                    {
                        totalPerformance += component.OverallPerformance;
                    }
                    var averagePerformance = totalPerformance / components.Count;
                    return averagePerformance + base.OverallPerformance;
                }
                return base.OverallPerformance;
            }
        }
        public override decimal Price
        {
            get
            {
                decimal totalPrice = base.Price;
                if (components.Count > 0)
                {
                    totalPrice += components.Sum(x => x.Price);
                }
                if (peripherals.Count > 0)
                {
                    totalPrice += peripherals.Sum(x => x.Price);
                }
                return totalPrice;
            }
        }
        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();
        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            double peripheralsAverageOverallPerformance = peripherals.Count == 0 ? 0 : peripherals.Sum(x => x.OverallPerformance) / peripherals.Count;
            result.Append(base.ToString());
            result.Append($" \nComponents ({components.Count}):");
            foreach (var component in components)
            {
                result.Append($"  \n{component}");
            }
            result.Append($" \nPeripherals ({peripherals.Count}); Average Overall Performance ({peripheralsAverageOverallPerformance:f2}):");
            foreach (var peripheral in peripherals)
            {
                result.Append($"  \n{peripheral}");
            }
            return result.ToString();
        }
        public void AddComponent(IComponent component)
        {
            if (components.Exists(x => x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }
            components.Add(component);
        }
        public IComponent RemoveComponent(string componentType)
        {
            var component = components.Find(x => x.GetType().Name == componentType);
            if (component == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }
            components.Remove(component);
            return component;
        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Exists(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }
            peripherals.Add(peripheral);
        }
        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var peripheral = peripherals.Find(x => x.GetType().Name == peripheralType);
            if (peripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }
            peripherals.Remove(peripheral);
            return peripheral;
        }
    }
}
