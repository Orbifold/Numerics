using System;
using NUnit.Framework;
using System.Linq;

namespace Orbifold.Numerics.Tests.Algebra
{
	[TestFixture]
	public class MatrixTests
	{

		private const double Accuracy = 1E-6;

		[Test]
		[Category("Algebra")]
		public void MatrixMethods()
		{
			var	m = RMatrix.Random();
			Assert.AreEqual(m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] + m[4, 4], m.Trace(), Accuracy, "Wrong trace.");
		
			m = new RMatrix(new double[,]{ { 3, 4 }, { 7, 7 } });
			Assert.AreEqual(11.09053650640942, m.Norm(), Accuracy, "Incorrect Euclidean norm.");
			Assert.AreEqual(8.10061115014517, m.Norm(5), Accuracy, "Incorrect 5-norm.");
			Assert.AreEqual(21, m.Sum(), Accuracy, "Incorrect sum.");
		}

	}

}