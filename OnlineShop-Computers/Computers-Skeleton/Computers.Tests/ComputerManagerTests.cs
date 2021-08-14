using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    public class Tests
    {
        private ComputerManager manager;
        private Computer computer;
        private const string manufacturer = "Dell";
        private const string model = "XPS";
        private const decimal price = 2000;
        [SetUp]
        public void Setup()
        {
            manager = new ComputerManager();
            computer = new Computer(manufacturer, model, price);
        }

        [Test]
        public void Constructor_InitiatesAList()
        {
            Assert.IsNotNull(manager.Computers);
        }
        [Test]
        public void Computers_WhenManagerIsEmpty_ShouldReturnZero()
        {
            Assert.IsTrue(manager.Computers.Count == 0);
        }
        [Test]
        public void Count_WhenManagerIsEmpty_ShouldReturnZero()
        {
            Assert.IsTrue(manager.Count == 0);
        }
        [Test]
        public void AddComputer_NullComputerParameter_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => manager.AddComputer(null));
            StringAssert.Contains("Can not be null!", ex.Message);
        }
        [Test]
        public void AddComputer_ValidComputerParameter_ShouldBeAddedToTheList()
        {
            manager.AddComputer(computer);
            Assert.IsTrue(manager.Count == 1);
        }
        [Test]
        public void AddComputer_AlreadyAddedComputer_ShouldThrowArgumentException()
        {
            manager.AddComputer(computer);
            var ex = Assert.Throws<ArgumentException>(() => manager.AddComputer(computer));
            StringAssert.Contains("This computer already exists.", ex.Message);
        }
        [Test]
        public void RemoveComputer_NotExistingComputerInTheManager_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => manager.RemoveComputer(manufacturer, model));
            StringAssert.Contains("There is no computer with this manufacturer and model.", ex.Message);
        }
        [Test]
        public void RemoveComputer_ExistingComputerFromTheManager_ShouldRemoveItFromTheList()
        {
            manager.AddComputer(computer);
            manager.RemoveComputer(manufacturer, model);
            var expected = 0;
            var actual = manager.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void RemoveComputer_ExistingComputerFromTheManager_ShouldReturnTheComputer()
        {
            manager.AddComputer(computer);
            
            var expected = computer;
            var actual = manager.RemoveComputer(manufacturer, model);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(null, model)]
        [TestCase(manufacturer, null)]
        public void GetComputer_NullParameter_ShouldThrowArgumentNullException(string manufacturer, string model)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => manager.GetComputer(manufacturer, model));
            StringAssert.Contains("Can not be null!", ex.Message);
        }
        [Test]
        public void GetComputer_ExistingComputerFromTheManager_ShouldReturnTheComputer()
        {
            manager.AddComputer(computer);

            var expected = computer;
            var actual = manager.GetComputer(manufacturer, model);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetComputer_NotExistingComputer_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => manager.GetComputer(manufacturer, model));
            StringAssert.Contains("There is no computer with this manufacturer and model.", ex.Message);
        }
        [Test]
        public void GetComputersByManufacturer_NullManufacturerParameter_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => manager.GetComputersByManufacturer(null));
            StringAssert.Contains("Can not be null!", ex.Message);
        }
        [Test]
        public void GetComputersByManufacturer_WithExistingManufacturer_ShouldReturnCollectionOfMatchingComputers()
        {
            Computer newComputer = new Computer(manufacturer, "Latitude", 800); 
            manager.AddComputer(computer);
            manager.AddComputer(newComputer);
            var expected = new List<Computer>() { computer, newComputer };
            var actual = manager.GetComputersByManufacturer(manufacturer);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetComputersByManufacturer_WithNotExistingManufacturer_ShouldReturnEmptyCollection()
        {
            Computer newComputer = new Computer(manufacturer, "Latitude", 800);
            manager.AddComputer(computer);
            manager.AddComputer(newComputer);
            var expected = new List<Computer>();
            var actual = manager.GetComputersByManufacturer("Lenovo");
            Assert.AreEqual(expected, actual);
        }
    }
}