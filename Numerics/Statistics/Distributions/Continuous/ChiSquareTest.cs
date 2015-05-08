using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    /// <summary>
    ///     Chi-square fitness test with Pearson p-value.
    /// </summary>
    public class ChiSquareTest
    {
        private static readonly ChiSquareDistribution oneChiSquareDistribution;

        private readonly int degreesOfFreedom = 1;

        private RMatrix matrix;

        static ChiSquareTest()
        {
            oneChiSquareDistribution = new ChiSquareDistribution();
        }

        public ChiSquareTest(IReadOnlyCollection<double> expected, IReadOnlyCollection<double> observed, int degreesOfFreedom = 1)
        {
            if (expected == null) throw new ArgumentNullException("expected");
            if (observed == null) throw new ArgumentNullException("observed");
            if (expected.Count != observed.Count) throw new Exception("Dimensions of the given vectors should be the same.");
            this.Expected = expected.ToArray();
            this.Observed = observed.ToArray();
            this.degreesOfFreedom = degreesOfFreedom;

            this.StatisticDistribution = new ChiSquareDistribution(this.degreesOfFreedom);

            // how likely is it with respect to the chi2 probability distribution?
            this.ComputeStandardPValue();
            // the R calc is somewhat different
            this.ComputeRStatsPValue();
        }

        private double[] Expected { get; set; }

        private double[] Observed { get; set; }

        private RMatrix Matrix
        {
            get
            {
                if (this.matrix.ColumnCount==0) this.matrix = new RMatrix(this.Expected, this.Observed);
                return this.matrix;
            }
        }

        public double Statistic { get; protected set; }

        public ChiSquareDistribution StatisticDistribution { get; set; }

        public double PValue { get; set; }

        public double RPValue { get; set; }

        protected void ComputeStandardPValue()
        {
            this.Statistic = this.Chi2();

            this.PValue = this.StatisticDistribution.ComplementaryDistributionFunction(this.Statistic);
        }

        /// <summary>
        ///     The classic chi2 sum.
        /// </summary>
        private double Chi2()
        {
            var sum = 0.0;
            for (var i = 0; i < this.Observed.Length; i++)
            {
                var d = this.Observed[i] - this.Expected[i];

                if (d != 0) sum += (d * d) / this.Expected[i];
            }
            return sum;
        }

        /*
            Conversion of the internal R-code implementing which is
            somewhat different from the standard way.
            In fact, there are many p-value implementations, this StackOVerflow item is illuminating
            http://stackoverflow.com/questions/27555414/chi-squared-test-in-r-to-compare-real-data-to-theoretical-normal-distribution 

            The original R-code converted in this method is
				  
                  x = as.matrix(df)
                  n = sum(x)
                  nr <- as.integer(nrow(x))
                  nc <- as.integer(ncol(x))
                  sr <- rowSums(x)
                  sc <- colSums(x)
                  E <- outer(sr, sc, "*")/n
                  v <- function(r, c, n) c * r * (n - r) * (n - c)/n^3
                  V <- outer(sr, sc, v, n)
                  dimnames(E) <- dimnames(x)
				  
                  if ( nrow(x) == 2L && ncol(x) == 2L) {
                    YATES <- min(0.5, abs(x - E))
                    if (YATES > 0) 
                      METHOD <- paste(METHOD, "with Yates' continuity correction")
                  }
                  else YATES <- 0
                  STATISTIC <- sum((abs(x - E) - YATES)^2/E)
                  PARAMETER <- (nr - 1L) * (nc - 1L)
                  PVAL <- pchisq(STATISTIC, PARAMETER, lower.tail = FALSE)
                  return(1-PVAL)
			
        */

        private void ComputeRStatsPValue()
        {
            var x = this.Matrix;
            var n = this.Matrix.Sum();
            var nr = this.Expected.Length;
            var nc = 2;
            var sr = this.Matrix.RowSums();
            var sc = this.Matrix.ColSums();
            var E = sr.Outer(sc) / n;
            // Yates correction
            var yates = 0d;
            if (nr == 2 && nc == 2) yates = ((x - E).Abs()).Min(0.5);
            var statistic = ((((x - E).Abs() - yates) ^ 2) / E).Sum();
            this.RPValue = this.StatisticDistribution.ComplementaryDistributionFunction(statistic);
        }

        /// <summary>
        /// Computes the Pearson p-value for the given vectors.
        /// 
        /// </summary>
        /// <remarks>
        /// This computation is identical to the R version, contrary to the non-static PValue property of this class.
        /// https://stat.ethz.ch/R-manual/R-patched/library/stats/html/chisq.test.html
        /// </remarks>
        /// <param name="observed"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static double RStatsPValue(double[] observed, double[] expected)
        {
            var x = new RMatrix(observed, expected);
            var n = x.Sum();
            var nr = expected.Length;
            var nc = 2;
            var sr = x.RowSums();
            var sc = x.ColSums();
            var E = sr.Outer(sc) / n;
            // Yates correction
            var yates = 0d;
            if (nr == 2 && nc == 2) yates = ((x - E).Abs()).Min(0.5);
            var statistic = ((((x - E).Abs() - yates) ^ 2) / E).Sum();
            return oneChiSquareDistribution.ComplementaryDistributionFunction(statistic);
        }
    }
}