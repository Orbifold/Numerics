
using System;

namespace Orbifold.Numerics
{
	public abstract class ContinuousDistribution
	/*:
	   IContinuousGenerator,
	   IContinuousProbabilityDistribution*/
	{
		IStochastic stochasticSourceSource;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContinuousDistribution"/> class.
		/// </summary>
		protected ContinuousDistribution()
			: this(new DefaultSource())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContinuousDistribution"/> class.
		/// </summary>
		/// <param name="stochasticSourceSource">The stochastic source source.</param>
		protected ContinuousDistribution(IStochastic stochasticSourceSource)
		{
			if (stochasticSourceSource == null) throw new ArgumentNullException("stochasticSourceSource");
			this.stochasticSourceSource = stochasticSourceSource;
		}

		/// <summary>
		/// Returns a distributed floating point random number.
		/// </summary>
		/// <returns>A distributed double-precision floating point number.</returns>
		public abstract double NextDouble();

		/// <summary>
		/// Continuous probability density function (pdf) of this probability distribution.
		/// </summary>
		public abstract double ProbabilityDensity(double x);

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		public abstract double CumulativeDistribution(double x);

		/// <summary>
		/// Resets the random number distribution, so that it produces the same random number sequence again.
		/// </summary>
		public void Reset()
		{
			this.stochasticSourceSource.Reset();
		}

		/// <summary>
		/// Lower limit of a random variable with this probability distribution.
		/// </summary>
		public abstract double Minimum { get; }

		/// <summary>
		/// Upper limit of a random variable with this probability distribution.
		/// </summary>
		public abstract double Maximum { get; }

		/// <summary>
		/// The expected value of a random variable with this probability distribution.
		/// </summary>
		public abstract double Mean { get; set; }

		/// <summary>
		/// The value separating the lower half part from the upper half part of a random variable with this probability distribution.
		/// </summary>
		public abstract double Median { get; }

		/// <summary>
		/// Average of the squared distances to the expected value of a random variable with this probability distribution.
		/// </summary>
		public abstract double Variance { get; }

		/// <summary>
		/// Measure of the asymmetry of this probability distribution.
		/// </summary>
		public abstract double Skewness { get; }

		/// <summary>
		/// Gets or sets a <see cref="StochasticSource"/> object that can be used
		/// as underlying random number generator.
		/// </summary>
		public virtual IStochastic StochasticSource
		{
			get { return this.stochasticSourceSource; }
			set { this.stochasticSourceSource = value; }
		}

		/// <summary>
		/// Gets a value indicating whether the random number distribution can be reset,
		/// so that it produces the same  random number sequence again.
		/// </summary>
		public bool CanReset
		{
			get { return this.stochasticSourceSource.CanReset; }
		}
	}
}