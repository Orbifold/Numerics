namespace Orbifold.Numerics
{
	/// <summary>
	/// Enumerates the moments at which payments occur.
	/// </summary>
	public enum DueDate
	{
		/// <summary>
		/// Payments occur at the begin of the period.
		/// </summary>
		BeginOfPeriod,

		/// <summary>
		/// Payments occur at the end of the period.
		/// </summary>
		EndOfPeriod
	}
}