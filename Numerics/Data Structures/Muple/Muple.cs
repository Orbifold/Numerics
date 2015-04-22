namespace Orbifold.Numerics
{
	/// <summary>
	/// An empty, mutable tuple.
	/// </summary>
	public abstract class Muple
	{
		/// <summary>
		/// Creates a 1-muple.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <param name="t1">The t1.</param>
		/// <returns></returns>
		public static Muple<T1> Create<T1>(T1 t1)
		{
			return new Muple<T1>(t1);
		}
		/// <summary>
		/// Creates a 2-muple.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="t1">The t1.</param>
		/// <param name="t2">The t2.</param>
		/// <returns></returns>
		public static Muple<T1, T2> Create<T1, T2>(T1 t1, T2 t2)
		{
			return new Muple<T1, T2>(t1, t2);
		}
		/// <summary>
		/// Creates a 3-muple.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="T3">The type of the 3.</typeparam>
		/// <param name="t1">The t1.</param>
		/// <param name="t2">The t2.</param>
		/// <param name="t3">The t3.</param>
		/// <returns></returns>
		public static Muple<T1, T2, T3> Create<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
		{
			return new Muple<T1, T2, T3>(t1, t2, t3);
		}
	}
}