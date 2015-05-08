using System;

namespace Orbifold.Numerics
{
    /// <summary>
	/// Gaussian or Normal probability distribution.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>See <see cref="http://en.wikipedia.org/wiki/Normal_distribution">
	/// Wikipedia - Normal distribution</see>.</description></item>
	/// <item>
	/// <description>The Box-Muller transformation: http://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform .</description></item>
	/// <item>
	/// <description>See the <i>Box-Muller</i> algorithm for generating random deviates
	/// with a normal distribution: <see cref="http://www.library.cornell.edu/nr/">
	/// Numerical recipes in C</see> (chapter 7)</description></item></list>
	/// </remarks>
	public sealed class GaussianDistribution : ContinuousDistribution
	{
		private double mean;
		private double stdDev;

		/// <summary>
		/// Initializes a new instance of the <see cref="GaussianDistribution"/> class.
		/// </summary>
		public GaussianDistribution()
		{
		}

		public GaussianDistribution(double mean, double standardDeviation)
		{
			SetParameters(mean, standardDeviation);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GaussianDistribution"/> class.
		/// </summary>
		/// <param name="random">The random.</param>
		public GaussianDistribution(IStochastic random)
			: base(random)
		{
		}

		/// <summary>
		/// Gets the maximum possible value of the distribution.
		/// </summary>
		public override double Maximum {
			get {
				return double.MaxValue;
			}
		}

		/// <summary>
		/// Gets the mean value of the distribution.
		/// </summary>
		public override double Mean {
			get {
				return mean;
			}

			set {
				SetParameters(value, stdDev);
			}
		}

		/// <summary>
		/// Gets the median of this Gaussian distribution.
		/// </summary>
		public override double Median {
			get {
				return this.mean;
			}
		}

		/// <summary>
		/// Gets the minimum possible value of this distribution.
		/// </summary>
		public override double Minimum {
			get {
				return double.MinValue;
			}
		}

		/// <summary>
		/// Gets the skewness of this distribution.
		/// </summary>
		public override double Skewness {
			get {
				return 0.0;
			}
		}

		/// <summary>
		/// Gets the variance of distribution.
		/// </summary>
		public override double Variance {
			get {
				return this.stdDev * this.stdDev;
			}
		}

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		/// <param name="x">The upper value of the integral.</param>
		/// <returns></returns>
		public override double CumulativeDistribution(double x)
		{
			// pretty much the definition of the Erf function
			return 0.5 * (1.0 - Functions.Erf((this.mean - x) / (this.stdDev * Constants.Sqrt2)));
		}


		/// <summary>
		/// Inverses the cumulative distribution.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <returns></returns>
		public double InverseCumulativeDistribution(double x)
		{
			return Constants.Sqrt1Over2 * Functions.ErfC(2.0 * x - 1.0);
		}

		/// <summary>
		/// Returns a distributed floating point random number.
		/// </summary>
		/// <returns>
		/// A distributed double-precision floating point number.
		/// </returns>
		public override double NextDouble()
		{
			return mean + (stdDev * SampleBoxMuller(StochasticSource).Item1);
		}

		/// <summary>
		/// Checks whether the parameters of the distribution are valid. 
		/// </summary>
		/// <param name="mean">The mean of the normal distribution.</param>
		/// <param name="stddev">The standard deviation of the normal distribution.</param>
		/// <returns><c>true</c> when the parameters are valid, <c>false</c> otherwise.</returns>
		private static bool IsValidParameterSet(double mean, double stddev)
		{
			if(stddev < 0.0 || Double.IsNaN(mean) || Double.IsNaN(stddev)) {
				return false;
			}

			return true;
		}

		private void SetParameters(double mean, double stddev)
		{
			if(!IsValidParameterSet(mean, stddev))
				throw new ArgumentOutOfRangeException(Resource.InvalidDistributionParameters);

			this.mean = mean;
			this.stdDev = stddev;
		}

		/// <summary>
		/// Samples a pair of standard normal distributed random variables using the <i>Box-Muller</i> algorithm.
		/// </summary>
		/// <param name="rnd">The random number generator to use.</param>
		/// <returns>a pair of random numbers from the standard normal distribution.</returns>
		internal static Tuple<double, double> SampleBoxMuller(IStochastic rnd)
		{
			var v1 = (2.0 * rnd.NextDouble()) - 1.0;
			var v2 = (2.0 * rnd.NextDouble()) - 1.0;
			var r = (v1 * v1) + (v2 * v2);
			while(r >= 1.0 || r == 0.0) {
				v1 = (2.0 * rnd.NextDouble()) - 1.0;
				v2 = (2.0 * rnd.NextDouble()) - 1.0;
				r = (v1 * v1) + (v2 * v2);
			}

			var fac = System.Math.Sqrt(-2.0 * System.Math.Log(r) / r);
			return new Tuple<double, double>(v1 * fac, v2 * fac);
		}

		/// <summary>
		/// Continuous probability density function (pdf) of this probability distribution.
		/// </summary>
		public override double ProbabilityDensity(double x)
		{
			return Constants.InvSqrt2Pi * System.Math.Exp(x * x / -2.0);
		}
	}
}