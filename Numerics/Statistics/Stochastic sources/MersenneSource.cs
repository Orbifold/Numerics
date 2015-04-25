using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// The Mersenne twister is a pseudorandom number generator developed in 1997 by Makoto Matsumoto 
	/// and Takuji Nishimura that is based on a matrix linear recurrence over a finite binary field.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>For implementation details see the Mersenne Twister Home Page: http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html .</description></item>
	/// <item>
	/// <description>See Wikipedia for general info on this topic: http://en.wikipedia.org/wiki/Mersenne_twister .</description></item></list>
	/// </remarks>
	public class MersenneSource : StochasticBase
	{
		/// <summary>
		/// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0 
		///   and less than 1.0 when it gets applied to a nonnegative 32-bit signed integer.
		/// </summary>
		private const double IntToDoubleMultiplier = 1.0 / (int.MaxValue + 1D);

		/// <summary>
		/// Represents the least significant r bits. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x7fffffff.</remarks>
		private const uint LowerMask = 0x7fffffffU;

		/// <summary>
		/// Represents a constant used for generation of unsigned random numbers. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 397.</remarks>
		private const int M = 397;

		/// <summary>
		/// Represents the number of unsigned random numbers generated at one time. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 624.</remarks>
		private const int N = 624;

		/// <summary>
		/// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0 
		///   and less than 1.0  when it gets applied to a 32-bit unsigned integer.
		/// </summary>
		private const double UIntToDoubleMultiplier = 1.0 / (uint.MaxValue + 1D);

		/// <summary>
		/// Represents the most significant w-r bits. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x80000000.</remarks>
		private const uint UpperMask = 0x80000000U;

		/// <summary>
		/// Represents the constant vector a. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x9908b0dfU.</remarks>
		private const uint VectorA = 0x9908b0dfU;

		/// <summary>
		/// Stores the state vector array.
		/// </summary>
		private readonly long[] mt;

		/// <summary>
		/// Stores the used seed value.
		/// </summary>
		private readonly int seed;

		/// <summary>
		/// Stores the used seed array.
		/// </summary>
		private readonly int[] seedArray;

		/// <summary>
		/// Stores an <see cref="uint"/> used to generate up to 32 random <see cref="bool"/> values.
		/// </summary>
		private int bitBuffer;

		/// <summary>
		/// Stores how many random <see cref="bool"/> values still can be generated from <see cref="bitBuffer"/>.
		/// </summary>
		private int bitCount;

		/// <summary>
		/// Stores an index for the state vector array element that will be accessed next.
		/// </summary>
		private uint mti;

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using a time-dependent default 
		///   seed value.
		/// </summary>
		public MersenneSource()
			: this(System.Math.Abs(Environment.TickCount))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed value.
		/// </summary>
		/// <param name="seed">
		/// An unsigned number used to calculate a starting value for the pseudo-random number sequence.
		/// </param>
		public MersenneSource(int seed)
		{

			this.mt = new long[N];
			this.seed = System.Math.Abs(seed);
			this.Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed array.
		/// </summary>
		/// <param name="seedArray">
		/// An array of numbers used to calculate a starting values for the pseudo-random number sequence.
		/// If negative numbers are specified, the absolute values of them are used. 
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="seedArray"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public MersenneSource(IList<int> seedArray)
		{
			if (seedArray == null) throw new ArgumentNullException("seedArray");

			this.mt = new long[N];
			this.seed = 19650218;

			this.seedArray = new int[seedArray.Count];
			for (var index = 0; index < seedArray.Count; index++) this.seedArray[index] = System.Math.Abs(seedArray[index]);

			this.Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed array.
		/// </summary>
		/// <param name="seedArray">
		/// An array of unsigned numbers used to calculate a starting values for the pseudo-random number sequence.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="seedArray"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public MersenneSource(int[] seedArray)
		{
			if (seedArray == null)
			{
				throw new ArgumentNullException("seedArray");
			}
			this.mt = new long[N];
			this.seed = 19650218;
			this.seedArray = seedArray;
			this.Reset();
		}

		/// <summary>
		/// Returns a nonnegative random number less than <see cref="Int32.MaxValue"/>.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to 0, and less than <see cref="Int32.MaxValue"/>; that is, 
		///   the range of return values includes 0 but not <paramref>
		///                                                   <name>Int32.MaxValue</name>
		///                                                 </paramref> .
		/// </returns>
		public override int Next()
		{
			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N)
			{
				// generate N words at one time
				this.GenerateUnsignedInts();
			}

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			var result = (int)(y >> 1);

			// Exclude Int32.MaxValue from the range of return values.
			return result == int.MaxValue ? this.Next() : result;
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
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue");
			}

			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N)
			{
				// generate N words at one time
				this.GenerateUnsignedInts();
			}

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			// The shift operation and extra int cast before the first multiplication give better performance.
			// See comment in NextDouble().
			return (int)((int)(y >> 1) * IntToDoubleMultiplier * maxValue);
		}

		/// <summary>
		/// Returns a random number within the specified range. 
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
			if (minValue > maxValue) throw new ArgumentOutOfRangeException("maxValue");

			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			var range = maxValue - minValue;
			//if (range < 0)
			//{
			//    // The range is greater than Int32.MaxValue, so we have to use slower floating point arithmetic.
			//    // Also all 32 random bits (uint) have to be used which again is slower (See comment in NextDouble()).
			//    return minValue + (int)(y * UIntToDoubleMultiplier * (maxValue - (double)minValue));
			//}

			// 31 random bits (int) will suffice which allows us to shift and cast to an int before the first multiplication and gain better performance.
			// See comment in NextDouble().
			//if ((int)((y >> 1) * IntToDoubleMultiplier * range) < 0) throw new Exception(string.Format("Trouble in the y-value; {0} -> {1}", ((y >> 1) * IntToDoubleMultiplier * range), Convert.ToInt32(((int)y >> 1) * IntToDoubleMultiplier * range)));
			if (y > int.MaxValue) y = y % int.MaxValue;
			var r = (Convert.ToInt32(y) >> 1) * IntToDoubleMultiplier * range;
			if (r > int.MaxValue) r = r % int.MaxValue;
			//if (r < 0) throw new Exception("Trouble in the algorithm!");
			return minValue + Convert.ToInt32(r);
		}

		/// <summary>
		/// Returns a random Boolean value.
		/// </summary>
		/// <remarks>
		/// Buffers 32 random bits (1 uint) for future calls, so a new random number is only generated every 32 calls.
		/// </remarks>
		/// <returns>A <see cref="bool"/> value.</returns>
		public override bool NextBoolean()
		{
			if (this.bitCount == 32)
			{
				// Generate 32 more bits (1 uint) and store it for future calls.
				// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
				if (this.mti >= N) this.GenerateUnsignedInts();

				var y = this.mt[this.mti++];

				// Tempering
				y ^= y >> 11;
				y ^= (y << 7) & 0x9d2c5680U;
				y ^= (y << 15) & 0xefc60000U;
				this.bitBuffer = (int)(y ^ (y >> 18));

				// Reset the bitCount and use rightmost bit of buffer to generate random bool.
				this.bitCount = 1;
				return (this.bitBuffer & 0x1) == 1;
			}

			// Increase the bitCount and use rightmost bit of shifted buffer to generate random bool.
			this.bitCount++;
			return ((this.bitBuffer >>= 1) & 0x1) == 1;
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers. 
		/// </summary>
		/// <remarks>
		/// Each element of the array of bytes is set to a random number greater than or equal to 0, and less than or 
		///   equal to <see cref="Byte.MaxValue"/>.
		/// </remarks>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="buffer"/> is a null reference (<see langword="Nothing"/> in Visual Basic). 
		/// </exception>
		public override void NextBytes(byte[] buffer)
		{
			if (buffer == null) throw new ArgumentNullException("buffer");

			// Fill the buffer with 4 bytes (1 uint) at a time.
			var i = 0;
			long y;
			while (i < buffer.Length - 3)
			{
				// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
				if (this.mti >= N)
				{
					// generate N words at one time
					this.GenerateUnsignedInts();
				}

				y = this.mt[this.mti++];

				// Tempering
				y ^= y >> 11;
				y ^= (y << 7) & 0x9d2c5680U;
				y ^= (y << 15) & 0xefc60000U;
				y ^= y >> 18;

				buffer[i++] = (byte)y;
				buffer[i++] = (byte)(y >> 8);
				buffer[i++] = (byte)(y >> 16);
				buffer[i++] = (byte)(y >> 24);
			}

			// Fill up any remaining bytes in the buffer.
			if (i < buffer.Length)
			{
				// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
				if (this.mti >= N)
				{
					// generate N words at one time
					this.GenerateUnsignedInts();
				}

				y = this.mt[this.mti++];

				// Tempering
				y ^= y >> 11;
				y ^= (y << 7) & 0x9d2c5680U;
				y ^= (y << 15) & 0xefc60000U;
				y ^= y >> 18;

				buffer[i++] = (byte)y;
				if (i < buffer.Length)
				{
					buffer[i++] = (byte)(y >> 8);
					if (i < buffer.Length)
					{
						buffer[i++] = (byte)(y >> 16);
						if (i < buffer.Length)
						{
							buffer[i] = (byte)(y >> 24);
						}
					}
				}
			}
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
			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			// Here a ~2x speed improvement is gained by computing a value that can be cast to an int 
			//   before casting to a double to perform the multiplication.
			// Casting a double from an int is a lot faster than from an uint and the extra shift operation 
			//   and cast to an int are very fast (the allocated bits remain the same), so overall there's 
			//   a significant performance improvement.
			return (int)(y >> 1) * IntToDoubleMultiplier;
		}

		/// <summary>
		/// Returns a nonnegative floating point random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to 0.0. 
		/// </param>
		/// <returns>
		/// A double-precision floating point number greater than or equal to 0.0, and less than <paramref name="maxValue"/>; 
		///   that is, the range of return values includes 0 but not <paramref name="maxValue"/>. 
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="maxValue"/> is less than 0. 
		/// </exception>
		public override double NextDouble(double maxValue)
		{
			if (maxValue < 0.0) throw new ArgumentOutOfRangeException("maxValue");

			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			// The shift operation and extra int cast before the first multiplication give better performance.
			// See comment in NextDouble().
			return (int)(y >> 1) * IntToDoubleMultiplier * maxValue;
		}

		/// <summary>
		/// Returns a floating point random number within the specified range. 
		/// </summary>
		/// <param name="minValue">
		/// The inclusive lower bound of the random number to be generated. 
		/// The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to
		///   <see cref="Double.MaxValue"/>.
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

			if (double.IsPositiveInfinity(range)) throw new ArgumentException("maxValue");

			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			// The shift operation and extra int cast before the first multiplication give better performance.
			// See comment in NextDouble().
			return minValue + ((int)(y >> 1) * IntToDoubleMultiplier * range);
		}

		/// <summary>
		/// Returns a nonnegative random number less than or equal to <see cref="Int32.MaxValue"/>.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to 0, and less than or equal to <see cref="Int32.MaxValue"/>; 
		///   that is, the range of return values includes 0 and <paramref>
		///                                                        <name>Int32.MaxValue</name>
		///                                                      </paramref> .
		/// </returns>
		public int NextInclusiveMaxValue()
		{
			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			return (int)(y >> 1);
		}

		/// <summary>
		/// Returns an unsigned random number.
		/// </summary>
		/// <returns>
		/// A 32-bit unsigned integer greater than or equal to <see cref="uint.MinValue"/> and 
		///   less than or equal to <see cref="uint.MaxValue"/>.
		/// </returns>
		public int NextUInt()
		{
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			return (int)(y ^ (y >> 18));
		}

		/// <summary>
		/// Generates <see cref="MersenneSource.N"/> unsigned random numbers.
		/// </summary>
		/// <remarks>
		/// Generated random numbers are 32-bit unsigned integers greater than or equal to <see cref="uint.MinValue"/> 
		///   and less than or equal to <see cref="uint.MaxValue"/>.
		/// </remarks>
		private void GenerateUnsignedInts()
		{
			int kk;
			long y;
			var mag01 = new[] { 0x0U, VectorA };

			for (kk = 0; kk < N - M; kk++)
			{
				y = (this.mt[kk] & UpperMask) | (this.mt[kk + 1] & LowerMask);
				this.mt[kk] = this.mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
			}

			for (; kk < N - 1; kk++)
			{
				y = (this.mt[kk] & UpperMask) | (this.mt[kk + 1] & LowerMask);
				this.mt[kk] = this.mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
			}

			y = (this.mt[N - 1] & UpperMask) | (this.mt[0] & LowerMask);
			this.mt[N - 1] = this.mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

			this.mti = 0;
		}

		/// <summary>
		/// Extends resetting of the <see cref="MersenneSource"/> using the <see cref="seedArray"/>.
		/// </summary>
		private void ResetBySeedArray()
		{
			uint i = 1;
			uint j = 0;
			var k = (N > this.seedArray.Length) ? N : this.seedArray.Length;
			for (; k > 0; k--)
			{
				this.mt[i] = (this.mt[i] ^ ((this.mt[i - 1] ^ (this.mt[i - 1] >> 30)) * 1664525U)) + this.seedArray[j] + j; // non linear
				i++;
				j++;

				if (i >= N)
				{
					this.mt[0] = this.mt[N - 1];
					i = 1;
				}

				if (j >= this.seedArray.Length) j = 0;
			}

			for (k = N - 1; k > 0; k--)
			{
				this.mt[i] = (this.mt[i] ^ ((this.mt[i - 1] ^ (this.mt[i - 1] >> 30)) * 1566083941U)) - i; // non linear
				i++;
				if (i >= N)
				{
					this.mt[0] = this.mt[N - 1];
					i = 1;
				}
			}

			this.mt[0] = 0x80000000U; // MSB is 1; assuring non-0 initial array
		}

		/// <summary>
		/// Resets the <see cref="MersenneSource"/>,
		/// so that it produces the same pseudo-random number sequence again.
		/// </summary>
		public override sealed void Reset()
		{
			this.mt[0] = this.seed & 0xffffffffU;
			for (this.mti = 1; this.mti < N; this.mti++)
			{
				this.mt[this.mti] = (1812433253U * (this.mt[this.mti - 1] ^ (this.mt[this.mti - 1] >> 30)) + this.mti);

				// See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier.
				// In the previous versions, MSBs of the seed affect only MSBs of the array mt[].
				// 2002/01/09 modified by Makoto Matsumoto
			}

			// If the object was instantiated with a seed array do some further (re)initialisation.
			if (this.seedArray != null) this.ResetBySeedArray();

			// Reset helper variables used for generation of random booleans.
			this.bitBuffer = 0;
			this.bitCount = 32;
		}
	}
}