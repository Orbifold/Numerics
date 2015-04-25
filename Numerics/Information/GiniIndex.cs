using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{

	/// <summary>
	/// This class calculates the Gini Index of any given vector. 
	/// </summary>
	public class GiniIndex : Impurity
	{
		/// <summary>
		/// The Gini coefficient (also known as the Gini index or Gini ratio) 
		/// is a measure of statistical dispersion intended to represent the income distribution of a nation's residents, 
		/// and is the most commonly used measure of inequality.
		/// </summary>
		public override double Calculate(Vector x)
		{
			var ar = x.ToArray();
			var px = (from i in ar.Distinct()
			          let q = (from j in ar
			                   where i.IsEqualTo(j)
			                   select j).Count()
			          select q / (double)x.Dimension);

			var g = 1 - px.Sum(d => d * d);

			return g;
		}
	}
	
}
