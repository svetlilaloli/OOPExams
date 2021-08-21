using System;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double health;
        private double armor;
        protected Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            Health = health;
            BaseArmor = armor;
            Armor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
                name = value;
            }
        }
        public double BaseHealth { get; }
        public double Health
        {
            get => health;
            internal set
            {
                if (value < 0)
                {
                    health = 0;
                }
                else if (value > BaseHealth)
                {
                    health = BaseHealth;
                }
                else
                {
                    health = value;
                }
            }
        }
        public double BaseArmor { get; }
        public double Armor
        {
            get => armor;
            private set
            {
                if (value > 0)
                {
                    armor = value;
                }
                else
                {
                    armor = 0;
                }
            }
        }
        public double AbilityPoints { get; }
        public bool IsAlive => Health > 0;
        public Bag Bag { get; }
        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();
            if (hitPoints > Armor)
            {
                hitPoints -= Armor;
                Armor = 0;
                Health -= hitPoints;
            }
            else
            {
                Armor -= hitPoints;
            }
        }
        public void UseItem(Item item)
        {
            EnsureAlive();
            item.AffectCharacter(this);
        }
    }
}