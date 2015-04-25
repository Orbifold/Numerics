using System;
using System.Linq;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Number theory utility functions for integers.
	/// </summary>
	public static class Numbers
	{
		/// <summary>
		/// Find out whether the provided 32 bit integer is an even number.
		/// </summary>
		/// <param name="number">The number to very whether it's even.</param>
		/// <returns>True if and only if it is an even number.</returns>
		public static bool IsEven(this int number)
		{
			return (number & 0x1) == 0x0;
		}

        /// <summary>
        /// Truncates the specified number by dropping its decimals.
        /// </summary>
        /// <param name="number">The number.</param>
	    public static long Truncate(this double number)
        {
            if (Math.Abs(number) < Constants.Epsilon) return 0;
            return number < 0 ? (long)System.Math.Ceiling(number) : (long)System.Math.Floor(number);
        }

	    /// <summary>
		/// Find out whether the provided 32 bit integer is an odd number.
		/// </summary>
		/// <param name="number">The number to very whether it's odd.</param>
		/// <returns>True if and only if it is an odd number.</returns>
		public static bool IsOdd(this int number)
		{
			return (number & 0x1) == 0x1;
		}

		/// <summary>
		/// Find out whether the provided 32 bit integer is a perfect power of two.
		/// </summary>
		/// <param name="number">The number to very whether it's a power of two.</param>
		/// <returns>True if and only if it is a power of two.</returns>
		public static bool IsPowerOfTwo(this int number)
		{
			return number > 0 && (number & (number - 1)) == 0x0;
		}

		/// <summary>
		/// Find out whether the provided 64 bit integer is a perfect power of two.
		/// </summary>
		/// <param name="number">The number to very whether it's a power of two.</param>
		/// <returns>True if and only if it is a power of two.</returns>
		public static bool IsPowerOfTwo(this long number)
		{
			return number > 0 && (number & (number - 1)) == 0x0;
		}

		/// <summary>
		/// Find the closest perfect power of two that is larger or equal to the provided
		/// 32 bit integer.
		/// </summary>
		/// <param name="number">The number of which to find the closest upper power of two.</param>
		/// <returns>A power of two.</returns>
		public static int CeilingToPowerOfTwo(this int number)
		{
			if (number == int.MinValue) return 0;

			const int MaxPowerOfTwo = 0x40000000;
			if (number > MaxPowerOfTwo) throw new ArgumentOutOfRangeException("number");

			number--;
			number |= number >> 1;
			number |= number >> 2;
			number |= number >> 4;
			number |= number >> 8;
			number |= number >> 16;
			return number + 1;
		}

		/// <summary>
		/// Raises 2 to the provided integer exponent (0 &lt;= exponent &lt; 31).
		/// </summary>
		/// <param name="exponent">The exponent to raise 2 up to.</param>
		/// <returns>2 ^ exponent.</returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public static int PowerOfTwo(this int exponent)
		{
			if (exponent < 0 || exponent >= 31) throw new ArgumentOutOfRangeException("exponent");
			return 1 << exponent;
		}

		/// <summary>
		/// Returns the greatest common divisor of the given list.
		/// </summary>
		public static int GreatestCommonDivisor(IList<int> integers)
		{
			if (null == integers) throw new ArgumentNullException("integers");
			if (integers.Count == 0) return 0;
			var gcd = System.Math.Abs(integers[0]);
			for (var i = 1; (i < integers.Count) && (gcd > 1); i++) gcd = GreatestCommonDivisor(gcd, integers[i]);
			return gcd;
		}

		/// <summary>
		/// Returns the greatest common divisor of the given numbers.
		/// </summary>
		public static int GreatestCommonDivisor(params int[] integers)
		{
			return GreatestCommonDivisor(integers.ToList());
		}

		/// <summary>
		/// Returns the least common multiple (<c>lcm</c>) of a set of integers using Euclid's algorithm.
		/// </summary>
		public static int LeastCommonMultiple(IList<int> integers)
		{
			if (null == integers) throw new ArgumentNullException("integers");
			if (integers.Count == 0) return 1;
			var lcm = System.Math.Abs(integers[0]);
			for (var i = 1; i < integers.Count; i++) lcm = LeastCommonMultiple(lcm, integers[i]);
			return lcm;
		}

		/// <summary>
		/// Returns the least common multiple of the given numbers.
		/// </summary>
		public static int LeastCommonMultiple(params int[] integers)
		{
			return LeastCommonMultiple((IList<int>)integers);
		}

		public static bool AlmostEqual(double a, double b, int numberOfDigits = 5)
		{
			return Math.Abs((a - b) * Math.Pow(10, numberOfDigits) - 1d) < Constants.Epsilon;
		}
	}
}