using System;
using System.Reflection;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
        private const double baseHealth = 100;
        private const double baseArmor = 50;
        private const double abilityPoints = 40;
        public Warrior(string name) : base(name, baseHealth, baseArmor, abilityPoints, new Satchel())
        {
        }
        public void Attack(Character character)
        {   
            EnsureAlive();
            
            if (this == character)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }
            character.TakeDamage(AbilityPoints);
        }
    }
}
