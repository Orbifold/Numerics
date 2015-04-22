using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A visitor that tracks (stores) keys from KeyValuePairs in the order they were visited.
	/// </summary>
	/// <typeparam name="TKey">The type of key of the KeyValuePair.</typeparam>
	/// <typeparam name="TValue">The type of value of the KeyValuePair.</typeparam>
	public sealed class KeyValueTrailVisitor<TKey, TValue> : IVisitor<KeyValuePair<TKey, TValue>>
	{
		private readonly List<TValue> values;
		private readonly List<TKey> keys;

		/// <inheritdoc/>
		public KeyValueTrailVisitor()
		{
			this.values = new List<TValue>();
			this.keys = new List<TKey>();
		}

		/// <summary>
		/// Gets a value indicating whether this instance has completed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has completed; otherwise, <c>false</c>.
		/// </value>
		public bool HasCompleted
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the trail of the values.
		/// </summary>
		/// <value>The value list.</value>
		public IList<TValue> Values
		{
			get
			{
				return this.values;
			}
		}

		/// <summary>
		/// Gets the trail of the keys.
		/// </summary>
		/// <value>The keys list.</value>
		public IList<TKey> Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>
		/// Visits the specified keypair.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public void Visit(KeyValuePair<TKey, TValue> obj)
		{
			this.values.Add(obj.Value);
			this.keys.Add(obj.Key);
		}
	}
}