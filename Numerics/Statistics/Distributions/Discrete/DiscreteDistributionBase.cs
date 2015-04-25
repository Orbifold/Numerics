using System;

namespace Orbifold.Numerics
{

	/// <summary>
	/// Base class for all discrete stochastic distributions.
	/// </summary>
	public abstract class DiscreteDistributionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteDistributionBase"/> class, using a 
		/// <see cref="DefaultSource"/> as underlying random number generator.
		/// </summary>
		protected DiscreteDistributionBase()
			: this(new DefaultSource())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteDistributionBase"/> class, using the
		/// specified <see cref="Randomizer"/> as underlying random number generator.
		/// </summary>
		/// <param name="generator">A <see cref="Randomizer"/> object aka stochastic source.</param>
		protected DiscreteDistributionBase(IStochastic generator)
		{
			if (generator == null) throw new ArgumentNullException("generator");
			this.Randomizer = generator;
		}

		/// <summary>
		/// Upper limit of a random variable with this probability distribution.
		/// </summary>
		public abstract int Maximum { get; }

		/// <summary>
		/// The expected value of a random variable with this probability distribution.
		/// </summary>
		public abstract double Mean { get; }

		/// <summary>
		/// The value separating the lower half part from the upper half part of a random variable with this probability distribution.
		/// </summary>
		public abstract int Median { get; }

		/// <summary>
		/// Lower limit of a random variable with this probability distribution.
		/// </summary>
		public abstract int Minimum { get; }

		/// <summary>
		/// Gets or sets a <see cref="Randomizer"/> object that can be used
		/// as underlying random number generator.
		/// </summary>
		public IStochastic Randomizer { get; set; }

		/// <summary>
		/// Measure of the asymmetry of this probability distribution.
		/// </summary>
		public abstract double Skewness { get; }

		/// <summary>
		/// Average of the squared distances to the expected value of a random variable with this probability distribution.
		/// </summary>
		public abstract double Variance { get; }

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		public abstract double CumulativeDistribution(double x);

		/// <summary>
		/// Returns a distributed integer random number.
		/// </summary>
		/// <returns>A distributed 32 bit signed integer number.</returns>
		public abstract int Next();

		/// <summary>
		/// Discrete probability mass function (pmf) of this probability distribution.
		/// </summary>
		public abstract double ProbabilityMass(int x);
	}
}