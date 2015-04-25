
namespace Orbifold.Numerics
{
	public static class EpsilonExtensions
	{
		#region Done/Reviewed

		/// <summary>
		/// Returns whether the given value is less than an <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value">A value.</param>
		/// <returns>
		///   <c>true</c> if less than <see cref="Constants.Epsilon"/>; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVerySmall(this double value)
		{
			return System.Math.Abs(value) < Constants.Epsilon;
		}

		/// <summary>
		/// Checks whether two values are close, i.e. the absolute value of their difference is less than <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns></returns>
		public static bool AreClose(double value1, double value2)
		{
			return value1.IsEqualTo(value2) || (value1 - value2).IsVerySmall();
		}

		/// <summary>
		/// Checks whether two values are not close, i.e. the absolute value of their difference is larger than <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns></returns>
		public static bool AreNotClose(double value1, double value2)
		{
			return !AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThanOrClose(double value1, double value2)
		{
			return value1 < value2 || AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThanOrCloseTo(this double value1, double value2)
		{
			return IsLessThanOrClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is less than or equal to another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than or equal to the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessOrEqual(double value1, double value2)
		{
			return value1 < (value2 + Constants.Epsilon);
		}

		/// <summary>
		/// Determines whether a given value is less than or equal to another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than or equal to the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessOrEqualTo(this double value1, double value2)
		{
			return IsLessOrEqual(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one  and not an epsilon away.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second and not an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThan(double value1, double value2)
		{
			return (value1 < value2) && !AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly bigger than another one and not an epsilon away.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second and not an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsGreaterThan(double value1, double value2)
		{
			return (value1 > value2) && !AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly bigger than another one or an epsilon away.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second or an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsGreaterThanOrClose(double value1, double value2)
		{
			return value1 > value2 || AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether the double values are equal up to an epsilon.
		/// </summary>
		/// <param name="value1">A double value.</param>
		/// <param name="value2">Another double value.</param>
		/// <returns>
		///   <c>True</c> If the values are equal; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEqualTo(this double value1, double value2)
		{
			return System.Math.Abs(value1 - value2) < Constants.Epsilon;
		}

		/// <summary>
		/// Determines whether the number is as good as zero within the <see cref="Constants.Epsilon" /> bounds.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="accuracy">The accuracy or bounds within which the value has to be in order to be considered as zero.</param>
		/// <returns>
		///   <c>true</c> if the specified absolute value is within the accuracy; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsZero(this double value, double accuracy = Constants.Epsilon)
		{
			return System.Math.Abs(value) < accuracy;
		}
        
        public static bool IsMachineZero(this double value)
		{
			return System.Math.Abs(value) < double.Epsilon;
		}

		/// <summary>
		/// Determines whether the number is not zero within the <see cref="Constants.Epsilon" /> bounds.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="accuracy">The accuracy or bounds within which the value has to be in order to be considered as zero.</param>
		/// <returns>
		///   <c>true</c> if the specified absolute value is within the accuracy; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNotZero(this double value, double accuracy = Constants.Epsilon)
		{
			return !IsZero(value, accuracy);
		}

		/// <summary>
		/// Determines whether the two values are different in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the values are equal up to epsilon; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNotEqualTo(this double value1, double value2)
		{
			return !value1.IsEqualTo(value2);
		}

		#endregion
		 
	}
}