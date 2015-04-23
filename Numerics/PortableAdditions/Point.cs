using System;
using System.Globalization;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Represents an x- and y-coordinate pair in two-dimensional space. Can also represent a logical point for certain property usages.
	/// </summary>
	public struct Point : IFormattable
	{
		internal double _x;
		internal double _y;

		public void Offset(double dx, double dy)
		{
			this._x += dx;
			this._y += dy;
		}

		/// <summary>
		/// Gets or sets the <see cref="P:System.Windows.Point.X"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure.
		/// </summary>
		/// 
		/// <returns>
		/// The <see cref="P:System.Windows.Point.X"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure. The default value is 0.
		/// </returns>
		public double X {
			get {
				return this._x;
			}
			set {
				this._x = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="P:System.Windows.Point.Y"/>-coordinate value of this <see cref="T:System.Windows.Point"/>.
		/// </summary>
		/// 
		/// <returns>
		/// The <see cref="P:System.Windows.Point.Y"/>-coordinate value of this <see cref="T:System.Windows.Point"/> structure.  The default value is 0.
		/// </returns>
		public double Y {
			get {
				return this._y;
			}
			set {
				this._y = value;
			}
		}

		/// <summary>
		/// Initializes a <see cref="T:System.Windows.Point"/> structure that contains the specified values.
		/// </summary>
		/// <param name="x">The x-coordinate value of the <see cref="T:System.Windows.Point"/> structure. </param><param name="y">The y-coordinate value of the <see cref="T:System.Windows.Point"/> structure. </param>
		public Point(double x, double y)
		{
			this._x = x;
			this._y = y;
		}

		/// <summary>
		/// Compares two <see cref="T:System.Windows.Point"/> structures for equality.
		/// </summary>
		/// 
		/// <returns>
		/// true if both the <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values of <paramref name="point1"/> and <paramref name="point2"/> are equal; otherwise, false.
		/// </returns>
		/// <param name="point1">The first <see cref="T:System.Windows.Point"/> structure to compare.</param><param name="point2">The second <see cref="T:System.Windows.Point"/> structure to compare.</param>
		public static bool operator ==(Point point1, Point point2)
		{
			if(point1.X == point2.X)
				return point1.Y == point2.Y;
			else
				return false;
		}

		/// <summary>
		/// Compares two <see cref="T:System.Windows.Point"/> structures for inequality
		/// </summary>
		/// 
		/// <returns>
		/// true if <paramref name="point1"/> and <paramref name="point2"/> have different <see cref="P:System.Windows.Point.X"/> or <see cref="P:System.Windows.Point.Y"/> values; false if <paramref name="point1"/> and <paramref name="point2"/> have the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values.
		/// </returns>
		/// <param name="point1">The first point to compare.</param><param name="point2">The second point to compare.</param>
		public static bool operator !=(Point point1, Point point2)
		{
			return !(point1 == point2);
		}

		/// <summary>
		/// Creates a <see cref="T:System.String"/> representation of this <see cref="T:System.Windows.Point"/>.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.String"/> containing the <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values of this <see cref="T:System.Windows.Point"/> structure.
		/// </returns>
		public override string ToString()
		{
			return this.ConvertToString((string)null, (IFormatProvider)null);
		}

		/// <summary>
		/// Creates a <see cref="T:System.String"/> representation of this <see cref="T:System.Windows.Point"/>.
		/// </summary>
		/// 
		/// <returns>
		/// A <see cref="T:System.String"/> containing the <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values of this <see cref="T:System.Windows.Point"/> structure.
		/// </returns>
		/// <param name="provider">Culture-specific formatting information.</param>
		public string ToString(IFormatProvider provider)
		{
			return this.ConvertToString((string)null, provider);
		}

		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}", (object)numericListSeparator, (object)this._x, (object)this._y);
		}

		/// <summary>
		/// Determines whether the specified object is a <see cref="T:System.Windows.Point"/> and whether it contains the same values as this <see cref="T:System.Windows.Point"/>.
		/// </summary>
		/// 
		/// <returns>
		/// true if <paramref name="obj"/> is a <see cref="T:System.Windows.Point"/> and contains the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values as this <see cref="T:System.Windows.Point"/>; otherwise, false.
		/// </returns>
		/// <param name="o">The object to compare.</param>
		public override bool Equals(object o)
		{
			if(o == null || !(o is Point))
				return false;
			else
				return this == (Point)o;
		}

		/// <summary>
		/// Compares two <see cref="T:System.Windows.Point"/> structures for equality.
		/// </summary>
		/// 
		/// <returns>
		/// true if both <see cref="T:System.Windows.Point"/> structures contain the same <see cref="P:System.Windows.Point.X"/> and <see cref="P:System.Windows.Point.Y"/> values; otherwise, false.
		/// </returns>
		/// <param name="value">The point to compare to this instance.</param>
		public bool Equals(Point value)
		{
			return this == value;
		}

		/// <summary>
		/// Returns the hash code for this <see cref="T:System.Windows.Point"/>.
		/// </summary>
		/// 
		/// <returns>
		/// The hash code for this <see cref="T:System.Windows.Point"/> structure.
		/// </returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}
	}
}