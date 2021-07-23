using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private const string DriverName = "Adam";
        private const string Model = "Porsche";
        private const int HorsePower = 450;
        private const double CubicCentimeters = 2.2;
        private const int MinParticipants = 2;
        private RaceEntry raceEntry;
        private UnitCar car;
        private UnitDriver driver;

        [SetUp]
        public void Setup()
        {
            raceEntry = new RaceEntry();
            car = new UnitCar(Model, HorsePower, CubicCentimeters);
            driver = new UnitDriver(DriverName, car);
        }

        [Test]
        public void Constructor_ShouldInitializeDictionary()
        {
            Assert.IsNotNull(raceEntry);
        }
        [Test]
        public void Counter_ShouldReturnCorrectDriversCount()
        {
            var expected = 0;
            var actual = raceEntry.Counter;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AddDriver_WithNullParameter_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(null));
            StringAssert.Contains("Driver cannot be null.", ex.Message);
        }
        [Test]
        public void AddDriver_WithAlreadyAddedDriver_ShouldThrowInvalidOperationException()
        {
            raceEntry.AddDriver(driver);
            var ex = Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(driver));
            StringAssert.Contains($"Driver {driver.Name} is already added.", ex.Message);
        }
        [Test]
        public void AddDriver_WithNewValidDriver_ShouldAddTheDriverToTheRaceEntry()
        {
            raceEntry.AddDriver(driver);
            var expected = 1;
            var actual = raceEntry.Counter;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AddDriver_WhenDriverIsAdded_ShouldReturnCorrectStringMessage()
        {
            var message = raceEntry.AddDriver(driver);
            
            StringAssert.Contains($"Driver {driver.Name} added in race.", message);
        }
        [Test]
        public void CalculateAverageHorsePower_WhenNotEnoughDriversInTheRace_ShouldThrowInvalidOperationException()
        {
            raceEntry.AddDriver(driver);
            var ex = Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
            StringAssert.Contains($"The race cannot start with less than {MinParticipants} participants.", ex.Message);
        }
        [Test]
        public void CalculateAverageHorsePower_WhenEnoughDriversInTheRace_ShouldReturnCorrectAverageHorsepower()
        {
            var driver2 = new UnitDriver("John", car);
            raceEntry.AddDriver(driver);
            raceEntry.AddDriver(driver2);
            var expected = 450;
            var actual = raceEntry.CalculateAverageHorsePower();
            Assert.AreEqual(expected, actual);
        }
    }
}