namespace OnlineShop.Models.Products.Peripherals
{
    public class Keyboard : Peripheral, IPeripheral
    {
        public Keyboard(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance, connectionType)
        {
        }
    }
}
