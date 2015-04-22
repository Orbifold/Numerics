using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A list implementation geared towards functional programming.
	/// </summary>
	/// <typeparam name="T">The data type on which the list is based.</typeparam>
	public sealed class FunctionalList<T> : IEnumerable<T>
	{
		/// <summary>
		/// The empty functional list.
		/// </summary>
		public static readonly FunctionalList<T> Empty = new FunctionalList<T>();

		private readonly T head;

		private readonly bool isEmpty;

		private readonly FunctionalList<T> tail;

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalList&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="head">The head.</param>
		/// <param name="tail">The tail (list).</param>
		public FunctionalList(T head, FunctionalList<T> tail)
		{
			this.head = head;
			this.tail = tail.IsEmpty ? Empty : tail;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalList&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="head">The head.</param>
		public FunctionalList(T head)
			: this(head, Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalList&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="firstValue">The first value.</param>
		/// <param name="values">The values.</param>
		public FunctionalList(T firstValue, params T[] values)
		{
			this.head = firstValue;
			if (values.Length > 0)
			{
				var newtail = Empty;
				for (var i = values.Length - 1; i >= 0; i--) newtail = newtail.Cons(values[i]);
				this.tail = newtail;
			}
			else
			{
				this.tail = Empty;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalList&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		public FunctionalList(IEnumerable<T> source)
		{
			var sourceArray = source.ToArray();
			var length = sourceArray.Length;
			if (length > 0)
			{
				this.head = sourceArray[0];
				this.tail = Empty;
				for (var i = length - 1; i > 0; i--) this.tail = this.tail.Cons(sourceArray[i]);
			}
			else
			{
				this.isEmpty = true;
			}
		}

		private FunctionalList()
		{
			this.isEmpty = true;
		}

		/// <summary>
		/// Gets the head of the list.
		/// </summary>
		public T Head
		{
			get
			{
				if (this.isEmpty) throw new InvalidOperationException("The list is empty.");
				return this.head;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
		/// </value>
		public bool IsEmpty
		{
			get
			{
				return this.isEmpty;
			}
		}

		/// <summary>
		/// Gets the tail of the list.
		/// </summary>
		public FunctionalList<T> Tail
		{
			get
			{
				if (this.isEmpty) throw new InvalidOperationException("The list is empty.");
				return this.tail;
			}
		}

		/// <summary>
		/// Prepends the given element to the given list.
		/// </summary>
		/// <param name="element">The element to prepend.</param>
		/// <param name="list">The list or tail.</param>
		/// <returns></returns>
		public static FunctionalList<T> Cons(T element, FunctionalList<T> list)
		{
			return list.IsEmpty ? new FunctionalList<T>(element) : new FunctionalList<T>(element, list);
		}

		/// <summary>
		/// Appends the given list.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public FunctionalList<T> Append(FunctionalList<T> other)
		{
			return FunctionalExtensions.Append(this, other);
		}

		/// <summary>
		/// Prepends the specified element.
		/// </summary>
		/// <remarks>This mimics the '::' operator from F#.</remarks>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public FunctionalList<T> Cons(T element)
		{
			return Cons(element, this);
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			for (var element = this; element != Empty; element = element.Tail)
			{
				yield return element.Head;
			}
		}

		/// <summary>
		/// Removes the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public FunctionalList<T> Remove(T element)
		{
			return FunctionalExtensions.Remove(this, element);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			var result = "[";
			var toomuch = this.Count() > 10;
			var bucket = toomuch ? this.Map(x => x.ToString()).Take(10) : this.Map(x => x.ToString());
			if (!this.IsEmpty) result += bucket.Fold((r, x) => r + ", " + x, string.Empty);
			result += toomuch ? "...]" : "]";
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}