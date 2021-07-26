using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private Item item2;
        private BankVault vault;
        private const string Owner = "Bond";
        private const string ItemId = "007";
        private const string InvalidCell = "a1";
        private const string ValidCell = "C4";
        private const string ValidCell2 = "B2";
        [SetUp]
        public void Setup()
        {
            item = new Item(Owner, ItemId);
            item2 = new Item(Owner, ItemId + 1);
            vault = new BankVault();
        }

        [Test]
        public void Constructor_ShouldInitializeTheDictionary()
        {
            bool actual = vault.VaultCells.Count > 0;
            
            Assert.IsTrue(actual);
        }
        [Test]
        public void VaultCells_ShouldReturnImmutableDictionary()
        {
            var expected = new Dictionary<string, Item>().ToImmutableDictionary().GetType().Name;
            var actual = vault.VaultCells.GetType().Name;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AddItem_IntoInvalidCell_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => vault.AddItem(InvalidCell, item));
            StringAssert.Contains("Cell doesn't exists!", ex.Message);
        }
        [Test]
        public void AddItem_IntoAlreadyTakenCell_ShouldThrowArgumentException()
        {
            vault.AddItem(ValidCell, item);
            var ex = Assert.Throws<ArgumentException>(() => vault.AddItem(ValidCell, item2));
            StringAssert.Contains("Cell is already taken!", ex.Message);
        }
        [Test]
        public void AddItem_ItemAlreadyInCell_ShouldThrowInvalidOperationException()
        {
            vault.AddItem(ValidCell, item);
            var ex = Assert.Throws<InvalidOperationException>(() => vault.AddItem(ValidCell2, item));
            StringAssert.Contains("Item is already in cell!", ex.Message);
        }
        [Test]
        public void AddItem_NewItemInEmptyCell_ShouldAddTheItemInTheCell()
        {
            vault.AddItem(ValidCell, item);
            var expected = item;
            var actual = vault.VaultCells[ValidCell];
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AddItem_NewItemInEmptyCell_ShouldReturnSuccessMessage()
        {
            var actual = vault.AddItem(ValidCell, item);
            
            StringAssert.Contains($"Item:{item.ItemId} saved successfully!", actual);
        }
        [Test]
        public void RemoveItem_FromInvalidCell_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => vault.RemoveItem(InvalidCell, item));
            StringAssert.Contains("Cell doesn't exists!", ex.Message);
        }
        [Test]
        public void RemoveItem_NonExistingItem_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => vault.RemoveItem(ValidCell, item));
            StringAssert.Contains("Item in that cell doesn't exists!", ex.Message);
        }
        [Test]
        public void RemoveItem_ExistingItemFromValidCell_ShouldSetTheCellToNull()
        {
            vault.AddItem(ValidCell, item);
            vault.RemoveItem(ValidCell, item);
            var actual = vault.VaultCells[ValidCell];
            Assert.IsNull(actual);
        }
        [Test]
        public void RemoveItem_ExistingItemFromValidCell_ShouldReturnSuccessMessage()
        {
            vault.AddItem(ValidCell, item);
            var actual = vault.RemoveItem(ValidCell, item);
            StringAssert.Contains($"Remove item:{item.ItemId} successfully!", actual);
        }
    }
}