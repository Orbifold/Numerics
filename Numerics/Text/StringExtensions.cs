using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Diverse extensions related to strings and text.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>The empty string.</summary>
		public const string EmptyStringHash = "#EMPTY#";

		/// <summary>Number of strings.</summary>
		public const string NumHash = "#NUM#";

		/// <summary>The symbol string.</summary>
		public const string SymbolHash = "#SYM#";

		/// <summary>
		/// Capitalizes the first character of the given string.
		/// </summary>
		/// <param name="s">The string to capiatalize.</param>
		public static string Capitalize(this string s)
		{
			if(string.IsNullOrEmpty(s))
				return string.Empty;
			var trimmed = s.Trim();
			return char.ToUpper(trimmed[0]) + trimmed.Substring(1);
		}

		/// <summary>
		/// Capitalizes the first letter after a period ('.').
		/// </summary>
		/// <param name="text">The text to be processes.</param>
		/// <returns></returns>
		public static string ToSentenceCase(string text)
		{
			// start by converting entire string to lower case
			var lowerCase = text.ToLower();
			// matches the first sentence of a string, as well as subsequent sentences
			var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
			// MatchEvaluator delegate defines replacement of setence starts to uppercase
			var result = r.Replace(lowerCase, s => s.Value.ToUpper());
			return result;
		}

		/// <summary>
		/// Ensures that the string does not contain punctuation, separators...
		/// 
		/// </summary>
		/// <param name="s">string.</param>
		/// <param name="checkNumber">(Optional) true to check number.</param>
		/// <returns>A string.</returns>
		public static string Sanitize(this string s, bool checkNumber = true)
		{
			if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
				return EmptyStringHash;

			s = s.Trim().ToUpperInvariant();
			var item = s.Trim();

			// kill inlined stuff that creates noise (like punctuation etc.)
			item = item.ToCharArray().Aggregate("",
				(x, a) => {
					return char.IsSymbol(a) || char.IsPunctuation(a) || char.IsSeparator(a) ? x : x + a;
				}
			);

			// since we killed everything
			// and it is still empty, it
			// must be a symbol
			if(string.IsNullOrEmpty(item))
				return SymbolHash;

			// number check
			if(checkNumber) {
				double check;
				if(double.TryParse(item, out check))
					return NumHash;
			}

			return item;
		}

		/// <summary>Lazy list of available characters in a given string.</summary>
		/// <param name="s">string.</param>
		/// <param name="exclusions">(Optional) characters to ignore.</param>
		/// <returns>returns key value.</returns>
		public static IEnumerable<string> GetChars(string s, string[] exclusions = null)
		{
			s = s.Trim().ToUpperInvariant();
			foreach(char a in s) {
				var key = a.ToString();
				if(string.IsNullOrWhiteSpace(key))
					continue;
				if(exclusions != null && exclusions.Length > 0 && exclusions.Contains(key))
					continue;
				key = char.IsSymbol(a) || char.IsPunctuation(a) || char.IsSeparator(a) ? SymbolHash : key;
				key = char.IsNumber(a) ? NumHash : key;
				yield return key;
			}
		}

		/// <summary>
		/// Gets the words from the given string. Punctuations and other characters are removed.
		/// </summary>
		/// <param name="s">The string to parse.</param>
		/// <param name="separator">The separating string.</param>
		/// <param name="exclusions">Optional list of strings to exclude.</param>
		public static IEnumerable<string> GetWords(string s, string separator = " ", string[] exclusions = null)
		{
			if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
				yield return EmptyStringHash;
			else {
				s = s.Trim().ToUpperInvariant();
				foreach(var w in s.Split(separator.ToCharArray())) {
					var key = Sanitize(w);					
					if(exclusions != null && exclusions.Length > 0 && exclusions.Contains(key, StringComparer.CurrentCultureIgnoreCase))
						continue;
					yield return key;
				}
			}
		}

		/// <summary>
		/// Counts the words specified in the <see cref="StringProperty.Dictionary"/>.
		/// </summary>
		/// <param name="item">The text to parse.</param>
		/// <param name="property">Defines how and what to count.</param>
		public static double[] GetWordCount(string item, StringFeature property)
		{
			var counts = new double[property.Dictionary.Length];
			var d = new Dictionary<string, int>();

			for(int i = 0; i < counts.Length; i++) {
				counts[i] = 0;
				d.Add(property.Dictionary[i], i);
			}
			var words = property.SplitType == StringSplitType.Character ?
				GetChars(item) :
				GetWords(item, property.Separator);

			foreach(var s in words)
				if(property.Dictionary.Contains(s))
					counts[d[s]]++;

			return counts;
		}

		/// <summary>Gets word position.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <param name="item">The item.</param>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="checkNumber">(Optional) true to check number.</param>
		/// <returns>The word position.</returns>
		public static int GetWordPosition(string item, string[] dictionary, bool checkNumber = true)
		{
			//string[] dictionary = property.Dictionary;
			if(dictionary == null || dictionary.Length == 0)
				throw new InvalidOperationException("Cannot get word position with an empty dictionary");

			item = Sanitize(item, checkNumber);

			// is this the smartest thing?
			for(int i = 0; i < dictionary.Length; i++)
				if(dictionary[i] == item)
					return i;

			throw new InvalidOperationException(
				string.Format("\"{0}\" does not exist in the property dictionary", item));
		}

		/// <summary>Builds character dictionary.</summary>
		/// <param name="examples">The examples.</param>
		/// <param name="exclusion">(Optional) the exclusion.</param>
		/// <returns>A Dictionary&lt;string,double&gt;</returns>
		public static Dictionary<string, double> BuildCharDictionary(IEnumerable<string> examples, string[] exclusion = null)
		{
			var d = new Dictionary<string, double>();

			foreach(string o in examples) {
				foreach(string key in GetChars(o, exclusion)) {
					if(d.ContainsKey(key))
						d[key] += 1;
					else
						d.Add(key, 1);
				}
			}

			return d;
		}

		/// <summary>Builds enum dictionary.</summary>
		/// <param name="examples">The examples.</param>
		/// <returns>A Dictionary&lt;string,double&gt;</returns>
		public static Dictionary<string, double> BuildEnumDictionary(IEnumerable<string> examples)
		{
			// TODO: Really need to consider this as an enum builder
			var d = new Dictionary<string, double>();

			// for holding string
			string s;

			foreach(string o in examples) {
				s = o.Trim().ToUpperInvariant();

				// kill inlined stuff that creates noise
				// (like punctuation etc.)
				s = s.ToCharArray().Aggregate("",
					(x, a) => {
						return char.IsSymbol(a) || char.IsPunctuation(a) || char.IsSeparator(a) ? x : x + a;
					}
				);

				// null or whitespace
				if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
					s = EmptyStringHash;

				if(d.ContainsKey(s))
					d[s] += 1;
				else
					d.Add(s, 1);
			}

			return d;
		}

		/// <summary>Builds word dictionary.</summary>
		/// <param name="examples">The examples.</param>
		/// <param name="separator">(Optional) separator string.</param>
		/// <param name="exclusion">(Optional) the exclusion.</param>
		/// <returns>A Dictionary&lt;string,double&gt;</returns>
		public static Dictionary<string, double> BuildWordDictionary(IEnumerable<string> examples, string separator = " ", string[] exclusion = null)
		{
			var d = new Dictionary<string, double>();

			foreach(string s in examples)
				foreach(string key in GetWords(s, separator, exclusion))
					if(d.ContainsKey(key))
						d[key] += 1;
					else
						d.Add(key, 1);

			return d;
		}
	}
}