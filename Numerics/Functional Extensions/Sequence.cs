using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Sequence extensions.
	/// </summary>
	public static class Sequence
	{
		static readonly Random Rand = new Random(Environment.TickCount);
        /// <summary>
        /// Calculates the standard deviation on the source collection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="fnPropSelector"></param>
        /// <param name="isSamplePopulation"></param>
        /// <returns></returns>
        public static double StandardDeviation<TSource>(this IEnumerable<TSource> source, Func<TSource, double> fnPropSelector, bool isSamplePopulation = false)
        {
            return System.Math.Sqrt(Variance(source, fnPropSelector, isSamplePopulation));
        }

        /// <summary>
        /// Calculates the variance on the source collection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="fnPropSelector"></param>
        /// <param name="isSamplePopulation"></param>
        /// <returns></returns>
        public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> fnPropSelector, bool isSamplePopulation = false)
        {
            return source.Select(s => System.Math.Pow(fnPropSelector(s) - source.Average(fnPropSelector), 2)).Sum() / (isSamplePopulation ? source.Count() - 1 : source.Count());
        }

		public static RMatrix ToMatrix(this IEnumerable<Vector> source)
		{
			var vecs = source.ToArray();
			var c = vecs.Count();
			if(c == 0)
				throw new InvalidOperationException("Cannot create matrix from an empty set.");

			var i = 0;
			var m = new double[c, vecs.First().Dimension];
			foreach(var v in vecs) {
				for(int j = 0; j < v.Dimension; j++)
					m[i, j] = v[j];
				i++;
			}

			return m;
		}

		public static Vector Mean(this IEnumerable<Vector> source)
		{
			var l = source.ToArray();
			var c = l.Count();
			if(c == 0)
				throw new InvalidOperationException("Cannot compute average of an empty set.");

			return l.Sum() / c;
		}

		public static Vector Sum(this IEnumerable<Vector> source)
		{
			return source.Aggregate((s, n) => s += n);
		}

		/// <summary>
		/// Compares the two given lists per item if they are the same.
		/// </summary>
		/// <typeparam name="T">The data type contained in the lists.</typeparam>
		/// <param name="list1">A sequence.</param>
		/// <param name="list2">Another sequence.</param>
		public static bool AreSameLists<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			if(list1 == null)
				throw new ArgumentNullException("list1");
			if(list2 == null)
				throw new ArgumentNullException("list2");
			var l1 = list1.ToArray();
			var l2 = list2.ToArray();
			if(l1.Count() != l2.Count())
				return false;
			for(var i = 0; i < l1.Count(); i++) {
				if(!EqualityComparer<T>.Default.Equals(l1.ElementAt(i), l2.ElementAt(i)))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns the index of the (first) minimum entry.
		/// </summary>
		/// <returns>The index.</returns>
		/// <param name="source">The enumerable.</param>
		/// <seealso cref="RVector.MaxIndex"/>
		/// <seealso cref="MinIndex"/>
		public static int MinIndex(this IEnumerable<double> source)
		{
			double minValue = double.MaxValue;
			int minIndex = -1;
			int index = -1;
			foreach(double x in source) {
				index++;
				if(x < minValue) {
					minValue = x;
					minIndex = index;
				}
			}

			return minIndex;
		}

		/// <summary>
		/// Returns the index of the (first) maximum entry.
		/// </summary>
		/// <returns>The index.</returns>
		/// <param name="source">The enumerable.</param>
		/// <seealso cref="RVector.MaxIndex"/>
		public static int MaxIndex(this IEnumerable<double> source)
		{
			double maxValue = double.MinValue;
			int maxIndex = -1;
			int index = -1;
			foreach(double x in source) {
				index++;
				if(x > maxValue) {
					maxValue = x;
					maxIndex = index;
				}
			}

			return maxIndex;
		}

		/// <summary>
		/// Creates a sequence.
		/// </summary>
		/// <typeparam name="TData">The data type of the sequence.</typeparam>
		/// <param name="getNext">The next method.</param>
		/// <param name="startVal">The start value.</param>
		/// <param name="endReached">The method specifying when the end has been reached.</param>
		/// <returns></returns>
		public static IEnumerable<TData> Create<TData>(Func<TData, TData> getNext, TData startVal, Func<TData, bool> endReached)
		{
			return CreateSequence(getNext, startVal, endReached);
		}

		/// <summary>
		/// Creates a sequence of random dates over the maximum datetime interval.
		/// </summary>
		/// <param name="count">The length of the sequence.</param>
		/// <returns></returns>
		public static IEnumerable<DateTime> CreateRandomDates(int count)
		{
			var minDate = DateTime.MinValue;
			var maxDate = DateTime.MaxValue;
			var delta = (int)Math.Floor((maxDate - minDate).TotalDays);
			return Range.Create(1, count).ToList().Select(m => minDate.AddDays(Rand.Next(delta)));
		}

		/// <summary>
		/// Creates a sequence of random integers.
		/// </summary>
		/// <param name="count">The length of the sequence.</param>
		/// <param name="min">The minimum value of the random integers.</param>
		/// <param name="max">The maximum value of the integers.</param>
		public static IEnumerable<int> CreateRandomNumbers(int count, int min = 0, int max = 1000)
		{
			return Range.Create(1, count).ToList().Select(m => Rand.Next(min, max));
		}

		/// <summary>
		/// Creates a sequence of random floating point numbers.
		/// </summary>
		/// <param name="count">The length of the sequence.</param>
		/// <param name="min">The minimum value of the random sequence values.</param>
		/// <param name="max">The maximum value of the random sequence values.</param>
		/// <returns></returns>
		public static IEnumerable<double> CreateRandomDoubles(int count, double min = 0, double max = 1000)
		{
			if(max <= min)
				throw new Exception("The maximum needs to be bigger than the minimum.");
			return Range.Create(1, count).ToList().Select(m => min + (Rand.NextDouble() * max));
		}

		/// <summary>
		/// Finds the duplicates in the list of points which leads to loops in the paths.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static IEnumerable<Point> FindDuplicates(IEnumerable<Point> source)
		{
			var seenKeys = new HashSet<Point>();
			return source.Where(element => !seenKeys.Add(element));
		}

		/// <summary>
		/// Creates a custom enumerable sequence.
		/// </summary>
		/// <remarks>The sequence can be infinite in contrast to lists and other similar structures.</remarks>
		/// <typeparam name="T">The data type of the sequence.</typeparam>
		/// <param name="getNext">The get next.</param>
		/// <param name="startVal">The start val.</param>
		/// <param name="endReached">The end reached.</param>
		/// <returns></returns>
		internal static IEnumerable<T> CreateSequence<T>(Func<T, T> getNext, T startVal, Func<T, bool> endReached)
		{
			if(getNext == null)
				yield break;
			yield return startVal;
			var val = startVal;
			while(endReached == null || !endReached(val)) {
				val = getNext(val);
				yield return val;
			}
		}

		/// <summary>
		/// Clones the specified list.
		/// </summary>
		/// <param name="list">The list to clone.</param>
		/// <returns>The cloned list.</returns>
		public static IEnumerable<T> Clone<T>(this IEnumerable<T> list)
		{
			if(list == null)
				throw new ArgumentNullException("list");
			var clone = new List<T>();
			list.ForEach(clone.Add);
			return clone;
		}

		/// <summary>
		/// Copies the content of the given array into the specified list.
		/// </summary>
		/// <typeparam name="T">The type type.</typeparam>
		/// <param name="sourceArray">The source array.</param>
		/// <param name="distinationList">The distination list.</param>
		public static void CopyTo<T>(this T[] sourceArray, IList<T> distinationList)
		{
			for(int i = 0, n = sourceArray.Length; i < n; ++i)
				distinationList.Add(sourceArray[i]);
		}

		/// <summary>
		/// Returns a more readable format of the string sequence.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="amountToShow">The amount of items to present in the output.</param>
		/// <returns></returns>
		public static string ToPrettyFormat<T>(this IEnumerable<T> list, int amountToShow = 10)
		{
			if(amountToShow < 1)
				throw new Exception("The amount should be bigger than one.");
			var sb = new StringBuilder();
			var l = list.ToList();
			l.Take(amountToShow).ForEach(s => sb.AppendFormat("{0}, ", s));
			sb.Remove(sb.Length - 2, 2);
			sb.Insert(0, "{");
			if(l.Count() > amountToShow)
				sb.Append("...");
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		/// Returns the variance of the given sequence.
		/// </summary>
		/// <param name="data">A sequence.</param>
		/// <returns></returns>
		public static double Variance(this IEnumerable<double> data)
		{
			if(data == null)
				throw new ArgumentNullException("data");
			var variance = 0d;
			var current = 0d;
			var i = 0;

			using (var enumerator = data.GetEnumerator()) {
				if(enumerator.MoveNext()) {
					i++;
					current = enumerator.Current;
				}

				while(enumerator.MoveNext()) {
					var c = enumerator.Current;
					current += c;
					i++;
					var diff = (i * c) - current;
					variance += (diff * diff) / (i * (i - 1));
				}
			}
			return variance / (i - 1);
		}

		/// <summary>
		/// Returns the standard deviation of the sequence.
		/// </summary>
		public static double StandardDeviation(this IEnumerable<double> data)
		{
			return Math.Sqrt(Variance(data));
		}

		/// <summary>
		/// Creates a sequence using the given function.
		/// </summary>
		/// <typeparam name="T">The data type of the sequence.</typeparam>
		/// <param name="f">The function which maps the zero-based enumeration to the type of the sequence.</param>
		/// <param name="count">The length of the sequence to generate.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">count</exception>
		public static IEnumerable<T> Create<T>(Func<int, T> f, int count = 100)
		{
			if(count <= 0)
				throw new ArgumentException("count");
			for(var i = 0; i < count; i++)
				yield return f(i);
		}

		public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> seq, int index)
		{
			if(index < 0)
				throw new ArgumentOutOfRangeException("index");
			var l = seq.ToArray();
			if(!l.Any())
				throw new ArgumentException("Sequence is empty.", "index");
			if(index >= l.Count())
				throw new ArgumentOutOfRangeException("index");
			var i = 0;
			foreach(var x in l) {
				if(i != index)
					yield return x;
				i++;
			}
		}


		/// <summary>
		/// Returns the index numbers within the given source satisfying the predicate.
		/// </summary>
		/// <param name="source">The sequence of items to test.</param>
		/// <param name="f">The predicate test.</param>
		public static IEnumerable<int> Indices<T>(this IEnumerable<T> source, Predicate<T> f)
		{
			int i = -1;
			foreach(var item in source) {
				++i;
				if(f(item))
					yield return i;
			}
		}

		/// <summary>
		/// Drops the element at the specified position. For position values this is equivalent to the <see cref="RemoveAt{T}"/> method. For negative values 
		/// the -1 position will remove the last elements and so on.
		/// </summary>
		/// <typeparam name="T">The data type of the sequence.</typeparam>
		/// <param name="list">The list.</param>
		/// <param name="position">The position at which the element has to be removed.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Cannot drop at position zero; the sequence is empty.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">The negative position is beyond the start of the sequence.;position</exception>
		public static IEnumerable<T> Drop<T>(this IEnumerable<T> list, int position)
		{
			var l = list.ToArray();
			if(position == 0) {
				if(!l.Any())
					throw new Exception("Cannot drop at position zero; the sequence is empty.");
				return l.RemoveAt(0);
			}
			if(position > 0)
				return l.RemoveAt(position);
			if(-position > l.Count())
				throw new ArgumentOutOfRangeException("position", "The negative position is beyond the start of the sequence.");
			return l.RemoveAt(l.Count() + position);
		}


		/// <summary>
		/// Sums the elements of the two sequences.
		/// </summary>
		/// <param name="seqs">A sequence of doubles.</param>
		/// <param name="seqs">Another sequence of doubles.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">The length of the sequences are not the same.</exception>
		public static IEnumerable<double> Plus(params IEnumerable<double>[] seqs)
		{
			var c = seqs.First().Count();
			if(seqs.Any(l => l.Count() != c))
				throw new Exception("The length of the sequences are not the same.");
			var enums = seqs.Map(s => s.GetEnumerator());
			var enumerators = enums as IEnumerator<double>[] ?? enums.ToArray();
			while(enumerators.Fold((b, e) => e.MoveNext() && b, true))
				yield return enumerators.Sum(e => e.Current);
		}

		public static IEnumerable<int> Plus(params IEnumerable<int>[] seqs)
		{
			var c = seqs.First().Count();
			if(seqs.Any(l => l.Count() != c))
				throw new Exception("The length of the sequences are not the same.");
			var enums = seqs.Map(s => s.GetEnumerator());
			var enumerators = enums as IEnumerator<int>[] ?? enums.ToArray();
			while(enumerators.Fold((b, e) => e.MoveNext() && b, true))
				yield return enumerators.Sum(e => e.Current);
		}

	}


}
