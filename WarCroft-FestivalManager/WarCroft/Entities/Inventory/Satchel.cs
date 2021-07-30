namespace WarCroft.Entities.Inventory
{
    public class Satchel : Bag, IBag
    {
        private const int capacity = 20;
        public Satchel() : base(capacity)
        {
        }
    }
}
