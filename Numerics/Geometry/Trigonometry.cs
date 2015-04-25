namespace Orbifold.Numerics
{
	/// <summary>
	/// Trigonometry functions and extensions.
	/// </summary>
	public static class Trigonometry
	{
		/// <summary>
		/// Converts degrees to radians.
		/// </summary>
		public static double DegreesToRadians(double degree)
		{
			return degree * Constants.PiOver180;
		}

		/// <summary>
		/// Converts the given value to radians assuming it's a value spefifying degrees.
		/// </summary>
		/// <param name="degree">The degrees.</param>
		/// <returns></returns>
		/// <seealso cref="DegreesToRadians"/>
		public static double ToRadians(this double degree)
		{
			return DegreesToRadians(degree);
		}
		
		/// <summary>
		/// Converts the given value to degrees assuming it's a value spefifying radians.
		/// </summary>
		/// <param name="radians">The radians.</param>
		/// <returns></returns>
		/// <seealso cref="RadiansToDegrees"/>
		public static double ToDegrees(this double radians)
		{
			return DegreesToRadians(radians);
		}

		/// <summary>
		/// Converts radians to degrees.
		/// </summary>
		public static double RadiansToDegrees(double radian)
		{
			return radian / Constants.PiOver180;
		}

		/// <summary>
		/// Returns the sine of the given angle (in radians).
		/// </summary>
		public static double Sine(double radian)
		{
			return System.Math.Sin(radian);
		}

		/// <summary>
		/// Returns the cosine of the given angle (in radians).
		/// </summary>
		public static double Cosine(double radian)
		{
			return System.Math.Cos(radian);
		}

		/// <summary>
		/// Returns the tangent of the given angle (in radians).
		/// </summary>
		public static double Tangent(double radian)
		{
			return System.Math.Tan(radian);
		}

		/// <summary>
		/// Returns the cotangent of the given angle (in radians).
		/// </summary>
		public static double Cotangent(double radian)
		{
			return 1 / System.Math.Tan(radian);
		}

		/// <summary>
		/// Returns the secant of the given angle (in radians).
		/// </summary>
		/// <remarks>Sec = 1/Cos.</remarks>
		public static double Secant(double radian)
		{
			return 1 / System.Math.Cos(radian);
		}

		/// <summary>
		/// Returns the cosecant of the given angle (in radians).
		/// </summary>
		/// <remarks>Cosec = 1/Sin.</remarks>
		public static double Cosecant(double radian)
		{
			return 1 / System.Math.Sin(radian);
		}

		/// <summary>
		/// Returns the inverse sine of the given value.
		/// </summary>
		/// <param name="real">The value.</param>
		/// <returns>The angle in radians.</returns>
		public static double InverseSine(double real)
		{
			return System.Math.Asin(real);
		}

		/// <summary>
		/// Returns the inverse cosine of the given value.
		/// </summary>
		public static double InverseCosine(double real)
		{
			return System.Math.Acos(real);
		}

		/// <summary>
		/// Returns the inverse tangent of the given value.
		/// </summary>
		public static double InverseTangent(double real)
		{
			return System.Math.Atan(real);
		}

		/// <summary>
		/// Returns the inverse tangent of the given ratio values.
		/// </summary>
		/// <param name="nominator">The nominator.</param>
		/// <param name="denominator">The denominator.</param>
		public static double InverseTangentFromRatio(double nominator, double denominator)
		{
			return System.Math.Atan2(nominator, denominator);
		}

		/// <summary>
		/// Returns the inverse cotangent of the given value.
		/// </summary>
		public static double InverseCotangent(double real)
		{
			return System.Math.Atan(1 / real);
		}

		/// <summary>
		/// Returns the inverse secans of the given value.
		/// </summary>
		public static double InverseSecant(double real)
		{
			return System.Math.Acos(1 / real);
		}

		/// <summary>
		/// Returns the inverse cosecans of the given value.
		/// </summary>
		public static double InverseCosecant(double real)
		{
			return System.Math.Asin(1 / real);
		}

		/// <summary>
		/// Returns the hyperbolic sine of the given value.
		/// </summary>
		public static double HyperbolicSine(double radian)
		{
			return System.Math.Sinh(radian);
		}

		/// <summary>
		/// Returns the hyperbolic cosine of the given value.
		/// </summary>
		public static double HyperbolicCosine(double radian)
		{
			return System.Math.Cosh(radian);
		}

		/// <summary>
		/// Returns the hyperbolic tangent of the given value.
		/// </summary>
		public static double HyperbolicTangent(double radian)
		{
			return System.Math.Tanh(radian);
		}

		/// <summary>
		/// Returns the hyperbolic cotangent of the given value.
		/// </summary>
		public static double HyperbolicCotangent(double radian)
		{
			return 1 / System.Math.Tanh(radian);
		}

		/// <summary>
		/// Returns the hyperbolic secans of the given value.
		/// </summary>
		public static double HyperbolicSecant(double radian)
		{
			return 1 / HyperbolicCosine(radian);
		}

		/// <summary>
		/// Returns the hyperbolic cosecans of the given value.
		/// </summary>
		public static double HyperbolicCosecant(double radian)
		{
			return 1 / HyperbolicSine(radian);
		}

		/// <summary>
		/// Returns the inverse hyperbolic since of the given value.
		/// </summary>
		public static double InverseHyperbolicSine(double real)
		{
			return System.Math.Log(real + System.Math.Sqrt((real * real) + 1), System.Math.E);
		}

		/// <summary>
		/// Returns the inverse hyperbolic cosine of the given value.
		/// </summary>
		public static double InverseHyperbolicCosine(double real)
		{
			return System.Math.Log(real + (System.Math.Sqrt(real - 1) * System.Math.Sqrt(real + 1)), System.Math.E);
		}

		/// <summary>
		/// Returns the inverse hyperbolic tangent of the given value.
		/// </summary>
		public static double InverseHyperbolicTangent(double real)
		{
			return 0.5 * System.Math.Log((1 + real) / (1 - real), System.Math.E);
		}

		/// <summary>
		/// Returns the inverse hyperbolic cotangent of the given value.
		/// </summary>
		public static double InverseHyperbolicCotangent(double real)
		{
			return 0.5 * System.Math.Log((real + 1) / (real - 1), System.Math.E);
		}

		/// <summary>
		/// Returns the inverse hyperbolic cosine of the given value.
		/// </summary>
		public static double InverseHyperbolicSecant(double real)
		{
			return InverseHyperbolicCosine(1 / real);
		}

		/// <summary>
		/// Returns the inverse hyperbolic cosecans of the given value.
		/// </summary>
		public static double InverseHyperbolicCosecant(double real)
		{
			return InverseHyperbolicSine(1 / real);
		}
	}
}
