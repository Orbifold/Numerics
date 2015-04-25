namespace Orbifold.Numerics
{
	/// <summary>
	/// A collection of predefined text samples on which a Markov text generation can be based.
	/// </summary>
	/// <seealso cref="MarkovTextGenerator"/>
	public enum TextSamples
	{
		/// <summary>
		/// Generates a random variation of a philosophical text.
		/// </summary>
		/// <remarks>Based on Hume's Treatise of Human Nature; http://www.gutenberg.org/cache/epub/4705/pg4705.txt .</remarks>
		Philosophy,

		/// <summary>
		/// Generates a random variation of a Latin text.
		/// </summary>
		/// <remarks>Based on the Philosophiae Naturalis Principia Mathematica of Isaac Newton; http://www.gutenberg.org/cache/epub/28233/pg28233.txt . </remarks>
		Latin,
		/// <summary>
		/// Generates a random variation of a biology text.
		/// </summary>
		/// <remarks>Based on the Origin of Species by Charles Darwin; http://www.gutenberg.org/cache/epub/22764/pg22764.txt .</remarks>
		Biology,

		/// <summary>
		/// Generates a random variation of a Spanish text.
		/// </summary>
		/// <remarks>Based on Los cuatro jinetes del apocalipsis by Vicente  Blasco Ibáñez; http://www.gutenberg.org/cache/epub/24536/pg24536.txt .</remarks>
		Spanish,

		/// <summary>
		/// Generates a random variation of a Bulgarian text.
		/// </summary>
		/// <remarks>Based on Epical Songs by Pencho Slaveykov; http://www.gutenberg.org/cache/epub/3433/pg3433.txt .</remarks>
		Bulgarian,
		/// <summary>
		/// Generates a random variation of an English text.
		/// </summary>
		/// <remarks>Based on the Illiad by Homer; http://www.gutenberg.org/cache/epub/2199/pg2199.txt .</remarks>
		English1,
		/// <summary>
		/// Generates a random variation of an English text.
		/// </summary>
		/// <remarks>Based on The Hound of the Baskerville by Sir Arthur Conan Doyle; http://www.gutenberg.org/cache/epub/2852/pg2852.txt .</remarks>
		English2,
		/// <summary>
		/// Generates a randaom medical text.
		/// </summary>
		Medical

	}
}