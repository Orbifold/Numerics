using System.Collections;
using System.Windows;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Methods related to distance calculations between points, lines and other geometry entities.
	/// </summary>
	public static class DistanceExstensions
	{
		/// <summary>
		/// Returns the point on a rectangle with shortest distance to a given point.
		/// </summary>
		/// <param name="pt">The pt.</param>
		/// <param name="rectangle">The rectangle.</param>
		/// <returns></returns>
		public static Point DistanceToRectanglePoint(Point pt, Rect rectangle)
		{
			return new Point(DistToRectSelect(pt.X, rectangle.Left, rectangle.Right), DistToRectSelect(pt.Y, rectangle.Top, rectangle.Bottom));
		}

		/// <summary>
		/// Returns the shortest distance of a point to a rectangle defined by an upper-right and lower left point.
		/// </summary>
		/// <param name="pointX">The point X.</param>
		/// <param name="rectX1">The rect x1.</param>
		/// <param name="rectX2">The rect x2.</param>
		/// <returns></returns>
		public static double DistToRectSelect(double pointX, double rectX1, double rectX2)
		{
			double closer, farther;
			Closer(pointX, rectX1, rectX2, out closer, out farther);
			return Utils.BetweenOrEqual(pointX, closer, farther) ? pointX : closer;
		}

		/// <summary>
		/// Returns the shortest distance of the given point to the given rectangle.
		/// </summary>
		/// <param name="pt">The pt.</param>
		/// <param name="rc">The rc.</param>
		/// <returns></returns>
		public static double DistanceToRectangle(Point pt, Rect rc)
		{
			var nearest = DistanceToRectanglePoint(pt, rc);
			return pt.Distance(nearest);
		}

		/// <summary>
		/// Returns the shortest distance to the Bezier curve.
		/// </summary>
		public static double DistanceToBezierCurve(Point point, IList bezierPoints)
		{
			var upperBound = 1000000D;
			var segmentCount = bezierPoints.Count / 3;
			for(var spline = 0; spline < segmentCount; ++spline) {
				var x0 = ((Point)bezierPoints[(spline * 3) + 0]).X;
				var y0 = ((Point)bezierPoints[(spline * 3) + 0]).Y;
				var x1 = ((Point)bezierPoints[(spline * 3) + 1]).X;
				var y1 = ((Point)bezierPoints[(spline * 3) + 1]).Y;
				var x2 = ((Point)bezierPoints[(spline * 3) + 2]).X;
				var y2 = ((Point)bezierPoints[(spline * 3) + 2]).Y;
				var x3 = ((Point)bezierPoints[(spline * 3) + 3]).X;
				var y3 = ((Point)bezierPoints[(spline * 3) + 3]).Y;

				var a3 = (x3 - x0 + (3 * (x1 - x2))) / 8;
				var b3 = (y3 - y0 + (3 * (y1 - y2))) / 8;
				var a2 = (x3 + x0 - x1 - x2) * 3 / 8;
				var b2 = (y3 + y0 - y1 - y2) * 3 / 8;
				var a1 = ((x3 - x0) / 2) - a3;
				var b1 = ((y3 - y0) / 2) - b3;
				var a0 = ((x3 + x0) / 2) - a2;
				var b0 = ((y3 + y0) / 2) - b2;

				var x4 = point.X;
				var y4 = point.Y;
				double s = 0;
				double z = 0;
				double s1 = 0;
				var u1 = -1.0;
				double z1 = 0;

				const double Stepsize = 2.0 / 9;
				double u;
				// -1 corresponds to the start point and +1 the endpoint of the Bezier curve.
				// this is a reparametrization of the [0,1] parameter.
				for(u = -1.0; u < 1.0; u += Stepsize) {
					Utils.GetBezierCoefficients(ref a0, ref a1, ref a2, ref a3, ref b0, ref b1, ref b2, ref b3, ref u, ref s, ref z, ref x4, ref y4);
					if(System.Math.Abs(s) < Constants.Epsilon) {
						u1 = u;
						z1 = z;
						s1 = s;
						break;
					}
					if(System.Math.Abs(u + 1.0) < Constants.Epsilon) {
						u1 = u;
						z1 = z;
						s1 = s;
					}
					if(s >= s1)
						continue;
					u1 = u;
					z1 = z;
					s1 = s;
				}

				if(System.Math.Abs(s1) > Constants.Epsilon) {
					u = u1 + Stepsize;
					if(u > 1.0)
						u = 1.0 - Stepsize;
					for(var cnt = 0; cnt < 20; cnt++) {
						Utils.GetBezierCoefficients(ref a0, ref a1, ref a2, ref a3, ref b0, ref b1, ref b2, ref b3, ref u, ref s, ref z, ref x4, ref y4);
						if(System.Math.Abs(s) < Constants.Epsilon)
							break;
						if(System.Math.Abs(z) < Constants.Epsilon)
							break;

						var u2 = u;
						var z2 = z;
						var d = z2 - z1;
						if(System.Math.Abs(d) > Constants.Epsilon)
							u = ((z2 * u1) - (z1 * u2)) / d;
						else
							u = (u1 + u2) / 2;

						if(u > 1.0)
							u = 1.0;
						else if(u < -1.0)
							u = -1.0;

						if(System.Math.Abs(u - u2) < Constants.Epsilon)
							break;
						u1 = u2;
						z1 = z2;
					}
				}

				if(upperBound > System.Math.Sqrt(s))
					upperBound = System.Math.Sqrt(s);
				if(upperBound.IsEqualTo(0))
					return 0;
			}

			return upperBound;
		}

		/// <summary>
		/// Returns the shortest distance to the polyline.
		/// </summary>
		public static double DistanceToPolyline(Point point, IList polyline)
		{
			var closestSegmentToPoint = 0;
			return DistanceToPolyline(point, polyline, ref closestSegmentToPoint);
		}

		/// <summary>
		/// Returns the shortest distance to the polyline.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="polyline">The polyline.</param>
		/// <param name="closestSegmentToPoint">The closest segment to point.</param>
		/// <returns></returns>
		public static double DistanceToPolyline(Point point, IList polyline, ref int closestSegmentToPoint)
		{
			var minimumDistance = double.MaxValue;
			closestSegmentToPoint = 0;

			for(var s = 0; s < polyline.Count - 1; ++s) {
				var p1 = (Point)polyline[s];
				var p2 = (Point)polyline[s + 1];

				var distance = DistanceToLine(point, p1, p2);
				if(distance < minimumDistance) {
					minimumDistance = distance;
					closestSegmentToPoint = s;
				}
			}

			return minimumDistance;
		}

		/// <summary>
		/// Returns the distance of the given point to a line segment.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="polyline">The polyline.</param>
		/// <param name="delta">The delta.</param>
		/// <returns></returns>
		public static double DistanceToLineSegment(Point point, IList polyline, double delta)
		{
			var minimumDistance = double.NaN;

			for(var s = 0; s < polyline.Count - 1; ++s) {
				var p1 = (Point)polyline[s];
				var p2 = (Point)polyline[s + 1];
				var distance = DistanceToLine(point, p1, p2);
				var isPointBetween = p1.X.IsEqualTo(p2.X) ? point.IsYBetween(p1, p2) : point.IsXBetween(p1, p2);

				if((distance < delta && isPointBetween) || p1.ContainsInNeighborhood(point, delta)) {
					minimumDistance = distance;
					break;
				}
			}

			return minimumDistance;
		}

		/// <summary>
		/// Returns the distance from the given point to the line (segment).
		/// </summary>
		public static double DistanceToLine(Point p, Point a, Point b)
		{
			return Distance(p, PointExtensions.ProjectPointOnLine(p, a, b));
		}


		/// <summary>
		/// Distances to line squared.
		/// </summary>
		/// <param name="p">The p.</param>
		/// <param name="a">A.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public static double DistanceToLineSquared(Point p, Point a, Point b)
		{
			if(a == b)
				return DistanceSquared(p, a);
			var dx = b.X - a.X;
			var dy = b.Y - a.Y;
			// the same as the sine projection onto the normal
			var area = ((p.Y - a.Y) * dx) - ((p.X - a.X) * dy);
			return area * area / ((dx * dx) + (dy * dy));
		}

		/// <summary>
		/// Returns the distance of a point to a segment. If the projection
		/// of the point on the segment is outside the segment the distance is
		/// the distance to the closest point of the segment. This in effect defines
		/// some kind of elliptic neighborhood around the segment.
		/// </summary>
		public static double DistanceToSegmentSquared(Point p, Point a, Point b)
		{
			if(a == b)
				return DistanceSquared(p, a);

			var dx = b.X - a.X;
			var dy = b.Y - a.Y;

			var dotProduct = ((p.X - a.X) * dx) + ((p.Y - a.Y) * dy);
			if(dotProduct < 0)
				return DistanceSquared(a, p);

			dotProduct = ((b.X - p.X) * dx) + ((b.Y - p.Y) * dy);
			if(dotProduct < 0)
				return DistanceSquared(b, p);

			return DistanceToLineSquared(p, a, b);
		}

		/// <summary>
		/// Returns the distance of the point to the origin.
		/// </summary>
		/// <param name="p">The point.</param>
		/// <returns></returns>
		public static double Distance(this Point p)
		{
			return p.Distance(new Point(0, 0));
		}

		/// <summary>
		/// Returns the distance of the point to the origin.
		/// </summary>
		public static double Distance(double x, double y)
		{
			return System.Math.Sqrt((x * x) + (y * y));
		}

		/// <summary>
		/// Returns the distance between the specified points.
		/// </summary>
		public static double Distance(this Point a, Point b)
		{
			////non-Euclidean geometry anyone?
			return System.Math.Sqrt(((a.X - b.X) * (a.X - b.X)) + ((a.Y - b.Y) * (a.Y - b.Y)));
		}

		/// <summary>
		/// Returns the squared distance between the given points.
		/// </summary>
		public static double DistanceSquared(Point a, Point b)
		{
			return ((a.X - b.X) * (a.X - b.X)) + ((a.Y - b.Y) * (a.Y - b.Y));
		}

		/// <summary>
		/// Given an interval and a value this will output the value which is closer to the given value.
		/// </summary>
		public static double Closer(double value, double choice1, double choice2)
		{
			double closer, farther;
			Closer(value, choice1, choice2, out closer, out farther);
			return closer;
		}

		/// <summary>
		/// Given an interval and a value this will output the value which is closer to the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="choice1">The first choice.</param>
		/// <param name="choice2">The second choice.</param>
		/// <param name="nearestValue">The nearest value.</param>
		/// <param name="otherValue">The other value.</param>
		public static void Closer(double value, double choice1, double choice2, out double nearestValue, out double otherValue)
		{
			Utils.Sort(ref choice1, ref choice2);
			bool firstOptionIsClose;
			if(value < choice1)
				firstOptionIsClose = true;
			else if(value > choice2)
				firstOptionIsClose = false;
			else
				firstOptionIsClose = value - choice1 < choice2 - value;
			if(!firstOptionIsClose) {
				nearestValue = choice2;
				otherValue = choice1;
			} else {
				nearestValue = choice1;
				otherValue = choice2;
			}
		}

		/// <summary>
		/// Returns the point of the interval which sits the closest to the given point.
		/// </summary>
		/// <param name="point">The point seeking the closes neighbor.</param>
		/// <param name="p1">The first point in the interval.</param>
		/// <param name="p2">The second point in the interval.</param>
		/// <returns></returns>
		public static Point Closer(Point point, Point p1, Point p2)
		{
			return PointExtensions.AreDistanceOrdered(point, p1, p2) ? p1 : p2;
		}

	}
}