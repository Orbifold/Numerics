using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Provides generation of chi distributed random numbers.
	/// </summary>
	/// <remarks>
	/// The implementation of the <see cref="ChiDistribution"/> type bases upon information presented on
	///   <a href="http://en.wikipedia.org/wiki/Chi_distribution">Wikipedia - Chi distribution</a>.
	/// </remarks>
	public sealed class ChiDistribution : ContinuousDistribution
	{
		private readonly GaussianDistribution gaussian;

		private int degreesOfFreedom;

		private double lngammaDegreesOfFreedomHalf;

		private double? mean;

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public ChiDistribution()
		{
			this.gaussian = new GaussianDistribution(this.StochasticSource);
			this.SetDistributionParameters(1);
		}

		/// <summary>
		/// Initializes a new instance, using the specified <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		/// <param name="random">A <see cref="DefaultSource"/> object.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="random"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public ChiDistribution(IStochastic random)
			: base(random)
		{
			this.gaussian = new GaussianDistribution(random);
			this.SetDistributionParameters(1);
		}

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public ChiDistribution(int degreesOfFreedom)
		{
			this.gaussian = new GaussianDistribution(this.StochasticSource);
			this.SetDistributionParameters(degreesOfFreedom);
		}

		/// <summary>
		/// Gets or sets the degrees of freedom (the number of standard distributed random variables) parameter.
		/// </summary>
		public int DegreesOfFreedom
		{
			get
			{
				return this.degreesOfFreedom;
			}
			set
			{
				this.SetDistributionParameters(value);
			}
		}

		/// <summary>
		/// Gets the maximum possible value of generated random numbers.
		/// </summary>
		public override double Maximum
		{
			get
			{
				return double.MaxValue;
			}
		}

		/// <summary>
		/// Gets the mean value of generated random numbers.
		/// </summary>
		public override double Mean
		{
			get
			{
				return this.mean
				       ??
				       (double)
				       (this.mean =
				        Constants.Sqrt2
				        * System.Math.Exp(Functions.GammaLn(0.5 * (this.degreesOfFreedom + 1)) - this.lngammaDegreesOfFreedomHalf));
			}
			set
			{
				this.Mean = value;
			}
		}

        /// <summary>
        /// Returns the median of the distribution.
        /// </summary>
		public override double Median
		{
			get
			{
			    return Math.Sqrt(2 * Functions.InverseGammaRegularized(this.degreesOfFreedom / 2d, .5));
			}
		}

		/// <summary>
		/// Gets the minimum possible value of generated random numbers.
		/// </summary>
		public override double Minimum
		{
			get
			{
				return 0.0;
			}
		}

		/// <summary>
		/// Gets the skewness of generated random numbers.
		/// </summary>
		public override double Skewness
		{
			get
			{
				var variance = this.Variance;
				return this.Mean / System.Math.Pow(variance, 1.5) * (1.0 - 2.0 * variance);
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="DefaultSource"/> object that can be used
		/// as underlying random number generator.
		/// </summary>
		public override IStochastic StochasticSource
		{
			get
			{
				return base.StochasticSource;
			}

			set
			{
				base.StochasticSource = value;
				this.gaussian.StochasticSource = value;
			}
		}

		/// <summary>
		/// Gets the variance of generated random numbers.
		/// </summary>
		public override double Variance
		{
			get
			{
				return this.degreesOfFreedom - Functions.Squared(this.Mean);
			}
		}

		/// <summary>
		/// Determines whether the specified parameters is valid.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if degreesOfFreedom is greater than 0.0; otherwise, <see langword="false"/>.
		/// </returns>
		public static bool IsValidParameterSet(int degreesOfFreedom)
		{
			return degreesOfFreedom > 0.0;
		}

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		public override double CumulativeDistribution(double x)
		{
			return Functions.GammaRegularized(0.5 * this.degreesOfFreedom, 0.5 * x * x);
		}

		/// <summary>
		/// Returns a chi distributed floating point random number.
		/// </summary>
		/// <returns>A chi distributed double-precision floating point number.</returns>
		public override double NextDouble()
		{
			var sum = 0.0;
			for (var i = 0; i < this.degreesOfFreedom; i++)
			{
				var std = this.gaussian.NextDouble();
				sum += std * std;
			}

			return System.Math.Sqrt(sum);
		}

		/// <summary>
		/// Continuous probability density function (pdf) of this probability distribution.
		/// </summary>
		public override double ProbabilityDensity(double x)
		{
			return
				System.Math.Exp(
					(1.0 - 0.5 * this.degreesOfFreedom) * Constants.Ln2 + (this.degreesOfFreedom - 1) * System.Math.Log(x)
					- (0.5 * x * x) - this.lngammaDegreesOfFreedomHalf);
		}

		/// <summary>
		/// Configure all distribution parameters.
		/// </summary>
		public void SetDistributionParameters(int degreesOfFreedom)
		{
			if (!IsValidParameterSet(degreesOfFreedom))
			{
				throw new ArgumentException("degreesOfFreedom");
			}

			this.degreesOfFreedom = degreesOfFreedom;
			this.lngammaDegreesOfFreedomHalf = Functions.GammaLn(0.5 * degreesOfFreedom);
			this.mean = null;
		}
	}
}