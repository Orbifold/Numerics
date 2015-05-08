using System;

namespace Orbifold.Numerics
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Gamma_distribution
    /// </summary>
    public class GammaDistribution : ContinuousDistribution
    {
        private readonly double theta;

        private readonly double k;

        public GammaDistribution(IStochastic random) : base(random)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theta">The shape parameter.</param>
        /// <param name="k">The scale parameter.</param>
        public GammaDistribution(double theta, double k)
        {
            this.theta = theta;
            this.k = k;
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
        ///     The expected value of a random variable with this probability distribution.
        /// </summary>
        public override double Mean
        {
            get
            {
                return this.k * this.theta;
            }
            set
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException("There is no simple closed form for the median.");
            }
        }

        /// <summary>
        ///     Average of the squared distances to the expected value of a random variable with this probability distribution.
        /// </summary>
        public override double Variance
        {
            get
            {
                throw new NotImplementedException();
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
        ///     Returns a distributed floating point random number.
        /// </summary>
        /// <returns>A distributed double-precision floating point number.</returns>
        public override double NextDouble()
        {
            return Random(this.k, this.theta);
        }

        /// <summary>
        ///     Generates a random vector of observations from the
        ///     Gamma distribution with the given parameters.
        /// </summary>
        /// <param name="scale">The scale parameter theta.</param>
        /// <param name="shape">The shape parameter k.</param>
        /// <param name="samples">The number of samples to generate.</param>
        /// <returns>An array of double values sampled from the specified Gamma distribution.</returns>
        public static double[] Random(double shape, double scale, int samples)
        {
            var r = new double[samples];

            if (shape < 1)
            {
                var d = shape + 1.0 - 1.0 / 3.0;
                var c = 1.0 / Math.Sqrt(9 * d);

                for (var i = 0; i < r.Length; i++)
                {
                    double U = Numerics.Random.Next();
                    r[i] = scale * Gamma.Random(d, c) * Math.Pow(U, 1.0 / shape);
                }
            }
            else
            {
                var d = shape - 1.0 / 3.0;
                var c = 1.0 / Math.Sqrt(9 * d);

                for (var i = 0; i < r.Length; i++) r[i] = scale * Gamma.Random(d, c);
            }

            return r;
        }

        /// <summary>
        ///     Generates a random observation from the
        ///     Gamma distribution with the given parameters.
        /// </summary>
        /// <param name="scale">The scale parameter theta.</param>
        /// <param name="shape">The shape parameter k.</param>
        /// <returns>A random double value sampled from the specified Gamma distribution.</returns>
        public static double Random(double shape, double scale)
        {
            if (shape < 1)
            {
                var d = shape + 1.0 - 1.0 / 3.0;
                var c = 1.0 / Math.Sqrt(9 * d);

                double U = Numerics.Random.Next();
                return scale * Gamma.Random(d, c) * Math.Pow(U, 1.0 / shape);
            }
            else
            {
                var d = shape - 1.0 / 3.0;
                var c = 1.0 / Math.Sqrt(9 * d);

                return scale * Gamma.Random(d, c);
            }
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
    }
}