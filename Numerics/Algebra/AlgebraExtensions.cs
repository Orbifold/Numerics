#region Copyright

// Copyright (c) 2007-2013, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System;

namespace Orbifold.Numerics
{
    public static class AlgebraExtensions
    {
        /// <summary>
        /// Gauss-Jordan elimination method to solve Ax = b for x.
        /// </summary>
        /// <param name="A">A real-valued matrix.</param>
        /// <param name="b">The real-valued vector.</param>
        /// <param name="maxIterations">The maximum amount of iterations.</param>
        /// <param name="tolerance">The tolerance.</param>
        public static Vector GaussJacobi(RMatrix A, Vector b, int maxIterations = 10, double tolerance = 1E-4)
        {
            var bSize = b.Dimension;
            var x = new Vector(bSize);
            for (var nIteration = 0; nIteration < maxIterations; nIteration++)
            {
                var xOld = x.Clone();
                for (var i = 0; i < bSize; i++)
                {
                    var entry = b[i];
                    var diagonal = A[i, i];
                    if (Math.Abs(diagonal) < Constants.Epsilon) throw new Exception("Diagonal element went into epsilon.");
                    for (var j = 0; j < bSize; j++) if (j != i) entry -= A[i, j] * xOld[j];
                    x[i] = entry / diagonal;
                }
                var dx = x - xOld;
                if (dx.Norm() < tolerance) return x;
            }
            return x;
        }

        /// <summary>
        /// Gauss-Jordan elimination method solving the Ax=b inear set of equations.
        /// </summary>
        /// <param name="A">A real-valued matrix.</param>
        /// <param name="b">A real-valued vector.</param>
        ///<remarks>http://en.wikipedia.org/wiki/Gauss-Jordan</remarks>
        public static Vector GaussJordan(RMatrix A, Vector b)
        {
            Triangulate(A, b);
            var bSize = b.Dimension;
            var x = new Vector(bSize);
            for (var i = bSize - 1; i >= 0; i--)
            {
                var aii = A[i, i];
                if (Math.Abs(aii) < Constants.Epsilon) throw new Exception("Diagonal element is too small!");
                x[i] = (b[i] - Vector.Dot(A.GetRowVector(i), x)) / aii;
            }
            return x;
        }

        public static Vector GaussSeidel(RMatrix A, Vector b, int maxIterations = 10, double tolerance = 1E-4)
        {
            var size = b.Dimension;
            var x = new Vector(size);
            for (var nIteration = 0; nIteration < maxIterations; nIteration++)
            {
                var xOld = x.Clone();
                for (var i = 0; i < size; i++)
                {
                    var entry = b[i];
                    var diagonal = A[i, i];
                    if (Math.Abs(diagonal) < Constants.Epsilon) throw new Exception("Diagonal element is too small!");
                    for (var j = 0; j < i; j++) entry -= A[i, j] * x[j];
                    for (var j = i + 1; j < size; j++) entry -= A[i, j] * xOld[j];
                    x[i] = entry / diagonal;
                }
                var dx = x - xOld;
                if (dx.Norm() < tolerance) return x;
            }
            return x;
        }

        /// <summary>
        /// Triangulation of matrix A using LU decomposition on the basis of the Crout algorithm with pivoting.
        /// </summary>
        /// <param name="A">The real-valued matrix.</param>
        /// <param name="b">The real-valued vector.</param>
        /// <returns></returns>
        public static double LUCrout(RMatrix A, Vector b)
        {
            LUDecompose(A);
            return LUSubstitute(A, b);
        }

        /// <summary>
        /// Returns the LU RMatrix Inverse.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static RMatrix LUInverse(RMatrix matrix)
        {
            var nRows = matrix.RowCount;
            var u = matrix.IdentityMatrix();
            LUDecompose(matrix);
            for (var i = 0; i < nRows; i++)
            {
                var uv = u.GetRowVector(i);
                LUSubstitute(matrix, uv);
                u.ReplaceRow(uv, i);
            }
            var inverse = u.GetTranspose();
            return inverse;
        }

        /// <summary>
        /// LU decomposition (where 'LU' stands for 'Lower Upper', and also called LU factorization) 
        /// factors a matrix as the product of a lower triangular matrix and an upper triangular matrix.
        /// </summary>
        /// <param name="matrix">A matrix.</param>
        /// <remarks>http://en.wikipedia.org/wiki/LU_decomposition</remarks>
        public static void LUDecompose(RMatrix matrix)
        {
            if (matrix.RowCount != matrix.ColumnCount) throw new Exception("LU decomposition only works on square matrices.");
            var nRows = matrix.RowCount;
            for (var i = 0; i < nRows; i++)
            {
                for (var j = 0; j < nRows; j++)
                {
                    var w = matrix[i, j];
                    for (var k = 0; k < Math.Min(i, j); k++) w -= matrix[i, k] * matrix[k, j];
                    if (j > i)
                    {
                        var s = matrix[i, i];
                        if (Math.Abs(w) < Constants.Epsilon) throw new Exception("Diagonal element is too small!");
                        w /= s;
                    }
                    matrix[i, j] = w;
                }
            }
        }

        public static Tuple<RMatrix, RMatrix> LUProjector(RMatrix matrix)
        {
            LUDecompose(matrix);
            var size = matrix.RowCount;
            // lower triangular has 1 on diagonal and entries otherwise
            var lower = new double[size, size];
            for (var i = 0; i < size; i++) for (var j = 0; j <= i; j++) lower[i, j] = i == j ? 1 : matrix[i, j];
            // upper is the upper of lu including the diagonal
            var upper = new double[size, size];
            for (var i = 0; i < size; i++) for (var j = i; j < size; j++) upper[i, j] = matrix[i, j];
            return Tuple.Create((RMatrix)lower, (RMatrix)upper);
        }

        private static double LUSubstitute(RMatrix matrix, Vector vec)
        {
            var size = vec.Dimension;
            var det = 1.0;
            for (var i = 0; i < size; i++)
            {
                var w = vec[i];
                for (var j = 0; j < i; j++) w -= matrix[i, j] * vec[j];
                var p = matrix[i, i];
                if (Math.Abs(w) < double.Epsilon) throw new Exception("Diagonal element is too small!");
                w /= p;
                vec[i] = w;
                det *= matrix[i, i];
            }
            for (var i = size - 1; i >= 0; i--)
            {
                var s = vec[i];
                for (var j = i + 1; j < size; j++) s -= matrix[i, j] * vec[j];
                vec[i] = s;
            }
            return det;
        }

        private static void Triangulate(RMatrix A, Vector b)
        {
            var nRows = A.RowCount;
            for (var i = 0; i < nRows - 1; i++)
            {
                var diagonalElement = PivotGaussJordan(A, b, i);
                if (Math.Abs(diagonalElement) < Constants.Epsilon) throw new Exception("Diagonal element is too small!");
                for (var j = i + 1; j < nRows; j++)
                {
                    var w = A[j, i] / diagonalElement;
                    for (var k = i + 1; k < nRows; k++) A[j, k] -= w * A[i, k];
                    b[j] -= w * b[i];
                }
            }
        }

        private static double PivotGaussJordan(RMatrix A, Vector b, int q)
        {
            var bSize = b.Dimension;
            var c = q;
            var d = 0.0;
            for (var j = q; j < bSize; j++)
            {
                var w = Math.Abs(A[j, q]);
                if (w > d)
                {
                    d = w;
                    c = j;
                }
            }
            if (c > q)
            {
                A.SwapRMatrixow(q, c);
                b.SwapVectorEntries(q, c);
            }
            return A[q, q];
        }
    }
}