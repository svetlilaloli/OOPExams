using HAD.Contracts;
using HAD.Entities.Items;
using HAD.Entities.Miscellaneous;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Telerik.JustMock;

namespace HAD.Tests
{
    public class HeroInventoryTests
    {
        private IInventory inventory;
        private IItem commonItem;
        private IRecipe recipeItem;
        private const string ItemName = "Stick";
        private const int StrengthBonus = 10;
        private const int AgilityBonus = 10;
        private const int IntelligenceBonus = 10;
        private const int HitPointsBonus = 10;
        private const int DamageBonus = 10;

        [SetUp]
        public void Setup()
        {
            inventory = new HeroInventory();
            commonItem = new CommonItem(ItemName, StrengthBonus, AgilityBonus, IntelligenceBonus, HitPointsBonus, DamageBonus);
            recipeItem = new RecipeItem(ItemName, StrengthBonus, AgilityBonus, IntelligenceBonus, HitPointsBonus, DamageBonus, new List<string>());
        }
        [Test]
        public void HeroInventory_ShouldInitializeItemAndRecipeDictionaries()
        {
            Type type = inventory.GetType();
            FieldInfo commonItems = type.GetField("commonItems", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo recipeItems = type.GetField("recipeItems", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(commonItems.GetValue(inventory));
            Assert.IsNotNull(recipeItems.GetValue(inventory));
        }
        [Test]
        public void TotalStrengthBonus_ShouldReturnTheSumOfAllCommonItemsStrengthBonuses()
        {
            //var mockedInventory = Mock.Create<IInventory>();
            //Mock.Arrange(() => mockedInventory.TotalStrengthBonus).Returns(23);
            Assert.AreEqual(0, inventory.TotalStrengthBonus);
        }
        [Test]
        public void TotalAgilityBonus_ShouldReturnTheSumOfAllCommonItemsAgilityBonuses()
        {
            Assert.AreEqual(0, inventory.TotalAgilityBonus);
        }
        [Test]
        public void TotalIntelligenceBonus_ShouldReturnTheSumOfAllCommonItemsIntelligenceBonuses()
        {
            Assert.AreEqual(0, inventory.TotalIntelligenceBonus);
        }
        [Test]
        public void TotalHitPointsBonus_ShouldReturnTheSumOfAllCommonItemsHitPointsBonuses()
        {
            Assert.AreEqual(0, inventory.TotalHitPointsBonus);
        }
        [Test]
        public void TotalDamageBonus_ShouldReturnTheSumOfAllCommonItemsDamageBonuses()
        {
            Assert.AreEqual(0, inventory.TotalDamageBonus);
        }
        [Test]
        public void CommonItems_ShouldReturnCommonItemsValuesAsReadOnlyCollection()
        {
            Assert.AreEqual("ReadOnlyCollection`1", inventory.CommonItems.GetType().Name);
        }
        [Test]
        public void AddCommonItem_ShouldAddTheItemToCommonItemsDictionary()
        {
            inventory.AddCommonItem(commonItem);
            Assert.AreEqual(1, inventory.CommonItems.Count);
        }
        //[Test]
        //public void AddCommonItem_ShouldCallCheckRecipesMethod()
        //{
        //    inventory.AddCommonItem(commonItem);

        //}
        [Test]
        public void AddRecipeItem_ShouldAddTheItemToRecipeItemsDictionary()
        {
            inventory.AddRecipeItem(recipeItem);
            
            var inventoryType = inventory.GetType();
            FieldInfo recipeItems = inventoryType.GetField("recipeItems", BindingFlags.NonPublic | BindingFlags.Instance);
            var value = (IDictionary<string, IRecipe>)recipeItems.GetValue(inventory);
            Assert.AreEqual(1, value.Count);
        }
    }
}