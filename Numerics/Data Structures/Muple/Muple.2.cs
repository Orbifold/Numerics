namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension two.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	/// <typeparam name="T2">The data type of the second item.</typeparam>
	public class Muple<T1, T2> : Muple<T1>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1, T2&gt;"/> class.
		/// </summary>
		/// <param name="item1">The item1.</param>
		/// <param name="item2">The item2.</param>
		public Muple(T1 item1, T2 item2)
			: base(item1)
		{
			this.Item2 = item2;
		}

		/// <summary>
		/// Gets or sets the second item.
		/// </summary>
		/// <value>
		/// The item2.
		/// </value>
		public T2 Item2 { get; set; }
	}
}