#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Abstract base class for calculating impurities when creating decision trees.
    /// </summary>
    /// <seealso cref="http://en.wikipedia.org/wiki/Decision_tree_learning"/>
    /// <seealso cref="DecisionTreeMachineLearningModelCreator"/>
    public abstract class Impurity
    {
        public Range<double>[] Segments { get; set; }

        public bool Discrete { get; set; }

        /// <summary>
        /// Calculates the impurity of the given vector.
        /// </summary>
        public abstract double Calculate(Vector x);

        public double SegmentedConditional(Vector y, Vector x, int segments)
        {
            return this.SegmentedConditional(y, x, x.Segment(segments));
        }

        public double SegmentedConditional(Vector y, Vector x, IEnumerable<Range<double>> ranges)
        {
            double result = 0, count = x.Dimension;
            this.Segments = ranges.OrderBy(r => r.Start).ToArray();
            this.Discrete = false;
            result += (from s in this.Segments.Select(range => x.Indices(d => (d >= range.Start && d < range.End))) select s as int[] ?? s.ToArray() into enumerable let p = enumerable.Count() / count let h = this.Calculate(y.Slice(enumerable)) select p * h).Sum();
            return result;
        }

        public double Conditional(Vector y, Vector x)
        {
            double result = 0, count = x.Dimension; 
            var values = x.ToArray().Distinct().OrderBy(z => z).ToList();
            this.Segments = values.Select(z => Range.Create(z, z)).ToArray();
            this.Discrete = true;
            result += (from i in values select x.Indices(d => d.IsEqualTo(i)) into s select s as int[] ?? s.ToArray() into enumerable let p = enumerable.Count() / count let h = this.Calculate(y.Slice(enumerable)) select p * h).Sum();
            return result;
        }

        public double Gain(Vector y, Vector x)
        {
            return this.Calculate(y) - this.Conditional(y, x);
        }

        public double SegmentedGain(Vector y, Vector x, int segments)
        {
            return this.Calculate(y) - this.SegmentedConditional(y, x, segments);
        }

        public double SegmentedGain(Vector y, Vector x, IEnumerable<Range<double>> ranges)
        {
            return this.Calculate(y) - this.SegmentedConditional(y, x, ranges);
        }

        public double RelativeGain(Vector y, Vector x)
        {
            var inpurityY = this.Calculate(y);
            return (inpurityY - this.Conditional(y, x)) / inpurityY;
        }
        /// <summary>
        /// Measures the relative gain when dealing with a regression tree.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="segments">The number of children nodes to create.</param>
        /// <returns></returns>
        public double SegmentedRelativeGain(Vector y, Vector x, int segments)
        {
            var inpurityY = this.Calculate(y);
            return (inpurityY - this.SegmentedConditional(y, x, segments)) / inpurityY;
        }

        public double SegmentedRelativeGain(Vector y, Vector x, IEnumerable<Range<double>> ranges)
        {
            var impurityY = this.Calculate(y);
            return (impurityY -  this.SegmentedConditional(y, x, ranges)) / impurityY;
        }
    }
}