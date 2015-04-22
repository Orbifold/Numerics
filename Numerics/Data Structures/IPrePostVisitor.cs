namespace Orbifold.Numerics
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">The data type being visited.</typeparam>
	public interface IPrePostVisitor<T> : IVisitor<T>
	{
		/// <summary>
		/// Pre-visit action.
		/// </summary>
		/// <param name="item">The item.</param>
		void PreVisit(T item);

		/// <summary>
		/// Post-visit action.
		/// </summary>
		/// <param name="item">The item.</param>
		void PostVisit(T item);
	}
}