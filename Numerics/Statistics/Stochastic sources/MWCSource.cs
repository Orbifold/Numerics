using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A stochastic source based on the <see cref="MultiplyWithCarryRandomGenerator"/> generator.
	/// </summary>
	//	public class MWCSource:StochasticBase
	//	{
	//		/// <summary>
	//		/// Returns a new positive random number less than the specified maximum.
	//		/// </summary>
	//		/// <returns>A positive random number.</returns>
	//		/// <param name="maxValue">Max value.</param>
	//		public override int Next(int maxValue)
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform(maxValue);
	//		}
	//
	//		/// <summary>
	//		/// Returns a new positive random number in the specified interval.
	//		/// </summary>
	//		/// <returns>A positive random number in the given interval.</returns>
	//		/// <param name="minValue">Minimum value.</param>
	//		/// <param name="maxValue">Max value.</param>
	//		public override int Next(int minValue, int maxValue)
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform(minValue, maxValue);
	//		}
	//
	//		/// <summary>
	//		/// Returns a new random number in the [0,1) interval.
	//		/// </summary>
	//		/// <returns>A floating random number.</returns>
	//		public override double NextDouble()
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform();
	//		}
	//
	//		/// <summary>
	//		/// Returns a new floating random number less than the specified maximum.
	//		/// </summary>
	//		/// <returns>A floating random number.</returns>
	//		/// <param name="maxValue">Max value.</param>
	//		public override double NextDouble(double maxValue)
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform(0, maxValue);
	//		}
	//
	//		/// <summary>
	//		/// Returns a new floating random number in the specified interval.
	//		/// </summary>
	//		/// <returns>A floating random number.</returns>
	//		/// <param name="minValue">Minimum value.</param>
	//		/// <param name="maxValue">Max value.</param>
	//		public override double NextDouble(double minValue, double maxValue)
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform(minValue, maxValue);
	//		}
	//
	//		/// <summary>
	//		/// Returns a random boolean.
	//		/// </summary>
	//		/// <returns></returns>
	//		public override bool NextBoolean()
	//		{
	//			return MultiplyWithCarryRandomGenerator.GetUniform() < 0.5;
	//		}
	//
	//		/// <summary>
	//		/// Returns a random byte.
	//		/// </summary>
	//		/// <param name="buffer">The buffer.</param>
	//		public override void NextBytes(byte[] buffer)
	//		{
	//			throw new NotImplementedException("Under consideration.");
	//		}
	//
	//		/// <summary>
	//		/// Reset this instance.
	//		/// </summary>
	//		public override void Reset()
	//		{
	//			throw new NotImplementedException("Resetting this generator is not supported.");
	//		}
	//
	//		/// <summary>
	//		/// Initializes a new instance of the <see cref="Orbifold.Numerics.MWCSource"/> class.
	//		/// </summary>
	//		public MWCSource()
	//		{
	//		}
	//
	//		/// <summary>
	//		/// Returns a new positive random number.
	//		/// </summary>
	//		/// <returns>A positive random number.</returns>
	//		public override int Next()
	//		{
	//			return MultiplyWithCarryRandomGenerator.NextInt();
	//		}
	//	}
}