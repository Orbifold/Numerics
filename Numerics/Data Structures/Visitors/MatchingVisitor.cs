using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A visitor helping to find a specific element.
	/// </summary>
	/// <typeparam name="T">The data type.</typeparam>
	public class MatchingVisitor<T> : IVisitor<T>
	{
		private readonly T matchObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="MatchingVisitor&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="whatMatch">The what to match.</param>
		public MatchingVisitor(T whatMatch)
		{
			this.matchObject = whatMatch;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has completed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has completed; otherwise, <c>false</c>.
		/// </value>
		public bool HasCompleted { get; private set; }

		/// <summary>
		/// Visits the specified object.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public void Visit(T obj)
		{
			this.HasCompleted = EqualityComparer<T>.Default.Equals(this.matchObject, obj);
		}
	}
}