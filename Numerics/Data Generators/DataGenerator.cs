
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A collection of methods related to random data generation.
	/// </summary>
	public static class DataGenerator
	{
		private static MarkovNameGenerator markovFemaleNameGenerator;
		private static MarkovNameGenerator markovFamilyNameGenerator;
		private static MarkovNameGenerator markovTaskNameGenerator;

		/// <summary>
		/// The randomizer at the basis of all.
		/// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		/// Returns a random paragraph of lipsum text of the specified length.
		/// </summary>
		/// <param name="numWords">The num words; default is 25 and must be more than 5.</param>
		/// <returns></returns>
		public static string RandomLipsum(int numWords = 25)
		{
			numWords = System.Math.Max(numWords, 5);
			var sb = new StringBuilder();
			sb.Append("Lorem ipsum dolor sit amet");
			var counter = 5;
			var capitalize = false;
			for(var i = 0; i <= (numWords - 6); i++) {
				var newWord = DataStore.LipsumData[Rand.Next(DataStore.LipsumData.Length)];
				if(capitalize) {
					newWord = newWord.Capitalize();
					capitalize = false;
				}
				sb.AppendFormat(" {0}", newWord);
				counter++;

				// create sentences of random length;
				if(counter >= 15 && Rand.NextDouble() < .7) {
					sb.Append(".");
					counter = 0;
					capitalize = true;
				}
			}

			sb.Append(".");
			return sb.ToString();
		}

		/// <summary>
		/// Returns a collection of person names.
		/// </summary>
		/// <param name="type">The option specifying the format of the returned names.</param>
		/// <param name="count">The amount of person names to be returned.</param>
		/// <returns></returns>
		public static IEnumerable<string> RandomPersonNameCollection(PersonDataType type = PersonDataType.FullNameWithMiddleInitial, int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomPersonName(type) };
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(RandomPersonName(type)));
			return list;
		}

		/// <summary>
		/// Returns a random person name.
		/// </summary>
		/// <param name="type">The option specifying the format of the returned name. Default is <see cref="PersonDataType.FullNameWithMiddleInitial"/>.</param>
		public static string RandomPersonName(PersonDataType type = PersonDataType.FullNameWithMiddleInitial)
		{
			switch(type) {
			case PersonDataType.FirstName:
				return RandomFirstName();
			case PersonDataType.MaleFirstName:
				return RandomMaleName();
			case PersonDataType.FemalFirstName:
				return RandomFemaleName();
			case PersonDataType.FamilyName:
				return RandomFamilyName();
			case PersonDataType.FullName:
				return String.Format("{0} {1}", RandomFirstName(), RandomFamilyName());
			case PersonDataType.FullNameWithMiddleInitial:
				return String.Format("{0} {1}. {2}", RandomFirstName(), RandomLetter(CaseType.UpperCase), RandomFamilyName());
			default:
				throw new ArgumentOutOfRangeException("type");
			}
		}

		/// <summary>
		/// Returns a random (English) male or female first name.
		/// </summary>
		/// <returns>The first name.</returns>
		public static string RandomFirstName()
		{
			return Rand.NextDouble() < .5 ? RandomFemaleName() : RandomMaleName();
		}

		/// <summary>
		/// Returns a random (English) male name (first name).
		/// </summary>
		/// <returns>The male name.</returns>
		public static string RandomMaleName()
		{
			return DataStore.EnglishMaleNames[Rand.Next(DataStore.EnglishMaleNames.Length)];
		}

		public static List<string> RandomMaleNames(int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");

			if(count == 1)
				return new List<string> { RandomMaleName() };
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(RandomMaleName()));
			return list;
		}

		public static string RandomFemaleName()
		{
			if(markovFemaleNameGenerator == null)
				markovFemaleNameGenerator = new MarkovNameGenerator(DataStore.EnglishFemaleNames);
			return markovFemaleNameGenerator.NextName;
		}

		public static string RandomSalesTerm()
		{
			return DataStore.SalesTerms[Rand.Next(DataStore.SalesTerms.Length), 0];
		}

		public static Concept RandomSalesConcept()
		{
			var i = Rand.Next(DataStore.SalesTerms.GetUpperBound(0));
			return new Concept {
				Name = DataStore.SalesTerms[i, 0],
				Description = DataStore.SalesTerms[i, 1]
			};
		}

		public static string RandomTaskName()
		{
			if(markovTaskNameGenerator == null)
				markovTaskNameGenerator = new MarkovNameGenerator(DataStore.TaskNames);
			return markovTaskNameGenerator.NextName;
		}


		public static List<string> RandomTaskNames(int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomTaskName() };

			if(markovTaskNameGenerator == null)
				markovTaskNameGenerator = new MarkovNameGenerator(DataStore.TaskNames);
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(markovTaskNameGenerator.NextName));
			return list;
		}

		/// <summary>
		/// Returns a random family name.
		/// </summary>
		/// <returns>The family name.</returns>
		public static string RandomFamilyName()
		{
			if(markovFamilyNameGenerator == null)
				markovFamilyNameGenerator = new MarkovNameGenerator(DataStore.FamilyNames);
			return markovFamilyNameGenerator.NextName;
		}

		public static List<string> RandomFemaleNames(int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomFemaleName() };

			if(markovFemaleNameGenerator == null)
				markovFemaleNameGenerator = new MarkovNameGenerator(DataStore.EnglishFemaleNames);
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(markovFemaleNameGenerator.NextName));
			return list;
		}

		/// <summary>
		/// Resets the markov chains and re-initializes on the basis of the data in the <see cref="DataStore"/>.
		/// Use this method if you have defined your own custom arrays in the <see cref="DataStore"/>.
		/// </summary>
		public static void ResetMarkovChains()
		{
			markovFamilyNameGenerator = null;
			markovFemaleNameGenerator = null;
		}

		public static string RandomLetter(CaseType type)
		{
			char start;
			switch(type) {
			case CaseType.UpperCase:
				start = 'A';
				break;
			case CaseType.LowerCase:
				start = 'A';
				break;
			default:
				throw new ArgumentOutOfRangeException("type");
			}
			var num = Rand.Next(0, 26);
			var result = (char)((short)start + num);
			return result.ToString();
		}

		/// <summary>
		/// Returns a random city name.
		/// </summary>
		/// <returns></returns>
		public static string RandomCityName()
		{
			return DataStore.CityNames[Rand.Next(DataStore.CityNames.Length)];
		}

		/// <summary>
		/// Returns a random zip code.
		/// </summary>
		/// <returns></returns>
		public static string RandomZipCode()
		{
			return DataStore.ZipCodes[Rand.Next(DataStore.ZipCodes.Length)];
		}

		/// <summary>
		/// Returns a random state name.
		/// </summary>
		/// <returns></returns>
		public static string RandomStateName()
		{
			return DataStore.StateNames[Rand.Next(DataStore.StateNames.Length)];
		}

		/// <summary>
		/// Returns a random country name.
		/// </summary>
		/// <returns></returns>
		public static string RandomCountryName()
		{
			return DataStore.CountryNames[Rand.Next(DataStore.CountryNames.Length)];
		}

		/// <summary>
		/// Returns a random company name.
		/// </summary>
		/// <returns></returns>
		public static string RandomCompanyName()
		{
			return Rand.NextDouble() < .4
					? String.Format(
				"{0} {1}",
				DataStore.CompanyNames[Rand.Next(DataStore.CompanyNames.Length)],
				DataStore.CompanySuffixes[Rand.Next(DataStore.CompanySuffixes.Length)])
					: DataStore.CompanyNames[Rand.Next(DataStore.CompanyNames.Length)];
		}

		/// <summary>
		/// Generates a collection of random document titles.
		/// </summary>
		/// <param name="count">The amount of items to generate.</param>
		/// <returns></returns>
		public static List<string> RandomDocumentTitleCollection(int count)
		{
			var rx = new Regex(@"(\S.+?[.!?])(?=\s+|$)");
			var list = new List<string>();
			while(list.Count < count) {
				list.AddRange((from Match match in rx.Matches(MarkovTextGenerator.Generate(DataStore.DocumentTitleSample, 140, 3))
				               let i = match.Index
				               select match).Select(match => StringExtensions.ToSentenceCase(match.Value)));
			}
			return list.Take(count).ToList();
		}



		/// <summary>
		/// Returns a random street name.
		/// </summary>
		/// <returns></returns>
		public static string RandomStreetName(bool includeHouseNumber = true)
		{
			return includeHouseNumber ? String.Format("{0} {1}", DataStore.StreetNames[Rand.Next(DataStore.StreetNames.Length)], Rand.Next(542)) : DataStore.StreetNames[Rand.Next(DataStore.StreetNames.Length)];
		}

		/// <summary>
		/// Generates a random address.
		/// </summary>
		/// <remarks>The address type if a flag type so you can combine the options. The default includes all options.</remarks>
		/// <param name="type">The address type.</param>
		/// <returns></returns>
		public static string RandomAddress(AddressType type = AddressType.CompanyName | AddressType.CountryName | AddressType.StateName)
		{
			var sb = new StringBuilder();
			if((type & AddressType.CompanyName) == AddressType.CompanyName)
				sb.AppendLine(RandomCompanyName());
			sb.AppendLine(RandomStreetName());
			sb.AppendLine(String.Format("{0} {1}", RandomZipCode(), RandomCityName()));
			if((type & AddressType.StateName) == AddressType.StateName)
				sb.AppendLine(String.Format("{0}, {1}", RandomStateName(), RandomCountryName()));
			else
				sb.AppendLine(RandomCountryName());
			return sb.ToString();
		}

		/// <summary>
		/// Returns a collection of address.
		/// </summary>
		/// <param name="type">The option specifying the format of the returned addresses.</param>
		/// <param name="count">The amount of addresses to be returned.</param>
		/// <returns></returns>
		public static IEnumerable<string> RandomAddressCollection(AddressType type = AddressType.CompanyName | AddressType.StateName | AddressType.CountryName, int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of addresses to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomAddress(type) };
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(RandomAddress(type)));
			return list;
		}

		/// <summary>
		/// Generates a random variation of a philosophical text.
		/// </summary>
		/// <param name="style">The style of text to base the variation on.</param>
		/// <param name="size">The size of the text to generate.</param>
		/// <returns></returns>
		public static string RandomTextVariation(TextSamples style, int size = 1000)
		{
			string sample;
			switch(style) {
			case TextSamples.Latin:
				sample = DataStore.LatinSample;
				break;
			case TextSamples.Spanish:
				sample = DataStore.SpanishSample;
				break;
			case TextSamples.Bulgarian:
				sample = DataStore.BulgarianSample;
				break;
			case TextSamples.Medical:
				sample = DataStore.MedicalSymptoms;
				break;
			case TextSamples.Biology:
				sample = DataStore.BiologySample;
				break;
			case TextSamples.Philosophy:
				sample = DataStore.PhilosophySample;
				break;
			case TextSamples.English1:
				sample = DataStore.English1Sample;
				break;
			case TextSamples.English2:
				sample = DataStore.English2Sample;
				break;
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			return MarkovTextGenerator.Generate(sample, size);
		}

		/// <summary>
		/// Returns a random file extension.
		/// </summary>
		/// <remarks>The optional <see cref="FileExtensionType"/> is a flag enumeration and you can combine different types.</remarks>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static string RandomFileExtension(FileExtensionType type = FileExtensionType.CommonFiles | FileExtensionType.MsOffice)
		{
			var list = new List<string>();
			if((type & FileExtensionType.CommonFiles) == FileExtensionType.CommonFiles)
				list.AddRange(DataStore.CommonExtensions.Keys);
			if((type & FileExtensionType.MsOffice) == FileExtensionType.MsOffice)
				list.AddRange(DataStore.OfficeExtensions.Keys);
			return list[Rand.Next(list.Count)].ToLower();
		}

		/// <summary>
		/// Gets the file extension description, if available.
		/// </summary>
		/// <param name="extension">The extension to look up.</param>
		/// <returns></returns>
		public static string GetFileExtensionDescription(string extension)
		{
			if(DataStore.CommonExtensions.ContainsKey(extension))
				return DataStore.CommonExtensions[extension];
			if(DataStore.OfficeExtensions.ContainsKey(extension))
				return DataStore.OfficeExtensions[extension];
			return "Unknown file extensions.";
		}

		/// <summary>
		/// Generates a variation on the given text sample.
		/// </summary>
		/// <param name="sample">The sample on which the variation is based.</param>
		/// <param name="size">The size of the generated text.</param>
		/// <seealso cref="MarkovTextGenerator"/>
		/// <returns></returns>
		public static string RandomTextVariation(string sample, int size = 1000)
		{
			if(String.IsNullOrEmpty(sample))
				throw new Exception("The sample cannot be empty.");
			if(size < 1)
				throw new Exception("The variation's length cannot be zero.");
			return MarkovTextGenerator.Generate(sample, size);

		}

		/// <summary>
		/// Returns a random letter.
		/// </summary>
		/// <param name="charType">The types of characters to include in the pool.</param>
		/// <returns></returns>
		public static string RandomLetter(CharType charType = CharType.UpperCaseLetters)
		{
			return RandomString(1, charType);
		}

		/// <summary>
		/// Generates a random string.
		/// </summary>
		/// <param name="size">The length of the generated string.</param>
		/// <param name="charType">The types of characters to include in the pool.</param>
		/// <returns></returns>
		public static string RandomString(int size, CharType charType = (CharType.French | CharType.Brackets | CharType.Numbers | CharType.Special | CharType.LowerCaseLetters | CharType.UpperCaseLetters))
		{
			var input = "";
			if((charType & CharType.UpperCaseLetters) == CharType.UpperCaseLetters)
				input += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			if((charType & CharType.LowerCaseLetters) == CharType.LowerCaseLetters)
				input += "abcdefghijklmnopqrstuvwxyz";
			if((charType & CharType.Numbers) == CharType.Numbers)
				input += "0123456789";
			if((charType & CharType.French) == CharType.French)
				input += "éèçà";
			if((charType & CharType.Brackets) == CharType.Brackets)
				input += "()[]{}";
			if((charType & CharType.Special) == CharType.Special)
				input += "&@#§!-_°*$µ£ù%;:,?=<>/";

			var chars = Enumerable.Range(0, size).Select(x => input[Rand.Next(0, input.Length)]);
			return new string(chars.ToArray());
		}

		/// <summary>
		/// Generates a random string on the basis of the given input string.
		/// </summary>
		/// <param name="size">The length of the generated string.</param>
		/// <param name="input">The string from which a new random string will be generated.</param>
		/// <returns></returns>
		public static string RandomString(int size, string input)
		{
			if(size < 1)
				throw new ArgumentException("The size cannot be smaller than one.", "size");
			if(string.IsNullOrEmpty(input))
				throw new ArgumentNullException("input");
			var chars = Enumerable.Range(0, size).Select(x => input[Rand.Next(0, input.Length)]);
			return new string(chars.ToArray());
		}

		/// <summary>
		/// Generates a random date within the specified interval.
		/// </summary>
		/// <param name="minDate">The start date of the interval.</param>
		/// <param name="maxDate">The end date of the interval.</param>
		/// <returns></returns>
		public static DateTime RandomDate(DateTime minDate, DateTime maxDate)
		{
			var totalDays = (int)DateTimeUtil.DateDiff(DateIntervalType.Day, minDate, maxDate);
			var randomDays = Rand.Next(0, totalDays);

			// set the time to zero and add a random time within the same day
			return (minDate.AddDays(randomDays) + new TimeSpan(0, 0, 0)).AddMilliseconds(Rand.Next(72000000));
		}

		/// <summary>
		/// Returns a random <c>TimeSpan</c> within the given interval.
		/// </summary>
		/// <param name="minDate">The min date.</param>
		/// <param name="maxDate">The max date.</param>
		/// <returns></returns>
		public static TimeSpan RandomTimeSpan(DateTime minDate, DateTime maxDate)
		{
			var diff = (int)System.Math.Floor((maxDate - minDate).TotalMilliseconds);
			return minDate.AddMilliseconds(Rand.Next(diff)) - minDate;
		}

		/// <summary>
		/// Returns a random date within the full span of the <c>DateTime</c> range.
		/// </summary>
		/// <returns></returns>
		public static DateTime RandomDate()
		{
			return RandomDate(DateTime.MinValue, DateTime.MaxValue);
		}


	}
}