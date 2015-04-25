using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Defines a metric function or structure.
	/// </summary>
	public interface IDistance
	{
		/// <summary>
		/// Calculate the distance between the given vectors.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		double Compute(Vector x, Vector y);
	}

	/// <summary>
	/// The cosine metric.
	/// </summary>
	public sealed class CosineDistance : IDistance
	{
		/// <summary>Computes.</summary>
		/// <param name="x">The RVector to process.</param>
		/// <param name="y">The RVector to process.</param>
		/// <returns>A double.</returns>
		public double Compute(Vector x, Vector y)
		{   
			return 1d - (x.Dot(y) / (x.Norm() * y.Norm()));
		}
	}

	/// <summary>
	/// A similarity measure or similarity function is a real-valued function that quantifies the similarity between two objects.
	/// Although no single definition of a similarity measure exists, usually similarity measures are in some sense the inverse 
	/// of distance metrics: they take on large values for similar objects and either zero or a negative value for very dissimilar objects.
	/// </summary>
	public interface ISimilarity
	{
		/// <summary>
		/// Returns the similarity of the given vectors.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		double Compute(Vector x, Vector y);
	}

	/// <summary>
	/// The cosine similarity measure.
	/// </summary>
	public sealed class CosineSimilarity : ISimilarity
	{
		/// <summary>
		/// Returns the similarity of the given vectors.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public double Compute(Vector x, Vector y)
		{
			return x.Dot(y) / (x.Norm() * y.Norm());
		}
	}

	/// <summary>
	/// The standard Euclidean metric.
	/// </summary>
	public sealed class EuclidianDistance : IDistance
	{
		/// <summary>
		/// Calculate the distance between the given vectors.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public double Compute(Vector x, Vector y)
		{
			return (x - y).Norm();
		}
	}

	public sealed class EuclidianSimilarity : ISimilarity
	{
		public double Compute(Vector x, Vector y)
		{
			return 1 / (1 + (x - y).Norm(2));
		}
	}

	/// <summary>
	/// In information theory, the Hamming distance between two strings of equal length is the number of positions at which 
	/// the corresponding symbols are different. In another way, it measures the minimum number of substitutions required to change 
	/// one string into the other, or the minimum number of errors that could have transformed one string into the other.
	/// </summary>
	public sealed class HammingDistance : IDistance
	{
		public double Compute(Vector x, Vector y)
		{
			if(EpsilonExtensions.AreNotClose(x.Dimension, y.Dimension))
				throw new InvalidOperationException("The distance between vectors of different dimensions is not defined.");

			double sum = 0;
			for(int i = 0; i < x.Dimension; i++)
				if(EpsilonExtensions.AreNotClose(x[i], y[i]))
					sum++;
			return sum;
		}
	}

	public sealed class ManhattanDistance : IDistance
	{
		/// <summary>Computes.</summary>
		/// <param name="x">The RVector to process.</param>
		/// <param name="y">The RVector to process.</param>
		/// <returns>A double.</returns>
		public double Compute(Vector x, Vector y)
		{
			return (x - y).Norm(1);
		}
	}

	public sealed class PearsonCorrelation : ISimilarity
	{

		public double Compute(Vector x, Vector y)
		{
			if(x.Dimension != y.Dimension)
				throw new InvalidOperationException("The Pearson correlation is not defined for vectors with different dimensions.");
			var xm = x.Mean();
			var ym = y.Mean();
			return ((x.Dot(y) / x.Dimension) - (xm * ym)) / (x.StandardDeviation() * y.StandardDeviation());
		}
	}

}

