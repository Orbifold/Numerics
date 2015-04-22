namespace Orbifold.Numerics
{
	/// <summary>
	/// The default implementation of the <see cref="IVisitor{T}"/> interface.
	/// </summary>
	/// <typeparam name="T">The type of objects to be visited.</typeparam>
	public abstract class Visitor<T> : IVisitor<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Visitor{T}"/> class.
		/// </summary>
		protected Visitor()
		{
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has completed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has completed; otherwise, <c>false</c>.
		/// </value>
		public bool HasCompleted { get; protected set; }

		/// <summary>
		/// Visits the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		public virtual void Visit(T item)
		{
		}
	}
}