using System.Collections.Generic;

namespace Orbifold.Numerics.Tests
{
	/// <summary>
	/// This visitor keeps a trail of the visited item in the <see cref="Trail"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class TrailVisitor<T> : IVisitor<T>
	{
		private readonly List<T> trail;

		public TrailVisitor()
		{
			this.trail = new List<T>();
		}

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
		public IList<T> Trail
		{
			get
			{
				return this.trail;
			}
		}

		public void Visit(T obj)
		{
			this.trail.Add(obj);
		}
	}
}