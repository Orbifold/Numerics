using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// The binomial distribution of random numbers.
	/// </summary>
	/// <remarks>
	/// The binomial distribution generates only discrete numbers.<br />
	/// The implementation bases upon information presented on  <a href="http://en.wikipedia.org/wiki/binomial_distribution">Wikipedia - Binomial distribution</a>.
	/// </remarks>
	public sealed class BinomialDistribution : DiscreteDistributionBase
	{
		/// <summary>
		/// The number of trials
		/// </summary>
		private int n;

		/// <summary>
		/// The probability.
		/// </summary>
		private double p;

		/// <summary>
		/// Gets the maximum possible value of generated random numbers.
		/// </summary>
		public override int Maximum
		{
			get
			{
				return this.n;
			}
		}

		/// <summary>
		/// Gets the mean value of generated random numbers.
		/// </summary>
		public override double Mean
		{
			get
			{
				return this.p * this.n;
			}
		}

		/// <summary>
		/// Gets the median of generated random numbers.
		/// </summary>
		public override int Median
		{
			get
			{
				return (int)System.Math.Floor(this.p * this.n);
			}
		}

		/// <summary>
		/// Gets the minimum possible value of generated random numbers.
		/// </summary>
		public override int Minimum
		{
			get
			{
				return 1;
			}
		}

		/// <summary>
		/// Gets or sets the number of trials parameter.
		/// </summary>
		/// <remarks>Call <see cref="IsValidParameterSet"/> to determine whether a value is valid and therefore assignable.</remarks>
		public int NumberOfTrials
		{
			get
			{
				return this.n;
			}
			set
			{
				this.SetDistributionParameters(this.p, value);
			}
		}

		/// <summary>
		/// Gets or sets the success probability parameter.
		/// </summary>
		public double ProbabilityOfSuccess
		{
			get
			{
				return this.p;
			}
			set
			{
				this.SetDistributionParameters(value, this.n);
			}
		}

		/// <summary>
		/// Gets the skewness of generated random numbers.
		/// </summary>
		public override double Skewness
		{
			get
			{
				return (1.0 - 2.0 * this.p) / Math.Sqrt(this.p * (1.0 - this.p) * this.n);
			}
		}

		/// <summary>
		/// Gets the variance of generated random numbers.
		/// </summary>
		public override double Variance
		{
			get
			{
				return this.p * (1.0 - this.p) * this.n;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinomialDistribution" /> class.
		/// </summary>
		public BinomialDistribution()
		{
			this.SetDistributionParameters(0.5, 1);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinomialDistribution" /> class.
		/// </summary>
		/// <param name="random">The random.</param>
		public BinomialDistribution(IStochastic random) : base(random)
		{
			this.SetDistributionParameters(0.5, 1);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinomialDistribution" /> class.
		/// </summary>
		/// <param name="probabilityOfSuccess">The probability of success.</param>
		/// <param name="numberOfTrials">The number of trials.</param>
		public BinomialDistribution(double probabilityOfSuccess, int numberOfTrials)
		{
			this.SetDistributionParameters(probabilityOfSuccess, numberOfTrials);
		}

		/// <summary>
		/// Continuous cumulative distribution function (cdf) of this probability distribution.
		/// </summary>
		public override double CumulativeDistribution(double x)
		{
			return Functions.BetaRegularized(this.n - x, x + 1, 1 - this.p);
		}

		/// <summary>
		/// Determines whether the specified parameters is valid.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if probabilityOfSuccess is greater than or equal to 0.0, and less than or equal to 1.0,
		/// and numberOfTrials is greater than or equal to 0; otherwise, <see langword="false"/>.
		/// </returns>
		public static bool IsValidParameterSet(double probabilityOfSuccess, int numberOfTrials)
		{
			return (probabilityOfSuccess >= 0.0 && probabilityOfSuccess <= 1.0) && (numberOfTrials >= 0);
		}

		/// <summary>
		/// Returns a bernoulli distributed random number.
		/// </summary>
		/// <returns>A bernoulli distributed 32-bit signed integer.</returns>
		public override int Next()
		{
			var successes = 0;
			for (var i = 0; i < this.n; i++) if (this.Randomizer.NextDouble() < this.p) successes++;
			return successes;
		}

		/// <summary>
		/// Discrete probability mass function (pmf) of this probability distribution.
		/// </summary>
		public override double ProbabilityMass(int x)
		{
			return Functions.BinomialCoefficient(this.n, x) * System.Math.Pow(this.p, x) * System.Math.Pow(1 - this.p, this.n - x);
		}

		/// <summary>
		/// Configure all distribution parameters.
		/// </summary>
		public void SetDistributionParameters(double probabilityOfSuccess, int numberOfTrials)
		{
			if (!IsValidParameterSet(probabilityOfSuccess, numberOfTrials)) throw new ArgumentException();

			this.p = probabilityOfSuccess;
			this.n = numberOfTrials;
		}
	}

	/*public static double BinomialPDF(int x, int n, double p)
		{
			return Functions.Gamma(n + 1) * System.Math.Pow(p, x) * System.Math.Pow(1 - p, n - x) / Functions.Gamma(x + 1) / Functions.Gamma(n - x + 1);
		}

		public static double[] BinomialPDF(int[] x, int n, double p)
		{
			double[] y = new double[x.Length];
			for (int i = 0; i < x.Length; i++)
			{
				y[i] = BinomialPDF(x[i], n, p);
			}
			return y;
		}

		public static double NextBinomial(int n, double p)
		{
			double result = 0.0;
			for (int i = 0; i < n; i++)
			{
				if (rand.NextDouble() < p)
				{
					result++;
				}
			}
			return result;
		}

		public static double[] NextBinomial(int n, double p, int nLen)
		{
			double[] array = new double[nLen];
			for (int i = 0; i < nLen; i++)
			{
				array[i] = NextBinomial(n, p);
			}
			return array;
		}*/
}