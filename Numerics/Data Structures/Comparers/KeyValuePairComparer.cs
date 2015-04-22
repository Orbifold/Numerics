using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A comparer of key-value pairs based on a comparison of the respective keys.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class KeyValuePairComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
	{
		private readonly IComparer<TKey> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyValuePairComparer&lt;TKey, TValue&gt;"/> class.
		/// </summary>
		public KeyValuePairComparer()
		{
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyValuePairComparer&lt;TKey, TValue&gt;"/> class.
		/// </summary>
		/// <param name="comparer">The comparer.</param>
		public KeyValuePairComparer(IComparer<TKey> comparer)
		{
			if (comparer == null) throw new ArgumentNullException("comparer");
			this.comparer = comparer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyValuePairComparer&lt;TKey, TValue&gt;"/> class.
		/// </summary>
		/// <param name="comparison">The comparison.</param>
		public KeyValuePairComparer(Comparison<TKey> comparison)
		{
			if (comparison == null) throw new ArgumentNullException("comparison");
			this.comparer = new ComparisonComparer<TKey>(comparison);
		}

		/// <summary>
		/// Compares the two keypairs.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			return this.comparer.Compare(x.Key, y.Key);
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