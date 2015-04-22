using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A 2-tuple comparer assuming that the first entry acts as a comparable key.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class MupleComparer<TKey, TValue> : IComparer<Muple<TKey, TValue>>
		where TKey : IComparable
	{
		private readonly IComparer<TKey> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="MupleComparer{TKey,TValue}"/> class.
		/// </summary>
		public MupleComparer()
		{
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MupleComparer{TKey,TValue}"/> class.
		/// </summary>
		/// <param name="comparer">The comparer.</param>
		public MupleComparer(IComparer<TKey> comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>
		/// Gets the default comparer for the type of association specified.
		/// </summary>
		/// <value>The default comparer.</value>
		public static MupleComparer<TKey, TValue> DefaultComparer
		{
			get
			{
				return new MupleComparer<TKey, TValue>();
			}
		}

		/// <summary>
		/// Compares the two muples.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public int Compare(Muple<TKey, TValue> x, Muple<TKey, TValue> y)
		{
			return this.comparer.Compare(x.Item1, y.Item1);
		}

		/// <summary>
		/// Compares the two values.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public int Compare(TKey x, TKey y)
		{
			return this.comparer.Compare(x, y);
		}
	}
}