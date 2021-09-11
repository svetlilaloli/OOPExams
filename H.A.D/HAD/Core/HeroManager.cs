using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAD.Contracts;
using HAD.Core.Factory;
using HAD.Entities.Heroes;
using HAD.Entities.Items;
using HAD.Utilities;

namespace HAD.Core
{
    public class HeroManager : IManager
    {
        private readonly IDictionary<string, IHero> heroes;

        public HeroManager()
        {
            heroes = new Dictionary<string, IHero>();
        }

        public string AddHero(IList<string> arguments)
        {
            string heroName = arguments[0];
            string heroTypeName = arguments[1];

            IHero hero = new HeroFactory().CreateHero(heroTypeName, heroName);

            heroes.Add(heroName, hero);

            string result = string.Format(Constants.HeroCreateMessage, hero.GetType().Name, hero.Name); 
            return result;
        }

        public string AddItem(IList<string> arguments)
        {
            string itemName = arguments[0];
            string heroName = arguments[1];
            long strengthBonus = long.Parse(arguments[2]);
            long agilityBonus = long.Parse(arguments[3]);
            long intelligenceBonus = long.Parse(arguments[4]);
            long hitPointsBonus = long.Parse(arguments[5]);
            long damageBonus = long.Parse(arguments[6]);

            IItem newItem = new CommonItem(
                itemName,
                strengthBonus,
                agilityBonus,
                intelligenceBonus,
                hitPointsBonus,
                damageBonus);

            this.heroes[heroName].AddItem(newItem);

            string result = string.Format(Constants.ItemCreateMessage, newItem.Name, heroName);
            return result;
        }

        public string AddRecipe(IList<string> arguments)
        {
            string itemName = arguments[0];
            string heroName = arguments[1];
            long strengthBonus = long.Parse(arguments[2]);
            long agilityBonus = long.Parse(arguments[3]);
            long intelligenceBonus = long.Parse(arguments[4]);
            long hitPointsBonus = long.Parse(arguments[5]);
            long damageBonus = long.Parse(arguments[6]);
            List<string> requiredItems = new List<string>();

            for (int i = 7; i < arguments.Count; i++)
            {
                requiredItems.Add(arguments[i]);
            }

            IRecipe newItem = new RecipeItem(
                itemName,
                strengthBonus,
                agilityBonus,
                intelligenceBonus,
                hitPointsBonus,
                damageBonus,
                requiredItems);

            this.heroes[heroName].AddRecipe(newItem);

            string result = string.Format(Constants.RecipeCreateMessage, newItem.Name, heroName);
            return result;
        }

        public string Inspect(IList<string> arguments)
        {
            string heroName = arguments[0];

            return this.heroes[heroName].ToString();
        }

        public string Quit()
        {
            int counter = 1;

            StringBuilder result = new StringBuilder();

            var sortedHeroes = this.heroes
                .Values
                .OrderByDescending(h => h.Strength + h.Intelligence + h.Agility)
                .ThenByDescending(h => h.HitPoints + h.Damage)
                .ToList();

            foreach (var hero in sortedHeroes)
            {
                string itemLine = hero.Items.Count > 0
                    ? string.Join(", ", hero.Items.Select(i => i.Name))
                    : "None";

                result
                    .AppendLine($"{counter}. {hero.GetType().Name}: {hero.Name}")
                    .AppendLine($"###HitPoints: {hero.HitPoints}")
                    .AppendLine($"###Damage: {hero.Damage}")
                    .AppendLine($"###Strength: {hero.Strength}")
                    .AppendLine($"###Agility: {hero.Agility}")
                    .AppendLine($"###Intelligence: {hero.Intelligence}")
                    .AppendLine($"###Items: {itemLine}");

                counter++;
            }

            return result.ToString().Trim();
        }
    }
}