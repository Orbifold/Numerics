using System;
using System.Collections.Generic;
using System.Windows;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Implementation of the <see cref="Range"/> for a set of common data types.
	/// </summary>
	public static class Range
	{
		public static Range<double> Create(double min)
		{
			return  Range.Create(min, min + 0.00001d);
		}

		/// <summary>
		/// Returns a <see cref="Range"/> object on the basis of the <see cref="RVector"/> seen as an array of doubles.
		/// </summary>
		/// <param name="v">A vector.</param>
		public static Range<double> Create(Vector v)
		{
			if(v.Dimension == 1) {
				throw new Exception("A one-dimensional vector cannot be used to create a Range.");
			}
			return new Range<double>(v.ToArray());
		}

		/// <summary>
		/// Creates a range of bytes within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<byte> Create(byte start, byte end)
		{
			return start <= end ? new Range<byte>(start, end, GetNext) : new Range<byte>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of <c>Int16</c> integers within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<short> Create(short start, short end)
		{
			return start <= end ? new Range<short>(start, end, GetNext) : new Range<short>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of integers within the given interval.
		/// </summary>
		/// <param name="start">Inclusive start of the range.</param>
		/// <param name="end">Inclusive end of the range.</param>
		/// <returns></returns>
		public static Range<int> Create(int start, int end)
		{
			return start <= end ? new Range<int>(start, end, GetNext) : new Range<int>(start, end, GetNextDesc);
		}

		public static Range<int> Create(int start, int end, int step)
		{
			if(step == 0)
				throw new Exception("The range leads to an infinite series.");
			if(start < end && step < 0)
				throw new Exception("The range leads to an infinite series.");
			if(start > end && step > 0)
				throw new Exception("The range leads to an infinite series.");

			return new Range<int>(start, end, d => d + step);
		}

		/// <summary>
		/// Creates a range of <c>Int64</c> integers within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<long> Create(long start, long end)
		{
			return start <= end ? new Range<long>(start, end, GetNext) : new Range<long>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Returns an array representation of the range.
		/// </summary>
		/// <typeparam name="T">The data type of the range.</typeparam>
		/// <param name="range">The range.</param>
		public static T[] ToArray<T>(this Range<T> range)
		{
			if(range == null)
				throw new ArgumentNullException("range");
			var list = new List<T>();
			range.ForEach(list.Add);
			return list.ToArray();
		}

		/// <summary>
		/// Creates a range of doubles within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<double> Create(double start, double end)
		{
			return start <= end ? new Range<double>(start, end, GetNext) : new Range<double>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of doubles starting from a certain and with an amount of step.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="step">The step.</param>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">amount;The amount should be greater than one.</exception>
		public static Range<double> Create(double start, double step, int amount = 10)
		{
			if(amount < 1)
				throw new ArgumentException("amount", "The amount should be greater than one.");
			var end = start + (amount - 1) * step;
			return Create(start, end, d => d + step);
		}

		/// <summary>
		/// Creates a range of floats within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<float> Create(float start, float end)
		{
			return start <= end ? new Range<float>(start, end, GetNext) : new Range<float>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of decimals within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<decimal> Create(decimal start, decimal end)
		{
			return start <= end ? new Range<decimal>(start, end, GetNext) : new Range<decimal>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of chars within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<char> Create(char start, char end)
		{
			return start <= end ? new Range<char>(start, end, GetNext) : new Range<char>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of datetime values within the given interval.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static Range<DateTime> Create(DateTime start, DateTime end)
		{
			return start <= end ? new Range<DateTime>(start, end, GetNext) : new Range<DateTime>(start, end, GetNextDesc);
		}

		/// <summary>
		/// Creates a range of values within the given interval using a 'next' functional and a comparison to ensure the end-value can be compared..
		/// </summary>
		/// <typeparam name="T">The data type of the sequence.</typeparam>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="getNext">The get next.</param>
		/// <param name="compare">The compare.</param>
		/// <returns></returns>
		public static Range<T> Create<T>(T start, T end, Func<T, T> getNext, Comparison<T> compare)
		{
			return new Range<T>(start, end, getNext, compare);
		}

		/// <summary>
		/// Creates a range of values within the given interval using a 'next' functional.
		/// </summary>
		/// <typeparam name="T">The data type of the sequence.</typeparam>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="getNext">The get next.</param>
		/// <returns></returns>
		public static Range<T> Create<T>(T start, T end, Func<T, T> getNext)
		{
			return new Range<T>(start, end, getNext);
		}

		private static byte GetNext(byte val)
		{
			return (byte)(val + 1);
		}

		private static short GetNext(short val)
		{
			return (short)(val + 1);
		}

		private static int GetNext(int val)
		{
			return val + 1;
		}

		private static long GetNext(long val)
		{
			return val + 1;
		}

		private static double GetNext(double val)
		{
			return val + 1;
		}

		private static float GetNext(float val)
		{
			return val + 1;
		}

		private static decimal GetNext(decimal val)
		{
			return val + 1;
		}

		private static DateTime GetNext(DateTime val)
		{
			return val.AddDays(1);
		}

		private static char GetNext(char val)
		{
			return (char)(val + 1);
		}

		private static byte GetNextDesc(byte val)
		{
			return (byte)(val - 1);
		}

		private static short GetNextDesc(short val)
		{
			return (short)(val - 1);
		}

		private static int GetNextDesc(int val)
		{
			return val - 1;
		}

		private static long GetNextDesc(long val)
		{
			return val - 1;
		}

		private static double GetNextDesc(double val)
		{
			return val - 1;
		}

		private static float GetNextDesc(float val)
		{
			return val - 1;
		}

		private static decimal GetNextDesc(decimal val)
		{
			return val - 1;
		}

		private static DateTime GetNextDesc(DateTime val)
		{
			return val.AddDays(-1);
		}

		private static char GetNextDesc(char val)
		{
			return (char)(val - 1);
		}

		/// <summary>
		/// Ensures the range.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public static double EnsureRange(double value, double? min, double? max)
		{
			if(min.HasValue && (value < min.Value))
				return min.Value;
			if(max.HasValue && (value > max.Value))
				return max.Value;
			return value;
		}

		/// <summary>
		/// Creates the rectangle.
		/// </summary>
		/// <param name="startPoint">The start point.</param>
		/// <param name="endPoint">The end point.</param>
		/// <returns></returns>
		public static Rect CreateRectangle(Point startPoint, Point endPoint)
		{
			return new Rect(startPoint, new Size(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y));
		}
	}
}