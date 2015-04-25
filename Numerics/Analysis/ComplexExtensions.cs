using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Numerics;
namespace Orbifold.Numerics
{
    /// <summary>
    /// Extension methods 
    /// </summary>
    public static class ComplexExtensions
    {
        /// <summary>
        /// The regex pattern recognizing 'a+bi' and all its variations.
        /// </summary>
        const string ComplexNumberPattern = @"(([-+]?(\d+\.?\d*|\d*\.?\d+)([Ee][-+]?[0-2]?\d{1,2})?)(?!=[ij]))?(\s*(([-+]?\s*[ij](\d+\.?\d*|\d*\.?\d+)([Ee][-+]?[0-2]?\d{1,2})?)|([-+]?\s*((\d+\.?\d*|\d*\.?\d+)([Ee][-+]?[0-2]?\d{1,2})?)?[ij])))?";

        /// <summary>
        /// Return whether the <see cref="Complex"/> number is zero (in an <see cref="Constants.Epsilon"/> sense).
        /// </summary>
        public static bool IsZero(this Complex complex)
        {
            return complex.Real.IsZero() && complex.Imaginary.IsZero();
        }
        
        /// <summary>
        /// Return whether the <see cref="Complex"/> number is not zero (in an <see cref="Constants.Epsilon"/> sense).
        /// </summary>
        public static bool IsNotZero(this Complex complex)
        {
            return !IsZero(complex);
        }

        /// <summary>
        /// Calculates the modulus of the complex number in polar form.
        /// </summary>
        /// <returns>The modulus of the complex number in polar form.</returns>
        public static double PolarFormModulus(this Complex c)
        {
            var a = c.Real;
            var b = c.Imaginary;

            return System.Math.Sqrt((System.Math.Pow(a, 2) + System.Math.Pow(b, 2)));
        }

        /// <summary>
        /// Calculates the angle in radians for the complex number in polar form.
        /// </summary>
        /// <returns>The angle in radians for the complex number in polar form.</returns>
        public static double PolarFormAngle(this Complex c)
        {
            var a = c.Real;
            var b = c.Imaginary;
            return System.Math.Atan2(a, b);
        }

        /// <summary>
        /// Inverts the sign of the imaginary number part.
        /// </summary>
        public static Complex NegateImaginaryNumberPart(this Complex c)
        {
            return new Complex(c.Real, -c.Imaginary);
        }

        /// <summary>
        /// Inverts the sign of the real number part.
        /// </summary>
        public static Complex NegateRealNumberPart(this Complex c)
        {
            return new Complex(-c.Real, c.Imaginary);
        }

        /// <summary>
        /// Returns whether the <see cref="Complex"/> number is one (in an <see cref="Constants.Epsilon"/> sense).
        /// </summary>
        public static bool IsOne(this Complex complex)
        {
            return complex.Real.IsEqualTo(1.0) && complex.Imaginary.IsZero();
        }

        /// <summary>
        /// Returns whether the <see cref="Complex"/> number is the imaginary unit.
        /// </summary>
        public static bool IsImaginaryOne(this Complex complex)
        {
            return complex.Real.IsZero() && complex.Imaginary.IsEqualTo(1.0);
        }

        /// <summary>
        /// Returns whether either one or both parts of the <see cref="Complex"/> number is not a number (NaN).
        /// </summary>
        public static bool IsNaN(this Complex complex)
        {
            return double.IsNaN(complex.Real) || double.IsNaN(complex.Imaginary);
        }

        /// <summary>
        /// Returns whether either one or both parts of the <see cref="Complex"/> number is infinite (<see cref="double.IsInfinity"/>).
        /// </summary>
        public static bool IsInfinity(this Complex complex)
        {
            return double.IsInfinity(complex.Real) || double.IsInfinity(complex.Imaginary);
        }

        /// <summary>
        /// Returns whether the <see cref="Complex"/> number is purely real.
        /// </summary>
        public static bool IsReal(this Complex complex)
        {
            return complex.Imaginary.IsZero();
        }

        /// <summary>
        /// Returns whether the <see cref="Complex"/> number is purely real and negative.
        /// </summary>
        public static bool IsRealNonNegative(this Complex complex)
        {
            return complex.Imaginary.IsZero() && complex.Real >= 0;
        }

        /// <summary>
        /// Returns the <see cref="Complex"/> conjugate of the number.
        /// </summary>
        public static Complex Conjugate(this Complex complex)
        {
            return new Complex(complex.Real, -complex.Imaginary);
        }

        /// <summary>
        /// Gets the squared norm of the <see cref="Complex"/> number.
        /// </summary>
        public static double NormSquared(this Complex complex)
        {
            return (complex.Real * complex.Real) + (complex.Imaginary * complex.Imaginary);
        }


        /// <summary>
        /// Returns the (natural) exponent of the given <see cref="Complex"/> number.
        /// </summary>
        public static Complex Exp(this Complex complex)
        {
            var exp = System.Math.Exp(complex.Real);
            return complex.IsReal() ? new Complex(exp, 0.0) : new Complex(exp * System.Math.Cos(complex.Imaginary), exp * System.Math.Sin(complex.Imaginary));
        }

        /// <summary>
        /// Returns the (natural) logarithm of the given <see cref="Complex"/> number.
        /// </summary>
        public static Complex Log(this Complex complex)
        {
            return complex.IsRealNonNegative() ? new Complex(System.Math.Log(complex.Real), 0.0) : new Complex(0.5 * System.Math.Log(complex.NormSquared()), complex.Phase);
        }

        /// <summary>
        /// Returns the given <see cref="Complex"/> number to a power.
        /// </summary>
        public static Complex Pow(this Complex complex, Complex exponent)
        {
            if (complex.IsZero())
            {
                if (exponent.IsZero()) return Complex.One;
                if (exponent.Real > 0.0) return Complex.Zero;
                if (exponent.Real < 0)
                    return exponent.Imaginary.IsZero()
                            ? new Complex(double.PositiveInfinity, 0.0)
                            : new Complex(double.PositiveInfinity, double.PositiveInfinity);

                return double.NaN;
            }

            return (exponent * complex.Log()).Exp();
        }

        /// <summary>
        /// Returns the given <see cref="Complex"/> number to root.
        /// </summary>
        public static Complex Root(this Complex complex, Complex rootExponent)
        {
            return Pow(complex, 1 / rootExponent);
        }

        /// <summary>
        /// The square of the given <see cref="Complex"/> number.
        /// </summary>
        public static Complex Squared(this Complex complex)
        {
            return complex.IsReal() ? new Complex(complex.Real * complex.Real, 0.0) : new Complex((complex.Real * complex.Real) - (complex.Imaginary * complex.Imaginary), 2 * complex.Real * complex.Imaginary);
        }

        /// <summary>
        /// The square root of the given <see cref="Complex"/> number.
        /// </summary>
        public static Complex SquareRoot(this Complex complex)
        {
            if (complex.IsRealNonNegative()) return new Complex(System.Math.Sqrt(complex.Real), 0.0);
            Complex result;
            var absReal = System.Math.Abs(complex.Real);
            var absImag = System.Math.Abs(complex.Imaginary);
            double w;
            if (absReal >= absImag)
            {
                var ratio = complex.Imaginary / complex.Real;
                w = System.Math.Sqrt(absReal) * System.Math.Sqrt(0.5 * (1.0 + System.Math.Sqrt(1.0 + (ratio * ratio))));
            }
            else
            {
                var ratio = complex.Real / complex.Imaginary;
                w = System.Math.Sqrt(absImag) * System.Math.Sqrt(0.5 * (System.Math.Abs(ratio) + System.Math.Sqrt(1.0 + (ratio * ratio))));
            }

            if (complex.Real >= 0.0) result = new Complex(w, complex.Imaginary / (2.0 * w));
            else if (complex.Imaginary >= 0.0) result = new Complex(absImag / (2.0 * w), w);
            else result = new Complex(absImag / (2.0 * w), -w);

            return result;
        }

        /// <summary>
        /// The norm of the given <see cref="Complex"/> number.
        /// </summary>
        public static double Norm(this Complex complex)
        {
            return System.Math.Sqrt((complex.Real * complex.Real) + (complex.Imaginary * complex.Imaginary));
        }

        /// <summary>
        /// The squared norm of the difference of the given <see cref="Complex"/> numbers.
        /// </summary>
        public static double NormOfDifference(this Complex u, Complex v)
        {
            return (u - v).NormSquared();
        }

        /// <summary>
        /// Parses the complex number in form of 'a + ib'. If the parsing fails an exception will be thrown.
        /// </summary>
        /// <param name="s">The complex number to parse.</param>
        /// <returns>The parsed complex number.</returns>
        public static Complex ToComplex(this string s)
        {
            var regex = new Regex(ComplexNumberPattern);
            var match = regex.Match(s);
            var singularplus = new[] { "i", "+i", "j", "+j" };
            var singularminus = new[] { "-i", "-j" };
            if (match.Success && match.Groups.Count == 14)
            {
                var real = 0d;
                var img = 0d;
                if (singularplus.Any(c => match.Groups[0].Value == c)) return new Complex(0, 1);
                if (singularminus.Any(c => match.Groups[0].Value == c)) return new Complex(0, -1);
                if (match.Groups[5].Value == "i" || match.Groups[5].Value == "j")
                {
                    return new Complex(0, double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    real = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                    // could be purely real
                    if (!string.IsNullOrEmpty(match.Groups[8].Value) || !string.IsNullOrEmpty(match.Groups[12].Value))
                        img = double.Parse(string.IsNullOrEmpty(match.Groups[8].Value) ? match.Groups[12].Value : match.Groups[8].Value, CultureInfo.InvariantCulture);

                }
                return new Complex(real, img);
            }
            throw new ArgumentException(string.Format("The string '{0}' could not be converted to a complex number.", s));

        }
    }
}