using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Generates Poisson distributed values.
	/// </summary>
	/// <remarks> 
	/// <para>See http://en.wikipedia.org/wiki/Poisson_distribution .</para>
	/// <para>See http://www.lkn.ei.tum.de/lehre/scn/cncl/doc/html/cncl_toc.html .</para>
	/// </remarks>
	public sealed class PoissonDistribution : DiscreteDistributionBase
	{
		double lambda;
		double rapidity;

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public PoissonDistribution()
		{
			this.SetDistributionParameters(1.0);
		}

		/// <summary>
		/// Initializes a new instance, using the specified <see cref="IStochastic"/>
		/// as underlying random number generator.
		/// </summary>
		/// <param name="random">A <see cref="IStochastic"/> object.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="random"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public PoissonDistribution(IStochastic random)
			: base(random)
		{
			this.SetDistributionParameters(1.0);
		}

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public PoissonDistribution(double lambda)
		{
			this.SetDistributionParameters(lambda);
		}

		/// <summary>
		/// Gets or sets the lambda parameter.
		/// </summary>
		public double Lambda {
			get { return this.lambda; }
			set { this.SetDistributionParameters(value); }
		}

		/// <summary>
		/// Configure all distribution parameters.
		/// </summary>
		public void SetDistributionParameters(double mean)
		{
			if(!IsValidParameterSet(mean))
				throw new ArgumentException("lambda");
			this.lambda = mean;
			this.rapidity = System.Math.Exp(-this.lambda);
		}

		/// <summary>
		/// Determines whether the specified parameters are valid.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if value is greater than 0.0; otherwise, <see langword="false"/>.
		/// </returns>
		public static bool IsValidParameterSet(double lambda)
		{
			return lambda > 0.0;
		}

		/// <summary>
		/// Gets the minimum possible value of generated random numbers.
		/// </summary>
		public override int Minimum {
			get { return 0; }
		}

		/// <summary>
		/// Gets the maximum possible value of generated random numbers.
		/// </summary>
		public override int Maximum {
			get { return int.MaxValue; }
		}

		/// <summary>
		/// Gets the mean value of generated random numbers. 
		/// </summary>
		public override double Mean {
			get { return this.lambda; }
		}

		/// <summary>
		/// Gets the median of generated random numbers.
		/// </summary>
		/// <remarks>This is an approximation and not an exact value.</remarks>
		public override int Median {
			get { return (int)System.Math.Floor(this.lambda + 1.0 / 3.0 - 0.2 / this.lambda); }
		}

		/// <summary>
		/// Gets the variance of generated random numbers.
		/// </summary>
		public override double Variance {
			get { return this.lambda; }
		}

		/// <summary>
		/// Gets the skewness of generated random numbers.
		/// </summary>
		public override double Skewness {
			get { return 1d / Math.Sqrt(this.lambda); }
		}

		/// <summary>
		/// Discrete probability mass function (pmf) of this probability distribution.
		/// See http://en.wikipedia.org/wiki/Probability_mass_function .
		/// </summary>
		public override double ProbabilityMass(int x)
		{
			return System.Math.Exp(-this.lambda + x * System.Math.Log(this.lambda) - Functions.FactorialLn(x));
		}

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// See http://en.wikipedia.org/wiki/Cumulative_distribution_function .
		/// </summary>
		public override double CumulativeDistribution(double x)
		{
			return 1.0 - Functions.GammaRegularized(x + 1, this.lambda);
		}

		/// <summary>
		/// Returns a poisson distributed random number.
		/// </summary>
		public override int Next()
		{
			var count = 0;
			for(var product = this.Randomizer.NextDouble(); product >= this.rapidity; product *= this.Randomizer.NextDouble())
				count++;
			return count;
		}
	}
}