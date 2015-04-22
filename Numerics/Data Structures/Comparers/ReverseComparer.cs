using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Reverses another comparer.
	/// </summary>
	/// <typeparam name="T">The data type of the comparer.</typeparam>
	public sealed class ReverseComparer<T> : IComparer<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReverseComparer&lt;T&gt;"/> class.
		/// </summary>
		public ReverseComparer()
		{
			this.Comparer = Comparer<T>.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReverseComparer&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="comparer">The comparer.</param>
		public ReverseComparer(IComparer<T> comparer)
		{
			this.Comparer = comparer;
		}

		/// <summary>
		/// Gets or sets the comparer used in this instance.
		/// </summary>
		/// <value>The comparer.</value>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
		public IComparer<T> Comparer { get; set; }

		/// <summary>
		/// Compares the specified x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public int Compare(T x, T y)
		{
			return this.Comparer.Compare(y, x);
		}
	}
}