#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Vector related extension methods and utilities.
    /// </summary>
    public static class VectorExtensions
    {
        public static Vector Sort(this Vector v)
        {
            var w = v.Clone().ToArray();
            Array.Sort(w);
            return w;
        }

        /// <summary>
        /// Returns the distribution of the values in the vector.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        public static RMatrix Stats(this Vector x)
        {
            var ar = x.ToArray();
            return (from i in ar.Distinct().OrderBy(d => d) let q = (from j in ar where j == i select j).Count() select new[] { i, q, q / (double)ar.Length }).ToMatrix();
        }

        /// <summary>
        /// Returns the entry appearing the most in the vector.
        /// </summary>
        public static double MostAppearingElement(this Vector source)
        {
            if (source.Dimension == 0) return double.NaN;
            var ar = source.ToArray();
            var appearingNumbers = ar.GroupBy(i => i).Select(g => Tuple.Create(g.Key, ar.Count(d => g.Key.IsEqualTo(d)))).ToList();
            appearingNumbers.Sort((t1, t2) => t1.Item2.CompareTo(t2.Item2));

            return appearingNumbers.Last().Item1;
        }

        /// <summary>
        /// Linear interpolation between the given vectors.
        /// </summary>
        /// <param name="u">A point.</param>
        /// <param name="v">Another point.</param>
        /// <param name="fraction">A value in the [0,1] interval. At zero the interpolation returns the first vector, at one it results in the second vector.</param>
        /// <returns></returns>
        public static Vector2D Lerp(Vector2D u, Vector2D v, double fraction)
        {
            return new Vector2D(Utils.Lerp(u.X, v.X, fraction), Utils.Lerp(u.Y, v.Y, fraction));
        }

        /// <summary>
        /// Turns this array into a <see cref="RVector"/>.
        /// </summary>
        /// <returns>The vector.</returns>
        /// <param name="array">Array.</param>
        public static Vector ToVector(this double[] array)
        {
            return new Vector(array);
        }

        /// <summary>
        /// Returns whether one or more of the vector entries is not a number.
        /// </summary>
        /// <returns><c>true</c> if is na n the specified vector; otherwise, <c>false</c>.</returns>
        /// <param name="vector">Vector.</param>
        public static bool IsNaN(this Vector vector)
        {
            var nan = true;
            for (var i = 0; i < vector.Dimension; i++) nan = nan && double.IsNaN(vector[i]);
            return nan;
        }

        /// <summary>
        /// Returns the correlation between the given vectors.
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/Correlation_and_dependence"/>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public static double Correlation(this Vector x, Vector y)
        {
            var s = (x.StandardDeviation() * y.StandardDeviation());
            return EpsilonExtensions.IsZero(s) ? double.NaN : x.Covariance(y) / s;
        }

        public static double Variance(this Vector x)
        {
            var mean = x.Mean();
            var sum = 0d;
            for (var i = 0; i < x.Dimension; i++) sum += Math.Pow(x[i] - mean, 2);

            return sum / (x.Dimension - 1);
        }

        /// <summary>
        /// The covariance between the given vectors.
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/Covariance"/>
        /// <param name="x">A vector.</param>
        /// <param name="y">A vector.</param>
        public static double Covariance(this Vector x, Vector y)
        {
            if (x.Dimension != y.Dimension) throw new InvalidOperationException("The given vectors are not of the same dimension.");

            var xmean = x.Mean();
            var ymean = y.Mean();

            var sum = 0d;
            for (var i = 0; i < x.Dimension; i++) sum += (x[i] - xmean) * (y[i] - ymean);

            return sum / (x.Dimension - 1);
        }

        /// <summary>Returns the dot (scalar) multiplication with the given vector.</summary>
        /// <param name="v">A vector.</param>
        /// <param name="x">Another vector.</param>
        /// <returns>The scalar multiplication.</returns>
        public static double Dot(this Vector2D v, Vector2D x)
        {
            return Vector2D.Dot(v, x);
        }

        /// <summary>
        /// Returns the (standard Euclidean) norm of the given vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        public static double Norm(this Vector vector, double p = 2)
        {
            return Vector.Norm(vector, p);
        }

        /// <summary>
        /// Returns the Mahattan distance to the given vector.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="v">A vector.</param>
        /// <param name="w">A vector.</param>
        public static double MahattanDistance(this Vector v, Vector w)
        {
            return new ManhattanDistance().Compute(v, w);
        }

        /// <summary>
        /// Returns the average or mean value of the given vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        public static double Average(this Vector v)
        {
            return v.ToArray().Sum() / v.Dimension;
        }

        /// <summary>
        /// Returns the average or mean value of the given vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        public static double Mean(this Vector v)
        {
            return v.Average();
        }

        /// <summary>
        /// Measures the amount of variation or dispersion from the average.
        /// </summary>
        /// <returns>The deviation.</returns>
        /// <param name="v">A vector.</param>
        /// <seealso cref="http://en.wikipedia.org/wiki/Standard_deviation"/>
        public static double StandardDeviation(this Vector v)
        {
            var av = v.Mean();
            return Math.Sqrt(v.Each(d => Math.Pow(d - av, 2), true).Sum() / (v.Dimension - 1));
        }

        /// <summary>
        /// Returns the correlation similarity with the given vector.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="v">A vector.</param>
        /// <param name="w">A vector.</param>
        public static double PearsonCorrelation(this Vector v, Vector w)
        {
            return new PearsonCorrelation().Compute(v, w);
        }

        /// <summary>
        /// Returns the Euclidean distance to the given vector.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="v">A vector.</param>
        /// <param name="w">A vector.</param>
        public static double EuclideanDistance(this Vector v, Vector w)
        {
            return new EuclidianDistance().Compute(v, w);
        }

        /// <summary>
        /// Returns the maximum value of the vector's entries.
        /// </summary>
        /// <param name="v">A vector.</param>
        public static double Max(this Vector v)
        {
            return v.ToArray().Max();
        }

        /// <summary>
        /// Returns the minimum value of the vector's entries.
        /// </summary>
        /// <param name="v">A vector.</param>
        public static double Min(this Vector v)
        {
            return v.ToArray().Min();
        }

        /// <summary>
        /// Applies the given function to the elements of the vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <param name="transform">The transformation to apply to the members of the vector.</param>
        /// <param name="asCopy">If set to <c>true</c> the transformation is applied to a copy of the vector, otherwise the given vector itself will be transformed.</param>
        public static Vector Each(this Vector v, Func<double, double> transform, bool asCopy = false)
        {
            var vector = v;
            if (asCopy) vector = v.Clone();

            for (var i = 0; i < vector.Dimension; i++) vector[i] = transform(vector[i]);

            return vector;
        }

        /// <summary>
        /// Returns equal ranges starting at the minimum and ending the maximum of the given vector.
        /// </summary>
        /// <param name="x">A vector.</param>
        /// <param name="segments">The amount of segments or ranges to generate.</param>
        public static Range<double>[] Segment(this Vector x, int segments)
        {
            if (x.Dimension <= 1) throw new Exception("Cannot segment a one-dimensional vector.");
            if (segments < 2) throw new InvalidOperationException("The segmentation requires at least two segments.");

            var max = x.Max();
            var min = x.Min();
            var range = (max - min) / segments;
            var ranges = new Range<double>[segments];
            ranges[0] = Range.Create(min, min + range);

            for (var i = 1; i < segments; i++) ranges[i] = Range.Create(ranges[i - 1].End, ranges[i - 1].End + range);

            // make last range slightly larger 
            // to maintain r.Min <= d < r.Max
            ranges[ranges.Length - 1] = Range.Create(ranges[ranges.Length - 1].Start, ranges[ranges.Length - 1].End + .01d);

            return ranges;
        }

        /// <summary>
        /// Returns the index numbers within the given source satisfying the predicate.
        /// </summary>
        /// <param name = "v"></param>
        /// <param name="v">The vector to test.</param>
        /// <param name="f">The predicate test.</param>
        public static IEnumerable<int> Indices(this Vector v, Predicate<double> f)
        {
            return v.ToArray().Indices(f);
        }

        /// <summary>
        /// Returns a sub-vector based on the given indices.
        /// </summary>
        /// <param name="v">V.</param>
        /// <param name="indices">Indices.</param>
        public static Vector Slice(this Vector v, IEnumerable<int> indices)
        {
            var q = indices.Distinct().Where(j => j < v.Dimension);
            var n = q.Count();

            var vector = new Vector(n);
            var i = -1;
            foreach (var j in q.OrderBy(k => k)) vector[++i] = v[j];

            return vector;
        }

        /// <summary>Returns the dot (scalar) multiplication with the given vector.</summary>
        /// <param name="v">A vector.</param>
        /// <param name="x">Another vector.</param>
        /// <returns>The scalar multiplication.</returns>
        public static double Dot(this Vector v, Vector x)
        {
            return Vector.Dot(v, x);
        }
        
        /// <summary>
        /// Returns the outer product with the given vector.
        /// </summary>
        /// <param name="a">A vector</param>
        /// <param name="b">Another vector</param>
        /// <returns></returns>
        public static RMatrix Outer(this Vector a, Vector b)
        {
            var result = new RMatrix(a.Dimension, 2);
            for (var i = 0; i < a.Dimension; i++)
            {
                for (var j = 0; j < b.Dimension; j++)
                {
                    result[i, j] = a[i] * b[j];
                }
            }
            return result;
        }

        /// <summary>
        /// Returns the mirrored vector with respect to the X-coordinate.
        /// </summary>
        /// <param name="v">The vector to mirror.</param>
        /// <returns>The mirrored vector.</returns>
        public static Vector2D MirrorHorizontally(this Vector2D v)
        {
            return new Vector2D(v.Y, -v.X);
        }

        /// <summary>
        /// Returns the mirrored vector with respect to the Y-coordinate.
        /// </summary>
        /// <param name="v">The vector to mirror.</param>
        /// <returns>The mirrored vector.</returns>
        public static Vector2D MirrorVertically(this Vector2D v)
        {
            return new Vector2D(-v.Y, v.X);
        }

        /// <summary>
        /// Returns a unit vector in the direction specified by the angle.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>A unit vector.</returns>
        public static Vector2D UnitVector(double degrees)
        {
            var rad = Trigonometry.DegreesToRadians(degrees);
            return new Vector2D(Math.Cos(rad), Math.Sin(rad));
        }

        /// <summary>
        /// Returns the perpendicular of the specified vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns></returns>
        public static Vector2D Perpendicular(Vector2D v)
        {
            return new Vector2D(v.Y, -v.X);
        }

        /// <summary>
        /// Normalizeds the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector2D Normalized(this Vector2D vector)
        {
            var v = new Vector2D(vector.X, vector.Y);
            var length = v.Length;
            return !length.IsVerySmall() ? v / length : new Vector2D(0, 1);
        }

        /// <summary>
        /// Returns a normalized clone of the given vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        public static Vector Normalized(this Vector vector)
        {
            var v = vector.Clone();
            v.Normalize();
            return v;
        }
    }
}