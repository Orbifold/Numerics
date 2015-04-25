using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Orbifold.Numerics.Tests
{
	public class ArbitraryPrediction
	{
		public enum PredictionLabel
		{
			Minimum,
			Medium,
			Maximum
		}

		[Feature]
		public decimal FirstTestFeature { get; set; }

		[Feature]
		public decimal SecondTestFeature { get; set; }

		[Feature]
		public decimal ThirdTestFeature { get; set; }

		[Label]
		public PredictionLabel OutcomeLabel { get; set; }

		public static ArbitraryPrediction[] GetData()
		{
			var returnData = new List<ArbitraryPrediction>();

			for(int i = 0; i < 80; i++) {
				returnData.Add(
					new ArbitraryPrediction {
						FirstTestFeature = 1.0m,
						SecondTestFeature = i,
						ThirdTestFeature = 1.2m,
						OutcomeLabel = (i < 50) ? PredictionLabel.Minimum : PredictionLabel.Maximum
					});
			}

			return returnData.ToArray();
		}

		public static IEnumerable<ArbitraryPrediction> GetDataUsingNamedIterator()
		{
			for(int i = 0; i < 80; i++) {
				yield return new ArbitraryPrediction {
					FirstTestFeature = 1.0m,
					SecondTestFeature = i,
					ThirdTestFeature = 1.2m,
					OutcomeLabel = (i < 50) ? PredictionLabel.Minimum : PredictionLabel.Maximum
				};
			}
		}
	}
	
}