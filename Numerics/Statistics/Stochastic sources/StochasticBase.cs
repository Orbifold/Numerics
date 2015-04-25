
namespace Orbifold.Numerics
{
	/// <summary>
	/// Base class for stochastic distributions.
	/// </summary>
	public abstract class StochasticBase : IStochastic
	{
		/// <summary>
		/// Returns a new positive random number.
		/// </summary>
		/// <returns>
		/// A positive random number.
		/// </returns>
		public abstract int Next();

		/// <summary>
		/// Returns a new positive random number less than the specified maximum.
		/// </summary>
		/// <returns>
		/// A positive random number.
		/// </returns>
		public abstract int Next(int maxValue);

		/// <summary>
		/// Returns a new positive random number in the specified interval.
		/// </summary>
		/// <returns>
		/// A positive random number in the given interval.
		/// </returns>
		public abstract int Next(int minValue, int maxValue);

		/// <summary>
		/// Returns a new random number in the [0,1) interval.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		public abstract double NextDouble();

		/// <summary>
		/// Returns a new floating random number less than the specified maximum.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		public abstract double NextDouble(double maxValue);

		/// <summary>
		/// Returns a new floating random number in the specified interval.
		/// </summary>
		/// <returns>
		/// A floating random number.
		/// </returns>
		public abstract double NextDouble(double minValue, double maxValue);


		/// <summary>
		/// Returns a random boolean.
		/// </summary>
		/// <returns></returns>
		public abstract bool NextBoolean();


		/// <summary>
		/// Returns a random byte.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		public abstract void NextBytes(byte[] buffer);

		public abstract void Reset();

		public virtual bool CanReset { get; set; }
	}
}