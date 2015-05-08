using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A Markov chain implementation geared towards the generation of single words.
	/// See for example http://en.wikipedia.org/wiki/Markov_chain .
	/// </summary>
	public class MarkovNameGenerator
	{
		private readonly Dictionary<string, List<char>> chains = new Dictionary<string, List<char>>();

		private readonly int minLength;

		private readonly int order;

        private readonly System.Random rnd = new System.Random(Environment.TickCount);

		private readonly List<string> samples = new List<string>();

		private readonly List<string> used = new List<string>();

		/// <summary>
		/// Gets or sets whether the generated items are unique across the lifetime of this generator.
		/// </summary>
		/// <value>
		///   <c>true</c> if uniqueness should be ensured; otherwise, <c>false</c>.
		/// </value>
		public static bool EnsureUniqueness { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MarkovNameGenerator"/> class.
		/// </summary>
		/// <param name="sampleNames">The sample names.</param>
		/// <param name="order">The order.</param>
		/// <param name="minLength">Length of the min.</param>
		public MarkovNameGenerator(IEnumerable<string> sampleNames, int order=3, int minLength=5)
		{
			if (order < 1) order = 1;
			if (minLength < 1) minLength = 1;
			this.order = order;
			this.minLength = minLength;

			foreach (
				var upper in
					sampleNames.Select(line => line.Split(',')).SelectMany(tokens => tokens, (tokens, word) => word.Trim().ToUpper()).
						Where(upper => upper.Length >= order + 1)) this.samples.Add(upper);

		
			foreach (var word in this.samples)
			{
				for (var letter = 0; letter < word.Length - order; letter++)
				{
					var token = word.Substring(letter, order);
					List<char> entry;
					if (this.chains.ContainsKey(token)) entry = this.chains[token];
					else
					{
						entry = new List<char>();
						this.chains[token] = entry;
					}
					entry.Add(word[letter + order]);
				}
			}
		}
		
		public string NextName
		{
			get
			{
		
				string s;
				do
				{
					var n = this.rnd.Next(this.samples.Count);
					var nameLength = this.samples[n].Length;
					s = this.samples[n].Substring(this.rnd.Next(0, this.samples[n].Length - this.order), this.order);
					while (s.Length < nameLength)
					{
						var token = s.Substring(s.Length - this.order, this.order);
						var c = this.GetLetter(token);
						if (c != '?') s += this.GetLetter(token);
						else break;
					}

					if (s.Contains(" "))
					{
						var tokens = s.Split(' ');
						s = "";
						for (var t = 0; t < tokens.Length; t++)
						{
							if (tokens[t] == "") continue;
							if (tokens[t].Length == 1) tokens[t] = tokens[t].ToUpper();
							else tokens[t] = tokens[t].Substring(0, 1) + tokens[t].Substring(1).ToLower();
							if (s != "") s += " ";
							s += tokens[t];
						}
					}
					else s = s.Substring(0, 1) + s.Substring(1).ToLower();
				}
				while ((this.used.Contains(s) && EnsureUniqueness)|| s.Length < this.minLength);
				if(EnsureUniqueness) this.used.Add(s);
				return s;
			}
		}

		/// <summary>
		/// Resets the collection of used names.
		/// </summary>
		public void Reset()
		{
			this.used.Clear();
		}

		
		private char GetLetter(string token)
		{
			if (!this.chains.ContainsKey(token)) return '?';
			var letters = this.chains[token];
			var n = this.rnd.Next(letters.Count);
			return letters[n];
		}
	}

}