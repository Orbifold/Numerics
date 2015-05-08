using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Orbifold.Numerics
{
	public static class MatrixExtensions
	{
        public static Vector StdDev(this Matrix source, VectorType t)
        {
            throw new NotImplementedException();
            //return Matrix.StdDev(source, t);
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

		public static Vector Diag(this RMatrix m)
		{
			var length = m.ColumnCount > m.RowCount ? m.RowCount : m.ColumnCount;
			var v = Vector.Zeros(length);
			for(int i = 0; i < length; i++)
				v[i] = m[i, i];
			return v;
		}

		/// <summary>Stack a set of vectors into a matrix.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <param name="m">Input Matrix.</param>
		/// <param name="t">Row or Column sum.</param>
		/// <returns>A Matrix.</returns>
		public static RMatrix Stack(this RMatrix m, RMatrix t)
		{
			if(m.ColumnCount != t.ColumnCount)
				throw new InvalidOperationException("Invalid dimension for stack operation!");

			var p = new RMatrix(m.RowCount + t.RowCount, t.ColumnCount);
			for(int i = 0; i < p.RowCount; i++) {
				for(int j = 0; j < p.ColumnCount; j++) {
					if(i < m.RowCount)
						p[i, j] = m[i, j];
					else
						p[i, j] = t[i - m.RowCount, j];
				}
			}

			return p;
		}

		public static RMatrix Identity(int n)
		{
			return RMatrix.Identity(n, n);
		}

		public static Vector CovarianceDiag(this RMatrix source, VectorType t = VectorType.Col)
		{
			int length = t == VectorType.Row ? source.RowCount : source.ColumnCount;
			var vector = new Vector(length);
			for(int i = 0; i < length; i++)
				vector[i] = source[i, t].Variance();
			return vector;
		}

		public static Vector Sum(this RMatrix m, VectorType t)
		{
			if(t == VectorType.Row) {
				Vector result = new Vector(m.ColumnCount);
				for(int i = 0; i < m.ColumnCount; i++)
					for(int j = 0; j < m.RowCount; j++)
						result[i] += m[j, i];
				return result;
			} else {
				Vector result = new Vector(m.RowCount);
				for(int i = 0; i < m.RowCount; i++)
					for(int j = 0; j < m.ColumnCount; j++)
						result[i] += m[i, j];
				return result;
			}
		}

		public static Vector Mean(this RMatrix source, VectorType t)
		{
			int count = t == VectorType.Row ? source.ColumnCount : source.RowCount;
			VectorType type = t == VectorType.Row ? VectorType.Col : VectorType.Row;
			Vector v = new Vector(count);
			for(int i = 0; i < count; i++)
				v[i] = source[i, type].Mean();
			return v;
		}

		/// <summary>Computes the sum of every element of the matrix.</summary>
		/// <param name="m">Input Matrix.</param>
		/// <param name="i">Zero-based index of the.</param>
		/// <param name="t">Row or Column sum.</param>
		/// <returns>sum.</returns>
		public static double Sum(this RMatrix m, int i, VectorType t)
		{
			return m[i, t].Sum();
		}

        public static Vector RowSums(this RMatrix matrix)
        {
            
            var result = new double[matrix.RowCount];
            for (var i = 0; i < matrix.RowCount; i++)
            {
                result[i] = matrix[i, 0] + matrix[i, 1];
            }
            return new Vector(result);
        }
        
        public static Vector ColSums(this RMatrix matrix)
        {
            var result = new double[2];
            for (var i = 0; i < matrix.RowCount; i++)
            {
                result[0] += matrix[i, 0];
                result[1] += matrix[i, 1];
            }
          return new Vector(result);
        }

        /// <summary>
        /// Returns the minimum value of the given one and each entry of the matrix.
        /// </summary>
        /// <param name="m">A real-valued matrix.</param>
        /// <param name="v">A real value.</param>
        public static double Min(this RMatrix m, double v)
        {
            var min = double.MaxValue;
            for (var i = 0; i < m.RowCount; i++)
                for (var j = 0; j < m.ColumnCount; j++)
                    min = Math.Min(min, Math.Min(v, m[i, j]));
            return min;
        }

        /// <summary>
        /// Returns a new matrix with the entries being the absolute value of the given matrix.
        /// </summary>
        /// <param name="m">A real-valued matrix.</param>
        public static RMatrix Abs(this RMatrix m)
        {
            var clone = new RMatrix(m.RowCount, m.ColumnCount);
            for (var i = 0; i < m.RowCount; i++)
                for (var j = 0; j < m.ColumnCount; j++)
                    clone[i, j] = Math.Abs(m[i, j]);
            return clone;
        }
		public static Tuple<Vector, RMatrix> Evd(this RMatrix A)
		{
			EigenCalculator eigs = new EigenCalculator(A);
			eigs.Compute();
			return new Tuple<Vector, RMatrix>(eigs.EigenValues, eigs.EigenVectors);
		}

		public static RMatrix Covariance(this RMatrix source, VectorType t = VectorType.Col)
		{
			var length = t == VectorType.Row ? source.RowCount : source.ColumnCount;
			var m = new RMatrix(length);
			//for (int i = 0; i < length; i++)
			Parallel.For(0, length, i =>
				//for (int j = i; j < length; j++) // symmetric matrix
				Parallel.For(i, length, j =>
					m[i, j] = m[j, i] = source[i, t].Covariance(source[j, t])));
			return m;
		}

		/// <summary>
		/// Returns the trace of the matrix.
		/// </summary>
		/// <param name="m">M.</param>
		public static double Trace(this RMatrix m)
		{
			double t = 0;
			for(int i = 0; i < m.RowCount && i < m.ColumnCount; i++)
				t += m[i, i];
			return t;
		}

		/// <summary>Computes the sum of every entry in the matrix.</summary>
		/// <param name="m">A matrix.</param>
		public static double Sum(this RMatrix m)
		{
			var sum = 0d;
			for(var i = 0; i < m.RowCount; i++)
				for(var j = 0; j < m.ColumnCount; j++)
					sum += m[i, j];
			return sum;
		}

		/// <summary>
		/// Returns the Chebyshev p-norm of the given matrix.
		/// </summary>
		/// <param name="m">A matrix.</param>
		/// <param name="p">The order parameter.</param>
		/// <seealso cref="VectorExtensions.Norm"/>
		public static double Norm(this RMatrix m, double p)
		{
			double norm = 0;
			for(int i = 0; i < m.RowCount; i++)
				for(int j = 0; j < m.ColumnCount; j++)
					norm += Math.Pow(Math.Abs(m[i, j]), p);
			return Math.Pow(norm, 1d / p);
		}

		/// <summary>
		/// The standard Euclidean matrix norm.
		/// </summary>
		/// <param name="m">A matrix.</param>
		public static double Norm(this RMatrix m)
		{
			return m.Norm(2);
		}

		 
		public static IEnumerable<int> Indices(this RMatrix source, Func<Vector, bool> f, VectorType t = VectorType.Row)
		{
			int max = t == VectorType.Row ? source.RowCount : source.ColumnCount;
			for(int i = 0; i < max; i++)
				if(f(source[i, t]))
					yield return i;
		}

		/// <summary>Slices.</summary>
		/// <param name="m">Input Matrix.</param>
		/// <param name="indices">The indices.</param>
		/// <param name="t">Row or Column sum.</param>
		/// <returns>A Matrix.</returns>
		public static RMatrix Slice(this RMatrix m, IEnumerable<int> indices, VectorType t = VectorType.Row)
		{
			var q = indices.Distinct();

			var rows = t == VectorType.Row ? q.Count(j => j < m.RowCount) : m.RowCount;
			var cols = t == VectorType.Col ? q.Count(j => j < m.ColumnCount) : m.ColumnCount;

			var n = new RMatrix(rows, cols);

			int i = -1;
			foreach(var j in q.OrderBy(k => k))
				n[++i, t] = m[j, t];

			return n;
		}

        /// <summary>
        /// The standard matrix multiplication. Note that the *-operator in the RMatrix class
        /// is en entry-by-entry multiplication.
        /// </summary>
	    public static RMatrix Times(this RMatrix m1, RMatrix m2)
	    {
            if (m1.ColumnCount != m2.RowCount) throw new Exception("The numbers of columns of the" + " first matrix must be equal to the number of " + " rows of the second matrix!");
            var result = new RMatrix(m1.RowCount, m2.ColumnCount);
            for (var i = 0; i < m1.RowCount; i++)
            {
                for (var j = 0; j < m2.ColumnCount; j++)
                {
                    var tmp = result[i, j];
                    for (var k = 0; k < result.RowCount; k++) tmp += m1[i, k] * m2[k, j];
                    result[i, j] = tmp;
                }
            }
            return result;	        
	    }
	}
	
}