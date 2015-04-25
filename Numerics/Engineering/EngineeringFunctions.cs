using System;
using System.Numerics;

namespace Orbifold.Numerics
{
    /// <summary>
    /// This class contains all the Excel engineering functions.
    /// </summary>
    public sealed class EngineeringFunctions
    {
        /// <summary>
        /// Returns the modified Bessel function, which is equivalent to the Bessel function evaluated for purely imaginary arguments.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELI(double x, double n)
        {
            // note tha Excel truncates the order
            // note also that Mathematica switches the argumens and the order is the first parameter there
            return Functions.BesselI(x, n.Truncate());
        }

        /// <summary>
        /// Returns the Bessel function.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELJ(double x, double n)
        {
            return Functions.BesselJ(x, n.Truncate());
        }

        /// <summary>
        /// Returns the modified Bessel function, which is equivalent to the Bessel functions evaluated for purely imaginary arguments.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELK(double x, double n)
        {
            return Functions.BesselK(x, n.Truncate());
        }

        /// <summary>
        /// Returns the Bessel function, which is also called the Weber function or the Neumann function.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELY(double x, double n)
        {
            return Functions.BesselY(x, n.Truncate());
        }

        /// <summary>
        /// Converts a binary number to decimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). 
        /// The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static long BIN2DEC(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return 0L;

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return Convert.ToInt64(number, 2) - 512;
            }
            return Convert.ToInt64(number, 2);
        }

        /// <summary>
        /// Converts a binary number to hexadecimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places"> The number of characters to use. If places is omitted, BIN2HEX uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string BIN2HEX(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            return OCT2HEX(BIN2OCT(number), places);
        }

        /// <summary>
        /// Converts a binary number to hexadecimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string BIN2HEX(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            return OCT2HEX(BIN2OCT(number));
        }

        /// <summary>
        /// Converts a binary number to octal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, BIN2OCT uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string BIN2OCT(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2OCT(Convert.ToInt64(number, 2) - 512, places);
            }
            return DEC2OCT(Convert.ToInt64(number, 2), places);
        }

        /// <summary>
        /// Converts a binary number to octal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string BIN2OCT(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2OCT(Convert.ToInt64(number, 2) - 512);
            }
            return DEC2OCT(Convert.ToInt64(number, 2));
        }

        /// <summary>
        /// Returns a bitwise 'AND' of two numbers.
        /// </summary>
        /// <param name="number1">Must be in decimal form and greater than or equal to 0.</param>
        /// <param name="number2">Must be in decimal form and greater than or equal to 0.</param>
        /// <returns></returns>
        public static long BITAND(long number1, long number2)
        {
            return number1 & number2;
        }

        /// <summary>
        /// Returns a number shifted left by the specified number of bits.
        /// </summary>
        /// <param name="number">Must be an integer greater than or equal to 0.</param>
        /// <param name="amount">Must be an integer.</param>
        public static long BITLSHIFT(long number, int amount)
        {
            return amount < 0 ? BITRSHIFT(number, -amount) : number << amount;
        }

        /// <summary>
        /// Returns a bitwise 'OR' of two numbers.
        /// </summary>
        /// <param name="number1"> Must be in decimal form and greater than or equal to 0.</param>
        /// <param name="number2"> Must be in decimal form and greater than or equal to 0.</param>
        public static long BITOR(long number1, long number2)
        {
            return number1 | number2;
        }

        /// <summary>
        /// Returns a number shifted right by the specified number of bits.
        /// </summary>
        /// <param name="number">Must be an integer greater than or equal to 0.</param>
        /// <param name="amount">Must be an integer.</param>
        public static long BITRSHIFT(long number, int amount)
        {
            return amount < 0 ? BITLSHIFT(number, -amount) : number >> amount;
        }

        /// <summary>
        /// Returns a bitwise 'XOR' of two numbers.
        /// </summary>
        /// <param name="number1">Must be greater than or equal to 0.</param>
        /// <param name="number2">Must be greater than or equal to 0.</param>
        public static long BITXOR(long number1, long number2)
        {
            return number1 ^ number2;
        }

        /// <summary>
        /// Converts real and imaginary coefficients into a complex number of the form x + yi or x + yj.
        /// </summary>
        /// <param name="a">The real coefficient of the complex number.</param>
        /// <param name="b">The imaginary coefficient of the complex number.</param>
        /// <param name="complexSymbol"> The suffix for the imaginary component of the complex number. If omitted, suffix is assumed to be "i".</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Only 'i' or 'j' is allowed for the imaginary number.</exception>
        public static string COMPLEX(double a, double b, string complexSymbol = "i")
        {
            if (complexSymbol != "i" || complexSymbol != "j") throw new Exception("Only 'i' or 'j' is allowed for the imaginary number.");
            return string.Format("{0}+{1}{2}", a, b, complexSymbol);
        }

        /// <summary>
        /// Converts a number from one measurement system to another. For example, CONVERT can translate a table of distances in miles to a table of distances in kilometers.
        /// </summary>
        /// <param name="number">The value in from_units to convert.</param>
        /// <param name="from_unit"> The units for number.</param>
        /// <param name="to_unit">The units for the result. .</param>
        /// <seealso cref="MultiplierPrefix"/>
        public static double CONVERT(double number, string from_unit, string to_unit)
        {
            return Converter.Convert(number, from_unit, to_unit);
        }

        /// <summary>
        /// Converts a decimal number to binary.
        /// </summary>
        /// <param name="number"> The decimal integer you want to convert. If number is negative, valid place values are ignored and DEC2BIN returns a 10-character (10-bit) 
        /// binary number in which the most significant bit is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <exception cref="System.Exception">
        /// If the number should not be less than -512 and not bigger than 511 or if the number of characters to use cannot be less than one or if the number of characters to use cannot be more than ten.
        /// </exception>
        public static string DEC2BIN(double number)
        {
            if (number < -512 || number > 511) throw new Exception("The inum should not be less than -512 and not bigger than 511.");
            var inum = number.Truncate();
            if (inum == 0) return "0";
            if (inum < 0) return DEC2BIN(inum, 10);
            return Convert.ToString(inum, 2);
        }

        /// <summary>
        /// Converts a decimal number to binary.
        /// </summary>
        /// <param name="number"> The decimal integer you want to convert. If number is negative, valid place values are ignored and DEC2BIN returns a 10-character (10-bit) 
        /// binary number in which the most significant bit is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, DEC2BIN uses the minimum number of characters necessary. 
        /// Places is useful for padding the return value with leading 0s (zeros).</param>
        /// <exception cref="System.Exception">
        /// If the number should not be less than -512 and not bigger than 511 or if the number of characters to use cannot be less than one or if the number of characters to use cannot be more than ten.
        /// </exception>
        public static string DEC2BIN(double number, int places)
        {
            // se the Excel documentation for details on the restrictions
            if (number < -512 || number > 511) throw new Exception("The number should not be less than -512 and not bigger than 511.");
            if (places <= 0) throw new Exception("The number of characters to use cannot be less than one.");
            if (places > 10) throw new Exception("The number of characters to use cannot be more than ten.");
            var inum = number.Truncate();

            if (inum < 0) inum = 0xFFFFFFFF + inum + 1;
            if (inum == 0) return "0".PadLeft(places, '0');

            var s = Convert.ToString(inum, 2).PadLeft(places, '0');
            if (s.Length > places) s = s.Substring(s.Length - 10);
            return s;
        }

        /// <summary>
        /// Converts a decimal number to hexadecimal.
        /// </summary>
        /// <param name="number">The decimal integer you want to convert. If number is negative, places is ignored and DEC2HEX returns a 10-character (40-bit) hexadecimal number in which the most significant bit is the sign bit. The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, DEC2BIN uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros)</param>
        public static string DEC2HEX(double number, int places)
        {
            return OCT2HEX(DEC2OCT(number), places);
        }

        /// <summary>
        /// Converts a decimal number to hexadecimal.
        /// </summary>
        /// <param name="number">The decimal integer you want to convert. If number is negative, places is ignored and DEC2HEX returns a 10-character (40-bit) hexadecimal number in which the most significant bit is the sign bit. The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string DEC2HEX(double number)
        {
            return OCT2HEX(DEC2OCT(number));
        }

        /// <summary>
        /// Converts a decimal number to hexadecimal.
        /// </summary>
        /// <param name="number">The decimal integer you want to convert. If number is negative, places is ignored and DEC2HEX returns a 10-character (40-bit) hexadecimal number in which the most significant bit is the sign bit. 
        /// The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, DEC2HEX uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string DEC2OCT(double number, int places)
        {
            if (number < -549755813888 || number > 549755813887) throw new Exception("The number should not be less than -549755813888 and not bigger than 549755813887.");
            if (places <= 0) throw new Exception("The number of characters to use cannot be less than one.");
            if (places > 10) throw new Exception("The number of characters to use cannot be more than ten.");
            var inum = number.Truncate();
            if (inum < 0) inum = 0xFFFFFFFF + inum + 1;
            if (inum == 0) return "0".PadLeft(places, '0');

            var s = Convert.ToString(inum, 8).PadLeft(places, '0');
            if (s.Length > places) s = s.Substring(s.Length - 10);
            return s;
        }

        /// <summary>
        /// Converts a decimal number to hexadecimal.
        /// </summary>
        /// <param name="number">The decimal integer you want to convert. If number is negative, places is ignored and DEC2HEX returns a 10-character (40-bit) hexadecimal number in which the most significant bit is the sign bit. 
        /// The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, DEC2HEX uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string DEC2OCT(double number)
        {
            if (number < -549755813888 || number > 549755813887) throw new Exception("The number should not be less than -549755813888 and not bigger than 549755813887.");
            var inum = number.Truncate();
            if (inum < 0) inum = 0xFFFFFFFF + inum + 1;
            if (inum == 0) return "0";

            var s = Convert.ToString(inum, 8);
            if (s.Length > 10) s = s.Substring(s.Length - 10);
            return s;
        }

        /// <summary>
        /// Tests whether the value is equal to zero. Returns 1 if number = 0, returns 0 otherwise. 
        /// </summary>
        /// <param name="number">A number.</param>
        public static int DELTA(double number)
        {
            return EpsilonExtensions.IsZero(number) ? 1 : 0;
        }

        /// <summary>
        /// Tests whether two values are equal. Returns 1 if number1 = number2; returns 0 otherwise. 
        /// Use this function to filter a set of values. For example, by summing several DELTA functions you calculate 
        /// the count of equal pairs. This function is also known as the Kronecker Delta function.
        /// </summary>
        /// <param name="number1">A number.</param>
        /// <param name="number2">A number.</param>
        /// <returns></returns>
        public static int DELTA(double number1, double number2)
        {
            return DELTA(number1 - number2);
        }

        /// <summary>
        /// Returns the error function integrated between lower_limit and upper_limit.
        /// </summary>
        /// <param name="lower_limit">The lower limit of the integration.</param>
        /// <param name="upper_limit">The upper bound for integrating ERF. If omitted, ERF integrates between zero and lower_limit.</param>
        /// <returns></returns>
        public static double ERF(double lower_limit, double upper_limit)
        {
            // due to additive integrals
            return Functions.Erf(upper_limit) - Functions.Erf(lower_limit);
        }

        /// <summary>
        /// Returns the error function integrated between zero and the given upper_limit.
        /// </summary>
        /// <remarks>The ERF.Precise function of Excel returns a more accurate value but this difference has to be implemented in consumers of this library. 
        /// This function will return the most precise value possible and is, hence, the ERF.PRECISE engineering function. To get the ERF function just truncate the returned value.</remarks>
        /// <param name="upper_limit">The upper_limit.</param>
        /// <returns></returns>
        public static double ERF(double upper_limit)
        {
            return Functions.Erf(upper_limit);
        }

        /// <summary>
        /// Returns 1 if number ≥ 0; returns 0 (zero) otherwise.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int GESTEP(double number)
        {
            return (number >= 0) ? 1 : 0;
        }

        /// <summary>
        /// Returns 1 if number ≥ step; returns 0 (zero) otherwise. This is sometimes called the Heaviside step function, the Kronecker delta being the derivative of this function within the context of distributions.
        /// </summary>
        /// <param name="number">A number.</param>
        /// <param name="step">A step or threshold from which on the result is one.</param>
        /// <returns></returns>
        public static int GESTEP(double number, double step)
        {
            return GESTEP(number - step);
        }

        /// <summary>
        /// Converts a hexadecimal number to binary.
        /// </summary>
        /// <param name="number"> The hexadecimal number you want to convert. Number cannot contain more than 10 characters. The most significant bit of number is the sign bit (40th bit from the right). The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, HEX2BIN uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        /// <returns></returns>
        public static string HEX2BIN(string number, int places)
        {
            return OCT2BIN(HEX2OCT(number), places);
        }

        /// <summary>
        /// Converts a hexadecimal number to binary.
        /// </summary>
        /// <param name="number"> The hexadecimal number you want to convert. Number cannot contain more than 10 characters. The most significant bit of number is the sign bit (40th bit from the right). The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <returns></returns>
        public static string HEX2BIN(string number)
        {
            return OCT2BIN(HEX2OCT(number));
        }

        /// <summary>
        /// Converts a hexadecimal number to decimal.
        /// </summary>
        /// <param name="number">The hexadecimal number you want to convert. Number cannot contain more than 10 characters (40 bits). The most significant bit of number is the sign bit. The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static long HEX2DEC(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return 0;
            if (number.Length == 10) return OCT2DEC(HEX2OCT(number));
            return Convert.ToInt64(number, 16);
        }

        /// <summary>
        /// Converts a hexadecimal number to octal.
        /// </summary>
        /// <param name="number">The hexadecimal number you want to convert. Number cannot contain more than 10 characters. 
        /// The most significant bit of number is the sign bit. The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, HEX2OCT uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        /// <returns></returns>
        public static string HEX2OCT(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0".PadLeft(places, '0');
            string s;
            //if (number.Length == 10)
            //{
            //    number = number.Substring(1);
            //    s = Convert.ToString(Convert.ToInt64(number, 16), 8);
            //}
            //else
            s = Convert.ToString(Convert.ToInt64(number, 16), 8);
            s = s.PadLeft(places, '0');
            if (s.Length > places) s = s.Substring(s.Length - 10);
            return s.ToUpper();
        }

        /// <summary>
        /// Converts a hexadecimal number to octal.
        /// </summary>
        /// <param name="number">The hexadecimal number you want to convert. Number cannot contain more than 10 characters. 
        /// The most significant bit of number is the sign bit. The remaining 39 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string HEX2OCT(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            string s;
            //if (number.Length == 10)
            //{
            //    number = number.Substring(1);
            //    s = Convert.ToString(Convert.ToInt64(number, 16), 8);
            //}
            //else
            s = Convert.ToString(Convert.ToInt64(number, 16), 8);
            if (s.Length > 10) s = s.Substring(s.Length - 10);
            return s.ToUpper();
        }

        /// <summary>
        /// Returns the absolute value (modulus) of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static double IMABS(string complexNumber)
        {
            return Complex.Abs(complexNumber.ToComplex());
        }

        /// <summary>
        /// Ieturns the imaginary coefficient of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number for which you want the imaginary coefficient.</param>
        /// <seealso cref="COMPLEX"/>
        /// <seealso cref="ComplexExtensions"/>
        public static double IMAGINARY(string complexNumber)
        {
            return complexNumber.ToComplex().Imaginary;
        }

        /// <summary>
        /// Returns the argument of the polar representation of the specified complex number.
        /// </summary>
        /// <remarks>The returned angle is in radians.</remarks>
        /// <param name="complexNumber">A complex number.</param>
        public static double IMARGUMENT(string complexNumber)
        {
            var c = complexNumber.ToComplex();
            return c.Imaginary < Constants.Epsilon ? 0d : System.Math.Atan2(c.Imaginary, c.Real);
        }

        /// <summary>
        /// Returns the complex conjugate of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCONJUGATE(string complexNumber)
        {
            var c = complexNumber.ToComplex();
            return Complex.Conjugate(c);
        }

        /// <summary>
        /// Returns the cosine of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCOS(string complexNumber)
        {
            return Complex.Cos(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the hyperbolic cosine of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCOSH(string complexNumber)
        {
            return Complex.Cosh(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the cotangent of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCOT(string complexNumber)
        {
            return Complex.Divide(1, IMTAN(complexNumber));
        }

        /// <summary>
        /// Returns the hyperbolic secant of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCSC(string complexNumber)
        {
            return Complex.Divide(1, IMSIN(complexNumber));
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMCSCH(string complexNumber)
        {
            return Complex.Divide(1, IMSINH(complexNumber));
        }

        /// <summary>
        /// Returns the quotient of two complex numbers.
        /// </summary>
        /// <param name="complexNumber1">A complex number.</param>
        /// <param name="complexNumber2">A complex number.</param>
        public static Complex IMDIV(string complexNumber1, string complexNumber2)
        {
            return Complex.Divide(complexNumber1.ToComplex(), complexNumber2.ToComplex());
        }

        /// <summary>
        /// Returns the exponential of a complex number.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMEXP(string complexNumber)
        {
            return Complex.Exp(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the natural logarithm of a complex number.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMLN(string complexNumber)
        {
            return Complex.Log(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the base-10 logarithm of a complex number.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMLOG10(string complexNumber)
        {
            return Complex.Log10(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the base-10 logarithm of a complex number.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMLOG2(string complexNumber)
        {
            return Complex.Divide(IMLN(complexNumber), Constants.Ln2);
        }

        /// <summary>
        /// Returns a complex number in x + yi or x + yj text format raised to a power.
        /// </summary>
        /// <param name="complexNumber">A complex number you want to raise to a power.</param>
        /// <param name="power">The power to which you want to raise the complex number.</param>
        /// <returns></returns>
        public static Complex IMPOWER(string complexNumber, int power)
        {
            return Complex.Pow(complexNumber.ToComplex(), power);
        }

        /// <summary>
        /// Returns the product of two complex numbers
        /// </summary>
        /// <param name="complexNumber1">A complex number.</param>
        /// <param name="complexNumber2">A complex number.</param>
        public static Complex IMPRODUCT(string complexNumber1, string complexNumber2)
        {
            return Complex.Multiply(complexNumber1.ToComplex(), complexNumber2.ToComplex());
        }

        /// <summary>
        /// Returns the real coefficient of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static double IMREAL(string complexNumber)
        {
            return complexNumber.ToComplex().Real;
        }

        /// <summary>
        /// Returns the secant of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMSEC(string complexNumber)
        {
            return Complex.Divide(1, IMCOS(complexNumber));
        }

        /// <summary>
        /// Returns the hyperbolic secant of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMSECH(string complexNumber)
        {
            return Complex.Divide(1, IMCOSH(complexNumber));
        }

        /// <summary>
        /// Returns the sine of a complex number in x + yi or x + yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMSIN(string complexNumber)
        {
            return Complex.Sin(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the hyperbolic sine of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMSINH(string complexNumber)
        {
            return Complex.Sinh(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the square root of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMSQRT(string complexNumber)
        {
            return Complex.Sqrt(complexNumber.ToComplex());
        }

        /// <summary>
        /// Returns the difference between two complex numbers
        /// </summary>
        /// <param name="complexNumber1">A complex number.</param>
        /// <param name="complexNumber2">A complex number.</param>
        public static Complex IMSUB(string complexNumber1, string complexNumber2)
        {
            return Complex.Subtract(complexNumber1.ToComplex(), complexNumber2.ToComplex());
        }

        /// <summary>
        /// Returns the sum of two complex numbers
        /// </summary>
        /// <param name="complexNumber1">A complex number.</param>
        /// <param name="complexNumber2">A complex number.</param>
        public static Complex IMSUM(string complexNumber1, string complexNumber2)
        {
            return Complex.Add(complexNumber1.ToComplex(), complexNumber2.ToComplex());
        }

        /// <summary>
        /// Returns the tangent of a complex number in x+yi or x+yj text format.
        /// </summary>
        /// <param name="complexNumber">A complex number.</param>
        public static Complex IMTAN(string complexNumber)
        {
            return Complex.Tan(complexNumber.ToComplex());
        }

        /// <summary>
        /// Converts an octal number to binary.
        /// </summary>
        /// <param name="number">The octal number you want to convert. Number may not contain more than 10 characters. The most significant bit of number is the sign bit. The remaining 29 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, OCT2BIN uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string OCT2BIN(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0".PadLeft(places, '0');

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2BIN(Convert.ToInt64(number, 8) - 134217728, places);
            }
            return DEC2BIN(Convert.ToInt64(number, 8), places);
        }

        /// <summary>
        /// Converts an octal number to binary.
        /// </summary>
        /// <param name="number">The octal number you want to convert. Number may not contain more than 10 characters. The most significant bit of number is the sign bit. The remaining 29 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string OCT2BIN(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2BIN(Convert.ToInt64(number, 8) - 134217728);
            }
            return DEC2BIN(Convert.ToInt64(number, 8));
        }

        /// <summary>
        /// Converts an octal number to decimal.
        /// </summary>
        /// <param name="number">TThe octal number you want to convert. Number may not contain more than 10 octal characters (30 bits). 
        /// The most significant bit of number is the sign bit. The remaining 29 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static long OCT2DEC(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return 0L;

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return Convert.ToInt64(number, 8) - 134217728;
            }
            return Convert.ToInt64(number, 8);
        }

        /// <summary>
        /// Converts an octal number to hexadecimal.
        /// </summary>
        /// <param name="number">The octal number you want to convert. Number may not contain more than 10 octal characters (30 bits). 
        /// The most significant bit of number is the sign bit. The remaining 29 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, OCT2HEX uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string OCT2HEX(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0".PadLeft(places, '0');
            string s;
            s = Convert.ToString(Convert.ToInt64(number, 8), 16);

            s = s.PadLeft(places, '0');
            if (s.Length > places) s = s.Substring(s.Length - 10);
            return s.ToUpper();
        }

        /// <summary>
        /// Converts an octal number to hexadecimal.
        /// </summary>
        /// <param name="number">The octal number you want to convert. Number may not contain more than 10 octal characters (30 bits). 
        /// The most significant bit of number is the sign bit. The remaining 29 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string OCT2HEX(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            string s;
            if (number.Length == 10)
            {
                number = number.Substring(1);
                s = Convert.ToString(Convert.ToInt64(number, 8) - 134217728, 16);
            }
            else s = Convert.ToString(Convert.ToInt64(number, 8), 16);
            if (s.Length > 10) s = s.Substring(s.Length - 10);
            return s.ToUpper();
        }
    }
}