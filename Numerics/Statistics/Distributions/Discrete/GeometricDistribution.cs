using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Provides generation of geometric distributed random numbers.
	/// </summary>
	/// 
	/// <remarks>
	/// Note that are two different interpretation of what a geometric distribution is.
	/// 
	/// See http://en.wikipedia.org/wiki/Geometric_distribution
	/// </remarks>
	public sealed class GeometricDistribution : DiscreteDistributionBase
	{
		private double p;

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public GeometricDistribution()
		{
			this.SetDistributionParameters(0.5);
		}

		/// <summary>
		/// Initializes a new instance, using the specified <see cref="DiscreteDistributionBase.Randomizer"/>
		/// as underlying random number generator.
		/// </summary>
		public GeometricDistribution(IStochastic random)
			: base(random)
		{
			this.SetDistributionParameters(0.5);
		}

		/// <summary>
		/// Initializes a new instance, using a <see cref="DefaultSource"/>
		/// as underlying random number generator.
		/// </summary>
		public GeometricDistribution(double probabilityOfSuccess)
		{
			this.SetDistributionParameters(probabilityOfSuccess);
		}

		/// <summary>
		/// Gets the maximum possible value of generated random numbers.
		/// </summary>
		public override int Maximum {
			get {
				return int.MaxValue;
			}
		}

		/// <summary>
		/// Gets the mean value of generated random numbers.
		/// </summary>
		public override double Mean {
			get {
				return 1.0 / this.p;
			}
		}

		/// <summary>
		/// Gets the median of generated random numbers.
		/// </summary>
		public override int Median {
			get {
				return (int)System.Math.Ceiling(-Constants.Ln2 / System.Math.Log(1.0 - this.p));
			}
		}

		/// <summary>
		/// Gets the minimum possible value of generated random numbers.
		/// </summary>
		public override int Minimum {
			get {
				return 1;
			}
		}

		/// <summary>
		/// Gets or sets the success probability parameter.
		/// </summary>
		public double ProbabilityOfSuccess {
			get {
				return this.p;
			}
			set {
				this.SetDistributionParameters(value);
			}
		}

		/// <summary>
		/// Gets the skewness of generated random numbers.
		/// </summary>
		public override double Skewness {
			get {
				return (2.0 - this.p) / System.Math.Sqrt(1.0 - this.p);
			}
		}

		/// <summary>
		/// Gets the variance of generated random numbers.
		/// </summary>
		public override double Variance {
			get {
				return (1.0 - this.p) / (this.p * this.p);
			}
		}

		/// <summary>
		/// Determines whether the specified parameters are valid.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if value is greater than or equal to 0.0, and less than or equal to 1.0; otherwise, <see langword="false"/>.
		/// </returns>
		public static bool IsValidParameterSet(double probabilityOfSuccess)
		{
			return probabilityOfSuccess >= 0.0 && probabilityOfSuccess <= 1.0;
		}

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		public override double CumulativeDistribution(double x)
		{
			return x >= 0d ? 1.0 - System.Math.Pow(1.0 - this.p, 1 + Math.Floor(x)) : 0d;
		}

		/// <summary>
		/// Returns a geometric distributed floating point random number.
		/// </summary>
		/// <returns>A geometric distributed double-precision floating point number.</returns>
		public override int Next()
		{
			int count;
			for(count = 1; this.Randomizer.NextDouble() >= this.p; count++) {
			}

			return count;
		}

		/// <summary>
		/// Returns the discrete probability mass function of this probability distribution.
		/// </summary>
		public override double ProbabilityMass(int x)
		{
			return this.p * System.Math.Pow(1.0 - this.p, x - 1);
		}

		/// <summary>
		/// Configure all distribution parameters.
		/// </summary>
		public void SetDistributionParameters(double probabilityOfSuccess)
		{
			if(!IsValidParameterSet(probabilityOfSuccess))
				throw new ArgumentException("The probability of success in the geometric distribution should be in the [0,1] interval.", "probabilityOfSuccess");
			this.p = probabilityOfSuccess;
		}
	}
}