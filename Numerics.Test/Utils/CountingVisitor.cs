namespace Orbifold.Numerics.Tests
{
	public class CountingVisitor<T> : Visitor<T>
	{
		public int Count { get; set; }
		public override void Visit(T item)
		{
			this.Count++;
		}

		public CountingVisitor()
		{
			this.Count = 0;

		}
	}
}