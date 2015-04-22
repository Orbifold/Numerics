namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension one.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	/// <typeparam name="T2">The data type of the second item.</typeparam>
	/// <typeparam name="T3">The data type of the third item.</typeparam>
	/// <typeparam name="T4">The data type of the fourth item.</typeparam>
	public class Muple<T1, T2, T3, T4> : Muple<T1, T2, T3>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1, T2, T3, T4&gt;"/> class.
		/// </summary>
		/// <param name="item1">The item1.</param>
		/// <param name="item2">The item2.</param>
		/// <param name="item3">The item3.</param>
		/// <param name="item4">The item4.</param>
		public Muple(T1 item1, T2 item2, T3 item3, T4 item4)
			: base(item1, item2, item3)
		{
			this.Item4 = item4;
		}

		/// <summary>
		/// Gets or sets the fourth item.
		/// </summary>
		/// <value>
		/// The item4.
		/// </value>
		public T4 Item4 { get; set; }
	}
}