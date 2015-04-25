using System;
using System.Linq;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Math
{
	[TestFixture]
	public class InterpolationTests
	{
		[Test]
		[Category("Interpolation")]
		public void LinearTest()
		{
			const double a = 0.2;
			const double m = 11.7;
			// an exact line should obviously give an exact parametrization up to epsilon
			Func<double, double> f = i => a + m * i;
			var range = Range.Create(1d, 13d).ToArray();
			var target = range.Map(i => f(i)).ToArray();
			var interpol = InterpolationExtensions.LeastSquaresBestFitLine(range, target);
			Assert.IsTrue(System.Math.Abs(a - interpol[0]) < Constants.Epsilon);
			Assert.IsTrue(System.Math.Abs(m - interpol[1]) < Constants.Epsilon);
		}

		[Test]
		[Category("Interpolation")]
		public void ExponentialTest()
		{
			const double a = 10.2;
			const double c = 1.07;
			// an exact exponential should obviously give an exact parametrization up to epsilon
			Func<double, double> f = i => a * System.Math.Exp(c * i);
			var range = Range.Create(1d, 13d).ToArray();
			var target = range.Map(i => f(i)).ToArray();
			var interpol = InterpolationExtensions.LeastSquaresBestFitExponential(range, target);
			Assert.IsTrue(System.Math.Abs(a - interpol[0]) < Constants.Epsilon);
			Assert.IsTrue(System.Math.Abs(c - interpol[1]) < Constants.Epsilon);
		}

		[Test]
		[Category("Interpolation")]
		public void LinearExtrapolateTest()
		{
			var y = new[] { 6d, -5d, 2d, 14d, 12d };
			var more = InterpolationExtensions.FillLinear(y, 5);
			Console.WriteLine(more.ToPrettyFormat());
			var shouldBe = new[] { 15.1, 18.2, 21.3, 24.4, 27.5 };
			// you can take the values from Excel for instance
			for(var i = 0; i < more.Length; i++)
				Assert.IsTrue(System.Math.Abs(shouldBe[i] - more[i]) < Constants.Epsilon, string.Format("Not quite correct; {0} instead of {1}.", more[i], shouldBe[i]));
		}

		[Test]
		[Category("Interpolation")]
		public void ExponentialExtrapolateTest()
		{
			var y = new[] { 6d, 5d, 2d, 14d, 12d };
			var more = InterpolationExtensions.FillExponential(y);
			foreach(var d in more)
				Console.WriteLine((int)d);
			//now go and open Excel, paste the extra values and insert a line-chart; the result is a nice exponential.
		}

	}
}
