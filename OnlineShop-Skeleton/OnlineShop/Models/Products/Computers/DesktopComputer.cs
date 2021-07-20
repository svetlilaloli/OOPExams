namespace OnlineShop.Models.Products.Computers
{
    public class DesktopComputer : Computer, IComputer
    {
        private const double overallPerformance = 15;
        public DesktopComputer(int id, string manufacturer, string model, decimal price)
            : base (id, manufacturer, model, price, overallPerformance)
        {
        }
    }
}
