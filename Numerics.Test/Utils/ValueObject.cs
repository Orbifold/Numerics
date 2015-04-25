using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Orbifold.Numerics.Tests
{
	public class ValueObject
	{
		[Feature]
		public int V1 { get; set; }

		[Feature]
		public int V2 { get; set; }

		[StringLabel]
		public string R { get; set; }

		public static IEnumerable<ValueObject> GetData()
		{
			for(int i = 0; i < 100; i++) {
				yield return new ValueObject { V1 = 1, V2 = i, R = (i > 50) ? "l" : "s" };
			}
		}
	}
	
}