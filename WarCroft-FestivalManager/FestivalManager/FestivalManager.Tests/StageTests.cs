// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {
		private Song song;
		private Performer performer;
		private Stage stage;
		private const string Name = "Let it be";
		private TimeSpan duration = new TimeSpan(0, 0, 60);
		private const string FirstName = "John";
		private const string LastName = "Smith";
		private const int Age = 18;
		[SetUp]
		public void SetUp()
        {
			song = new Song(Name, duration);
			performer = new Performer(FirstName, LastName, Age);
			stage = new Stage();
        }
		[Test]
	    public void Constructor_InitializesAListOfPerformers()
	    {
			Assert.IsNotNull(stage.Performers);
		}
		[TestCase(17)]
		public void AddPerformer_PerformerYoungerThan18_ShouldThrowArgumentException(int age)
		{
			var tooYoungPerformer = new Performer(FirstName, LastName, age);
			var ex = Assert.Throws<ArgumentException>(() => stage.AddPerformer(tooYoungPerformer));
			StringAssert.Contains("You can only add performers that are at least 18.", ex.Message);
		}
		[Test]
		public void AddPerformer_Performer18OrOver_ShouldAddItToThePerformersList()
		{
			stage.AddPerformer(performer);
			var expected = 1;
			var actual = stage.Performers.Count;
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void AddSong_SongShorterThan1Minute_ShouldThrowArgumentException()
        {
			var tooShortSong = new Song(Name, new TimeSpan(0, 0, 59));
			var ex = Assert.Throws<ArgumentException>(() => stage.AddSong(tooShortSong));
			StringAssert.Contains("You can only add songs that are longer than 1 minute.", ex.Message);
        }
		[Test]
		public void AddSong_SongLongerThan1Minute_ShouldNotThrowException()
        {
			Assert.DoesNotThrow(() => stage.AddSong(song));
        }
		[Test]
		public void AddSongToPerformer_WithValidParameters_ShouldAddTheSongToPerformersList()
        {
			stage.AddPerformer(performer);
			stage.AddSong(song);
			stage.AddSongToPerformer(Name, FirstName + ' ' + LastName);
			var expected = 1;
			var actual = performer.SongList.Count;
			Assert.AreEqual(expected, actual);
        }
		[Test]
		public void AddSongToPerformer_ShouldReturnSuccessMessage()
        {
			stage.AddPerformer(performer);
			stage.AddSong(song);
			string fullName = FirstName + ' ' + LastName;
			StringAssert.Contains($"{Name} ({duration:mm\\:ss}) will be performed by {fullName}", stage.AddSongToPerformer(Name, fullName));
		}
		[Test]
		public void Play_ShouldReturnSuccessMessage()
        {
			stage.AddPerformer(performer);
			stage.AddSong(song);
			stage.AddSongToPerformer(Name, FirstName + ' ' + LastName);
			StringAssert.Contains("1 performers played 1 songs", stage.Play());
        }
	}
}