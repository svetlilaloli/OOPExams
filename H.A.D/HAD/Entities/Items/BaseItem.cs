using System.Text;
using HAD.Contracts;

namespace HAD.Entities.Items
{
    public abstract class BaseItem : IItem
    {
        protected BaseItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitPointsBonus, long damageBonus)
        {
            Name = name;
            StrengthBonus = strengthBonus;
            AgilityBonus = agilityBonus;
            IntelligenceBonus = intelligenceBonus;
            HitPointsBonus = hitPointsBonus;
            DamageBonus = damageBonus;
        }

        public string Name { get; private set; }

        public long StrengthBonus { get; private set; }

        public long AgilityBonus { get; private set; }

        public long IntelligenceBonus { get; private set; }

        public long HitPointsBonus { get; private set; }

        public long DamageBonus { get; private set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"###+{StrengthBonus} Strength");
            result.AppendLine($"###+{AgilityBonus} Agility");
            result.AppendLine($"###+{IntelligenceBonus} Intelligence");
            result.AppendLine($"###+{HitPointsBonus} HitPoints");
            result.Append($"###+{DamageBonus} Damage");

            return result.ToString();
        }
    }
}