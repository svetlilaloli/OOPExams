namespace OnlineShop.Models.Products.Computers
{
    public class Laptop : Computer, IComputer
    {
        private const double overallPerformance = 10;
        public Laptop(int id, string manufacturer, string model, decimal price) 
            : base (id, manufacturer, model, price, overallPerformance)
        {
        }
    }
}
