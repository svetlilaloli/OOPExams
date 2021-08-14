using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly List<IComputer> computers;
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        public Controller()
        {
            computers = new List<IComputer>();
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            if (!computers.Exists(x => x.Id == computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            if (components.Exists(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }
            IComponent component = Factory.CreateComponent(id, componentType, manufacturer, model, price, overallPerformance, generation);

            var computer = computers.Find(x => x.Id == computerId);
            computer.AddComponent(component);
            components.Add(component);
            return string.Format(SuccessMessages.AddedComponent, componentType, component.Id, computer.Id);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Exists(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            IComputer computer = Factory.CreateComputer(id, computerType, manufacturer, model, price);

            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, computer.Id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            if (!computers.Exists(x => x.Id == computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            if (peripherals.Exists(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }
            IPeripheral peripheral = Factory.CreatePeripheral(id, peripheralType, manufacturer, model, price, overallPerformance, connectionType);

            var computer = computers.Find(x => x.Id == computerId);
            computer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, peripheral.Id, computer.Id);
        }

        public string BuyBest(decimal budget)
        {
            if (computers.Count == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }
            var computersInTheBudget = computers.FindAll(x => x.Price <= budget);
            if (computersInTheBudget.Count == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }
            var bestComputer = computersInTheBudget.Find(x => x.OverallPerformance == computersInTheBudget.Max(x => x.OverallPerformance));
            computers.Remove(bestComputer);
            return bestComputer.ToString();
        }

        public string BuyComputer(int id)
        {
            var foundComputer = computers.Find(x => x.Id == id);
            if (foundComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            computers.Remove(foundComputer);
            return foundComputer.ToString();
        }
        public void Close()
        {
            Environment.Exit(0);
        }
        public string GetComputerData(int id)
        {
            var foundComputer = computers.Find(x => x.Id == id);
            if (foundComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            return foundComputer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var foundComputer = computers.Find(x => x.Id == computerId);
            if (foundComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            var foundComponent = foundComputer.Components.ToList().Find(x => x.GetType().Name == componentType);
            if (foundComponent == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, foundComputer.Id));
            }
            foundComputer.RemoveComponent(componentType);
            components.Remove(foundComponent);
            return string.Format(SuccessMessages.RemovedComponent, componentType, foundComponent.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var foundComputer = computers.Find(x => x.Id == computerId);
            if (foundComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            var foundPeripheral = foundComputer.Peripherals.ToList().Find(x => x.GetType().Name == peripheralType);
            if (foundPeripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, foundComputer.Id));
            }
            foundComputer.RemovePeripheral(peripheralType);
            peripherals.Remove(foundPeripheral);
            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, foundPeripheral.Id);
        }
    }
}
