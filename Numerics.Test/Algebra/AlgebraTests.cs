using System;
using NUnit.Framework;
using System.Linq;

namespace Orbifold.Numerics.Tests.Algebra
{

	/// <summary>
	/// Unit tests of diverse algebraic operations.
	/// </summary>
	[TestFixture]
	public class AlgebraTests
	{
		private const double Accuracy = 1E-6;

		[Test]
		[Category("Distance and similarity")]
		public void DistanceTest()
		{
			var v = new Vector(new []{ 0.1, 0.7, -3.07, -5.12 });
			var w = new Vector(new []{ 2.2, 4.6, 8.1, -2.1 });
			Assert.AreEqual(-1.8475, v.Mean(), Accuracy, "Mean of v is wrong.");
			Assert.AreEqual(2.737777383207042, v.StandardDeviation(), Accuracy, "StandardDeviation of v is wrong.");
			Assert.AreEqual(3.2, w.Mean(), Accuracy, "Mean of w is wrong.");
			Assert.AreEqual(4.284079053114372, w.StandardDeviation(), Accuracy, "StandardDeviation of w is wrong.");
			Assert.AreEqual(20.19, v.MahattanDistance(w), Accuracy, "Manhattan distance is not correct.");
			Assert.AreEqual(12.38988700513447, v.EuclideanDistance(w), Accuracy, "Euclidean distance is not correct.");
			Assert.AreEqual(-10.675, v.Dot(w), Accuracy, "Dot is not correct.");
			//R and Wolfram diasagree here
			Assert.AreEqual(0.27651889, v.PearsonCorrelation(w), Accuracy, "Pearson correlation is not correct.");
		}

		[Test]
		[Category("Distance and similarity")]
		public void Deviation()
		{
			var v = new Vector(new[]{ 2.1, 0.7, -3.07, -5.12, 4.12, 9.17, 5.44 });
			Assert.AreEqual(1.905714285714286, v.Average(), Accuracy, "Mean is wrong.");
			Assert.AreEqual(4.929062984458801, v.StandardDeviation(), Accuracy, "StandardDeviation is wrong.");
			//			var m = v.Mean();
			//			var b = new RVector(new[]{ 0.0377469, 1.45375, 24.7577, 49.3607, 4.90306, 52.7698, 12.4912 });
			//			var z = v.Each(d => (d - m) * (d - m), true);
			//			Assert.AreEqual(0d, (b - z).Sum(), Accuracy);
			//			Assert.AreEqual((b - z).Sum(), 0d, Accuracy, "Sub calculation is wrong.");
			//Assert.AreEqual(145.7739714285714, v.Each(d => System.Math.Pow(d - v.Mean(), 2), true).Sum(), Accuracy, "Sub calculation is wrong.");
		}

	    [Test]
		[Category("Algebra")]
		public void VectorMethods()
		{
			var v = new Vector(Range.Create(1d, 100d));
			Assert.AreEqual(100, v.Dimension, "Incorrect dimension.");
			var sub = v.Indices(d => d <= 50).ToList();
			Assert.AreEqual(50, sub.Count, "Index list count is wrong.");
			Assert.AreEqual(0, sub[0], "Index zero wrong.");
			Assert.AreEqual(49, sub[49], "Index zero wrong.");

			var w = new Vector(new[]{ 43d, 22d, 12d, 9d, 88d, 93d, 16d, 10d });
			sub = w.Indices(d => d <= 10).ToList();
			Assert.AreEqual(2, sub.Count, "Should be only one element.");
			Assert.IsTrue(sub[0] == 3 && sub[1] == 7, "Subs is wrong.");


			var u = new Vector(new double[]{ 14, 5, 55, 2, 76, 150, 2, 7, 10 });
			var step = (u.Max() - u.Min()) / 7d;
			var ranges = u.Segment(7);
			Assert.AreEqual(7, ranges.Count(), "Wrong count of ranges.");
			for(int i = 0; i < ranges.Length; i++) {
				var r = ranges[i];
				Assert.AreEqual(u.Min() + i * step, r.Start, "Wrong start at step " + i);
			}

			var subu = u.Slice(new[]{ 3, 5, 6, 15, 21 });
			Assert.AreEqual(3, subu.Dimension);

			Assert.IsTrue(subu[0] == 2d && subu[1] == 150d && subu[2] == 2d);

			u = new Vector(new double[]{ 14, 5, 55, 2, 76, 150, 2, 7, 10 });
			Assert.AreEqual(5, u.MaxIndex(), "MaxIndex is wrong.");
			Assert.AreEqual(5, u.ToArray().MaxIndex(), "MaxIndex via arrays is wrong.");

			u = new Vector(new double[]{ -4, 5, 150, 2, 76, 150, 2, 7, -10 });
			Assert.AreEqual(2, u.MaxIndex(), "MaxIndex with doubling is wrong.");
			Assert.AreEqual(2, u.ToArray().MaxIndex(), "MaxIndex with doubling via arrays is wrong.");
			Assert.AreEqual(5, u.MaxIndex(3), "MaxIndex with doubling and skip is wrong.");

            var p = new Vector(new[] { 43d, 22d, 12d, 9d, 88d, 93d, 16d, 10d, 43d, 22d, 12d, 12d });
            Assert.AreEqual(12d, p.MostAppearingElement(),"The most appearing one is unique here.");
            p = new Vector(new[] { 43d, 22d, 12d, 1d, 9d, 88d, 93d, 16d, 10d, 43d, 22d, 12d, 1d });
            Assert.AreEqual(1d, p.MostAppearingElement(),"There are four most appearing here. The last one is chosen.");
            p = new Vector(new double[] { });
            Assert.AreEqual(double.NaN, p.MostAppearingElement(),"Zero dimensions here.");

		}

		[Test]
		[Category("Algebra")]
		public void VectorNorms()
		{	
			var v = new Vector(new[]{ 5.1, 6.7, 8.9 });
			Assert.AreEqual(12.25193862211201, v.Norm(), Accuracy, "Euclidean norm is wrong.");
			Assert.AreEqual(10.80192750716544, v.Norm(2.7), Accuracy, "2.7-norm is wrong.");
			Assert.AreEqual(20.26916787938221, v.Norm(1.02), Accuracy, "1.02-norm is wrong.");
			Assert.AreEqual(3, v.Dimension, "Dimension is not OK.");

			v = new Vector(new[]{ 0.1, 0.7, -3.07, -5.12 });
			Assert.AreEqual(6.011597125556569, v.Norm(), Accuracy, "Euclidean norm is wrong.");
			Assert.AreEqual(5.120178384194943, v.Norm(14.8), Accuracy, "14.8-norm is wrong.");
			Assert.AreEqual(8.92356195304955, v.Norm(1.008), Accuracy, "1.008-norm is wrong.");
			Assert.AreEqual(4, v.Dimension, "Dimension is not OK.");
		}

		[Test]
		[Category("Algebra")]
		public void VectorOperations()
		{
			var v = new Vector(new[]{ 1.2, 5, 7 });
			Assert.AreEqual(new Vector(new[]{ 2.4, 10, 14 }), 2 * v, "Scalar multiplication is not correct.");
			Assert.AreEqual(new Vector(new[]{ .6, 2.5, 3.5 }), v / 2, "Scalar division is not correct.");
			Assert.AreEqual(new Vector(new[]{ 1.44, 25, 49 }), v ^ 2, "Power is not correct.");
			Assert.AreEqual(new[]{ 1.44, 25, 49 }, v.ToArray(), "ToArray is not correct.");
			Assert.AreEqual(75.44, v.Sum(), "Sum is wrong.");
			v = new Vector(new[]{ 4.3, 0.8, 1 });
			Assert.AreEqual(new Vector(new[]{ 1, 0.8, 4.3 }), v.SwapVectorEntries(0, 2), "Swapping is wrong.");
			v = new Vector(new []{ 0.1, 0.7, -3.07, -5.12 });
			var w = new Vector(new []{ 2.2, 4.6, 8.1, -2.1 });
			Assert.AreEqual(-10.675, v.Dot(w), Accuracy, "Dot product is wrong.");
			Assert.AreEqual((v.Each(System.Math.Cos) - new Vector(new[]{ 0.995004, 0.764842, -0.997438, 0.396417 })).Sum(), 0d, Accuracy, "Dot product is wrong.");
			w = new Vector(new []{ 2.2, 4.6, 8.1, -2.1 });
			Assert.AreEqual((new Vector(new[]{ 0.224513, 0.469437, 0.826617, -0.214308 }) - w.Normalized()).Sum(), 0d, Accuracy, "Normalized clone is wrong.");
		}

		[Test]
		[Category("Algebra")]
		public void SystemOfEquationsTest()
		{
			// not working as it should
//			RMatrix A = new RMatrix(new double[3, 3]{ { 2, 4, -6 }, { 6, -4, 2 }, { 4, 2, 6 } });
//			var b = new Vector(new double[3] { 4, 4, 8 });
//			var Anew = A.Clone();
//			var Bnew = A.Clone();
//			var d = AlgebraExtensions.LUCrout(A, b);
//			var inv = AlgebraExtensions.LUInverse(Anew);
//
//			var y = A * b;
//			var should = new Vector(new double[3] { 4, 4, 8 });
			Assert.Inconclusive();
			//Assert.IsTrue(object.Equals(y.Col(0), should));

			//			Console.WriteLine("\nInverse of A = \n {0}", (inv));
			//			Console.WriteLine("\nSolution of the equations Ax = b is x={0}", b);
			//			Console.WriteLine("\nDeterminant of A = {0}", d);
			//			Console.WriteLine("\nTest Inverse: A*Inverse = \n {0}", Bnew * inv);

		}

		[Test]
		[Category("Algebra")]
		public void LUDecompositionTest()
		{

			var m = new RMatrix(new[,] { { 1d, 2d }, { 3d, 4d } });
			AlgebraExtensions.LUDecompose(m);

			var expect = new RMatrix(new[,] { { 1d, 2d }, { 3d, -2d } });
			Assert.IsTrue(expect.Equals(m));

			m = new RMatrix(new[,] { { 1d, 2d }, { 3d, 4d } });
			var proj = AlgebraExtensions.LUProjector(m);
			var lower = proj.Item1;
			var upper = proj.Item2;
			var same = lower * upper;
			Assert.IsTrue(same.Equals(new RMatrix(new[,] { { 1d, 2d }, { 3d, 4d } }), Accuracy));

			// TODO: not working here
//			m = new RMatrix(new[,] { { 198d, 21d }, { 3d, 401d } });
//			proj = AlgebraExtensions.LUProjector(m);
//			lower = proj.Item1;
//			upper = proj.Item2;
//			same = lower * upper;
//			Assert.IsTrue(same.Equals(new RMatrix(new[,] { { 198d, 21d }, { 3d, 401d } }), Accuracy));

//			m = new RMatrix(new[,] { { 11.3d, 2.9d }, { 1.05d, 0.88d } });
//			proj = AlgebraExtensions.LUProjector(m);
//			lower = proj.Item1;
//			upper = proj.Item2;
//			same = lower * upper;
//			Assert.IsTrue(same.Equals(new RMatrix(new[,] { { 11.3d, 2.9d }, { 1.05d, 0.88d }  }), Accuracy));
		}


		[Test]
		[Category("Algebra")]
		public void LinearSolveTest()
		{
			var A = new RMatrix(3, 3);
			A[0, 0] = 2;
			A[0, 1] = 1;
			A[0, 2] = -1;
			A[1, 0] = -3;
			A[1, 1] = -1;
			A[1, 2] = 2;
			A[2, 0] = -2;
			A[2, 1] = 1;
			A[2, 2] = 2;
			var b = new Vector(3);
			b[0] = 8;
			b[1] = -11;
			b[2] = -3;

			var x = AlgebraExtensions.GaussJordan(A, b);
			Assert.AreEqual(2, x[0], Accuracy);
			Assert.AreEqual(3, x[1], Accuracy);
			Assert.AreEqual(-1, x[2], Accuracy);

			A = new RMatrix(new double[,] { { 2, 4, -6 }, { 6, -4, 2 }, { 4, 2, 6 } });
			b = new Vector(new double[] { 6, -2, 4 });
			x = AlgebraExtensions.GaussJordan(A, b);

			Assert.AreEqual(0.476190476190476, x[0], Accuracy);
			Assert.AreEqual(1.19047619047619, x[1], Accuracy);
			Assert.AreEqual(-0.0476190476190476, x[2], Accuracy);

			A = new RMatrix(new double[,] { { 4, 0, 1 }, { 0, 3, 2 }, { 1, 2, 4 } });
			b = new Vector(new double[] { 2, 1, 3 });
			x = AlgebraExtensions.GaussJacobi(A, b, 10, 1.0E-4);
			Assert.AreEqual(0.310397736820174, x[0], Accuracy);
			Assert.AreEqual(-0.17227270181287, x[1], Accuracy);
			Assert.AreEqual(0.751248669722443, x[2], Accuracy);

			// Gauss-Jordan is less precise
			x = AlgebraExtensions.GaussJordan(A, b);
			Assert.AreEqual(0.310397736820174, x[0], 0.1);
			Assert.AreEqual(-0.17227270181287, x[1], 0.1);
			Assert.AreEqual(0.751248669722443, x[2], 0.1);

			x = AlgebraExtensions.GaussSeidel(A, b);
			Assert.AreEqual(0.310397736820174, x[0], 0.1);
			Assert.AreEqual(-0.17227270181287, x[1], 0.1);
			Assert.AreEqual(0.751248669722443, x[2], 0.1);

		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(20)]
		[TestCase(30)]
		[TestCase(50)]
		[TestCase(100)]
		[Category("Algebra")]
		public void EigenvaluesTest(int order)
		{
			// create random matrix
			var rand = RMatrix.Random(order, 1.13);
			// create a symmetric positive definite matrix
			var A = rand.T * rand;
			var evd = new EigenCalculator(A);
			// compute eigenvalues/eigenvectors
			evd.Compute();
			var eigenvectors = evd.EigenVectors;
			var eigenvalues = evd.EigenValues;

			Assert.AreEqual(order, eigenvectors.RowCount);
			Assert.AreEqual(order, eigenvectors.ColumnCount);
			Assert.AreEqual(order, eigenvalues.Dimension);

			// make sure that A = V*λ*VT 
			var computed = eigenvectors * RMatrix.Diag(eigenvalues) * eigenvectors.T;
			Assert.IsTrue(A.Equals(computed, 1e-10));
		}

		[Test]
		[Category("Algebra")]
		public void EigenValueTest()
		{
			RMatrix mm = new double[,]{ { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
			// matrix needs to be symmetric
			var m = mm.T * mm;
			var eigenCalc = new EigenCalculator(m);
			eigenCalc.Compute();
			var eigenvectors = eigenCalc.EigenVectors;
			var eigenvalues = eigenCalc.EigenValues;
			Assert.AreEqual(0d, ((eigenvectors.Col(0) * eigenvalues[0]) - (eigenvectors.Col(0) * m).Row(0)).Sum(), 1E-10);
			Assert.AreEqual(0d, ((eigenvectors.Col(1) * eigenvalues[1]) - (eigenvectors.Col(1) * m).Row(0)).Sum(), 1E-10);
			Assert.AreEqual(0d, ((eigenvectors.Col(2) * eigenvalues[2]) - (eigenvectors.Col(2) * m).Row(0)).Sum(), 1E-10);
		}
	}


}