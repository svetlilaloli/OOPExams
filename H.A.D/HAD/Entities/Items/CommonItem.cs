using HAD.Contracts;
using System.Text;

namespace HAD.Entities.Items
{
    public class CommonItem : BaseItem, IItem
    {
        public CommonItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitPointsBonus, long damageBonus)
            : base(name, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus, damageBonus)
        { 
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"###Item: {Name}");
            result.AppendLine(base.ToString());

            return result.ToString();
        }
    }
}