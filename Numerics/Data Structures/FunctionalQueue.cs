using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// An implementation of a queue geared towards functional programming.
	/// </summary>
	/// <typeparam name="TData">The data type contained in the queue.</typeparam>
	public sealed class FunctionalQueue<TData>
	{
		/// <summary>
		/// Returns the empty queue.
		/// </summary>
		public static readonly FunctionalQueue<TData> Empty = new FunctionalQueue<TData>();

		private readonly FunctionalList<TData> f, r;

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalQueue&lt;TData&gt;"/> class.
		/// </summary>
		public FunctionalQueue()
			: this(FunctionalList<TData>.Empty, FunctionalList<TData>.Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalQueue&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		public FunctionalQueue(IEnumerable<TData> source)
		{
			var acc = source.Aggregate(Empty, (current, item) => current.Snoc(item));
			this.f = acc.f;
			this.r = acc.r;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalQueue&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="values">The values.</param>
		public FunctionalQueue(TData first, params TData[] values)
		{
			var acc = Empty;
			acc = acc.Snoc(first);
			acc = values.Aggregate(acc, (current, item) => current.Snoc(item));
			this.f = acc.f;
			this.r = acc.r;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalQueue&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="f">The first.</param>
		/// <param name="r">The values.</param>
		private FunctionalQueue(FunctionalList<TData> f, FunctionalList<TData> r)
		{
			this.f = f;
			this.r = r;
		}

		/// <summary>
		/// Gets the number of elements in this queue.
		/// </summary>
		public int Count
		{
			get
			{
				return IsEmpty ? 0 :this.r.Count() +1;
			}
		}

		/// <summary>
		/// Gets the head of this queue.
		/// </summary>
		public TData Head
		{
			get
			{
				return this.f.Head;
			}
		}

		/// <summary>
		/// Gets this queue is empty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
		/// </value>
		public bool IsEmpty
		{
			get
			{
				return this.f.IsEmpty;
			}
		}

		/// <summary>
		/// Gets the tail of this queue.
		/// </summary>
		public FunctionalQueue<TData> Tail
		{
			get
			{
				return CheckBalance(new FunctionalQueue<TData>(this.f.Tail, this.r));
			}
		}

		/// <summary>
		/// Snocs the specified element, i.e.. adds it to the end of the queue.
		/// </summary>
		/// <param name="queue">The queue.</param>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static FunctionalQueue<TData> Snoc(FunctionalQueue<TData> queue, TData element)
		{
			return CheckBalance(new FunctionalQueue<TData>(queue.f, queue.r.Cons(element)));
		}

		/// <summary>
		/// Appends the given element.
		/// </summary>
		/// <param name="element">The element to append to the queue.</param>
		/// <returns>The resulting queue.</returns>
		public FunctionalQueue<TData> Snoc(TData element)
		{
			return Snoc(this, element);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("[f:{0} r:{1}]", this.f, this.r);
		}

		private static FunctionalQueue<TData> CheckBalance(FunctionalQueue<TData> q)
		{
			return q.f.IsEmpty ? new FunctionalQueue<TData>(new FunctionalList<TData>(FunctionalExtensions.Reverse(q.r)), FunctionalList<TData>.Empty) : q;
		}
	}
}