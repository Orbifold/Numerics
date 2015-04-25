using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Generic range implementation.
	/// </summary>
	/// <typeparam name="TData">The data type of the range.</typeparam>
	public class Range<TData> : IEnumerable<TData>
	{
		private readonly Comparison<TData> compare;

		private readonly TData end;

		private readonly IEnumerable<TData> sequence;

		private readonly TData start;

		/// <summary>
		/// Initializes a new instance of the <see cref="Range&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="start">The start-element of the range.</param>
		/// <param name="end">The end-element of the range.</param>
		/// <param name="nextElement">The functional mapping from one element to the next one.</param>
		/// <param name="compare">The comparison between two items.</param>
		public Range(TData start, TData end, Func<TData, TData> nextElement, Comparison<TData> compare)
		{
			this.start = start;
			this.end = end;
			this.compare = compare;
			if(compare(start, end) == 0)
				this.sequence = new []{ start };
			else if(compare(start, end) < 0)
				this.sequence = Sequence.CreateSequence(nextElement, start, v => compare(nextElement(v), end) > 0);
			else
				this.sequence = Sequence.CreateSequence(nextElement, start, v => compare(nextElement(v), end) < 0);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Range&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="nextElement">The get next.</param>
		public Range(TData start, TData end, Func<TData, TData> nextElement)
			: this(start, end, nextElement, Compare)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Range`1"/> class.
		/// </summary>
		/// <param name="source">Source.</param>
		public Range(IEnumerable<TData> source)
		{
			this.start = source.First();
			this.end = source.Last();
			this.sequence = source;
			this.compare = Compare;
		}

		/// <summary>
		/// Gets the end of the range.
		/// </summary>
		public TData End {
			get {
				return this.end;
			}
			 
		}

		/// <summary>
		/// Gets the start of the range.
		/// </summary>
		public TData Start {
			get {
				return this.start;
			}
		}

		/// <summary>
		/// Returns whether the given values sits in this range.
		/// </summary>
		/// <remarks>The given value should be larger or equal than the start of the range but
		/// strictly less than the end.
		/// </remarks>
		/// <param name="d">A value.</param>
		public bool Test(double d)
		{
			if(typeof(TData) == typeof(double) || typeof(TData) == typeof(decimal) || typeof(TData) == typeof(Int16) || typeof(TData) == typeof(Int32) || typeof(TData) == typeof(Int64)) {
				return d >= Convert.ToDouble(Start) && d < Convert.ToDouble(End);
			}
			 
			throw new NotSupportedException("The Test method is only supported for a range based on a numeric generic type.");
		}

		/// <summary>
		/// Returns whether the given value is in the range.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if the specified value is inside the range; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(TData value)
		{
			return this.compare(value, this.start) >= 0 && this.compare(this.end, value) >= 0;
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		IEnumerator<TData> IEnumerable<TData>.GetEnumerator()
		{
			return this.sequence.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TData>)this).GetEnumerator();
		}

		/// <summary>
		/// Compares the specified one.
		/// </summary>
		/// <param name="item1">The first item.</param>
		/// <param name="item2">The second item.</param>
		/// <returns></returns>
		private static int Compare(TData item1, TData item2)
		{
			return Comparer<TData>.Default.Compare(item1, item2);
		}
	}
}