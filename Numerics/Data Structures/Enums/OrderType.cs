namespace Orbifold.Numerics
{
	/// <summary>
	/// Enumerates the two ways a <see cref="PriorityQueue{TValue,TPriority}"/> orders its elements.
	/// </summary>
	public enum OrderType
	{
		/// <summary>
		/// Specifies that the element with the minimum priority will pop first in the <see cref="PriorityQueue{TValue, TPriority}"/>.
		/// </summary>
		Ascending = 0,

		/// <summary>
		/// Specifies that the element with the maximum priority will pop first in the <see cref="PriorityQueue{TValue, TPriority}"/>.
		/// </summary>
		Descending = 1
	}
}