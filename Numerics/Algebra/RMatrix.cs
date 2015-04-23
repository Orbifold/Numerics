using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A real-valued matrix.
	/// </summary>
	public struct RMatrix	: ICloneable
	{
		private readonly int rowCount;
		private readonly int colCount;
		private double[,] matrix;
		private bool isTransposed;

		public IEnumerable<Vector> GetRows()
		{
			for(int i = 0; i < RowCount; i++)
				yield return this[i, VectorType.Row];
		}

		public IEnumerable<Vector> GetCols()
		{
			for(int i = 0; i < ColumnCount; i++)
				yield return this[i, VectorType.Col];
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
			for(int i = 0; i < n; i++) {
				for(int j = 0; j < d; j++)
					if(i == j)
						m[i, j] = 1;
					else
						m[i, j] = 0;
			}

			return new RMatrix(n, d) {
				matrix = m, isTransposed = false
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix"/> struct.
		/// </summary>
		/// <param name="rowCount">Row count.</param>
		/// <param name="colCount">Col count.</param>
		public RMatrix(int rowCount, int colCount)
		{
			this.rowCount = rowCount;
			this.colCount = colCount;
			matrix = new double[rowCount, colCount];
			isTransposed = false;
			for(var i = 0; i < rowCount; i++)
				for(var j = 0; j < colCount; j++)
					matrix[i, j] = 0.0;
		}

		public RMatrix(int size) : this(size, size)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix"/> struct.
		/// </summary>
		/// <param name="matrix">A matrix specified as doubles.</param>
		/// <remarks>
		/// Note that the notation {{1,2},{3,4}} represents a matrix
		/// | 1  2|
		/// | 3  4|
		/// and so on. It maps identically to the Mathematica notation.
		/// </remarks>
		public RMatrix(double[,] matrix)
		{
			rowCount = matrix.GetLength(0);
			colCount = matrix.GetLength(1);
			this.matrix = matrix;
			isTransposed = false;
		}

	
		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix"/> struct.
		/// </summary>
		/// <param name="m">M.</param>
		public RMatrix(RMatrix m)
		{
			rowCount = m.RowCount;
			colCount = m.ColumnCount;
			matrix = m.matrix;
			isTransposed = false;
		}

		public RMatrix IdentityMatrix()
		{
			var m = new RMatrix(rowCount, colCount);
			for(var i = 0; i < rowCount; i++)
				for(var j = 0; j < colCount; j++)
					if(i == j)
						m[i, j] = 1;
			return m;
		}

		public RMatrix T {
			get {
				return new RMatrix(colCount, rowCount) {
					isTransposed = true,
					matrix = this.matrix
				};
			}
		}

		public double this[int m, int n] {
			get {
				if(isTransposed) {
					if(m < 0 || m > ColumnCount)
						throw new Exception("m-th row is out of range!");
					if(n < 0 || n > RowCount)
						throw new Exception("n-th col is out of range!");
					return matrix[n, m];
				} else {
					if(m < 0 || m > RowCount)
						throw new Exception("m-th row is out of range!");
					if(n < 0 || n > ColumnCount)
						throw new Exception("n-th col is out of range!");
					return matrix[m, n];
				}
				 
			}
			set { 
				if(isTransposed)
					throw new Exception("The transosed matrix is read-only.");
				matrix[m, n] = value; 
			}
		}

		public static implicit operator RMatrix(double[,] m)
		{
			return new RMatrix(m);
		}



		public RMatrix Center(VectorType t)
		{
			int max = t == VectorType.Row ? RowCount : ColumnCount;
			for(int i = 0; i < max; i++)
				this[i, t] -= this[i, t].Mean();
			return this;
		}

		public void Normalize(VectorType t)
		{
			int max = t == VectorType.Row ? this.RowCount : this.ColumnCount;
			for(int i = 0; i < max; i++)
				this[i, t] /= this[i, t].Norm();
		}

		public  Vector this[int i] {
			get {
				return this[i, VectorType.Row];
			}
			set {
				this[i, VectorType.Row] = value;
			}
		}

		public  Vector this[int i, VectorType t] {
			get {
				// switch it up if using a transposed version
				if(isTransposed)
					t = t == VectorType.Row ? VectorType.Col : VectorType.Row;


				if(t == VectorType.Row) {
					if(i >= RowCount)
						throw new IndexOutOfRangeException();

					return new Vector(matrix, i, true);
				} else {
					if(i >= ColumnCount)
						throw new IndexOutOfRangeException();

					return new Vector(matrix, i);
				}
			}
			set {
				if(isTransposed)
					throw new InvalidOperationException("Cannot modify matrix in read-only transpose mode!");

				if(t == VectorType.Row) {
					if(i >= RowCount)
						throw new IndexOutOfRangeException();

					if(value.Dimension > ColumnCount)
						throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} columns available.", ColumnCount));

					for(int k = 0; k < ColumnCount; k++)
						matrix[i, k] = value[k];
				} else {
					if(i >= ColumnCount)
						throw new IndexOutOfRangeException();

					if(value.Dimension > RowCount)
						throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} rows available.", RowCount));


					for(int k = 0; k < RowCount; k++)
						matrix[k, i] = value[k];
				}
			}
		}

		/// <summary>
		/// Gets the number or rows in this matrix.
		/// </summary>
		/// <value>The row count.</value>
		public int RowCount {
			get { return rowCount; }
		}

		/// <summary>
		/// Gets the number or columns in this matrix.
		/// </summary>
		/// <value>The column count.</value>
		public int ColumnCount {
			get { return colCount; }
		}

		/// <summary>
		/// Returns a clone of this matrix.
		/// </summary>
		public RMatrix Clone()
		{
			return new RMatrix(matrix) { matrix = (double[,])matrix.Clone() };
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		object ICloneable.Clone()
		{
			return this.Clone();
		}
        public Vector ToVector()
        {
            if (this.RowCount == 1)
                return this[0, VectorType.Row].Clone();

            if (this.ColumnCount == 1)
                return this[0, VectorType.Col].Clone();

            throw new InvalidOperationException("Only one-dimensional matrices can be converted to a vector.");
        }
		public override string ToString()
		{
			var strMatrix = "(";
			for(var i = 0; i < rowCount; i++) {
				var str = "";
				for(var j = 0; j < colCount - 1; j++) {
					str += matrix[i, j].ToString(CultureInfo.InvariantCulture) + ", ";
				}
				str += matrix[i, colCount - 1].ToString(CultureInfo.InvariantCulture);
				if(i != rowCount - 1 && i == 0)
					strMatrix += str + "\n";
				else if(i != rowCount - 1 && i != 0)
					strMatrix += " " + str + "\n";
				else
					strMatrix += " " + str + ")";
			}
			return strMatrix;
		}

		public override bool Equals(object obj)
		{
			return (obj is RMatrix) && Equals((RMatrix)obj);
		}

		public bool Equals(RMatrix m)
		{
			return Utils.ArraysEqual(matrix, m.matrix);
		}

		public override int GetHashCode()
		{
			return matrix.GetHashCode();
		}

		public static Vector Dot(RMatrix x, Vector v)
		{
			if(v.Dimension != x.ColumnCount)
				throw new InvalidOperationException("objects are not aligned");

			Vector toReturn = Vector.Zeros(x.RowCount);
			for(int i = 0; i < toReturn.Dimension; i++)
				toReturn[i] = Vector.Dot(x[i, VectorType.Row], v);
			return toReturn;
		}

		/// <summary>Dot product between a matrix and a vector.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <param name="v">Vector v.</param>
		/// <param name="x">Matrix x.</param>
		/// <returns>Vector dot product.</returns>
		public static Vector Dot(Vector v, RMatrix x)
		{
			if(v.Dimension != x.RowCount)
				throw new InvalidOperationException("objects are not aligned");

			Vector toReturn = Vector.Zeros(x.ColumnCount);
			for(int i = 0; i < toReturn.Dimension; i++)
				toReturn[i] = Vector.Dot(x[i, VectorType.Col], v);
			return toReturn;
		}

		public static RMatrix operator *(RMatrix m, Vector v)
		{
			var ans = RMatrix.Dot(m, v);
			return ans.ToMatrix(VectorType.Col);
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="v">The Vector to process.</param>
		/// <param name="m">The Matrix to process.</param>
		/// <returns>The result of the operation.</returns>
		public static RMatrix operator *(Vector v, RMatrix m)
		{
			var ans = RMatrix.Dot(v, m);
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
			if(!CompareDimension(m1, m2)) {
				throw new Exception("The dimensions of two matrices must be the same!");
			}
			var result = new RMatrix(m1.RowCount, m1.ColumnCount);
			for(var i = 0; i < m1.RowCount; i++) {
				for(var j = 0; j < m1.ColumnCount; j++)
					result[i, j] = m1[i, j] + m2[i, j];
			}
			return result;
		}

		public static RMatrix operator -(RMatrix m)
		{
			for(var i = 0; i < m.RowCount; i++) {
				for(var j = 0; j < m.ColumnCount; j++) {
					m[i, j] = -m[i, j];
				}
			}
			return m;
		}

		public static RMatrix operator -(RMatrix m1, RMatrix m2)
		{
			if(!CompareDimension(m1, m2)) {
				throw new Exception("The dimensions of two matrices must be the same!");
			}
			var result = new RMatrix(m1.RowCount, m1.ColumnCount);
			for(var i = 0; i < m1.RowCount; i++) {
				for(var j = 0; j < m1.ColumnCount; j++) {
					result[i, j] = m1[i, j] - m2[i, j];
				}
			}
			return result;
		}

		public static RMatrix operator *(RMatrix m, double d)
		{
			var result = new RMatrix(m.RowCount, m.ColumnCount);
			for(var i = 0; i < m.RowCount; i++) {
				for(var j = 0; j < m.ColumnCount; j++) {
					result[i, j] = m[i, j] * d;
				}
			}
			return result;
		}

		public static RMatrix operator *(double d, RMatrix m)
		{
			var result = new RMatrix(m.RowCount, m.ColumnCount);
			for(var i = 0; i < m.RowCount; i++) {
				for(var j = 0; j < m.ColumnCount; j++) {
					result[i, j] = m[i, j] * d;
				}
			}
			return result;
		}

		public static RMatrix operator /(RMatrix m, double d)
		{
			var result = new RMatrix(m.RowCount, m.ColumnCount);
			for(var i = 0; i < m.RowCount; i++) {
				for(var j = 0; j < m.ColumnCount; j++) {
					result[i, j] = m[i, j] / d;
				}
			}
			return result;
		}

		public static RMatrix operator /(double d, RMatrix m)
		{
			var result = new RMatrix(m.RowCount, m.ColumnCount);
			for(var i = 0; i < m.RowCount; i++) {
				for(var j = 0; j < m.ColumnCount; j++) {
					result[i, j] = m[i, j] / d;
				}
			}
			return result;
		}

		public static RMatrix operator *(RMatrix m1, RMatrix m2)
		{
			if(m1.ColumnCount != m2.RowCount) {
				throw new Exception("The numbers of columns of the" +
				" first matrix must be equal to the number of " +
				" rows of the second matrix!");
			}
			var result = new RMatrix(m1.RowCount, m2.ColumnCount);
			for(var i = 0; i < m1.RowCount; i++) {
				for(var j = 0; j < m2.ColumnCount; j++) {
					var tmp = result[i, j];
					for(var k = 0; k < result.RowCount; k++)
						tmp += m1[i, k] * m2[k, j];
					result[i, j] = tmp;
				}
			}
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
			var m = new RMatrix(colCount, rowCount);
			for(var i = 0; i < rowCount; i++) {
				for(var j = 0; j < colCount; j++) {
					m[j, i] = matrix[i, j];
				}
			}
			this = m;
		}

		public double GetTrace()
		{
			var sumOfDiag = 0.0;
			for(var i = 0; i < rowCount; i++) {
				if(i < colCount)
					sumOfDiag += matrix[i, i];
			}
			return sumOfDiag;
		}

		public bool IsSquared()
		{
			return rowCount == colCount;
		}

		public static bool CompareDimension(RMatrix m1, RMatrix m2)
		{
			return m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount;
		}

		public Vector GetRowVector(int m)
		{
			if(m < 0 || m > rowCount)
				throw new Exception("m-th row is out of range!");
			var rowVector = new Vector(colCount);
			for(var i = 0; i < colCount; i++)
				rowVector[i] = matrix[m, i];
			return rowVector;
		}

		public Vector GetColVector(int n)
		{
			if(n < 0 || n > colCount)
				throw new Exception("n-th col is out of range!");
			var colVector = new Vector(rowCount);
			for(var i = 0; i < rowCount; i++)
				colVector[i] = matrix[i, n];
			return colVector;
		}

		public RMatrix ReplaceRow(Vector vec, int m)
		{
			if(m < 0 || m > rowCount)
				throw new Exception("m-th row is out of range!");
			if(vec.Dimension != colCount)
				throw new Exception("Vector ndim is out of range!");
			for(var i = 0; i < colCount; i++)
				matrix[m, i] = vec[i];
			return new RMatrix(matrix);
		}

		public RMatrix ReplaceCol(Vector vec, int n)
		{
			if(n < 0 || n > colCount)
				throw new Exception("n-th col is out of range!");
			if(vec.Dimension != rowCount)
				throw new Exception("Vector ndim is out of range!");
			for(var i = 0; i < rowCount; i++)
				matrix[i, n] = vec[i];
			return new RMatrix(matrix);
		}

		public RMatrix SwapRMatrixow(int m, int n)
		{
			for(var i = 0; i < colCount; i++) {
				var temp = matrix[m, i];
				matrix[m, i] = matrix[n, i];
				matrix[n, i] = temp;
			}
			return new RMatrix(matrix);
		}

		public RMatrix SwapMatrixColumn(int m, int n)
		{
			for(var i = 0; i < rowCount; i++) {
				var temp = matrix[i, m];
				matrix[i, m] = matrix[i, n];
				matrix[i, n] = temp;
			}
			return new RMatrix(matrix);
		}
        public static Vector StdDev(RMatrix source, VectorType t = VectorType.Col)
        {
            int count = t == VectorType.Row ? source.ColumnCount : source.RowCount;
            VectorType type = t == VectorType.Row ? VectorType.Col : VectorType.Row;
            Vector v = new Vector(count);
            for (int i = 0; i < count; i++)
                v[i] = source[i, type].StandardDeviation();
            return v;
        }

		public static Vector Transform(RMatrix mat, Vector vec)
		{
			var result = new Vector(vec.Dimension);
			if(!mat.IsSquared()) {
				throw new Exception("The matrix must be squared!");
			}
			if(mat.ColumnCount != vec.Dimension) {
				throw new Exception("The ndim of the vector must be equal "
				+ "to the number of cols of the matrix!");
			}
			for(var i = 0; i < mat.RowCount; i++) {
				result[i] = 0.0;
				for(var j = 0; j < mat.ColumnCount; j++)
					result[i] += mat[i, j] * vec[j];
			}
			return result;
		}

		public static Vector Transform(Vector vec, RMatrix mat)
		{
			var result = new Vector(vec.Dimension);
			if(!mat.IsSquared())
				throw new Exception("The matrix must be squared!");
			if(mat.RowCount != vec.Dimension)
				throw new Exception("The ndim of the vector must be equal " + "to the number of rows of the matrix!");
			for(var i = 0; i < mat.RowCount; i++) {
				result[i] = 0.0;
				for(var j = 0; j < mat.ColumnCount; j++)
					result[i] += vec[j] * mat[j, i];
			}
			return result;
		}

		public static RMatrix Transform(Vector v1, Vector v2)
		{
			if(v1.Dimension != v2.Dimension) {
				throw new Exception("The vectors must have the same ndim!");
			}
			var result = new RMatrix(v1.Dimension, v1.Dimension);
			for(var i = 0; i < v1.Dimension; i++) {
				for(var j = 0; j < v1.Dimension; j++)
					result[j, i] = v1[i] * v2[j];
			}
			return result;
		}

		public static double Determinant(RMatrix mat)
		{
			var result = 0.0;
			if(!mat.IsSquared()) {
				throw new Exception("The matrix must be squared!");
			}
			if(mat.RowCount == 1)
				result = mat[0, 0];
			else {
				for(var i = 0; i < mat.RowCount; i++)
					result += Math.Pow(-1, i) * mat[0, i] * Determinant(Minor(mat, 0, i));
			}
			return result;
		}

		public static RMatrix Minor(RMatrix mat, int row, int col)
		{
			var mm = new RMatrix(mat.RowCount - 1, mat.ColumnCount - 1);
			var ii = 0;
			for(var i = 0; i < mat.RowCount; i++) {
				if(i == row)
					continue;
				var jj = 0;
				for(var j = 0; j < mat.ColumnCount; j++) {
					if(j == col)
						continue;
					mm[ii, jj] = mat[i, j];
					jj++;
				}
				ii++;
			}
			return mm;
		}

		public static RMatrix Adjoint(RMatrix mat)
		{
			if(!mat.IsSquared()) {
				throw new Exception("The matrix must be squared!");
			}
			var ma = new RMatrix(mat.RowCount, mat.ColumnCount);
			for(var i = 0; i < mat.RowCount; i++) {
				for(var j = 0; j < mat.ColumnCount; j++)
					ma[i, j] = Math.Pow(-1, i + j) * (Determinant(Minor(mat, i, j)));
			}
			return ma.GetTranspose();
		}

		public static RMatrix Inverse(RMatrix mat)
		{
			if(Math.Abs(Determinant(mat)) < Constants.Epsilon)
				throw new Exception("Cannot inverse a matrix with a zero determinant!");
			return (Adjoint(mat) / Determinant(mat));
		}

		/// <summary>
		/// Returns a random matrix.
		/// </summary>
		/// <param name="rows">The number of rows.</param>
		/// <param name="cols">The number of columns.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		public static RMatrix Random(int rows = 5, int cols = 5, double min = 0d, double max = 100d)
		{
			var r = new DefaultSource();
			var m = new RMatrix(rows, cols);
			for(int i = 0; i < rows; i++) {
				for(int j = 0; j < cols; j++) {
					m[i, j] = r.NextDouble(min, max);
				}
			}
			return m;
		}

		/// <summary>
		/// Returns the zero matrixx of specified square dimensions.
		/// </summary>
		/// <param name="n">The number of rows/columns.</param>
		public static RMatrix Zeros(int n)
		{
			return new RMatrix(n, n);
		}

		/// <summary>
		/// Returns the zero matrixx of specified dimensions.
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
			for(int i = 0; i < matrix.RowCount; i++)
				for(int j = 0; j < matrix.ColumnCount; j++)
					matrix[i, j] = f();
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
			for(int i = 0; i < matrix.RowCount; i++)
				for(int j = 0; j < matrix.ColumnCount; j++)
					matrix[i, j] = f(i, j);
			return matrix;
		}

		/// <summary>
		/// Creates a random matrix.
		/// </summary>
		/// <param name="rows">The row count.</param>
		/// <param name="columns">The column count.</param>
		/// <param name="min">The minimum value of the entries.</param>
		public static RMatrix Rand(int rows, int columns, double min = 0)
		{
			var m = new double[rows, columns];
			for(int i = 0; i < rows; i++) {
				for(int j = 0; j < columns; j++)
					m[i, j] = MultiplyWithCarryRandomGenerator.GetUniform() + min;
			}
			return new RMatrix(rows, columns) { matrix = m, isTransposed = false };
		}

		public static RMatrix Rand(int n, double min = 0)
		{
			return RMatrix.Rand(n, n, min);
		}

		public static RMatrix Diag(Vector v)
		{
			var m = RMatrix.Zeros(v.Dimension);
			for(var i = 0; i < v.Dimension; i++)
				m[i, i] = v[i];
			return m;
		}

		public bool Equals(RMatrix m, double tolerance)
		{
			if(RowCount != m.RowCount || ColumnCount != m.ColumnCount)
				return false;

			for(int i = 0; i < RowCount; i++)
				for(int j = 0; j < ColumnCount; j++)
					if(System.Math.Abs(this[i, j] - m[i, j]) > tolerance)
						return false;
			return true;
		}
	}

	public enum VectorType
	{
		/// <summary>An enum constant representing the row option.</summary>
		Row,
		/// <summary>An enum constant representing the col option.</summary>
		Col
	}
}