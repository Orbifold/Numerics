using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Orbifold.Numerics
{
	public class MarkovTextGenerator
	{
		private static string Cleanup(string sample)
		{
			var result = sample.Replace('\t', ' ');
			var compress = result;
			do
			{
				result = compress;
				compress = result.Replace(" ", " ");
			}
			while (result != compress);
			return result;
		}

		/// <summary>
		/// Generates a new text variation based on the given sample.
		/// </summary>
		/// <param name="sample">The sample on which the variation will be based..</param>
		/// <param name="size">The size of the text to generate.</param>
		/// <param name="windowSize">Size of the window.</param>
		/// <returns></returns>
		public static string Generate(string sample, int size = 1000, int windowSize=5)
		{
			// tokenise the input string
			var seedList = new List<string>(Split(Cleanup(sample)));
			
			var chain = new MarkovChain<string>(seedList, windowSize);

			// generate a new sequence using a starting word, and maximum return size
			var generated = new List<string>(chain.Generate(size));
			// output the results to the console
			var sb = new StringBuilder();
			generated.ForEach(item => sb.Append(item));
			
			var  goodcase = ToSentenceCase(sb.ToString().Trim());
			if(!goodcase.EndsWith(".")) goodcase += ".";
			return goodcase;
		}

		// tokenise a string into words (regex definition of word)
		private static IEnumerable<string> Split(string subject)
		{
			var tokens = new List<string>();
			var regex = new Regex(@"(\W+)");
			tokens.AddRange(regex.Split(subject));
			return tokens;
		}
		private static string ToSentenceCase(string data)
		{
			// start by converting entire string to lower case
			var lowerCase = data.ToLower();
			// matches the first sentence of a string, as well as subsequent sentences
			var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
			// MatchEvaluator delegate defines replacement of setence starts to uppercase
			var result = r.Replace(lowerCase, s => s.Value.ToUpper());
			return result;
		}
	}
}