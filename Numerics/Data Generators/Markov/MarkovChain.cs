using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A generic Markov chain implementation.
	/// </summary>
	/// <typeparam name="T">The data type of the elements.</typeparam>
	public class MarkovChain<T>
	{
		private readonly MarkovLink<T> root = new MarkovLink<T>(default(T));
		readonly int length;

		/// <summary>
		/// Initializes a new instance of the <see cref="MarkovChain&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="length">The length.</param>
		public MarkovChain(IEnumerable<T> input, int length)
		{
			this.length = length;
			this.root.Process(input, length);
		}

		/// <summary>
		/// Generates a new list of a certain size.
		/// </summary>
		/// <param name="size">The size of the result.</param>
		/// <returns></returns>
		public IEnumerable<T> Generate(int size)
		{
			return this.root.Generate(this.root.SelectRandomLink().Data, this.length, size).Select(next => next.Data);
		}

		/// <summary>
		/// Generates a new list of a certain size, starting with the given item.
		/// </summary>
		/// <param name="start">The item to start with</param>
		/// <param name="size">The size of the result.</param>
		/// <returns></returns>
		public IEnumerable<T> Generate(T start, int size)
		{
			return this.root.Generate(start, this.length, size).Select(next => next.Data);
		}
	}
}