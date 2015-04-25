using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Random number generator based on the <see cref="Random"/> class.
	/// </summary>
	public class DefaultSource : StochasticBase
	{
		/// <summary>
		/// Stores the used seed value.
		/// </summary>
		private readonly int seed;

		/// <summary>
		/// Stores an <see cref="Int32"/> used to generate up to 31 random <see cref="Boolean"/> values.
		/// </summary>
		private int bitBuffer;

		/// <summary>
		/// Stores how many random <see cref="Boolean"/> values still can be generated from <see cref="bitBuffer"/>.
		/// </summary>
		private int bitCount;

		private Random rand;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSource"/> class, using a time-dependent default 
		///   seed value.
		/// </summary>
		public DefaultSource()
			: this(Environment.TickCount)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSource"/> class, using the specified seed value.
		/// </summary>
		/// <param name="seed">
		/// A number used to calculate a starting value for the pseudo-random number sequence.
		/// If a negative number is specified, the absolute value of the number is used. 
		/// </param>
		public DefaultSource(int seed)
		{
			this.seed = System.Math.Abs(seed);
			this.Reset();
		}

		/// <summary>
		/// Returns a nonnegative random number.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to zero and less than <see cref="Int32.MaxValue"/>.
		/// </returns>
		public override int Next()
		{
			return this.rand.Next();
		}

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to 0. 
		/// </param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to 0, and less than <paramref name="maxValue"/>; that is, 
		///   the range of return values includes 0 but not <paramref name="maxValue"/>. 
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="maxValue"/> is less than 0. 
		/// </exception>
		public override int Next(int maxValue)
		{
			return this.rand.Next(maxValue);
		}

		/// <summary>
		/// Returns a random number within a specified range. 
		/// </summary>
		/// <param name="minValue">
		/// The inclusive lower bound of the random number to be generated. 
		/// </param>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. 
		/// </param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to <paramref name="minValue"/>, and less than 
		///   <paramref name="maxValue"/>; that is, the range of return values includes <paramref name="minValue"/> but 
		///   not <paramref name="maxValue"/>. 
		/// If <paramref name="minValue"/> equals <paramref name="maxValue"/>, <paramref name="minValue"/> is returned.  
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
		/// </exception>
		public override int Next(int minValue, int maxValue)
		{
			return this.rand.Next(minValue, maxValue);
		}

		/// <summary>
		/// Returns a random Boolean value.
		/// </summary>
		/// <remarks>
		/// Buffers 31 random bits (1 int) for future calls, so a new random number is only generated every 31 calls.
		/// </remarks>
		/// <returns>A <see cref="Boolean"/> value.</returns>
		public override bool NextBoolean()
		{
			if (this.bitCount == 0)
			{
				// Generate 31 more bits (1 int) and store it for future calls.
				this.bitBuffer = this.rand.Next();

				// Reset the bitCount and use rightmost bit of buffer to generate random bool.
				this.bitCount = 30;
				return (this.bitBuffer & 0x1) == 1;
			}

			// Decrease the bitCount and use rightmost bit of shifted buffer to generate random bool.
			this.bitCount--;
			return ((this.bitBuffer >>= 1) & 0x1) == 1;
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers. 
		/// </summary>
		/// <remarks>
		/// Each element of the array of bytes is set to a random number greater than or equal to zero, and less than or 
		///   equal to <see cref="Byte.MaxValue"/>.
		/// </remarks>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="buffer"/> is a null reference (<see langword="Nothing"/> in Visual Basic). 
		/// </exception>
		public override void NextBytes(byte[] buffer)
		{
			this.rand.NextBytes(buffer);
		}

		/// <summary>
		/// Returns a nonnegative floating point random number less than 1.0.
		/// </summary>
		/// <returns>
		/// A double-precision floating point number greater than or equal to 0.0, and less than 1.0; that is, 
		///   the range of return values includes 0.0 but not 1.0.
		/// </returns>
		public override double NextDouble()
		{
			return this.rand.NextDouble();
		}

		/// <summary>
		/// Returns a nonnegative floating point random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to zero. 
		/// </param>
		/// <returns>
		/// A double-precision floating point number greater than or equal to zero, and less than <paramref name="maxValue"/>; 
		///   that is, the range of return values includes zero but not <paramref name="maxValue"/>. 
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="maxValue"/> is less than 0. 
		/// </exception>
		public override double NextDouble(double maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue");
			}

			return this.rand.NextDouble() * maxValue;
		}

		/// <summary>
		/// Returns a floating point random number within the specified range. 
		/// </summary>
		/// <param name="minValue">
		/// The inclusive lower bound of the random number to be generated. 
		/// The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to
		///   <see cref="Double.MaxValue"/>
		/// </param>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.
		/// The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to
		///   <see cref="Double.MaxValue"/>.
		/// </param>
		/// <returns>
		/// A double-precision floating point number greater than or equal to <paramref name="minValue"/>, and less than 
		///   <paramref name="maxValue"/>; that is, the range of return values includes <paramref name="minValue"/> but 
		///   not <paramref name="maxValue"/>. 
		/// If <paramref name="minValue"/> equals <paramref name="maxValue"/>, <paramref name="minValue"/> is returned.  
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The range between <paramref name="minValue"/> and <paramref name="maxValue"/> is greater than
		///   <see cref="Double.MaxValue"/>.
		/// </exception>
		public override double NextDouble(double minValue, double maxValue)
		{
			if (minValue > maxValue) throw new ArgumentOutOfRangeException("maxValue");
			var range = maxValue - minValue;
			if (double.IsPositiveInfinity(range)) throw new ArgumentException("range");

			return minValue + this.rand.NextDouble() * range;
		}

		/// <summary>
		/// Resets the <see cref="DefaultSource"/>, so that it produces the same pseudo-random number sequence again.
		/// </summary> 
		public override sealed void Reset()
		{
			// Create a new Random object using the same seed.
			this.rand = new Random(this.seed);

			// Reset helper variables used for generation of random bools.
			this.bitBuffer = 0;
			this.bitCount = 0;
		}
	}
}