using System;
using System.Linq.Expressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Basic implementation of an angle and conversation between radials and degrees.
	/// </summary>
	public class Angle
	{
		public static Angle Zero { get { return new Angle(0d); } }

		public static Angle Pi { get { return new Angle(Math.PI); } }

		public static Angle TwoPi { get { return new Angle(2 * Math.PI); } }

		public static Angle PiHalf { get { return new Angle(Math.PI / 2d); } }

		public static Angle ThreePiHalf { get { return new Angle(3 * Math.PI / 2d); } }

		public static Angle Bisect { get { return new Angle(Math.PI / 4d); } }

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Angle"/> class.
		/// </summary>
		public Angle()
		{
			this.Radians = 0.0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Angle"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public Angle(double value)
		{
			this.Radians = value;
		}

		/// <summary>
		/// Returns the angle to the given point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public static Angle ToPoint(Point point)
		{
			return FromPoints(new Point(0.0, 0.0), point);
		}

		/// <summary>
		/// Returns the Angle from the first point to the second point.
		/// </summary>
		/// <param name="point1">A point.</param>
		/// <param name="point2">A point.</param>
		/// <returns></returns>
		public static Angle FromPoints(Point from, Point to)
		{
			var deltax = to.X - from.X;
			var deltay = to.Y - from.Y;
			if(deltay.IsZero())
				return deltax > 0 ? Zero : Pi;

			if(deltax.IsZero())
				return deltay > 0 ? PiHalf : ThreePiHalf;
			if(deltax > 0.0)
				return deltay < 0.0 ? new Angle(Math.Atan2(deltay, deltax) + 2 * Math.PI) : new Angle(Math.Atan2(deltay, deltax));
			return new Angle(Math.Atan(deltay / deltax) + Math.PI);
		}

		/// <summary>
		/// Gets or sets the angle in degrees.
		/// </summary>
		public double Degrees {
			get {
				return ((this.Radians / Math.PI) * 180.0);
			}
			set {
				this.Radians = (value / 180.0) * Math.PI;
			}
		}

		/// <summary>
		/// Gets or sets the angle in radians.
		/// </summary>
		public double Radians { get; set; }
	}
}