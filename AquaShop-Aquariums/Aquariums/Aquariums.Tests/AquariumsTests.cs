namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    public class AquariumsTests
    {
        private const string reportMessage = "Fish available at RiverWorld: Nemo, Nemo";
        private const string aquariumName = "RiverWorld";
        private const int Capacity = 2;
        private const string fishName = "Nemo";
        private Aquarium aquarium;
        private Fish fish;
        [SetUp]
        public void SetUp()
        {
            aquarium = new Aquarium(aquariumName, Capacity);
            fish = new Fish(fishName);
        }
        [Test]
        public void Constructor_WithValidParameters_ShouldSetTheFieldsCorrectlyAndInitializeAList()
        {
            var expected = 0;
            var actual = aquarium.Count;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(aquariumName, aquarium.Name);
            Assert.AreEqual(Capacity, aquarium.Capacity);
        }
        [TestCase(null)]
        [TestCase("")]
        public void Constructor_WithEmptyName_ShouldThrowArgumentNullException(string name)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Aquarium(name, Capacity));
            StringAssert.Contains("Invalid aquarium name!", ex.Message);
        }
        [TestCase(-1)]
        public void Constructor_WithNegativeCapacity_ShouldThrowArgumentException(int capacity)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Aquarium(aquariumName, capacity));
            StringAssert.Contains("Invalid aquarium capacity!", ex.Message);
        }
        [Test]
        public void Add_WhenCapacityIsFull_ShouldThrowInvalidOperationException()
        {
            aquarium.Add(fish);
            aquarium.Add(fish);
            var ex = Assert.Throws<InvalidOperationException>(() => aquarium.Add(fish));
            StringAssert.Contains("Aquarium is full!", ex.Message);
        }
        [Test]
        public void Add_WhenCapacityNotReached_AddsTheFishToTheList()
        {
            aquarium.Add(fish);
            var expected = 1;
            var actual = aquarium.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void RemoveFish_WhenFishIsNotFound_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(fishName));
            StringAssert.Contains($"Fish with the name {fishName} doesn't exist!", ex.Message);
        }
        [Test]
        public void RemoveFish_WhenFishIsFound_ShouldRemoveTheFish()
        {
            aquarium.Add(fish);
            aquarium.RemoveFish(fishName);
            var expected = 0;
            var actual = aquarium.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SellFish_WhenFishIsNotFound_ShouldThrowInvalidOperationException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => aquarium.SellFish(fishName));
            StringAssert.Contains($"Fish with the name {fishName} doesn't exist!", ex.Message);
        }
        [Test]
        public void SellFish_WhenFishIsFound_ShouldReturnTheFishAndSetNotAvailable()
        {
            aquarium.Add(fish);
            
            var expected = fish;
            var actual = aquarium.SellFish(fishName);
            
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(fish.Available);
        }
        [Test]
        public void Report_ShouldReturnStringReport()
        {
            aquarium.Add(fish);
            aquarium.Add(fish);
            var actual = aquarium.Report();
            StringAssert.Contains(reportMessage, actual);
        }
    }
}
