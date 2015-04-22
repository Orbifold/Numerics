using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A visitor which encloses a standard action.
	/// </summary>
	/// <typeparam name="T">The data type.</typeparam>
	public sealed class ActionVisitor<T> : IVisitor<T>
	{
		private Action<T> action;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionVisitor&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="action">The action.</param>
		public ActionVisitor(Action<T> action)
		{
			if (action == null) throw new ArgumentNullException("action");
			this.action = action;
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
		/// Visits the specified obj.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public void Visit(T obj)
		{
			this.action(obj);
		}
	}
}