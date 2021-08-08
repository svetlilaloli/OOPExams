namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [TestFixture]
    public class BagTests
    {
        private const string presentName = "Ball";
        private const double presentMagic = 20;
        private Present present;
        private Bag bag;
        [SetUp]
        public void SetUp()
        {
            present = new Present(presentName, presentMagic);
            bag = new Bag();
        }
        [Test]
        public void Bag_ShouldInitializeAListOfPresents()
        {
            Type type = bag.GetType();
            FieldInfo field = type.GetField("presents", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            var actual = field.GetValue(bag);
            
            Assert.IsNotNull(actual);
        }
        [Test]
        public void Create_WithNullPresentParameter_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => bag.Create(null));
            StringAssert.Contains("Present is null", ex.Message);
        }
        [Test]
        public void Create_WithExistingPresentParameter_ShouldThrowInvalidOperationException()
        {
            bag.Create(present);

            var ex = Assert.Throws<InvalidOperationException>(() => bag.Create(present));
            StringAssert.Contains("This present already exists!", ex.Message);
        }
        [Test]
        public void Create_WithNotExistingValidPresentParameter_ShouldAddItToTheBag()
        {
            bag.Create(present);
            Type type = bag.GetType();
            FieldInfo field = type.GetField("presents", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            var actual = field.GetValue(bag);
            Assert.Contains(present, (System.Collections.ICollection)actual);
        }
        [Test]
        public void Create_WithNotExistingValidPresentParameter_ShouldReturnString()
        {
            StringAssert.Contains($"Successfully added present {present.Name}.", bag.Create(present));
        }
        [Test]
        public void Remove_WhenThePresentIsRemoved_ShouldReturnTrue()
        {
            bag.Create(present);
            Assert.IsTrue(bag.Remove(present));
        }
        [Test]
        public void Remove_WhenThePresentWasNotFound_ShouldReturnFalse()
        {
            Assert.IsFalse(bag.Remove(present));
        }
        [Test]
        public void GetPresentWithLeastMagic_ShouldReturnPresentWithLeastMagic()
        {
            var presentWithLeastMagic = new Present(presentName, 13);
            bag.Create(present);
            bag.Create(presentWithLeastMagic);
            Assert.AreSame(presentWithLeastMagic, bag.GetPresentWithLeastMagic());
        }
        [Test]
        public void GetPresent_WithExistingPresentNameParameter_ShouldReturnThePresent()
        {
            bag.Create(present);
            var expected = present;
            var actual = bag.GetPresent(presentName);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetPresent_WithNotExistingPresentNameParameter_ShouldReturnNull()
        {
            var actual = bag.GetPresent(presentName);
            Assert.AreEqual(null, actual);
        }
        [Test]
        public void GetPresents_ShouldReturnThePresentsListAsReadOnlyCollection()
        {
            var actual = bag.GetPresents();
            Assert.That(actual is IReadOnlyCollection<Present>);
        }
    }
}
