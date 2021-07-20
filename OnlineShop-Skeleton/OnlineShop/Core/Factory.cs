using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;

namespace OnlineShop.Core
{
    internal static class Factory
    {
        public static IComputer CreateComputer(int id, string computerType, string manufacturer, string model, decimal price)
        {
            IComputer computer = computerType switch
            {
                "DesktopComputer" => new DesktopComputer(id, manufacturer, model, price),
                "Laptop" => new Laptop(id, manufacturer, model, price),
                _ => throw new ArgumentException("Computer type is invalid."),
            };
            return computer;
        }
        public static IComponent CreateComponent(int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComponent component = componentType switch
            {
                "CentralProcessingUnit" => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                "Motherboard" => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                "PowerSupply" => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                "RandomAccessMemory" => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                "SolidStateDrive" => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                "VideoCard" => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException("Component type is invalid."),
            };
            return component;
        }
        public static IPeripheral CreatePeripheral(int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IPeripheral peripheral = peripheralType switch
            {
                "Headset" => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                "Keyboard" => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                "Monitor" => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                "Mouse" => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException("Peripheral type is invalid."),
            };
            return peripheral;
        }
    }
}
