using System;
using System.Linq;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	public delegate double ModelFunction(double x);
	public static class InterpolationExtensions
	{
        private static double[] ExtractRange(double?[] y)
        {
            List<double> range = new List<double>();

            for (int i = 0; i < y.Length; i++)
            {
                if (y[i].HasValue)
                {
                    range.Add(i + 1);
                }
            }

            return range.ToArray();
        }

        private static double[] ExtractValues(double?[] y)
        {
            List<double> values = new List<double>();
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i].HasValue)
                {
                    values.Add(y[i].Value);
                }
            }
            return values.ToArray();
        }

        private static double[] FillLinearInternal(double[] range, double[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
        {
            if (y.Length == 0) throw new Exception("No values specified.");
            if (howMany <= 0) throw new ArgumentOutOfRangeException("howMany", "The amount of values to extrapolate should be greater than zero.");
            if (y.Length == 1) return Range.Create(1, howMany).Map(x => y[0]).ToArray();

            // fetch the best fit
            var interpol = LeastSquaresBestFitLine(range, y);

            // shouldn't happen really
            if (interpol == null || interpol.Length != 3) throw new Exception("The linear interpolation failed.");
            // the actual fit
            Func<double, double> f = i => interpol[0] + interpol[1] * i;

            // make the folks happy
            return fromStart ? Range.Create(1, stepValue, howMany).Map(x => f(x)).ToArray() : Range.Create(y.Length + 1, stepValue, howMany).Map(x => f(x)).ToArray();
        }

        public static double[] FillLinear(double?[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
        {
            double[] range = ExtractRange(y);
            double[] values = ExtractValues(y);

            return FillLinearInternal(range, values, howMany, stepValue, fromStart);
        }

        /// <summary>
        /// Fills up an array of linear extrapolations on the basis of the given values.
        /// </summary>
        /// <param name="y">The array of values to interpolate.</param>
        /// <param name="howMany">How many items to generate.</param>
        /// <param name="stepValue">The step value. Considering the <paramref name="y"/>-values as function values of an array of real values, the <paramref name="stepValue"/> represents the step between these (functional domain) values.</param>
        /// <param name="fromStart">if set to <c>true</c> the resulting array will not continue (extrapolate) the given <paramref name="y"/>-array but rather return the actual values of the calculated interpolation. </param>
        /// <remarks>
        /// Use the <see cref="LeastSquaresBestFitLine" /> interpolation to get a linear function from which arbitrary functional values can be calculated.
        /// </remarks>
        public static double[] FillLinear(double[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
		{
			if (y == null) throw new ArgumentNullException("y");

			// assuming equal spaces integer x's
			var range = Range.Create(1d, stepValue, y.Length).ToArray();

            return FillLinearInternal(range, y, howMany, stepValue, fromStart);
		}

        public static double[] FillExponential(double?[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
        {
            if (y == null) throw new ArgumentNullException("y");

            var range = ExtractRange(y);
            var values = ExtractValues(y);

            return FillExponentialInternal(range, values, howMany, stepValue, fromStart);
        }

        private static double[] FillExponentialInternal(double[] range, double[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
        {
            if (y.Length == 0) throw new Exception("No values specified.");
            if (howMany <= 0) throw new ArgumentOutOfRangeException("howMany", "The amount of values to extrapolate should be greater than zero.");
            var sign = System.Math.Sign(y[0]);
            if (y.Any(x => System.Math.Sign(x) != sign)) throw new ArgumentException("y", "In order to have an exponential interpolation the values should all have the same sign.");
            if (y.Length == 1) return Range.Create(1, howMany).Map(x => y[0]).ToArray();

            // take the abs in case the values are negative
            var lny = y.Map(d => System.Math.Log(System.Math.Abs(d))).ToArray();

            // fetch the best fit
            var interpol = LeastSquaresBestFitLine(range, lny);

            // shouldn't happen really
            if (interpol == null || interpol.Length != 3) throw new Exception("The linear interpolation failed.");

            // the actual fit
            Func<double, double> f = i => sign * System.Math.Exp(interpol[0] + interpol[1] * i);

            return fromStart ? Range.Create(1, stepValue, howMany).Map(x => f(x)).ToArray() : Range.Create(y.Length + 1, stepValue, howMany).Map(x => f(x)).ToArray();
        }

		/// <summary>
		/// Fills up an array of exponential extrapolations on the basis of the given values.
		/// </summary>
		/// <param name="fromStart">if set to <c>true</c> the resulting array will not continue (extrapolate) the given <paramref name="y"/>-array but rather return the actual values of the calculated interpolation. </param>
		/// <param name="y">The values to inter-expolate.</param>
		/// <param name="howMany">How many items to generate.</param>
		/// <param name="stepValue">The step value. Considering the <paramref name="y"/>-values as function values of an array of real values, the <paramref name="stepValue"/> represents the step between these (functional domain) values.</param>
		public static double[] FillExponential(double[] y, int howMany = 10, double stepValue = 1d, bool fromStart = false)
		{
			if (y == null) throw new ArgumentNullException("y");

			// assuming equal spaces integer x's
			var range = Range.Create(1d, stepValue, y.Length).ToArray();

            return FillExponentialInternal(range, y, howMany, stepValue, fromStart);
		}

		public static double[] LeastSquaresBestFitExponential(double[] x, double[] y)
		{
			var lny = y.Map(System.Math.Log).ToArray();
			var interpol = LeastSquaresBestFitLine(x, lny);
			return new[] { System.Math.Exp(interpol[0]), interpol[1], interpol[2] };
		}

		public static double[] LeastSquaresBestFitLine(double[] x, double[] y)
		{
			if (x.Length != y.Length) throw new ArgumentException("x,y", "The length of the arrays should be equal.");
			//Calculates equation of best-fit line using short cuts
			var n = x.Length;
			var xMean = 0.0;
			var yMean = 0.0;
			var numeratorSum = 0.0;
			var denominatorSum = 0.0;
			var sumOfResidualsSquared = 0.0;

			//Calculates the mean values for x and y arrays
			for (var i = 0; i < n; i++)
			{
				xMean += x[i] / n;
				yMean += y[i] / n;
			}

			//Calculates the numerator and denominator for best-fit slope
			for (var i = 0; i < n; i++)
			{
				numeratorSum += y[i] * (x[i] - xMean);
				denominatorSum += x[i] * (x[i] - xMean);
			}

			//Calculate the best-fit slope and y-intercept
			var bestfitSlope = numeratorSum / denominatorSum;
			var bestfitYintercept = yMean - xMean * bestfitSlope;

			//Calculate the best-fit standard deviation
			for (var i = 0; i < n; i++) sumOfResidualsSquared += (y[i] - bestfitYintercept - bestfitSlope * x[i]) * (y[i] - bestfitYintercept - bestfitSlope * x[i]);
			var sigma = System.Math.Sqrt(sumOfResidualsSquared / (n - 2));

			return new[] { bestfitYintercept, bestfitSlope, sigma };
		}


		public static double[] LeastSquaresWeightedBestFitLine(double[] x, double[] y, double[] w)
		{
			//Calculates equation of best-fit line using short cuts
			var n = x.Length;
			var wxMean = 0.0;
			var wyMean = 0.0;
			var wSum = 0.0;
			var wnumeratorSum = 0.0;
			var wdenominatorSum = 0.0;
			var sumOfResidualsSquared = 0.0;

			//Calculates the sum of the weights w[i]
			for (var i = 0; i < n; i++) wSum += w[i];

			//Calculates the mean values for x and y arrays
			for (var i = 0; i < n; i++)
			{
				wxMean += w[i] * x[i] / wSum;
				wyMean += w[i] * y[i] / wSum;
			}

			//Calculates the numerator and denominator for best-fit slope
			for (var i = 0; i < n; i++)
			{
				wnumeratorSum += w[i] * y[i] * (x[i] - wxMean);
				wdenominatorSum += w[i] * x[i] * (x[i] - wxMean);
			}

			//Calculate the best-fit slope and y-intercept
			var bestfitSlope = wnumeratorSum / wdenominatorSum;
			var bestfitYintercept = wyMean - wxMean * bestfitSlope;

			//Calculate the best-fit standard deviation
			for (var i = 0; i < n; i++) sumOfResidualsSquared += w[i] * (y[i] - bestfitYintercept - bestfitSlope * x[i]) * (y[i] - bestfitYintercept - bestfitSlope * x[i]);
			var sigma = System.Math.Sqrt(sumOfResidualsSquared / (n - 2));

			return new[] { bestfitYintercept, bestfitSlope, sigma };
		}

		public static Vector LinearRegression(double[] x, double[] y, ModelFunction[] f, out double sigma)
		{
			//m = number of data points
			var m = f.Length;
			var fmatrix = new RMatrix(m, m);
			var bvector = new Vector(m);
			// n = number of linear terms in the regression equation
			var n = x.Length;

			//Calculate the B vector entries
			for (var k = 0; k < m; k++)
			{
				bvector[k] = 0.0;
				for (var i = 0; i < n; i++) bvector[k] += f[k](x[i]) * y[i];
			}

			//Calculate the F matrix entries
			for (var j = 0; j < m; j++)
			{
				for (var k = 0; k < m; k++)
				{
					fmatrix[j, k] = 0.0;
					for (var i = 0; i < n; i++) fmatrix[j, k] += f[j](x[i]) * f[k](x[i]);
				}
			}

			// FA = B so A = F^{-1}B
			var avector = AlgebraExtensions.GaussJordan(fmatrix, bvector);

			// Calculate the standard deviation to estimate error
			var sumOfResidualsSquared = 0.0;
			for (var i = 0; i < n; i++)
			{
				var sum = 0.0;
				for (var j = 0; j < m; j++) sum += avector[j] * f[j](x[i]);
				sumOfResidualsSquared += (y[i] - sum) * (y[i] - sum);
			}
			sigma = System.Math.Sqrt(sumOfResidualsSquared / (n - m));
			return avector;
		}

		public static Vector PolynomialFit(double[] x, double[] y, int m, out double sigma)
		{
			//m = number of data points which in this case
			//for polynomials = order or degree of polynomial P_m(x)
			m++; //minor adjust
			var fmatrix = new RMatrix(m, m);
			var bvector = new Vector(m);
			// n = number of linear terms in the regression equation
			var n = x.Length;

			//Calculate the B vector entries
			for (var k = 0; k < m; k++)
			{
				bvector[k] = 0.0;
				for (var i = 0; i < n; i++) bvector[k] += System.Math.Pow(x[i], k) * y[i];
			}

			//Calculate the F matrix entries
			for (var j = 0; j < m; j++)
			{
				for (var k = 0; k < m; k++)
				{
					fmatrix[j, k] = 0.0;
					for (var i = 0; i < n; i++) fmatrix[j, k] += System.Math.Pow(x[i], j + k);
				}
			}

			// FA = B so A = F^{-1}B
			var avector = AlgebraExtensions.GaussJordan(fmatrix, bvector);

			// Calculate the standard deviation to estimate error
			var sumOfResidualsSquared = 0.0;
			for (var i = 0; i < n; i++)
			{
				var sum = 0.0;
				for (var j = 0; j < m; j++) sum += avector[j] * System.Math.Pow(x[i], j);
				sumOfResidualsSquared += (y[i] - sum) * (y[i] - sum);
			}
			sigma = System.Math.Sqrt(sumOfResidualsSquared / (n - m));
			return avector;
		}
	}
}