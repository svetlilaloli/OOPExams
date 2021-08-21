using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private readonly List<Character> characterParty;
        private readonly Stack<Item> itemPool;
        public WarController()
        {
            characterParty = new List<Character>();
            itemPool = new Stack<Item>();
        }

        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];
            Character character;
            if (characterType == "Warrior")
            {
                character = new Warrior(name);
            }
            else if (characterType == "Priest")
            {
                character = new Priest(name);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }
            characterParty.Add(character);
            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            Item item;
            if (itemName == "FirePotion")
            {
                item = new FirePotion();
            }
            else if (itemName == "HealthPotion")
            {
                item = new HealthPotion();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }
            itemPool.Push(item);
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            var character = characterParty.Find(x => x.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }
            if (itemPool.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }
            var item = itemPool.Pop();
            character.Bag.AddItem(item);
            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            var character = characterParty.Find(x => x.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }
            character.UseItem(character.Bag.GetItem(itemName));
            return string.Format(SuccessMessages.UsedItem, characterName, itemName);
        }
        
        public string GetStats()
        {
            StringBuilder result = new StringBuilder();
            foreach (var ch in characterParty.OrderByDescending(x => x.Health))
            {
                result.AppendLine(string.Format(SuccessMessages.CharacterStats, ch.Name, ch.Health, ch.BaseHealth, ch.Armor, ch.BaseArmor, ch.IsAlive? "Alive" : "Dead"));
            }
            return result.ToString();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            var attacker = characterParty.Find(x => x.Name == attackerName);
            EnsureNotNull(attacker, attackerName);
            var receiver = characterParty.Find(x => x.Name == receiverName);
            EnsureNotNull(receiver, receiverName);

            if (attacker is Priest)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attacker.Name));
            }

            var warrior = (Warrior)attacker;
            warrior.Attack(receiver);
            
            var result = string.Format(SuccessMessages.AttackCharacter, attackerName, receiverName, attacker.AbilityPoints, receiverName, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor);
            
            if (!receiver.IsAlive)
            {
                result += string.Format(Environment.NewLine + SuccessMessages.AttackKillsCharacter, receiver.Name);
            }
            
            return result;
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string receiverName = args[1];

            var healer = characterParty.Find(x => x.Name == healerName);
            EnsureNotNull(healer, healerName);
            var receiver = characterParty.Find(x => x.Name == receiverName);
            EnsureNotNull(receiver, receiverName);

            if (healer is Warrior)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healer.Name));
            }

            var priest = (Priest)healer;
            priest.Heal(receiver);
            
            return string.Format(SuccessMessages.HealCharacter, healer.Name, receiver.Name, healer.AbilityPoints, receiver.Name, receiver.Health);
        }
        private void EnsureNotNull(Character character, string name)
        {
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, name));
            }
        }
    }
}
