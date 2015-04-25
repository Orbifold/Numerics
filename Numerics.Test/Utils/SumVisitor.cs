using System.Collections.Generic;

namespace Orbifold.Numerics.Tests
{
	public class SumVisitor : Visitor<KeyValuePair<string, double>>
	{
		public double Sum { get; set; }
		public override void Visit(KeyValuePair<string, double> item)
		{
			this.Sum += item.Value;
		}

		public SumVisitor()
		{
			this.Sum = 0d;
		}
	}
}