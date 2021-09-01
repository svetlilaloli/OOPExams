using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private const string Name = "Peppa";
    private const int Level = 15;
    private Hero hero;
    private HeroRepository repository;
    [SetUp]
    public void SetUp()
    {
        hero = new Hero(Name, Level);
        repository = new HeroRepository();
    }
    [Test]
    public void Constructor_ShouldInitializeListOfData()
    {
        repository = new HeroRepository();
        Type type = repository.GetType();
        FieldInfo data = type.GetField("data", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(data);
    }
    [Test]
    public void Create_WithNullHero_ShouldThrowArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => repository.Create(null));
        StringAssert.Contains("Hero is null", ex.Message);
    }
    [Test]
    public void Create_AlreadyExistingHero_ShouldThrowInvalidOperationException()
    {
        repository.Create(hero);
        var ex = Assert.Throws<InvalidOperationException>(() => repository.Create(hero));
        StringAssert.Contains($"Hero with name {Name} already exists", ex.Message);
    }
    [Test]
    public void Create_NotExistingHero_ShouldAddItToTheDataList()
    {
        repository.Create(hero);
        Type type = repository.GetType();
        FieldInfo data = type.GetField("data", BindingFlags.NonPublic | BindingFlags.Instance);
        var actual = data.GetValue(repository) as IList;
        Assert.Contains(hero, actual);
    }
    [Test]
    public void Remove_WhiteSpaceNameParameter_ShouldThrowArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => repository.Remove(" "));
        StringAssert.Contains("Name cannot be null", ex.Message);
    }
    [Test]
    public void Remove_ExistingHeroName_ShouldRemoveTheHeroFromTheDataList()
    {
        repository.Create(hero);
        repository.Remove(Name);
        Type type = repository.GetType();
        FieldInfo data = type.GetField("data", BindingFlags.NonPublic | BindingFlags.Instance);
        var actual = data.GetValue(repository) as IList;
        Assert.IsEmpty(actual);
    }
    [Test]
    public void Remove_ExistingHeroName_ShouldReturnTrueIfTheHeroIsRemoved()
    {
        repository.Create(hero);
                
        Assert.IsTrue(repository.Remove(Name));
    }
    [Test]
    public void GetHeroWithHighestLevel_ShouldReturnTheHeroWithHighestLevel()
    {
        repository.Create(hero);
        repository.Create(new Hero("George", 3));
        var expected = hero;
        var actual = repository.GetHeroWithHighestLevel();
        Assert.AreEqual(expected, actual);
    }
    //[Test]
    //public void GetHeroWithHighestLevel_EmptyDataList_ShouldReturnNull()
    //{
    //    Hero expected = null;
    //    var actual = repository.GetHeroWithHighestLevel();
    //    Assert.AreEqual(expected, actual);
    //}
    [Test]
    public void GetHero_ExistingName_ShouldReturnTheHero()
    {
        repository.Create(hero);
        var expected = hero;
        var actual = repository.GetHero(Name);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void GetHero_NotExistingName_ShouldReturnNull()
    {
        Hero expected = null;
        var actual = repository.GetHero(Name);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void Heroes_ShouldReturnDataListAsReadOnlyCollection()
    {
        var actual = repository.Heroes.GetType().Name;
                
        Assert.IsTrue(actual == "ReadOnlyCollection`1");
    }
}