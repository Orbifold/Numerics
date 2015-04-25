using System;
using System.Linq;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests
{
	[TestFixture]
	public class StringTests
	{

		[Test]
		[Category("Strings")]
		public void GetWordsTest()
		{	
			var text = "Hoe echter oordeelt dan een geest die veel begrijpt?";
			var words = StringExtensions.GetWords(text);
			Assert.AreEqual(9, words.Count());

			words = StringExtensions.GetWords(text, " ", new []{ "dan", "veel" });
			Assert.AreEqual(7, words.Count());

			// use the StringProperty to specify the words to count
			var prop = new StringFeature();
			prop.Separator = "-";
			prop.SplitType = StringSplitType.Word;
			prop.Dictionary = new []{ "dit", "dat" };
			text = "Dit-en-dit-is-niet-slecht-maar-dat-ook-niet";
			var d = StringExtensions.GetWordCount(text, prop);
			Assert.AreEqual(2, d.Length);
		}

		[Test]
		[Category("Strings")]
		public void GetCharsTest()
		{	
			var word = "Syncoptical+Paramis";
			var chars = StringExtensions.GetChars(word, new[]{ "+" });
			Assert.AreEqual(18, chars.Count());
			word = "!Static?";
			chars = StringExtensions.GetChars(word);
			// note that symbols will appear as #SYM#
			Assert.AreEqual(6, chars.Count(p => p.Length == 1));
		}

		[Test]
		[Category("Strings")]
		public void CharDictionaryTest()
		{	
			var text = "However you look at it, things are always more complex than at first sight.";
			var chars = StringExtensions.GetChars(text);
			var stats = StringExtensions.BuildEnumDictionary(chars);
			Assert.AreEqual(5, stats["E"]);
			Assert.AreEqual(4, stats["R"]);
			Assert.AreEqual(4, stats["S"]);
		}

	}
	
}
