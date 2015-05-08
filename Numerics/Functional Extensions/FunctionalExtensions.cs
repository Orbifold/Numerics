using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	#if PCL
	public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
	#endif
	/// <summary>
	/// A collection of utility methods inspired by functional programming techniques in F#.
	/// </summary>
	public static class FunctionalExtensions
	{
		/*Swa: need to be able to replace the Random with IStochastic*/

		/// <summary>
		/// The randomizer.
		/// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		/// Appends the given list to the current one.
		/// </summary>
		/// <param name="list">The one.</param>
		/// <param name="otherList">The other.</param>
		/// <returns></returns>
		public static FunctionalList<T> Append<T>(this FunctionalList<T> list, FunctionalList<T> otherList)
		{
			return list.IsEmpty ? otherList : list.Reverse().Aggregate(otherList, (current, element) => current.Cons(element));
		}

		/// <summary>
		/// Returns whether all the elements are different in the given sequence.
		/// </summary>
		/// <typeparam name="T">The data type contained in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static bool AreAllDifferent<T>(this IEnumerable<T> list)
		{
			if(list == null) {
				throw new ArgumentNullException("list");
			}
			return !list.Any(m => list.Count(l => EqualityComparer<T>.Default.Equals(l, m)) > 1);
		}

		/// <summary>
		/// Collects elements into a new sequence by applying the given converter to each element.
		/// </summary>
		/// <typeparam name="TDomain">The data type of the list.</typeparam>
		/// <typeparam name="TRange">The data type of the target list.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static IEnumerable<TRange> Collect<TDomain, TRange>(this IEnumerable<TDomain> list, Converter<TDomain, IEnumerable<TRange>> converter)
		{
			var listOfLists = Map(list, converter);
			return Concat(listOfLists);
		}

		/// <summary>
		/// Concatenates the sequence of sequences into one sequence.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="sequences">The sequences.</param>
		/// <returns></returns>
		public static IEnumerable<T> Concat<T>(IEnumerable<IEnumerable<T>> sequences)
		{
			return sequences.Where(s => s != null).SelectMany(sequence => sequence);
		}

		/// <summary>
		/// Filters the list using a <see cref="Predicate{T}"/>.
		/// </summary>
		/// <typeparam name="T">The data type of the list being filtered.</typeparam>
		/// <param name="predicate">The predicate.</param>
		/// <param name="list">The list being filtered.</param>
		/// <returns></returns>
		public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			return list.Where(val => predicate(val));
		}

		/// <summary>
		/// Folds the list.
		/// </summary>
		/// <typeparam name="TDomain">The data type of the domain.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="list">The list.</param>
		/// <param name="accumulator">The accumulator functional; the first parameter is the current value of the accumulator and the second the current list value.</param>
		/// <param name="accumulatorStartValue">The accumulator's start value.</param>
		/// <returns></returns>
		public static TRange Fold<TDomain, TRange>(this IEnumerable<TDomain> list, Func<TRange, TDomain, TRange> accumulator, TRange accumulatorStartValue)
		{
			return list.Aggregate(accumulatorStartValue, accumulator);
		}

		/// <summary>
		/// Folds the list and returns the intermediate steps.
		/// </summary>
		/// <typeparam name="TDomain">The data type of the domain.</typeparam>
		/// <typeparam name="TTarget">The type of the range.</typeparam>
		/// <param name="list">The list.</param>
		/// <param name="accumulator">The accumulator functional; the first parameter is the current value of the accumulator and the second the current list value.</param>
		/// <param name="seed">The accumulator's start value.</param>
		/// <returns></returns>
		public static IEnumerable<TTarget> FoldList<TDomain, TTarget>(this IEnumerable<TDomain> list, Func<TTarget, TDomain, TTarget> accumulator, TTarget seed)
		{
			var e = list.GetEnumerator();
			var t = seed;
			while(e.MoveNext()) {
				t = accumulator(t, e.Current);
				yield return t;
			}
		}

		/// <summary>
		/// Applies the action to each element of the sequence.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach(var item in source)
				action(item);
		}

		/// <summary>
		/// Applies the action to each element of the sequence.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		public static void ForEach<T>(this IEnumerable<T> source, Action action)
		{
			if(source == null)
				throw new ArgumentNullException("source");
			if(action == null)
				throw new ArgumentNullException("action");
			source.ToList().ForEach(m => action());
		}

		/// <summary>
		/// Compares the two given lists if they have the same item content, not necessarily in the same order.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="list1">The list1.</param>
		/// <param name="list2">The list2.</param>
		/// <returns></returns>
		public static bool HaveSameContent<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			if(list1 == null)
				throw new ArgumentNullException("list1");
			if(list2 == null)
				throw new ArgumentNullException("list2");
			if(list1.Count() != list2.Count())
				return false;
			var orderedList1 = list1.OrderBy(m => m);
			var orderedList2 = list2.OrderBy(m => m);
			for(var i = 0; i < orderedList1.Count(); i++) {
				if(!EqualityComparer<T>.Default.Equals(orderedList1.ElementAt(i), orderedList2.ElementAt(i))) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Creates an array and initializes the element through the given functional.
		/// </summary>
		/// <typeparam name="T">The data type of the arry elements.</typeparam>
		/// <param name="length">The length.</param>
		/// <param name="elementInit">The functional used to initialize the elements.</param>
		/// <returns></returns>
		public static T[] InitArray<T>(int length, Func<int, T> elementInit)
		{
			var array = new T[length];
			for(var i = 0; i < length; i++) {
				array[i] = elementInit(i);
			}
			return array;
		}

		/// <summary>
		/// Returns a lazy instance defined by the given functional.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="func">The functional returning the instance for this lazy object.</param>
		/// <returns></returns>
		public static FunctionalLazy<T> Lazy<T>(this Func<T> func)
		{
			return new FunctionalLazy<T>(func);
		}

		/// <summary>
		/// Returns a lazy instance defined by the given value.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static FunctionalLazy<T> Lazy<T>(T value)
		{
			return new FunctionalLazy<T>(value);
		}

		/// <summary>
		/// Maps the elements of the list to a new list.
		/// </summary>
		/// <typeparam name="TOriginal">The type of the source elements.</typeparam>
		/// <typeparam name="TMapped">The type of the mapped elements.</typeparam>
		/// <param name="list">The list to map.</param>
		/// <param name="function">The function applied to the elements of the list.</param>
		/// <returns>The mapped list.</returns>
		public static IEnumerable<TMapped> Map<TOriginal, TMapped>(this IEnumerable<TOriginal> list, Converter<TOriginal, TMapped> function)
		{
			return list.Select(sourceVal => function(sourceVal));
		}

		/// <summary>
		/// Removes the specified list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="element">The element to remove.</param>
		/// <returns>The list without the removed element.</returns>
		public static FunctionalList<T> Remove<T>(FunctionalList<T> list, T element)
		{
			var to = FunctionalList<T>.Empty;
			var from = list;
			while(!from.IsEmpty && !EqualityComparer<T>.Default.Equals(from.Head, element)) {
				to = to.Cons(from.Head);
				from = from.Tail;
			}
			return !from.IsEmpty ? to.Aggregate(from.Tail, (current, item) => current.Cons(item)) : list;
		}

		/// <summary>
		/// Reverses the specified sequence.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static IEnumerable<T> Reverse<T>(IEnumerable<T> source)
		{
			var stack = source.Aggregate(FunctionalList<T>.Empty, (current, item) => current.Cons(item));
			while(stack != FunctionalList<T>.Empty) {
				yield return stack.Head;
				stack = stack.Tail;
			}
		}

		/// <summary>
		/// Returns a random reordering of the given collection.
		/// </summary>
		/// <typeparam name="T">The data type of the enumerable collection.</typeparam>
		/// <param name="source">The source collection to be scrambled.</param>
		/// <returns>The same collection of elements in a different order.</returns>
		public static IEnumerable<T> Scramble<T>(this IEnumerable<T> source)
		{
			var clone = source.ToList();
			while(clone.Any()) {
				var index = Rand.Next(clone.Count);
				var item = clone.ElementAt(index);
				clone.RemoveAt(index);
				yield return item;
			}
		}

		/// <summary>
		/// Interpolates linearly the specified range. to the specified target. 
		/// </summary>
		/// <example>
		/// If the range is [0,12] and the target [101,145] with a default subdivision of 100 the value zero will map to (0,101) and 50 to (6,123).
		/// The values outside the subdivision range will be clipped to the initial, respectively final value.
		/// </example>
		/// <param name="range">The range.</param>
		/// <param name="target">The target.</param>
		/// <param name="subdivision">The subdivision.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">subdivision</exception>
		public static Func<int, Tuple<double, double>> Interpolate(this Tuple<double, double> range, Tuple<double, double> target, int subdivision = 100)
		{
			if(subdivision <= 0)
				throw new ArgumentException("subdivision");
			var a = range.Item1;
			var b = range.Item2;
			var A = target.Item1;
			var B = target.Item2;

			var delta = (b - a) / subdivision;
			var Delta = (B - A) / subdivision;

			return e => {
				if(e < 0)
					return Tuple.Create(a, A);
				if(e > subdivision)
					return Tuple.Create(b, B);
				return Tuple.Create(a + e * delta, A + e * Delta);
			};
		}

		/// <summary>
		/// Swaps the specified func.
		/// </summary>
		/// <typeparam name="TDomain1">The type of the domain1.</typeparam>
		/// <typeparam name="TDomain2">The type of the domain2.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="func">The func.</param>
		/// <returns></returns>
		public static Func<TDomain2, Func<TDomain1, TRange>> Swap<TDomain1, TDomain2, TRange>(Func<TDomain1, Func<TDomain2, TRange>> func)
		{
			return p2 => p1 => func(p1)(p2);
		}

		/// <summary>
		/// Converts the given enumerable to a <see cref="FunctionalList{T}"/>.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>
		/// <param name="range">The range.</param>
		/// <returns></returns>
		public static FunctionalList<T> ToFunctionalList<T>(this IEnumerable<T> range)
		{
			return new FunctionalList<T>(range);
		}
	}
}