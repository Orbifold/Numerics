
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Histogram extensions.
	/// </summary>
	public static class HistogramExtensions
	{
		/// <summary>
		/// Creates the histogram within the specified interval.
		/// </summary>
		/// <param name="data">The data to partition in the histogram.</param>
		/// <param name="min">The start-value of the histogram.</param>
		/// <param name="max">The end-value of the histogram.</param>
		/// <param name="partitionCount">The nLumber of partitions.</param>
		public static double[] MakeHistogram(this IEnumerable<double> data, double min, double max, int partitionCount)
		{
			var histogram = new double[partitionCount];
			var binWidth = (max - min) / partitionCount;
			var doubles = data as double[] ?? data.ToArray();
			for(var i = 0; i < partitionCount; i++) {
				var nCounts = doubles.Count(t => t >= min + (i) * binWidth && t < min + (i + 1) * binWidth);
				histogram[i] = nCounts;
			}
			return histogram;
		}

		/// <summary>
		/// Creates an histogram for the given data.
		/// </summary>
		/// <param name="data">The data to partition in the histogram.</param>
		/// <param name="partitionCount">The number of partitions.</param>
		public static double[] MakeHistogram(this IEnumerable<double> data, int partitionCount)
		{
			var doubles = data as double[] ?? data.ToArray();
			// the max has to be slightly larger to ensure the last value is included
			return MakeHistogram(doubles, doubles.Min(), doubles.Max() + 1, partitionCount);
		}

	}
}