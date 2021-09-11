using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HAD.Contracts;
using HAD.Entities.Miscellaneous;

namespace HAD.Entities.Heroes
{
    public abstract class BaseHero : IHero
    {
        private long strength;
        private long agility;
        private long intelligence;
        private long hitPoints;
        private long damage;
        private readonly IInventory inventory;

        protected BaseHero(string name, long strength, long agility, long intelligence, long hitPoints, long damage)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            HitPoints = hitPoints;
            Damage = damage;
            inventory = new HeroInventory();
        }

        public string Name { get; }

        public long Strength
        {
            get => strength + inventory.TotalStrengthBonus;
            private set => strength = value;
        }

        public long Agility
        {
            get => agility + inventory.TotalAgilityBonus;
            private set => agility = value;
        }

        public long Intelligence
        {
            get => intelligence + inventory.TotalIntelligenceBonus;
            private set => intelligence = value;
        }

        public long HitPoints
        {
            get => hitPoints + inventory.TotalHitPointsBonus;
            private set => hitPoints = value;
        }

        public long Damage
        {
            get => damage + inventory.TotalDamageBonus;
            private set => damage = value;
        }

        public IReadOnlyCollection<IItem> Items
        {
            get
            {
                return inventory.CommonItems;
                // GetItems using reflection
                //return (IReadOnlyCollection<IItem>)GetType().BaseType.GetField("inventory", BindingFlags.NonPublic | BindingFlags.Instance)
                //    .GetValue(this).GetType().GetProperty("CommonItems").GetValue(this.inventory);
            }
        }

        public void AddItem(IItem item) => inventory.AddCommonItem(item);

        public void AddRecipe(IRecipe recipe) => inventory.AddRecipeItem(recipe);

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Hero: {Name}, Class: {GetType().Name}")
                .AppendLine($"HitPoints: {HitPoints}, Damage: {Damage}")
                .AppendLine($"Strength: {Strength}")
                .AppendLine($"Agility: {Agility}")
                .AppendLine($"Intelligence: {Intelligence}")
                .Append("Items:");

            if (Items.Count == 0)
            {
                result.Append(" None");
            }
            else
            {
                result.Append(Environment.NewLine);

                foreach (var item in Items)
                {
                    result.Append(item);
                }
            }

            return result.ToString().Trim();
        }
    }
}