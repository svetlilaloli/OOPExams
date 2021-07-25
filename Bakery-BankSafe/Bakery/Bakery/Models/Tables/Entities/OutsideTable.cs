using Bakery.Models.Tables.Contracts;

namespace Bakery.Models.Tables.Entities
{
    public class OutsideTable : Table, ITable
    {
        private const decimal InitialPricePerPerson = 3.5M;
        public OutsideTable(int tableNumber, int capacity)
            : base (tableNumber, capacity, InitialPricePerPerson)
        {
        }
    }
}
