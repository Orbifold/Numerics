using System;

namespace Orbifold.Numerics
{
    /// <summary>
    ///     http://en.wikipedia.org/wiki/Chi-squared_distribution
    /// </summary>
    public class ChiSquareDistribution : ContinuousDistribution
    {
        // Distribution parameters

        // Distribution measures
        private double? entropy;

        /// <summary>
        ///     Constructs a new Chi-Square distribution
        ///     with given degrees of freedom.
        /// </summary>
        public ChiSquareDistribution() : this(1)
        {
        }

        /// <summary>
        ///     Constructs a new Chi-Square distribution
        ///     with given degrees of freedom.
        /// </summary>
        /// <param name="degreesOfFreedom">The degrees of freedom for the distribution. Default is 1.</param>
        public ChiSquareDistribution(int degreesOfFreedom)
        {
            if (degreesOfFreedom <= 0) throw new ArgumentOutOfRangeException("degreesOfFreedom", "The number of degrees of freedom must be higher than zero.");

            this.DegreesOfFreedom = degreesOfFreedom;
        }

        /// <summary>
        ///     Gets the Degrees of Freedom for this distribution.
        /// </summary>
        public int DegreesOfFreedom { get; private set; }

        /// <summary>
        ///     Upper limit of a random variable with this probability distribution.
        /// </summary>
        public override double Maximum
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the mean for this distribution.
        /// </summary>
        /// <remarks>
        ///     The χ² distribution mean is the number of degrees of freedom.
        /// </remarks>
        public override double Mean
        {
            get
            {
                return this.DegreesOfFreedom;
            }
            set
            {
                this.DegreesOfFreedom = (int)value;
            }
        }

        /// <summary>
        ///     The value separating the lower half part from the upper half part of a random variable with this probability
        ///     distribution.
        /// </summary>
        public override double Median
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the variance for this distribution.
        /// </summary>
        /// <remarks>
        ///     The χ² distribution variance is twice its degrees of freedom.
        /// </remarks>
        public override double Variance
        {
            get
            {
                return 2.0 * this.DegreesOfFreedom;
            }
        }

        /// <summary>
        ///     Measure of the asymmetry of this probability distribution.
        /// </summary>
        public override double Skewness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the complementary cumulative distribution function
        ///     (ccdf) for the χ² distribution evaluated at point <c>x</c>.
        ///     This function is also known as the Survival function.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The Complementary Cumulative Distribution Function (CCDF) is
        ///         the complement of the Cumulative Distribution Function, or 1
        ///         minus the CDF.
        ///     </para>
        ///     <para>
        ///         The χ² complementary distribution function is defined in terms of the
        ///         <see cref="Gamma.UpperIncomplete">
        ///             Complemented Incomplete Gamma
        ///             Function Γc(a, x)
        ///         </see>
        ///         as CDF(x; df) = Γc(df/2, x/d).
        ///     </para>
        /// </remarks>
        public double ComplementaryDistributionFunction(double x)
        {
            return x <= 0 ? 1 : Gamma.UpperIncomplete(this.DegreesOfFreedom / 2.0, x / 2.0);
        }

        #region ISamplableDistribution<double> Members

        /// <summary>
        ///     Generates a random observation from the current distribution.
        /// </summary>
        /// <returns>A random observations drawn from this distribution.</returns>
        public override double NextDouble()
        {
            return GammaDistribution.Random(this.DegreesOfFreedom / 2.0, 2);
        }

        /// <summary>
        ///     Continuous probability density function (pdf) of this probability distribution.
        /// </summary>
        public override double ProbabilityDensity(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Continuous cumulative distribution function (cdf) of this probability distribution.
        /// </summary>
        public override double CumulativeDistribution(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Lower limit of a random variable with this probability distribution.
        /// </summary>
        public override double Minimum
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Generates a random vector of observations from the
        ///     Chi-Square distribution with the given parameters.
        /// </summary>
        /// <returns>An array of double values sampled from the specified Chi-Square distribution.</returns>
        public static double[] Random(int degreesOfFreedom, int samples)
        {
            return GammaDistribution.Random(degreesOfFreedom / 2.0, 2, samples);
        }

        /// <summary>
        ///     Generates a random observation from the
        ///     Chi-Square distribution with the given parameters.
        /// </summary>
        /// <param name="degreesOfFreedom">The degrees of freedom for the distribution.</param>
        /// <returns>A random double value sampled from the specified Chi-Square distribution.</returns>
        public static double Random(int degreesOfFreedom)
        {
            return GammaDistribution.Random(degreesOfFreedom / 2.0, 2);
        }

        #endregion
    }
}