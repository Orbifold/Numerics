using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Geometry
{
	/// <summary>
	/// Unit tests of diverse special functions and polynomials.
	/// </summary>
	[TestFixture]
	public class GeometryTests
	{
		private const double Accuracy = 1E-6;

		[Test]
		[Category("Geometry")]
		public void AngleTest()
		{
			Assert.AreEqual(Angle.PiHalf.Radians, Angle.ToPoint(new Point(0, 12)).Radians, "Should be right angle.");
			Assert.AreEqual(Angle.Bisect.Radians, Angle.ToPoint(new Point(10, 10)).Radians, "Should bisect.");
			Assert.AreEqual(Angle.Pi.Radians, Angle.ToPoint(new Point(-413, 0)).Radians, "Should be 180 degrees.");
		}



	}
}