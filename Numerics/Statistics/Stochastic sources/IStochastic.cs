namespace Orbifold.Numerics
{
	/// <summary>
	/// Defines a stochastic source.
	/// </summary>
	public interface IStochastic
	{
		/// <summary>
		/// Returns a new positive random number.
		/// </summary>
		/// <returns>
		/// A positive random number.
		/// </returns>
		int Next();

		/// <summary>
		/// Returns a new positive random number less than the specified maximum.
		/// </summary>
		/// <returns>
		/// A positive random number.
		/// </returns>
		int Next(int maxValue);

		/// <summary>
		/// Returns a new positive random number in the specified interval.
		/// </summary>
		/// <returns>
		/// A positive random number in the given interval.
		/// </returns>
		int Next(int minValue, int maxValue);

		/// <summary>
		/// Returns a new random number in the [0,1) interval.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		double NextDouble();

		/// <summary>
		/// Returns a new floating random number less than the specified maximum.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		double NextDouble(double maxValue);

		/// <summary>
		/// Returns a new floating random number in the specified interval.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		double NextDouble(double minValue, double maxValue);

		/// <summary>
		/// Returns a random boolean.
		/// </summary>
		/// <returns></returns>
		bool NextBoolean();

		/// <summary>
		/// Returns a random byte.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		void NextBytes(byte[] buffer);

		void Reset();

		bool CanReset { get; set; }
	}
}