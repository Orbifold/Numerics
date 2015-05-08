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
        public void Transpose()
        {
            var m = RMatrix.Random();
            Assert.AreEqual(m[0, 0] + m[1, 1] + m[2, 2] + m[3, 3] + m[4, 4], m.Trace(), Accuracy, "Wrong trace.");
            var mt = m.GetTranspose();
            Assert.IsTrue(mt[0, 0] == m[0, 0] && mt[1, 0] == m[0, 1] && mt[2, 0] == m[0, 2] && mt[3, 0] == m[0, 3] &&
                mt[4, 0] == m[0, 4] && mt[0, 3] == m[3, 0] && mt[1, 3] == m[3, 1] && mt[2, 3] == m[3, 2] && mt[4, 3] == m[3, 4], "Returned transposition failed.");
        }

        [Test]
        [Category("Algebra")]
        public void Norm()
        {
            var m = new RMatrix(new double[,] { { 3, 4 }, { 7, 7 } });
            Assert.AreEqual(11.09053650640942, m.Norm(), Accuracy, "Incorrect Euclidean norm.");
            Assert.AreEqual(8.10061115014517, m.Norm(5), Accuracy, "Incorrect 5-norm.");
            Assert.AreEqual(21, m.Sum(), Accuracy, "Incorrect sum.");
        }

        [Test]
        [Category("Algebra")]
        public void PlusMinus()
        {
            var m = RMatrix.Random();
            var num = Random.NextDouble();
            var mm = -m;
            var mt = m * num;
            var mg = m - num;
            var mh = m + num;
            var mp = m ^ num;
            Assert.AreEqual(-m[3, 2], mm[3, 2]);
            Assert.AreEqual(m[3, 1] * num, mt[3, 1]);
            Assert.AreEqual(m[4, 0] - num, mg[4, 0]);
            Assert.AreEqual(m[3, 1] + num, mh[3, 1]);
            Assert.AreEqual(System.Math.Pow(m[3, 1], num), mp[3, 1]);
        }

    }

}