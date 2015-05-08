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
    /// A  real-valued, arbitrary finite dimensional vector.
    /// </summary>
    public struct Vector : ICloneable
    {
        /// <summary>
        /// Whether this vector is a column in the referenced matrix.
        /// </summary>
        private readonly bool asCol;

        /// <summary>
        /// Whether this vector is actually a reference to a column or row in a matrix.
        /// </summary>
        private readonly bool asMatrixRef;

        /// <summary>
        /// The dimension of this vector.
        /// </summary>
        private readonly int dimension;

        /// <summary>
        /// A reference to the matrix if this vector is part of a matrix.
        /// </summary>
        private readonly double[,] matrix;

        /// <summary>
        /// The index of the row or column in the reference matrix, if this is a vector in a matrix.
        /// </summary>
        private readonly int staticIndex;

        /// <summary>
        /// The entries of this vector.
        /// </summary>
        private double[] vector;

        /// <summary>
        /// Initializes a new instance of the <see cref="RVector" /> struct.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        public Vector(int dimension, double init = 0.0)
        {
            this.dimension = dimension;
            this.vector = new double[dimension];
            this.asCol = false;
            this.staticIndex = -1;
            this.matrix = null;
            this.asMatrixRef = false;
            for (var i = 0; i < dimension; i++) this.vector[i] = init;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RVector" /> struct.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public Vector(IEnumerable<double> vector)
        {
            this.dimension = vector.Count();
            this.vector = vector.ToArray();
            this.asCol = false;
            this.staticIndex = -1;
            this.matrix = null;
            this.asMatrixRef = false;
        }

        public Vector(IEnumerable<int> vector)
        {
            this.dimension = vector.Count();
            this.vector = vector.Cast<double>().ToArray();
            this.asCol = false;
            this.staticIndex = -1;
            this.matrix = null;
            this.asMatrixRef = false;
        }

        internal Vector(double[,] m, int idx, bool asCol = false)
        {
            this.asCol = false;
            this.asCol = asCol;
            this.asMatrixRef = true;
            this.matrix = null;
            this.vector = null; // get/set will be redirected to the matrix
            this.matrix = m;
            this.staticIndex = -1;
            this.staticIndex = idx;
            this.dimension = asCol ? m.GetLength(1) : m.GetLength(0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orbifold.Numerics.RVector"/> struct.
        /// </summary>
        /// <param name="range">Range.</param>
        public Vector(Range<double> range)
        {
            this.dimension = range.Count();
            this.vector = range.ToArray();
            this.asCol = false;
            this.staticIndex = -1;
            this.matrix = null;
            this.asMatrixRef = false;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Double" /> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double" />.
        /// </value>
        /// <param name="i">The entry number.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Requested vector index is out of range!</exception>
        public double this[int i]
        {
            get
            {
                if (!this.asMatrixRef) return this.vector[i];
                else
                {
                    if (this.asCol) return this.matrix[this.staticIndex, i];
                    else return this.matrix[i, this.staticIndex];
                }
            }
            set
            {
                if (!this.asMatrixRef) this.vector[i] = value;
                else
                {
                    if (this.asCol) this.matrix[this.staticIndex, i] = value;
                    else this.matrix[i, this.staticIndex] = value;
                }
            }
        }

        public int Dimension
        {
            get
            {
                return this.dimension;
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Vector Clone()
        {
            var v = new Vector(this.vector)
                        {
                            vector = (double[])this.vector.Clone()
                        };
            return v;
        }

        /// <summary>
        /// Swaps the vector entries at the given positions.
        /// </summary>
        /// <returns>The vector with switched elements.</returns>
        /// <param name="m">A position index.</param>
        /// <param name="n">A position index.</param>
        public Vector SwapVectorEntries(int m, int n)
        {
            if (m < this.Dimension && n < this.Dimension)
            {
                var temp = this.vector[m];
                this.vector[m] = this.vector[n];
                this.vector[n] = temp;
                return new Vector(this.vector);
            }
            throw new Exception("The given positions are larger than the vector dimension.");
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.vector.Length > 5 ? "(" + String.Join(",", this.vector.Take(5)) + "...)" : "(" + String.Join(",", this.vector) + ")";
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Orbifold.Numerics.RVector"/>.
        /// </summary>
        /// <param name="v">The <see cref="System.Object"/> to compare with the current <see cref="Orbifold.Numerics.RVector"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Orbifold.Numerics.RVector"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object v)
        {
            if (v is Vector)
            {
                var m = (Vector)v;
                if (this.Dimension != m.Dimension) return false;

                for (var i = 0; i < this.Dimension; i++) if (this[i].IsNotEqualTo(m[i])) return false;

                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return this.vector.GetHashCode();
        }

        public static Vector operator +(Vector v, double s)
        {
            for (var i = 0; i < v.Dimension; i++) v[i] += s;

            return v;
        }

        public static implicit operator double[](Vector v)
        {
            return v.ToArray();
        }

        /// <summary>Vector casting operator.</summary>
        /// <param name="array">The array.</param>
        public static implicit operator Vector(double[] array)
        {
            return new Vector(array);
        }

        /// <summary>
        /// Entry-by-entry multiplication of the given vectors.
        /// </summary>
        public static Vector operator *(Vector one, Vector two)
        {
            if (one.Dimension != two.Dimension) throw new InvalidOperationException("Dimensions do not match!");
            var result = one.Clone();
            for (var i = 0; i < one.Dimension; i++) result[i] *= two[i];
            return result;
        }

        public static Vector operator +(double s, Vector v)
        {
            return v + s;
        }
        public static Vector operator ^(Vector one, double power)
        {
            return one.Each(d => System.Math.Pow(d, power));
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        public static Vector operator +(Vector v)
        {
            return v;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            var result = new Vector(v1.dimension);
            for (var i = 0; i < v1.dimension; i++) result[i] = v1[i] + v2[i];
            return result;
        }

        public static Vector operator -(Vector v)
        {
            var result = new double[v.dimension];
            for (var i = 0; i < v.dimension; i++) result[i] = -v[i];
            return new Vector(result);
        }

        public static Vector operator -(Vector v, double a)
        {
            var result = new double[v.dimension];
            for (var i = 0; i < v.dimension; i++) result[i] = v[i] - a;
            return new Vector(result);
        }

        public static Vector operator -(double a, Vector v)
        {
            var result = new double[v.dimension];
            for (var i = 0; i < v.dimension; i++) result[i] = a - v[i];
            return new Vector(result);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            var result = new Vector(v1.dimension);
            for (var i = 0; i < v1.dimension; i++) result[i] = v1[i] - v2[i];
            return result;
        }

        public static Vector operator ^(Vector v, int n)
        {
            return v.Each(d => Math.Pow(d, n));
        }

        public static Vector operator *(Vector v, double d)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = v[i] * d;
            return result;
        }

        public static Vector operator *(Vector v, int d)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = v[i] * d;
            return result;
        }

        public static Vector operator *(double d, Vector v)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = d * v[i];
            return result;
        }

        public static Vector operator *(int d, Vector v)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = d * v[i];
            return result;
        }

        public static Vector operator /(Vector v, double d)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = v[i] / d;
            return result;
        }

        public static Vector operator /(double d, Vector v)
        {
            var result = new Vector(v.dimension);
            for (var i = 0; i < v.dimension; i++) result[i] = v[i] / d;
            return result;
        }

        /// <summary>
        /// Returns the dot (scalar) product of the given vectors.
        /// </summary>
        /// <param name="v1">Vector1.</param>
        /// <param name="v2">Vector2.</param>
        public static double Dot(Vector v1, Vector v2)
        {
            var result = 0.0;
            for (var i = 0; i < v1.dimension; i++) result += v1[i] * v2[i];
            return result;
        }

        /// <summary>
        /// Create a vector on the basis of the given function.
        /// </summary>
        /// <param name="length">The length of the vector to create.</param>
        /// <param name="f">The parameterless function generating the vector elements.</param>
        public static Vector Create(int length, Func<double> f)
        {
            var vector = new double[length];
            for (var i = 0; i < length; i++) vector[i] = f();
            return new Vector(vector);
        }

        /// <summary>
        /// Create a vector on the basis of the given function.
        /// </summary>
        /// <param name="length">The length of the vector to create.</param>
        /// <param name="f">The function generating the vector elements with index argument.</param>
        public static Vector Create(int length, Func<int, double> f)
        {
            var vector = new double[length];
            for (var i = 0; i < length; i++) vector[i] = f(i);
            return new Vector(vector);
        }

        /// <summary>
        /// Returns the squared norm of the vector.
        /// </summary>
        /// <returns>The norm square.</returns>
        public double GetNormSquare()
        {
            var result = 0.0;
            for (var i = 0; i < this.dimension; i++) result += this.vector[i] * this.vector[i];
            return result;
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        /// <seealso cref="VectorExtensions.Normalized"/>
        public void Normalize()
        {
            var norm = this.Norm();
            if (EpsilonExtensions.IsZero(norm)) throw new Exception("The given vector has zero length.");
            for (var i = 0; i < this.dimension; i++) this.vector[i] /= norm;
        }

        public Vector GetUnitVector()
        {
            var result = new Vector(this.vector);
            result.Normalize();
            return result;
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            if (v1.dimension != 3) throw new Exception("Vector v1 must be 3 dimensional!");
            if (v2.dimension != 3) throw new Exception("Vector v2 must be 3 dimensional!");
            var result = new Vector(3);
            result[0] = v1[1] * v2[2] - v1[2] * v2[1];
            result[1] = v1[2] * v2[0] - v1[0] * v2[2];
            result[2] = v1[0] * v2[1] - v1[1] * v2[0];
            return result;
        }

        /// <summary>
        /// Returns the (standard Euclidean) norm of the given vector.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        public static double Norm(Vector x)
        {
            return Norm(x, 2);
        }

        /// <summary>
        /// Returns the sum of the elements aka trace of this vector.
        /// </summary>
        /// <param name="v">V.</param>
        public double Sum()
        {
            return this.ToArray().Sum();
        }

        /// <summary>
        /// Returns this vector as an array of its elements.
        /// </summary>
        /// <returns>The vector as an array of doubles.</returns>
        public double[] ToArray()
        {
            var toReturn = new double[this.Dimension];
            for (var i = 0; i < this.Dimension; i++) toReturn[i] = this[i];
            return toReturn;
        }

        /// <summary>
        /// Returns the Chebyshev p-norm of the given vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <param name="p">The order parameter.</param>
        /// <seealso cref="http://en.wikipedia.org/wiki/Chebyshev_distance"/>
        public static double Norm(Vector v, double p)
        {
            if (p < 1) throw new InvalidOperationException("The p-norm is not defined for non-positive values of p.");
            double value = 0;
            if (p == 1)
            {
                for (var i = 0; i < v.Dimension; i++) value += Math.Abs(v[i]);

                return value;
            }
            else if (p == int.MaxValue)
            {
                for (var i = 0; i < v.Dimension; i++) if (Math.Abs(v[i]) > value) value = Math.Abs(v[i]);
                return value;
            }
            else if (p == int.MinValue)
            {
                for (var i = 0; i < v.Dimension; i++) if (Math.Abs(v[i]) < value) value = Math.Abs(v[i]);
                return value;
            }
            else
            {
                for (var i = 0; i < v.Dimension; i++) value += Math.Pow(Math.Abs(v[i]), p);

                return Math.Pow(value, 1 / p);
            }
        }

        /// <summary>
        /// Returns a vector of the given dimension with all entries equal to zero.
        /// </summary>
        /// <param name="n">The dimension of the vector.</param>
        public static Vector Zeros(int n)
        {
            return new Vector(n);
        }

        /// <summary>
        /// Returns a vector of the given dimension with all entries equal to one.
        /// </summary>
        /// <param name="n">The dimension of the vector.</param>
        public static Vector Ones(int n)
        {
            return new Vector(n, 1.0);
        }

        /// <summary>Returns the index of the maximum value.</summary>
        /// <param name="startAt">The position to start looking for the maximum.</param>
        /// <returns>An int.</returns>
        public int MaxIndex(int startAt = 0)
        {
            var idx = startAt;
            double val = 0;
            for (var i = startAt; i < this.Dimension; i++)
            {
                if (val < this[i])
                {
                    idx = i;
                    val = this[i];
                }
            }

            return idx;
        }

        public RMatrix ToMatrix(VectorType t)
        {
            if (t == VectorType.Row)
            {
                var m = new RMatrix(1, this.Dimension);
                for (var j = 0; j < this.Dimension; j++) m[0, j] = this[j];

                return m;
            }
            else
            {
                var m = new RMatrix(this.Dimension, 1);
                for (var i = 0; i < this.Dimension; i++) m[i, 0] = this[i];

                return m;
            }
        }
    }
}