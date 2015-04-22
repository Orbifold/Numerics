namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension one.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	public class Muple<T1> : Muple
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1&gt;"/> class.
		/// </summary>
		/// <param name="item1">The item1.</param>
		public Muple(T1 item1)
		{
			this.Item1 = item1;
		}

		/// <summary>
		/// Gets or sets the first item.
		/// </summary>
		/// <value>
		/// The item1.
		/// </value>
		public T1 Item1 { get; set; }
	}
}