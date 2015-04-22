using System;

namespace Orbifold.Numerics
{
	public interface IQueue<T>
	{
		/// <summary>
		/// Enqueues the item at the back of the queue.
		/// </summary>
		/// <param name="item">The item.</param>
		void Enqueue(T item);

		/// <summary>
		/// Dequeues the item at the front of the queue.
		/// </summary>
		/// <returns>The item at the front of the queue.</returns>
		/// <exception cref="InvalidOperationException">The <see cref="Deque{T}"/> is empty.</exception>
		T Pop();

		/// <summary>
		/// Peeks at the item in the front of the queue, without removing it.
		/// </summary>
		/// <returns>The item at the front of the queue.</returns>
		/// <exception cref="InvalidOperationException">The <see cref="Deque{T}"/> is empty.</exception>
		T Peek();
	}
}