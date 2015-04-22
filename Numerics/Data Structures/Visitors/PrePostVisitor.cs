namespace Orbifold.Numerics
{
	/// <summary>
	/// A visitor with a pre and post action.
	/// </summary>
	/// <typeparam name="T">The dara type being visited.</typeparam>
	public class PrePostVisitor<T> : IVisitor<T>
	{
		private readonly IVisitor<T> visitorToUse;

		/// <summary>
		/// Initializes a new instance of the <see cref="PrePostVisitor&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="visitorToUse">The visitor to use.</param>
		public PrePostVisitor(IVisitor<T> visitorToUse)
		{
			this.visitorToUse = visitorToUse;
		}

		/// <summary>
		/// Determines whether this visitor is done.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// 	<c>true</c> if this visitor is done; otherwise, <c>false</c>.
		/// </returns>
		public bool HasCompleted
		{
			get
			{
				return this.visitorToUse.HasCompleted;
			}
		}

		/// <summary>
		/// Gets the visitor to use.
		/// </summary>
		/// <value>The visitor to use.</value>
		public IVisitor<T> VisitorToUse
		{
			get
			{
				return this.visitorToUse;
			}
		}

		/// <summary>
		/// Visits the specified object.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public virtual void Visit(T obj)
		{
			this.visitorToUse.Visit(obj);
		}
		 
		/// <summary>
		/// Post-visit method.
		/// </summary>
		/// <param name="item">The item being visited.</param>        
		public virtual void PostVisit(T item)
		{
			this.visitorToUse.Visit(item);
		}

		/// <summary>
		/// Pre-visit method.
		/// </summary>
		/// <param name="item">The item being visited.</param>         
		public virtual void PreVisit(T item)
		{
			this.visitorToUse.Visit(item);
		}
	}
}