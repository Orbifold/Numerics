namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension three.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	/// <typeparam name="T2">The data type of the second item.</typeparam>
	/// <typeparam name="T3">The data type of the third item.</typeparam>
	public class Muple<T1, T2, T3> : Muple<T1, T2>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1, T2, T3&gt;"/> class.
		/// </summary>
		/// <param name="item1">The item1.</param>
		/// <param name="item2">The item2.</param>
		/// <param name="item3">The item3.</param>
		public Muple(T1 item1, T2 item2, T3 item3)
			: base(item1, item2)
		{
			this.Item3 = item3;
		}

		/// <summary>
		/// Gets or sets the third item.
		/// </summary>
		/// <value>
		/// The item3.
		/// </value>
		public T3 Item3 { get; set; }
	}
}