using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A visitor that tracks (stores) keys from KeyValuePAirs in the order they were visited.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys for the items to be visited.</typeparam>
	/// <typeparam name="TValue">The type of the values for the items to be visited.</typeparam>
	public sealed class KeyTrackingVisitor<TKey, TValue> : IVisitor<KeyValuePair<TKey, TValue>>
	{
		private readonly List<TKey> tracks;

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyTrackingVisitor&lt;TKey, TValue&gt;"/> class.
		/// </summary>
		public KeyTrackingVisitor()
		{
			this.tracks = new List<TKey>();
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
		/// Gets the tracking list.
		/// </summary>
		/// <value>The tracking list.</value>
		public IList<TKey> TrackingList
		{
			get
			{
				return this.tracks;
			}
		}

		/// <summary>
		/// Visits the specified keypair.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public void Visit(KeyValuePair<TKey, TValue> obj)
		{
			this.tracks.Add(obj.Key);
		}
	}
}