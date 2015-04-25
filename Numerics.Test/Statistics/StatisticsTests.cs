using System;
using System.Linq;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Statistics
{

	/// <summary>
	/// Tested against values from Mathematica 10.
	/// </summary>
	[TestFixture]
	public class StatisticsTests
	{
		private const double Accuracy = 1E-6;

		[Test]
		[Category("Statistics")]
		public void HistogramTest()
		{
			var list = Range.Create(1d, 100d);
			var histo = list.MakeHistogram(10);
			Assert.AreEqual(10, histo.Count(), "Partition count is wrong.");
			Assert.AreEqual(10, histo[7], "Seventh value is wrong.");
			Assert.AreEqual(10, histo[9], "Last value is wrong.");
            
			var d = new[]{ 1d, 1d, 2d, 1d, 4d, 8d, 8d, 9d, 7d, 1d };
			histo = d.MakeHistogram(2);
			Assert.AreEqual(2, histo.Count(), "Partition count is wrong.");
			Assert.AreEqual(6, histo[0], "First value is wrong.");
			Assert.AreEqual(4, histo[1], "Second value is wrong.");
		}

		[Test]
		[Category("Statistics")]
		public void BinomialTest()
		{
			var bin = new BinomialDistribution(0.3, 6);
			Assert.AreEqual(0.999271, bin.CumulativeDistribution(5), Accuracy);
			Assert.AreEqual(0.7443099999999998, bin.CumulativeDistribution(2), Accuracy);
            
			bin = new BinomialDistribution(0.88, 16);
			Assert.AreEqual(0.116213420679984, bin.CumulativeDistribution(12), Accuracy);
			Assert.AreEqual(0.870663008567901, bin.CumulativeDistribution(15), Accuracy);
			Assert.AreEqual(14, bin.Median, Accuracy);
			Assert.AreEqual(14.08, bin.Mean, Accuracy);
			Assert.AreEqual(-0.5846845821518304, bin.Skewness, Accuracy);
			Assert.AreEqual(1.6896, bin.Variance, Accuracy);
		}

		[Test]
		[Category("Statistics")]
		public void ChiTest()
		{
			var bin = new ChiDistribution(13);
			Assert.AreEqual(0.97691627196627, bin.CumulativeDistribution(5), Accuracy);
			Assert.AreEqual(0.00880861355864613, bin.CumulativeDistribution(2), Accuracy);
			Assert.AreEqual(3.512798867365438, bin.Median, Accuracy);
			Assert.AreEqual(3.536942814987594, bin.Mean, Accuracy);
			Assert.AreEqual(0.2054807654904474, bin.Skewness, Accuracy);
			Assert.AreEqual(0.4900355235076361, bin.Variance, Accuracy);
		}

		[Test]
		[Category("Statistics")]
		public void PoissonTest()
		{
			var bin = new PoissonDistribution(13);
			Assert.AreEqual(0.01073388996002841, bin.CumulativeDistribution(5), Accuracy);
			Assert.AreEqual(0.0002226424465876339, bin.CumulativeDistribution(2), Accuracy);
			Assert.AreEqual(13, bin.Median, Accuracy);
			Assert.AreEqual(13, bin.Mean, Accuracy);
			Assert.AreEqual(0.2773500981126146, bin.Skewness, Accuracy);
			Assert.AreEqual(13, bin.Variance, Accuracy);
		}

		[Test]
		[Category("Statistics")]
		public void GeometricTest()
		{
			var bin = new GeometricDistribution(.27);
			Assert.AreEqual(0.848665773711, bin.CumulativeDistribution(5), Accuracy);
			Assert.AreEqual(0.610983, bin.CumulativeDistribution(2), Accuracy);
			//Assert.AreEqual(2, bin.Median, Accuracy);
			//Assert.AreEqual(2.703703703703703, bin.Mean, Accuracy);
			Assert.AreEqual(2.024811846493059, bin.Skewness, Accuracy);
			Assert.AreEqual(10.01371742112483, bin.Variance, Accuracy);
		}

		[Test]
		[Category("Statistics")]
		public void NormalDistributionTest()
		{
			var bin = new GaussianDistribution(10, 4.5);
			Assert.AreEqual(0.1332602629025055, bin.CumulativeDistribution(5), Accuracy);
			Assert.AreEqual(0.03772017981340023, bin.CumulativeDistribution(2), Accuracy);
			Assert.AreEqual(10, bin.Median, Accuracy);
			Assert.AreEqual(10, bin.Mean, Accuracy);
			Assert.AreEqual(0d, bin.Skewness, Accuracy);
			Assert.AreEqual(20.25, bin.Variance, Accuracy);
		}
	}
}
