namespace Orbifold.Numerics
{
	/// <summary>
	/// A mutable tuple of dimension seven.
	/// </summary>
	/// <typeparam name="T1">The data type of the first item.</typeparam>
	/// <typeparam name="T2">The data type of the second item.</typeparam>
	/// <typeparam name="T3">The data type of the third item.</typeparam>
	/// <typeparam name="T4">The data type of the fourth item.</typeparam>
	/// <typeparam name="T5">The data type of the fifth item.</typeparam>
	/// <typeparam name="T6">The type of the sixth item.</typeparam>
	/// <typeparam name="T7">The type of the seventh item.</typeparam>
	public class Muple<T1, T2, T3, T4, T5, T6, T7> : Muple<T1, T2, T3, T4, T5, T6>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Muple&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/> class.
		/// </summary>
		/// <param name="item1">The item1.</param>
		/// <param name="item2">The item2.</param>
		/// <param name="item3">The item3.</param>
		/// <param name="item4">The item4.</param>
		/// <param name="item5">The item5.</param>
		/// <param name="item6">The item6.</param>
		/// <param name="item7">The item7.</param>
		public Muple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
			: base(item1, item2, item3, item4, item5, item6)
		{
			this.Item7 = item7;
		}

		/// <summary>
		/// Gets or sets the seventh item.
		/// </summary>
		/// <value>
		/// The item7.
		/// </value>
		public T7 Item7 { get; set; }
	}
}