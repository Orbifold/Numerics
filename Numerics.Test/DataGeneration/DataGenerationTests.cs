using System;
using System.Linq;
using System.Text;

using NUnit.Framework;

#if SILVERLIGHT
using Microsoft.Silverlight.Testing;
#endif
namespace Orbifold.Numerics.Tests.DataGeneration
{
	[TestFixture]
	public sealed class DataGenerationTests
	{
        static readonly System.Random rand = new System.Random(Environment.TickCount);

		[Test]
		[Category("Data Generators")]
		public void LipsumTest()
		{
			var s = DataGenerator.RandomLipsum();
			Assert.IsNotNull(s, "Null lipsum.");
			Assert.IsTrue(s.Length > 0, "Null lipsum.");
			Console.WriteLine(s);
			// let's count words
			var count = rand.Next(14);
			for(var i = 0; i < count; i++) {
				var shouldContain = rand.Next(5, 145);
				s = DataGenerator.RandomLipsum(shouldContain);
				Assert.AreEqual(shouldContain, s.Split(' ').Length, string.Format("A sequence of {0} failed.", shouldContain));
			}
		}

		[Test]
		[Category("Data Generators")]
		public void MaleNamesTest()
		{
			var count = rand.Next(15, 78);
			var names = DataGenerator.RandomMaleNames(count);
			Assert.AreEqual(count, names.Count(), "The expected amount was not generated.");
			Console.WriteLine(names.ToPrettyFormat());
			// works but a matter of chance and size that it fails; Assert.IsFalse(names.Any(s => names.Count(m => m == s) > 1), "Some names are duplicate.");
		}

		[Test]
		[Category("Data Generators")]
		public void AlteringDataStoreTest()
		{
			DataStore.EnglishMaleNames = new[] { "John", "Fred", "Peter", "Julian" };
			DataGenerator.ResetMarkovChains();
			MarkovNameGenerator.EnsureUniqueness = false;
			var count = rand.Next(15, 78);
			var names = DataGenerator.RandomMaleNames(count);
			Assert.AreEqual(count, names.Count(), "The expected amount was not generated.");
			Console.WriteLine(names.ToPrettyFormat());
		}

		[Test]
		[Category("Data Generators")]
		public void FullNameTest()
		{
			var count = rand.Next(15, 78);
			var names = DataGenerator.RandomPersonNameCollection(PersonDataType.FullNameWithMiddleInitial, count);
			Assert.AreEqual(count, names.Count(), "The expected amount was not generated.");
			Assert.IsFalse(names.Any(s => names.Count(m => m == s) > 1), "Some names are duplicate.");
			Console.WriteLine(names.ToPrettyFormat(5));
		}

		[Test]
		[Category("Data Generators")]
		public void AddressGeneratorTest()
		{
			Range.Create(1, 25).ForEach(() => Console.WriteLine(DataGenerator.RandomAddress()));
		}

		[Test]
		[Category("Data Generators")]
		public void FileExtensionsTest()
		{
			Range.Create(1, 25).ForEach(() => Console.WriteLine(DataGenerator.RandomFileExtension()));
		}

		[Test]
		[Category("Data Generators")]
		public void MarkovTextTest()
		{
			var sample =
				@"The strange driver evidently heard the words, for he looked up with a gleaming smile.  The passenger turned his face away, at the same time	putting out his two fingers and crossing himself.  Give me the Herr's
				luggage, said the driver, and with exceeding alacrity my bags were handed out and put in the caleche.  Then I descended from the side of
				the coach, as the caleche was close alongside, the driver helping me with a hand which caught my arm in a grip of steel.  His strength must
				have been prodigious. Without a word he shook his reins, the horses turned, and we swept into the darkness of the pass.  As I looked back I saw the steam from
				the horses of the coach by the light of the lamps, and projected against it the figures of my late companions crossing themselves.
				Then the driver cracked his whip and called to his horses, and off they swept on their way to Bukovina.  As they sank into the darkness I
				felt a strange chill, and a lonely feeling come over me.  But a cloak was thrown over my shoulders, and a rug across my knees, and the
				driver said in excellent German. I did not take any, but it was a comfort to know it was there all the
				same.  I felt a little strangely, and not a little frightened.  I think had there been any alternative I should have taken it, instead
				of prosecuting that unknown night journey.  The carriage went at a hard pace straight along, then we made a complete turn and went along
				another straight road.  It seemed to me that we were simply going over and over the same ground again, and so I took note of some salient
				point, and found that this was so.  I would have liked to have asked the driver what this all meant, but I really feared to do so, for I
				thought that, placed as I was, any protest would have had no effect in case there had been an intention to delay.";

			Console.WriteLine(MarkovTextGenerator.Generate(sample));
		}

		[Test]
		[Category("Data Generators")]
		public void RandomStringTest()
		{
			Range.Create(1, 25).ForEach(() => Console.WriteLine(DataGenerator.RandomString(12, CharType.UpperCaseLetters | CharType.LowerCaseLetters)));
			
		}

		[Test]
		[Category("Data Generators")]
		public void TextVariationsTest()
		{
			Console.WriteLine(DataGenerator.RandomTextVariation(TextSamples.Bulgarian));
		}

		[Test]
		[Category("Data Generators")]
		public void RadomDatetTest()
		{
			var count = rand.Next(5, 145);
			var start = DateTime.Now;
			var end = DateTime.Now.AddDays(count);
			for(var c = 0; c < count; c++) {
				var d = DataGenerator.RandomDate(start, end);
				Assert.IsTrue(d < end && d > start, string.Format("Random date {0} is not within the range.", d));
			}
		}

		[Test]
		[Category("Data Generators")]
		public void TitleGenerationTest()
		{
			var count = rand.Next(15, 78);
			var list = DataGenerator.RandomDocumentTitleCollection(count);
			Assert.AreEqual(count, list.Count(), "The expected amount was not generated.");
			Console.WriteLine(list.ToPrettyFormat());
		}
	}
}