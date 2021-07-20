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
            var existingComputer = computers.Find(x => x.Id == computerId);
            if (existingComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            var existingComponent = existingComputer.Components.ToList().Find(x => x.Id == id);
            if (existingComponent != null)
            {
                throw new ArgumentException("Component with this id already exists.");
            }
            IComponent component = Factory.CreateComponent(id, componentType, manufacturer, model, price, overallPerformance, generation);

            existingComputer.AddComponent(component);
            components.Add(component);
            return $"Component {componentType} with id {component.Id} added successfully in computer with id {existingComputer.Id}.";
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            var existingComputer = computers.Find(x => x.Id == id);
            if (existingComputer != null)
            {
                throw new ArgumentException("Computer with this id already exists.");
            }

            IComputer computer = Factory.CreateComputer(id, computerType, manufacturer, model, price);

            computers.Add(computer);
            return $"Computer with id {computer.Id} added successfully.";
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var existingComputer = computers.Find(x => x.Id == computerId);
            if (existingComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            var existingPeripheral = existingComputer.Peripherals.ToList().Find(x => x.Id == id);
            if (existingPeripheral != null)
            {
                throw new ArgumentException("Peripheral with this id already exists.");
            }
            IPeripheral peripheral = Factory.CreatePeripheral(id, peripheralType, manufacturer, model, price, overallPerformance, connectionType);

            existingComputer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return $"Peripheral {peripheralType} with id {peripheral.Id} added successfully in computer with id {existingComputer.Id}.";
        }

        public string BuyBest(decimal budget)
        {
            if (computers.Count == 0)
            {
                throw new ArgumentException($"Can't buy a computer with a budget of ${budget}.");
            }
            var computersInTheBudget = computers.FindAll(x => x.Price <= budget);
            if (computersInTheBudget.Count == 0)
            {
                throw new ArgumentException($"Can't buy a computer with a budget of ${budget}.");
            }
            var bestComputer = computersInTheBudget.Find(x => x.OverallPerformance == computersInTheBudget.Max(x => x.OverallPerformance));
            computers.Remove(bestComputer);
            return bestComputer.ToString();
        }

        public string BuyComputer(int id)
        {
            var findComputer = computers.Find(x => x.Id == id);
            if (findComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            computers.Remove(findComputer);
            return findComputer.ToString();
        }
        public void Close()
        {
            Environment.Exit(0);
        }

        public string GetComputerData(int id)
        {
            var findComputer = computers.Find(x => x.Id == id);
            if (findComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            return findComputer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var findComputer = computers.Find(x => x.Id == computerId);
            if (findComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            var findComponent = findComputer.Components.ToList().Find(x => x.GetType().Name == componentType);
            if (findComponent == null)
            {
                throw new ArgumentException($"{componentType} does not exist in computer with id {findComputer.Id}.");
            }
            findComputer.RemoveComponent(componentType);
            components.Remove(findComponent);
            return $"Successfully removed {componentType} with id {findComponent.Id}.";
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var findComputer = computers.Find(x => x.Id == computerId);
            if (findComputer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }
            var findPeripheral = findComputer.Peripherals.ToList().Find(x => x.GetType().Name == peripheralType);
            if (findPeripheral == null)
            {
                throw new ArgumentException($"{peripheralType} does not exist in computer with id {findComputer.Id}.");
            }
            findComputer.RemovePeripheral(peripheralType);
            peripherals.Remove(findPeripheral);
            return $"Successfully removed {peripheralType} with id {findPeripheral.Id}.";
        }
    }
}
