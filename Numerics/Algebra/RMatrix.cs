using System;
using System.Collections.Generic;
using System.Globalization;

namespace Orbifold.Numerics
{
    /// <summary>
    ///     A real-valued matrix.
    /// </summary>
    public struct RMatrix : ICloneable
    {
        private readonly int colCount;

        private readonly int rowCount;

        private bool isTransposed;

        private double[,] matrix;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="rowCount">Row count.</param>
        /// <param name="colCount">Col count.</param>
        public RMatrix(int rowCount, int colCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.matrix = new double[rowCount, colCount];
            this.isTransposed = false;
            for (var i = 0; i < rowCount; i++) for (var j = 0; j < colCount; j++) this.matrix[i, j] = 0.0;
        }

        public RMatrix(int size)
            : this(size, size)
        {
        }

        public RMatrix(IReadOnlyList<double> a, IReadOnlyList<double> b)
        {
            this.matrix = new double[a.Count, 2];
            this.isTransposed = false;
            for (var i = 0; i < a.Count; i++)
            {
                this.matrix[i, 0] = a[i];
                this.matrix[i, 1] = b[i];
            }
            this.rowCount = a.Count;
            this.colCount = 2;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="matrix">A matrix specified as doubles.</param>
        /// <remarks>
        ///     Note that the notation {{1,2},{3,4}} represents a matrix
        ///     | 1  2|
        ///     | 3  4|
        ///     and so on. It maps identically to the Mathematica notation.
        /// </remarks>
        public RMatrix(double[,] matrix)
        {
            this.matrix = matrix;
            this.rowCount = matrix.GetLength(0);
            this.colCount = matrix.GetLength(1);
            this.isTransposed = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="m">M.</param>
        public RMatrix(RMatrix m)
        {
            this.rowCount = m.RowCount;
            this.colCount = m.ColumnCount;
            this.matrix = m.matrix;
            this.isTransposed = false;
        }

        public RMatrix T
        {
            get
            {
                return new RMatrix(this.colCount, this.rowCount)
                           {
                               isTransposed = true,
                               matrix = this.matrix
                           };
            }
        }

        public double this[int m, int n]
        {
            get
            {
                if (this.isTransposed)
                {
                    if (m < 0 || m > this.ColumnCount) throw new Exception("m-th row is out of range!");
                    if (n < 0 || n > this.RowCount) throw new Exception("n-th col is out of range!");
                    return this.matrix[n, m];
                }
                if (m < 0 || m > this.RowCount) throw new Exception("m-th row is out of range!");
                if (n < 0 || n > this.ColumnCount) throw new Exception("n-th col is out of range!");
                return this.matrix[m, n];
            }
            set
            {
                if (this.isTransposed) throw new Exception("The transosed matrix is read-only.");
                this.matrix[m, n] = value;
            }
        }

        public Vector this[int i]
        {
            get
            {
                return this[i, VectorType.Row];
            }
            set
            {
                this[i, VectorType.Row] = value;
            }
        }

        public Vector this[int i, VectorType t]
        {
            get
            {
                // switch it up if using a transposed version
                if (this.isTransposed) t = t == VectorType.Row ? VectorType.Col : VectorType.Row;

                if (t == VectorType.Row)
                {
                    if (i >= this.RowCount) throw new IndexOutOfRangeException();

                    return new Vector(this.matrix, i, true);
                }
                if (i >= this.ColumnCount) throw new IndexOutOfRangeException();

                return new Vector(this.matrix, i);
            }
            set
            {
                if (this.isTransposed) throw new InvalidOperationException("Cannot modify matrix in read-only transpose mode!");

                if (t == VectorType.Row)
                {
                    if (i >= this.RowCount) throw new IndexOutOfRangeException();

                    if (value.Dimension > this.ColumnCount) throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} columns available.", this.ColumnCount));

                    for (var k = 0; k < this.ColumnCount; k++) this.matrix[i, k] = value[k];
                }
                else
                {
                    if (i >= this.ColumnCount) throw new IndexOutOfRangeException();

                    if (value.Dimension > this.RowCount) throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} rows available.", this.RowCount));

                    for (var k = 0; k < this.RowCount; k++) this.matrix[k, i] = value[k];
                }
            }
        }

        /// <summary>
        ///     Gets the number or rows in this matrix.
        /// </summary>
        /// <value>The row count.</value>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        ///     Gets the number or columns in this matrix.
        /// </summary>
        /// <value>The column count.</value>
        public int ColumnCount
        {
            get
            {
                return this.colCount;
            }
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public IEnumerable<Vector> GetRows()
        {
            for (var i = 0; i < this.RowCount; i++) yield return this[i, VectorType.Row];
        }

        public IEnumerable<Vector> GetCols()
        {
            for (var i = 0; i < this.ColumnCount; i++) yield return this[i, VectorType.Col];
        }

        public Vector Col(int i)
        {
            return this[i, VectorType.Col];
        }

        public Vector Row(int i)
        {
            return this[i, VectorType.Row];
        }

        public static RMatrix Identity(int size)
        {
            return Identity(size, size);
        }

        public static RMatrix Identity(int n, int d)
        {
            var m = new double[n, d];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < d; j++)
                {
                    if (i == j) m[i, j] = 1;
                    else m[i, j] = 0;
                }
            }

            return new RMatrix(n, d)
                       {
                           matrix = m,
                           isTransposed = false
                       };
        }

        public RMatrix IdentityMatrix()
        {
            var m = new RMatrix(this.rowCount, this.colCount);
            for (var i = 0; i < this.rowCount; i++) for (var j = 0; j < this.colCount; j++) if (i == j) m[i, j] = 1;
            return m;
        }

        public static implicit operator RMatrix(double[,] m)
        {
            return new RMatrix(m);
        }

        public RMatrix Center(VectorType t)
        {
            var max = t == VectorType.Row ? this.RowCount : this.ColumnCount;
            for (var i = 0; i < max; i++) this[i, t] -= this[i, t].Mean();
            return this;
        }

        public void Normalize(VectorType t)
        {
            var max = t == VectorType.Row ? this.RowCount : this.ColumnCount;
            for (var i = 0; i < max; i++) this[i, t] /= this[i, t].Norm();
        }

        /// <summary>
        ///     Returns a clone of this matrix.
        /// </summary>
        public RMatrix Clone()
        {
            return new RMatrix(this.matrix)
                       {
                           matrix = (double[,])this.matrix.Clone()
                       };
        }

        public Vector ToVector()
        {
            if (this.RowCount == 1) return this[0, VectorType.Row].Clone();

            if (this.ColumnCount == 1) return this[0, VectorType.Col].Clone();

            throw new InvalidOperationException("Only one-dimensional matrices can be converted to a vector.");
        }

        public override string ToString()
        {
            var strMatrix = "(";
            for (var i = 0; i < this.rowCount; i++)
            {
                var str = "";
                for (var j = 0; j < this.colCount - 1; j++) str += this.matrix[i, j].ToString(CultureInfo.InvariantCulture) + ", ";
                str += this.matrix[i, this.colCount - 1].ToString(CultureInfo.InvariantCulture);
                if (i != this.rowCount - 1 && i == 0) strMatrix += str + "\n";
                else if (i != this.rowCount - 1 && i != 0) strMatrix += " " + str + "\n";
                else strMatrix += " " + str + ")";
            }
            return strMatrix;
        }

        public override bool Equals(object obj)
        {
            return (obj is RMatrix) && this.Equals((RMatrix)obj);
        }

        public bool Equals(RMatrix m)
        {
            return Utils.ArraysEqual(this.matrix, m.matrix);
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }

        public static Vector Dot(RMatrix x, Vector v)
        {
            if (v.Dimension != x.ColumnCount) throw new InvalidOperationException("objects are not aligned");

            var toReturn = Vector.Zeros(x.RowCount);
            for (var i = 0; i < toReturn.Dimension; i++) toReturn[i] = Vector.Dot(x[i, VectorType.Row], v);
            return toReturn;
        }

        /// <summary>Dot product between a matrix and a vector.</summary>
        /// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /// <param name="v">Vector v.</param>
        /// <param name="x">Matrix x.</param>
        /// <returns>Vector dot product.</returns>
        public static Vector Dot(Vector v, RMatrix x)
        {
            if (v.Dimension != x.RowCount) throw new InvalidOperationException("objects are not aligned");

            var toReturn = Vector.Zeros(x.ColumnCount);
            for (var i = 0; i < toReturn.Dimension; i++) toReturn[i] = Vector.Dot(x[i, VectorType.Col], v);
            return toReturn;
        }

        public static RMatrix operator *(RMatrix m, Vector v)
        {
            var ans = Dot(m, v);
            return ans.ToMatrix(VectorType.Col);
        }

        /// <summary>Multiplication operator.</summary>
        /// <param name="v">The Vector to process.</param>
        /// <param name="m">The Matrix to process.</param>
        /// <returns>The result of the operation.</returns>
        public static RMatrix operator *(Vector v, RMatrix m)
        {
            var ans = Dot(v, m);
            return ans.ToMatrix(VectorType.Row);
        }

        public static bool operator ==(RMatrix m1, RMatrix m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(RMatrix m1, RMatrix m2)
        {
            return !m1.Equals(m2);
        }

        public static RMatrix operator +(RMatrix m)
        {
            return m;
        }


        public static RMatrix operator +(RMatrix m1, RMatrix m2)
        {
            if (!CompareDimension(m1, m2)) throw new Exception("The dimensions of two matrices must be the same!");
            var result = new RMatrix(m1.RowCount, m1.ColumnCount);
            for (var i = 0; i < m1.RowCount; i++) for (var j = 0; j < m1.ColumnCount; j++) result[i, j] = m1[i, j] + m2[i, j];
            return result;
        }

        public static RMatrix operator -(RMatrix m)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) 
                for (var j = 0; j < m.ColumnCount; j++)
                    result[i, j] *= -1;
            return result;
        }

        public static RMatrix operator -(RMatrix m1, RMatrix m2)
        {
            if (!CompareDimension(m1, m2)) throw new Exception("The dimensions of two matrices must be the same!");
            var result = new RMatrix(m1.RowCount, m1.ColumnCount);
            for (var i = 0; i < m1.RowCount; i++) for (var j = 0; j < m1.ColumnCount; j++) result[i, j] = m1[i, j] - m2[i, j];
            return result;
        }

        public static RMatrix operator *(RMatrix m, double d)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] *= d;
            return result;
        }

        public static RMatrix operator *(double d, RMatrix m)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] *= d;
            return result;
        }

        public static RMatrix operator /(RMatrix m, double d)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] /= d;
            return result;
        }

        public static RMatrix operator /(RMatrix m, RMatrix q)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] /= q[i, j];
            return result;
        }

        /// <summary>
        /// The entry-by-entry multiplication.
        /// See the <see cref="MatrixExtensions.Times"/> method if you need the standard matrix multiplication.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static RMatrix operator *(RMatrix m, RMatrix q)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] *= q[i, j];
            return result;
        }

        public static RMatrix operator +(RMatrix m, double d)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] += d;
            return result;
        }

        public static RMatrix operator +(double d, RMatrix m)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] += d;
            return result;
        }

        public static RMatrix operator -(RMatrix m, double d)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] -= d;
            return result;
        }

        public static RMatrix operator -(double d, RMatrix m)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] += d;
            return result;
        }

        public static RMatrix operator /(double d, RMatrix m)
        {
            var result = new RMatrix(m.RowCount, m.ColumnCount);
            for (var i = 0; i < m.RowCount; i++)
            {
                for (var j = 0; j < m.ColumnCount; j++)
                {
                    // TODO: need to check the almost zero deivision here
                    result[i, j] = d / m[i, j];
                }
            }
            return result;
        }



        public static RMatrix operator ^(RMatrix m, double d)
        {
            var result = m.Clone();
            for (var i = 0; i < m.RowCount; i++) for (var j = 0; j < m.ColumnCount; j++) result[i, j] = Math.Pow(result[i, j], d);
            return result;
        }

        public RMatrix GetTranspose()
        {
            var m = this;
            m.Transpose();
            return m;
        }

        public void Transpose()
        {
            var m = new RMatrix(this.colCount, this.rowCount);
            for (var i = 0; i < this.rowCount; i++) for (var j = 0; j < this.colCount; j++) m[j, i] = this.matrix[i, j];
            this = m;
        }

        public double GetTrace()
        {
            var sumOfDiag = 0.0;
            for (var i = 0; i < this.rowCount; i++) if (i < this.colCount) sumOfDiag += this.matrix[i, i];
            return sumOfDiag;
        }

        public bool IsSquared()
        {
            return this.rowCount == this.colCount;
        }

        public static bool CompareDimension(RMatrix m1, RMatrix m2)
        {
            return m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount;
        }

        public Vector GetRowVector(int m)
        {
            if (m < 0 || m > this.rowCount) throw new Exception("m-th row is out of range!");
            var rowVector = new Vector(this.colCount);
            for (var i = 0; i < this.colCount; i++) rowVector[i] = this.matrix[m, i];
            return rowVector;
        }

        public Vector GetColVector(int n)
        {
            if (n < 0 || n > this.colCount) throw new Exception("n-th col is out of range!");
            var colVector = new Vector(this.rowCount);
            for (var i = 0; i < this.rowCount; i++) colVector[i] = this.matrix[i, n];
            return colVector;
        }

        public RMatrix ReplaceRow(Vector vec, int m)
        {
            if (m < 0 || m > this.rowCount) throw new Exception("m-th row is out of range!");
            if (vec.Dimension != this.colCount) throw new Exception("Vector ndim is out of range!");
            for (var i = 0; i < this.colCount; i++) this.matrix[m, i] = vec[i];
            return new RMatrix(this.matrix);
        }

        public RMatrix ReplaceCol(Vector vec, int n)
        {
            if (n < 0 || n > this.colCount) throw new Exception("n-th col is out of range!");
            if (vec.Dimension != this.rowCount) throw new Exception("Vector ndim is out of range!");
            for (var i = 0; i < this.rowCount; i++) this.matrix[i, n] = vec[i];
            return new RMatrix(this.matrix);
        }

        public RMatrix SwapRMatrixow(int m, int n)
        {
            for (var i = 0; i < this.colCount; i++)
            {
                var temp = this.matrix[m, i];
                this.matrix[m, i] = this.matrix[n, i];
                this.matrix[n, i] = temp;
            }
            return new RMatrix(this.matrix);
        }

        public RMatrix SwapMatrixColumn(int m, int n)
        {
            for (var i = 0; i < this.rowCount; i++)
            {
                var temp = this.matrix[i, m];
                this.matrix[i, m] = this.matrix[i, n];
                this.matrix[i, n] = temp;
            }
            return new RMatrix(this.matrix);
        }

        public static Vector StdDev(RMatrix source, VectorType t = VectorType.Col)
        {
            var count = t == VectorType.Row ? source.ColumnCount : source.RowCount;
            var type = t == VectorType.Row ? VectorType.Col : VectorType.Row;
            var v = new Vector(count);
            for (var i = 0; i < count; i++) v[i] = source[i, type].StandardDeviation();
            return v;
        }

        public static Vector Transform(RMatrix mat, Vector vec)
        {
            var result = new Vector(vec.Dimension);
            if (!mat.IsSquared()) throw new Exception("The matrix must be squared!");
            if (mat.ColumnCount != vec.Dimension) throw new Exception("The ndim of the vector must be equal " + "to the number of cols of the matrix!");
            for (var i = 0; i < mat.RowCount; i++)
            {
                result[i] = 0.0;
                for (var j = 0; j < mat.ColumnCount; j++) result[i] += mat[i, j] * vec[j];
            }
            return result;
        }

        public static Vector Transform(Vector vec, RMatrix mat)
        {
            var result = new Vector(vec.Dimension);
            if (!mat.IsSquared()) throw new Exception("The matrix must be squared!");
            if (mat.RowCount != vec.Dimension) throw new Exception("The ndim of the vector must be equal " + "to the number of rows of the matrix!");
            for (var i = 0; i < mat.RowCount; i++)
            {
                result[i] = 0.0;
                for (var j = 0; j < mat.ColumnCount; j++) result[i] += vec[j] * mat[j, i];
            }
            return result;
        }

        public static RMatrix Transform(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension) throw new Exception("The vectors must have the same ndim!");
            var result = new RMatrix(v1.Dimension, v1.Dimension);
            for (var i = 0; i < v1.Dimension; i++) for (var j = 0; j < v1.Dimension; j++) result[j, i] = v1[i] * v2[j];
            return result;
        }

        public static double Determinant(RMatrix mat)
        {
            var result = 0.0;
            if (!mat.IsSquared()) throw new Exception("The matrix must be squared!");
            if (mat.RowCount == 1) result = mat[0, 0];
            else for (var i = 0; i < mat.RowCount; i++) result += Math.Pow(-1, i) * mat[0, i] * Determinant(Minor(mat, 0, i));
            return result;
        }

        public static RMatrix Minor(RMatrix mat, int row, int col)
        {
            var mm = new RMatrix(mat.RowCount - 1, mat.ColumnCount - 1);
            var ii = 0;
            for (var i = 0; i < mat.RowCount; i++)
            {
                if (i == row) continue;
                var jj = 0;
                for (var j = 0; j < mat.ColumnCount; j++)
                {
                    if (j == col) continue;
                    mm[ii, jj] = mat[i, j];
                    jj++;
                }
                ii++;
            }
            return mm;
        }

        public static RMatrix Adjoint(RMatrix mat)
        {
            if (!mat.IsSquared()) throw new Exception("The matrix must be squared!");
            var ma = new RMatrix(mat.RowCount, mat.ColumnCount);
            for (var i = 0; i < mat.RowCount; i++) for (var j = 0; j < mat.ColumnCount; j++) ma[i, j] = Math.Pow(-1, i + j) * (Determinant(Minor(mat, i, j)));
            return ma.GetTranspose();
        }

        public static RMatrix Inverse(RMatrix mat)
        {
            if (Math.Abs(Determinant(mat)) < Constants.Epsilon) throw new Exception("Cannot inverse a matrix with a zero determinant!");
            return (Adjoint(mat) / Determinant(mat));
        }

        /// <summary>
        ///     Returns the zero matrixx of specified square dimensions.
        /// </summary>
        /// <param name="n">The number of rows/columns.</param>
        public static RMatrix Zeros(int n)
        {
            return new RMatrix(n, n);
        }

        /// <summary>
        ///     Returns the zero matrixx of specified dimensions.
        /// </summary>
        /// <param name="m">The number of rows.</param>
        /// <param name="n">The number of columns.</param>
        public static RMatrix Zeros(int m, int n)
        {
            return new RMatrix(m, n);
        }

        public static RMatrix Create(int n, Func<double> f)
        {
            return Create(n, n, f);
        }

        /// <summary>Creates a new Matrix.</summary>
        /// <param name="n">Size.</param>
        /// <param name="d">cols.</param>
        /// <param name="f">The Func&lt;int,int,double&gt; to process.</param>
        /// <returns>A Matrix.</returns>
        public static RMatrix Create(int n, int d, Func<double> f)
        {
            var matrix = new RMatrix(n, d);
            for (var i = 0; i < matrix.RowCount; i++) for (var j = 0; j < matrix.ColumnCount; j++) matrix[i, j] = f();
            return matrix;
        }

        /// <summary>Creates a new Matrix.</summary>
        /// <param name="n">Size.</param>
        /// <param name="f">The Func&lt;int,int,double&gt; to process.</param>
        /// <returns>A Matrix.</returns>
        public static RMatrix Create(int n, Func<int, int, double> f)
        {
            return Create(n, n, f);
        }

        /// <summary>Creates a new Matrix.</summary>
        /// <param name="n">Size.</param>
        /// <param name="d">cols.</param>
        /// <param name="f">The Func&lt;int,int,double&gt; to process.</param>
        /// <returns>A Matrix.</returns>
        public static RMatrix Create(int n, int d, Func<int, int, double> f)
        {
            var matrix = new RMatrix(n, d);
            for (var i = 0; i < matrix.RowCount; i++) for (var j = 0; j < matrix.ColumnCount; j++) matrix[i, j] = f(i, j);
            return matrix;
        }

        /// <summary>
        /// Returns a random matrix.
        /// </summary>
        /// <param name="rows">The row count.</param>
        /// <param name="columns">The column count.</param>
        /// <param name="min">The minimum value of the entries.</param>
        /// <param name="max">The maximum value of the entries</param>
        /// <param name="decimals">How many decimals should be taken.</param>
        public static RMatrix Random(int rows = 5, int columns = 5, double min = 0, double max = 100d, int decimals = 0)
        {
            if (decimals < 0) throw new Exception("The decimals parameter should be a positive integer.");
            var m = new double[rows, columns];
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < columns; j++)
                {
                    if (decimals == 0)
                    {
                        m[i, j] = (int)Math.Round(max * MultiplyWithCarryRandomGenerator.GetUniform() + min);

                    }
                    else
                    {
                        var p = Math.Pow(10, decimals);
                        m[i, j] = Math.Round((max * MultiplyWithCarryRandomGenerator.GetUniform() + min) * p) / p;
                    }
                }
            return new RMatrix(rows, columns)
                       {
                           matrix = m,
                           isTransposed = false
                       };
        }

        public static RMatrix Random(int order, double min = 0)
        {
            return Random(order, order, min, 100d, 2);
        }

        public static RMatrix Diag(Vector v)
        {
            var m = Zeros(v.Dimension);
            for (var i = 0; i < v.Dimension; i++) m[i, i] = v[i];
            return m;
        }

        public bool Equals(RMatrix m, double tolerance)
        {
            if (this.RowCount != m.RowCount || this.ColumnCount != m.ColumnCount) return false;

            for (var i = 0; i < this.RowCount; i++) for (var j = 0; j < this.ColumnCount; j++) if (Math.Abs(this[i, j] - m[i, j]) > tolerance) return false;
            return true;
        }


    }
}