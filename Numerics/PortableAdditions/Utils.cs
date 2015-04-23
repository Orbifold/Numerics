#region Copyright

// Copyright (c) 2007-2011, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Contains common helper methods.
	/// </summary>
	public static class Utils
	{

		/// <summary>
		/// Linear interpolation between the given values.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Linear_interpolation .</remarks>
		/// <param name="x">A value.</param>
		/// <param name="y">Another value.</param>
		/// <param name="fraction">A value in the [0,1] interval. At zero the interpolation returns the first value, at one it results in the second value.</param>
		/// <returns></returns>
		public static double Lerp(double x, double y, double fraction)
		{
			return (x * (1.0 - fraction)) + (y * fraction);
		}

		/// <summary>
		/// Gets the bezier coefficients, see http://processingjs.nihongoresources.com/bezierinfo/ .
		/// </summary>
		/// <param name="a0">The a0.</param>
		/// <param name="a1">The a1.</param>
		/// <param name="a2">The a2.</param>
		/// <param name="a3">The a3.</param>
		/// <param name="b0">The b0.</param>
		/// <param name="b1">The b1.</param>
		/// <param name="b2">The b2.</param>
		/// <param name="b3">The b3.</param>
		/// <param name="u">The u.</param>
		/// <param name="s">The s.</param>
		/// <param name="z">The z.</param>
		/// <param name="x4">The x4.</param>
		/// <param name="y4">The y4.</param>
		public static void GetBezierCoefficients(ref double a0, ref double a1, ref double a2, ref double a3, ref double b0, ref double b1, ref double b2, ref double b3, ref double u, ref double s, ref double z, ref double x4, ref double y4)
		{
			var x = a0 + (u * (a1 + (u * (a2 + (u * a3)))));
			var dx4 = x - x4;
			var dy = b1 + (u * ((2 * b2) + (3 * u * b3)));
			var y = b0 + (u * (b1 + (u * (b2 + (u * b3)))));
			var dy4 = y - y4;
			var dx = a1 + (u * ((2 * a2) + (3 * u * a3)));
			s = (dx4 * dx4) + (dy4 * dy4);
			z = (dx * dx4) + (dy * dy4);
		}

		/// <summary>
		/// Similar to Math.Sign but zero is a range instead of point
		/// </summary>
		/// <param name="val"></param>
		/// <param name="zeroLow"></param>
		/// <param name="zeroHigh"></param>
		/// <returns></returns>
		public static int StairValue(double val, double zeroLow, double zeroHigh)
		{
			if(val < zeroLow)
				return -1;
			if(val < zeroHigh)
				return 0;
			return 1;
		}

		/// <summary>
		/// 2D version of Sign()
		/// </summary>
		/// <param name="p"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2D StairValue(Point p, Rect r)
		{
			return new Vector2D(StairValue(p.X, r.Left, r.Right), StairValue(p.Y, r.Top, r.Bottom));
		}

		/// <summary>
		/// Approximation of a Bezier segment by a polyline.
		/// </summary>
		/// <param name="bezierPoints">The points defining the Bezier curve.</param>
		/// <param name="index">The index at which the four Bezier start.</param>
		/// <param name="quality">The quality of the approximation.</param>
		/// <returns></returns>
		/// <seealso cref="http://en.wikipedia.org/wiki/B%C3%A9zier_curve">Bezier curves on Wikipedia.</seealso>
		public static Point[] ApproximateBezierCurve(Point[] bezierPoints, int index, int quality)
		{
			var polyline = new Point[quality];
			var epsilon = 1.0 / quality;

			var x0 = bezierPoints[0 + index].X;
			var y0 = bezierPoints[0 + index].Y;
			var x1 = bezierPoints[1 + index].X;
			var y1 = bezierPoints[1 + index].Y;
			var x2 = bezierPoints[2 + index].X;
			var y2 = bezierPoints[2 + index].Y;
			var x3 = bezierPoints[3 + index].X;
			var y3 = bezierPoints[3 + index].Y;

			for(var i = 0; i < quality; ++i) {
				var t = i * epsilon;
				//these are just the classic polynomials related to Bezier
				var q0 = (1 - t) * (1 - t) * (1 - t);
				var q1 = 3 * t * (1 - t) * (1 - t);
				var q2 = 3 * t * t * (1 - t);
				var q3 = t * t * t;
				var xt = q0 * x0 + q1 * x1 + q2 * x2 + q3 * x3;
				var yt = q0 * y0 + q1 * y1 + q2 * y2 + q3 * y3;

				polyline[i] = new Point(xt, yt);
			}
			return polyline;
		}

		/// <summary>
		/// Returns whether the line (line segments) intersect and returns in the <see cref="crossingPoint"/> the actual crossing 
		/// point if they do.
		/// </summary>
		/// <param name="a">The first point of the first line.</param>
		/// <param name="b">The second point of the first line.</param>
		/// <param name="c">The first point of the second line.</param>
		/// <param name="d">The second point of the second line.</param>
		/// <param name="crossingPoint">The crossing point.</param>
		/// <returns></returns>
		public static bool AreLinesIntersecting(Point a, Point b, Point c, Point d, ref Point crossingPoint)
		{
			var tangensdiff = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);
			if(tangensdiff == 0)
				return false;
			var num1 = (a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y);
			var num2 = (a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y);
			var r = num1 / tangensdiff;
			var s = num2 / tangensdiff;
			if(r < 0 || r > 1 || s < 0 || s > 1)
				return false; //parallel cases
			crossingPoint = new Point(a.X + r * (b.X - a.X), a.Y + r * (b.Y - a.Y));

			return true;
		}

		/// <summary>
		/// Returns the barycentric coordinates as percentages with respect to the given rectangle.
		/// </summary>
		/// <param name="realPoint">The real point.</param>
		/// <param name="rectangle">The rectangle which acts as a barycentric coordinate system.</param>
		/// <returns>The percentages wrapped in a Point.</returns>
		/// <see cref="PointFromBarycentricPercentage(Point,System.Windows.Size)">The complementary method.</see>
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point BarycentricPercentageFromPoint(Point realPoint, Rect rectangle)
		{
			var percentage = new Point(50, 50);

			var w = rectangle.Right - rectangle.Left;
			var h = rectangle.Bottom - rectangle.Top;
			if(w != 0 && h != 0) {
				percentage.X = (realPoint.X - rectangle.Left) * 100 / w;
				percentage.Y = (realPoint.Y - rectangle.Top) * 100 / h;
			}

			return percentage;
		}

		/// <summary>
		/// Returns the squared distance between the given points.
		/// </summary>
		public static double DistanceSquared(Point a, Point b)
		{
			return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
		}

		/// <summary>
		/// Finds the intersection point of the lines defined by the
		/// specified point pairs.
		/// </summary>
		/// <returns>
		/// The intersection point of the specified lines or
		/// Point(float.MinValue, float.MinValue) if the lines
		/// do not intersect.
		/// </returns>
		public static Point FindLinesIntersection(Point a, Point b, Point c, Point d, bool acceptNaN = false)
		{
			var pt = acceptNaN ? new Point(Double.NaN, Double.NaN) : new Point(Double.MinValue, Double.MinValue);

			if(a.X == b.X && c.X == d.X)
				return pt;

			// Check if the first line is vertical
			if(a.X == b.X) {
				pt.X = a.X;
				pt.Y = (c.Y - d.Y) / (c.X - d.X) * pt.X + (c.X * d.Y - d.X * c.Y) / (c.X - d.X);

				return pt;
			}

			// Check if the second line is vertical
			if(c.X == d.X) {
				pt.X = c.X;
				pt.Y = (a.Y - b.Y) / (a.X - b.X) * pt.X + (a.X * b.Y - b.X * a.Y) / (a.X - b.X);

				return pt;
			}

			var a1 = (a.Y - b.Y) / (a.X - b.X);
			var b1 = (a.X * b.Y - b.X * a.Y) / (a.X - b.X);

			var a2 = (c.Y - d.Y) / (c.X - d.X);
			var b2 = (c.X * d.Y - d.X * c.Y) / (c.X - d.X);

			if((a1 != a2) || acceptNaN) {
				pt.X = (b2 - b1) / (a1 - a2);
				pt.Y = a1 * (b2 - b1) / (a1 - a2) + b1;
			}

			return pt;
		}

		/// <summary>
		/// Inflates the given rectangle with the specified amount.
		/// </summary>
		public static Rect Inflate(Rect rect, double deltaX, double deltaY)
		{
			//border case when the delta is negative and larger than the actual rectangle size
			if(rect.Width + 2 * deltaX < 0)
				deltaX = -rect.Width / 2;
			if(rect.Height + 2 * deltaY < 0)
				deltaY = -rect.Height / 2;
			return new Rect(rect.X - deltaX, rect.Y - deltaY, rect.Width + 2 * deltaX, rect.Height + 2 * deltaY);
		}

		/// <summary>
		/// Returs the middle point between the given points.
		/// </summary>
		/// <param name="p1">A point</param>
		/// <param name="p2">Another point</param>
		/// <returns></returns>
		public static Point Mid(Point p1, Point p2)
		{
			return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
		}

		/// <summary>
		/// Returns the mirrored vector with respect to the X-coordinate.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <returns></returns>
		//public static Vector MirrorHorizontally(this Vector v)
		//{
		//    return new Vector(v.Y, -v.X);
		//}

		public static Vector2D MirrorVertically(this Vector2D v)
		{
			return new Vector2D(-v.Y, v.X);
		}

		/// <summary>
		/// Given a percentage and a rectangle this will return the coordinates corresponding
		/// to the percentages given.
		/// </summary>
		/// <param name="percentage">A couple of values in percentage, e.g. a value of (50,50) will return the center of the rectangle.</param>
		/// <param name="size">The size from which the point will be extracted.</param>
		/// <returns></returns> 
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point PointFromBarycentricPercentage(Point percentage, Size size)
		{
			return new Point(percentage.X / 100D * size.Width, percentage.Y / 100D * size.Height);
		}

		/// <summary>
		/// Given a percentage and a rectangle this will return the coordinates corresponding
		/// to the percentages given.
		/// </summary>
		/// <param name="percentage">A couple of values in percentage, e.g. a value of (50,50) will return the center of the rectangle.</param>
		/// <param name="rectangle">The rectangle which acts as the barycentric system.</param>
		/// <returns></returns>
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point PointFromBarycentricPercentage(Point percentage, Rect rectangle)
		{
			return new Point(rectangle.Left + percentage.X / 100D * rectangle.Width, rectangle.Top + percentage.Y / 100D * rectangle.Height);
		}

		public static void CopyTo<T>(this T[] src, IList<T> dest)
		{
			for(int i = 0, n = src.Length; i < n; ++i)
				dest.Add(src[i]);
		}

		/// <summary>
		/// Converts the specified value from degrees to radians.
		/// </summary>
		public static double ToDegrees(this double radians)
		{
			return radians / Math.PI * 180;
		}

		/// <summary>
		/// Converts the specified value from degrees to radians.
		/// </summary>
		//public static double ToRadians(this double degrees)
		//{
		//    return degrees / 180 * Math.PI;
		//}

		public static Rect ToRect(this Size s)
		{
			return new Rect(0, 0, s.Width, s.Height);
		}

       
		/// <summary>
		/// Checks if the specified rectangle and circle intersect.
		/// </summary>
		public static bool CheckIntersect(Point pt, Rect rc, double rad)
		{
			//Translating coordinates, placing percentage at origin
			var leftX = rc.Left - pt.X;
			var rightX = rc.Right - pt.X;
			var topY = rc.Top - pt.Y;
			var bottomY = rc.Bottom - pt.Y;

			if(rightX < 0) {
				if(topY > 0) { //rect is SW from origin
					return (rightX * rightX + topY * topY < rad * rad);
				} else if(bottomY < 0) { //rect is NW from origin
					return (rightX * rightX + bottomY * bottomY < rad * rad);
				} else { //rect is W from circle
					return (Math.Abs(rightX) < rad);
				}
			} else if(leftX > 0) {
				if(topY > 0) { // rect is SE from origin
					return (leftX * leftX + topY * topY < rad * rad);
				} else if(bottomY < 0) { //rect is NE from origin
					return (leftX * leftX + bottomY * bottomY < rad * rad);
				} else { //rect is E from circle
					return (Math.Abs(leftX) < rad);
				}
			} else {
				if(topY > 0) { //rect is S from circle
					return (Math.Abs(topY) < rad);
				} else if(bottomY < 0) { // rect is N from circle
					return (Math.Abs(bottomY) < rad);
				} else { // rect contains origin
					return true;
				}
			}
		}

		public static List<Point> ApproximateBezierCurve(Point[] bezierPoints, int quality)
		{
			var approximation = new List<Point>();
			var epsilon = 1.0 / quality;

			// get the points defining the curve
			var x0 = bezierPoints[0].X;
			var y0 = bezierPoints[0].Y;
			var x1 = bezierPoints[1].X;
			var y1 = bezierPoints[1].Y;
			var x2 = bezierPoints[2].X;
			var y2 = bezierPoints[2].Y;
			var x3 = bezierPoints[3].X;
			var y3 = bezierPoints[3].Y;

			for(double t = 0; t <= 1.0; t += epsilon) {
				var q0 = (1 - t) * (1 - t) * (1 - t);
				var q1 = 3 * t * (1 - t) * (1 - t);
				var q2 = 3 * t * t * (1 - t);
				var q3 = t * t * t;
				var xt = q0 * x0 + q1 * x1 + q2 * x2 + q3 * x3;
				var yt = q0 * y0 + q1 * y1 + q2 * y2 + q3 * y3;

				// Draw straight line between last two calculated points
				approximation.Add(new Point(xt, yt));
			}

			return approximation;
		}

		public static void ArcConvert(Rect r, double startAngle, double sweep, out Point startPoint, out Point outsidePoint, out bool largeArc, out SweepDirection dir)
		{
			startPoint = ArcPoint(r, startAngle);
			outsidePoint = ArcPoint(r, startAngle + sweep);
			largeArc = Math.Abs(sweep) > 180;
			dir = sweep > 0 ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
		}

		/// <summary>
		/// Returns the point at an angle on the ellipse with axes specified by the given rectangle.
		/// </summary>
		public static Point ArcPoint(this Rect r, double angle)
		{
			var rads = angle.ToRadians();
			var cos = Math.Cos(rads);
			var rx = r.Width / 2;
			var ry = r.Height / 2;
			double x, y;

			if(cos == 0) {
				x = 0;
				y = ry * Math.Sin(rads); // for sign only
			} else {
				var sin = Math.Sin(rads);
				var r1 = 1 / Math.Sqrt(Sqr(cos / rx) + Sqr(sin / ry));
				x = r1 * cos;
				y = r1 * sin;
			}

			return new Point(r.Left + rx + x, r.Top + ry + y);
		}

		public static bool BetweenOrEqual(double n, double boundary1, double boundary2)
		{
			Sort(ref boundary1, ref boundary2);
			return BetweenOrEqualSorted(n, boundary1, boundary2);
		}

		internal static bool BetweenOrEqualSorted(double n, double boundary1, double boundary2)
		{
			return boundary1 <= n && n <= boundary2;
		}


		/// <summary>
		/// Calculates the bezier coefficients in the equation
		/// of the specified bezier curve.
		/// </summary>
		public static void CalcBezierCoef(ref double a0, ref double a1,
		                                        ref double a2, ref double a3, ref double b0, ref double b1,
		                                        ref double b2, ref double b3, ref double u, ref double s,
		                                        ref double z, ref double x4, ref double y4)
		{
			var x = a0 + u * (a1 + u * (a2 + u * a3));
			var y = b0 + u * (b1 + u * (b2 + u * b3));
			var dx4 = x - x4;
			var dy4 = y - y4;
			var dx = a1 + u * (2 * a2 + 3 * u * a3);
			var dy = b1 + u * (2 * b2 + 3 * u * b3);

			z = dx * dx4 + dy * dy4;
			s = dx4 * dx4 + dy4 * dy4;
		}

		/// <summary>
		/// Calculates the point of the the specified line segment which determines the distance from the specified
		/// point to the line segment
		/// </summary>
		public static Point DistancePoint(Point p, Point a, Point b)
		{
			if(a == b)
				return a;

			var dx = b.X - a.X;
			var dy = b.Y - a.Y;

			var dotProduct = (p.X - a.X) * dx + (p.Y - a.Y) * dy;
			if(dotProduct < 0)
				return a;

			dotProduct = (b.X - p.X) * dx + (b.Y - p.Y) * dy;
			if(dotProduct < 0)
				return b;

			var lf = VectorExtensions.MirrorHorizontally(new Vector2D(a.X - b.X, a.Y - b.Y));
			var n = new Point(p.X + lf.X, p.Y + lf.Y);
			return FindLinesIntersection(a, b, p, n, true);
		}

		/// <summary>
		/// Converts dekart coordinates to the corresponding
		/// polar coordinates, using the specified point as
		/// a center of the coordinate system.
		/// </summary>
		public static void CarteseanToPolar(Point coordCenter, Point dekart, ref double a, ref double r)
		{
			if(coordCenter == dekart) {
				a = 0;
				r = 0;
				return;
			}

			var dx = dekart.X - coordCenter.X;
			var dy = dekart.Y - coordCenter.Y;
			r = Distance(dx, dy);

			a = Math.Atan(-dy / dx) * 180 / Math.PI;
			if(dx < 0)
				a += 180;
		}

		/// <summary>
		/// Determines, given three points, if when travelling from the first to
		/// the second to the third, we travel in a counterclockwise direction.
		/// </summary>
		/// <remarks>
		/// 1 if the movement is in a counterclockwise direction, -1 if not.
		/// </remarks>
		public static int IsCounterClockWise(Point p0, Point p1, Point p2)
		{
			var dx1 = p1.X - p0.X;
			var dx2 = p2.X - p0.X;
			var dy1 = p1.Y - p0.Y;
			var dy2 = p2.Y - p0.Y;

			// This is basically a slope comparison: we don't do divisions
			// because of divide by zero possibilities with pure horizontal
			// and pure vertical lines
			return ((dx1 * dy2 > dy1 * dx2) ? 1 : -1);
		}

		/// <summary>
		/// Returns the center of the specified rectangle.
		/// </summary>
		//public static Point Center(this Rect r)
		//{
		//    return Mid(r.TopLeft(), r.BottomRight());
		//}


		internal static List<object> Clone(this IEnumerable<object> list)
		{
			var clone = new List<object>();
			clone.AddRange(list);
			return clone;
		}

		public static List<T> Clone<T>(this IEnumerable<T> list)
		{
			var clone = new List<T>();
			clone.AddRange(list);
			return clone;
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
		/// <param name="choice1">The choice1.</param>
		/// <param name="choice2">The choice2.</param>
		/// <param name="nearestValue">The nearest value.</param>
		/// <param name="otherValue">The other value.</param>
		public static void Closer(double value, double choice1, double choice2, out double nearestValue, out double otherValue)
		{
			Sort(ref choice1, ref choice2);
			bool choice1Closer;

			if(value < choice1)
				choice1Closer = true;
			else if(value > choice2)
				choice1Closer = false;
			else
				choice1Closer = value - choice1 < choice2 - value;

			if(choice1Closer) {
				nearestValue = choice1;
				otherValue = choice2;
			} else {
				nearestValue = choice2;
				otherValue = choice1;
			}
		}

		/// <summary>
		/// Returns the point from the specified pair, which lies
		/// closer to the specified pivot.
		/// </summary>
		public static Point Closer(Point pivot, Point p1, Point p2)
		{
			return AreDistanceOrdered(pivot, p1, p2) ? p1 : p2;
		}

		/// <summary>
		/// An infinitesimal.
		/// </summary>
		public const double Epsilon = 0.01;

		public static double Constrain(double val, double min, double max)
		{
			return Math.Max(min, Math.Min(val, max));
		}

		public static bool Contains(this Rect rc1, Rect rc2)
		{
			return
                rc1.Contains(rc2.BottomLeft()) &&
			rc1.Contains(rc2.BottomRight()) &&
			rc1.Contains(rc2.TopLeft()) &&
			rc1.Contains(rc2.TopRight());
		}

		public static List<Point> ConvertPolylineToBezier(List<Point> points)
		{
			var newPoints = new List<Point>();
			Point ptf;
			Point ptf2;
			newPoints.Add(points[0]);
			var i = 0;
			while(i < points.Count - 2) {
				ptf2 = points[i + 1];

				newPoints.Add(ptf2);
				newPoints.Add(ptf2);
				if(i != points.Count - 3) {
					var ptf3 = points[i + 2];
					ptf = new Point((ptf2.X + ptf3.X) / 2, (ptf2.Y + ptf3.Y) / 2);
					newPoints.Add(ptf);
				} else {
					newPoints.Add(points[i + 2]);
				}
				i += 1;
			}

			if(newPoints.Count == 1) {
				newPoints = new List<Point>();

				var ptf1 = points[0];
				ptf2 = points[points.Count - 1];
				ptf = new Point((ptf1.X + ptf2.X) / 2, (ptf1.Y + ptf2.Y) / 2);
				newPoints.Add(ptf1);
				newPoints.Add(ptf);
				newPoints.Add(ptf);
				newPoints.Add(ptf2);
			}

			return newPoints;
		}

		/// <summary>
		/// Calculates the shortest distance from the specified point
		/// to the specified bezier curve.
		/// </summary>
		public static double DistToBezier(Point pt, IList bezierPoints, int nPoints)
		{
			var nSplineNum = nPoints / 3;
			double nRes = 1000000;

			for(var nSpline = 0; nSpline < nSplineNum; ++nSpline) {
				var x0 = ((Point)bezierPoints[nSpline * 3 + 0]).X;
				var y0 = ((Point)bezierPoints[nSpline * 3 + 0]).Y;
				var x1 = ((Point)bezierPoints[nSpline * 3 + 1]).X;
				var y1 = ((Point)bezierPoints[nSpline * 3 + 1]).Y;
				var x2 = ((Point)bezierPoints[nSpline * 3 + 2]).X;
				var y2 = ((Point)bezierPoints[nSpline * 3 + 2]).Y;
				var x3 = ((Point)bezierPoints[nSpline * 3 + 3]).X;
				var y3 = ((Point)bezierPoints[nSpline * 3 + 3]).Y;

				var a3 = (x3 - x0 + 3 * (x1 - x2)) / 8;
				var b3 = (y3 - y0 + 3 * (y1 - y2)) / 8;
				var a2 = (x3 + x0 - x1 - x2) * 3 / 8;
				var b2 = (y3 + y0 - y1 - y2) * 3 / 8;
				var a1 = (x3 - x0) / 2 - a3;
				var b1 = (y3 - y0) / 2 - b3;
				var a0 = (x3 + x0) / 2 - a2;
				var b0 = (y3 + y0) / 2 - b2;

				var x4 = pt.X;
				var y4 = pt.Y;

				double s = 0, u1, z = 0;
				double s1 = 0;
				var u2 = u1 = -1.0;
				double z1 = 0;

				const double stepsize = 2.0 / 9;
				double u;
				for(u = -1.0; u < 1.0; u += stepsize) {
					CalcBezierCoef(ref a0, ref a1, ref a2, ref a3,
						ref b0, ref b1, ref b2, ref b3,
						ref u, ref s, ref z, ref x4, ref y4);

					if(Math.Abs(s) < 0.00001) {
						u1 = u;
						z1 = z;
						s1 = s;
						break;
					}

					if(Math.Abs(u + 1.0) < 0.00001) {
						u1 = u;
						z1 = z;
						s1 = s;
					}

					if(s < s1) {
						u1 = u;
						z1 = z;
						s1 = s;
					}
				}

				if(Math.Abs(s1) > 0.00001) {
					u = u1 + stepsize;
					if(u > 1.0)
						u = 1.0 - stepsize;
					for(var cnt = 0; cnt < 20; cnt++) {
						CalcBezierCoef(ref a0, ref a1, ref a2, ref a3,
							ref b0, ref b1, ref b2, ref b3,
							ref u, ref s, ref z, ref x4, ref y4);

						if(Math.Abs(s) < 0.00001)
							break;
						if(Math.Abs(z) < 0.00001)
							break;

						u2 = u;
						var z2 = z;

						var temp = z2 - z1;

						if(Math.Abs(temp) > 0.00001)
							u = (z2 * u1 - z1 * u2) / temp;
						else
							u = (u1 + u2) / 2;

						if(u > 1.0)
							u = 1.0;
						else if(u < -1.0)
							u = -1.0;

						if(Math.Abs(u - u2) < 0.0001)
							break;

						u1 = u2;
						z1 = z2;
					}
				}

				if(nRes > Math.Sqrt(s))
					nRes = Math.Sqrt(s);

				if(nRes == 0)
					return 0;
			}

			return nRes;
		}

		/// <summary>
		/// Calculates the shortest distance from the specified
		/// point to the specified polyline.
		/// </summary>
		public static double DistToPolyline(Point pt, IList linePoints, int nPoints)
		{
			var segmNum = 0;
			return DistToPolyline(pt, linePoints, nPoints, ref segmNum);
		}

		/// <summary>
		/// Calculates the shortest distance from the specified
		/// point to the specified polyline, also returning the
		/// index of the segment the point is closest to.
		/// </summary>
		public static double DistToPolyline(Point pt, IList linePoints, int nPoints, ref int segmNum)
		{
			var nMinDist = Double.MaxValue;
			segmNum = 0;

			for(var s = 0; s < nPoints - 1; ++s) {
				var p1 = (Point)linePoints[s];
				var p2 = (Point)linePoints[s + 1];

				var nDist = DistanceToLine(pt, p1, p2);
				if(nDist < nMinDist) {
					nMinDist = nDist;
					segmNum = s;
				}
			}

			return nMinDist;
		}

		/// <summary>
		/// Returns the distance of the point to the origin.
		/// </summary>
		/// <param name="p">The crossingPoint.</param>
		/// <returns></returns>
		public static double Distance(this Point p)
		{
			return p.Distance(new Point(0, 0));
		}

		/// <summary>
		/// Calculates the length of the hypotenuse of a right rectangle
		/// whose catheti have the specified length.
		/// </summary>
		public static double Distance(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
		}


		/// <summary>
		/// Returns the squared distance between the specified points.
		/// </summary>
		public static double DistanceToLineSquared(Point p, Point a, Point b)
		{
			if(a == b)
				return DistanceSquared(p, a);

			var dx = b.X - a.X;
			var dy = b.Y - a.Y;
			var area = (p.Y - a.Y) * dx - (p.X - a.X) * dy;//the same as the sine projection onto the normal
			return area * area / (dx * dx + dy * dy);
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

			var dotProduct = (p.X - a.X) * dx + (p.Y - a.Y) * dy;
			if(dotProduct < 0)
				return DistanceSquared(a, p);

			dotProduct = (b.X - p.X) * dx + (b.Y - p.Y) * dy;
			if(dotProduct < 0)
				return DistanceSquared(b, p);

			return DistanceToLineSquared(p, a, b);
		}

		/// <summary>
		/// Returns the distance from the given point to the line (segment).
		/// </summary>
		public static double DistanceToLine(Point p, Point a, Point b)
		{
			return DistanceExstensions.Distance(p, ProjectPointOnLine(p, a, b));
		}

		internal static void DumpBegin(object o, int indent)
		{
			Debug.WriteLine(Spaces(indent) + "<" + o.GetType().FullName + ">");
		}



		internal static void DumpEnd(object o, int indent)
		{
			Debug.WriteLine(Spaces(indent) + "</" + o.GetType().FullName + ">");
		}


		public static Point Forward(Point start, Vector2D unitVector, double distance)
		{
			return new Point(start.X + unitVector.X * distance, start.Y + unitVector.Y * distance);
		}

		/// <summary>
		/// Creates a new rectangle specified coordinates.
		/// </summary>
		public static Rect FromLTRB(double l, double t, double r, double b)
		{
			return new Rect(new Point(l, t), new Point(r, b));
		}



		/// <summary>
		/// Returns the center point of the specified rectangle.
		/// </summary>
		public static Point GetCenter(Rect rect)
		{
			return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
		}


		/// <summary>
		/// Calculate the intersection point between the ellipse
		/// with the specified bounds and the line segment defined
		/// by the specified points.
		/// </summary>
		public static void GetEllipseIntr(Rect rcBox,
		                                        Point pt1, Point pt2, ref Point pt)
		{
			var rc = new Rect(pt1, pt2);

			var x1 = pt1.X;
			var y1 = pt1.Y;
			var x2 = pt2.X;
			var y2 = pt2.Y;

			double A;
			double B;
			double X1;
			double Y2;
			double X2;
			double Y1;
			if(Math.Abs(x1 - x2) > 0.0001) {
				var cx = (rcBox.Left + rcBox.Right) / 2;
				var cy = (rcBox.Top + rcBox.Bottom) / 2;
				var ea = (rcBox.Right - rcBox.Left) / 2;
				var eb = (rcBox.Bottom - rcBox.Top) / 2;
				var a = (y1 - y2) / (x1 - x2);
				var b = (x1 * y2 - x2 * y1) / (x1 - x2);

				A = eb * eb + a * a * ea * ea;
				B = 2 * a * (b - cy) * ea * ea - 2 * cx * eb * eb;
				var c = eb * eb * cx * cx + ea * ea * (b - cy) * (b - cy) - ea * ea * eb * eb;

				var d = Math.Sqrt(B * B - 4 * A * c);
				X1 = (-B + d) / (2 * A);
				X2 = (-B - d) / (2 * A);
				Y1 = a * X1 + b;
				Y2 = a * X2 + b;

				pt.X = X1;
				pt.Y = Y1;
				if(pt.X >= rc.Left && pt.X <= rc.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;

				pt.X = X2;
				pt.Y = Y2;
				if(pt.X >= rc.Left && pt.X <= rc.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;
			} else {
				var cx = (rcBox.Left + rcBox.Right) / 2;
				var cy = (rcBox.Top + rcBox.Bottom) / 2;
				var ea = (rcBox.Right - rcBox.Left) / 2;
				var eb = (rcBox.Bottom - rcBox.Top) / 2;

				var x = x1;
				Y1 = cy - Math.Sqrt((1 - (x - cx) * (x - cx) / (ea * ea)) * eb * eb);
				Y2 = cy + Math.Sqrt((1 - (x - cx) * (x - cx) / (ea * ea)) * eb * eb);

				pt.X = x;
				pt.Y = Y1;
				if(pt.X >= rc.Left && pt.X <= rc.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;

				pt.X = x;
				pt.Y = Y2;
				if(pt.X >= rc.Left && pt.X <= rc.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;
			}
		}

		/// <summary>
		/// Gets the minimum allowed size of an arrowhead.
		/// </summary>
		public static double GetMinArrowheadSize() //GraphicsUnit currUnit)
		{
			//switch (currUnit)
			//{

			//    case GraphicsUnit.Millimeter:
			//        return 1;

			//    case GraphicsUnit.Inch:
			//        return 1.0f / 12;

			//    case GraphicsUnit.Point:
			//        return 72.0f / 12;

			//    case GraphicsUnit.Pixel:
			//        return 4;

			//    case GraphicsUnit.Document:
			//        return 300.0f / 12;

			//    case GraphicsUnit.Display:
			//        return 100.0f / 12;

			//}

			return 96 / 12;
		}

		/// <summary>
		/// Calculates the intersection point between the specified path
		/// and the segment defined by the specified point pair.
		/// </summary>
		public static bool GetPathIntersection(List<Point> points, Point org, Point end, ref Point result)
		{

			var currentIntersection = new Point();
			double nearestDistanceSq = Double.PositiveInfinity, currentDistanceSq;

			for(var i = 0; i < points.Count; i++) { //ppoints.GetLength(0); ++i)
				if(SegmentIntersect(points[i],
					                points[(i + 1) % points.Count], org, end, ref currentIntersection)) {
					currentDistanceSq = DistanceSquared(currentIntersection, end);
					if(currentDistanceSq < nearestDistanceSq) {
						nearestDistanceSq = currentDistanceSq;
						result = currentIntersection;
					}
				}
			}

			return !Double.IsPositiveInfinity(nearestDistanceSq);
		}

		/// <summary>
		/// Calculates the projections of the specified point on the
		/// sides of the specified rectangle.
		/// </summary>
		public static void GetProjections(Point pt, Rect rc, Point[] proj)
		{
			proj[0] = proj[1] = proj[2] = proj[3] = pt;
			proj[0].Y = rc.Top;
			proj[1].Y = rc.Bottom;
			proj[2].X = rc.Right;
			proj[3].X = rc.Left;
		}


		/// <summary>
		/// Calculates the intersection point between the specified
		/// rectangle and the line segment defined by the specified
		/// points.
		/// </summary>
		public static void GetRectIntr(Rect rcBox,
		                                     Point pt1, Point pt2, ref Point pt)
		{
			var rc = FromLTRB(pt1.X, pt1.Y, pt2.X, pt2.Y);

			var x1 = pt1.X;
			var y1 = pt1.Y;
			var x2 = pt2.X;
			var y2 = pt2.Y;

			if(x1 == x2) {
				pt.X = x1;

				// try with the top line
				pt.Y = rcBox.Top;
				if(pt.X >= rcBox.Left && pt.X <= rcBox.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;

				// try with the bottom line
				pt.Y = rcBox.Bottom;
				if(pt.X >= rcBox.Left && pt.X <= rcBox.Right &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;
			} else if(y1 == y2) {
				pt.Y = y1;

				// Try with the left line segment
				pt.X = rcBox.Left;
				if(pt.Y >= rcBox.Top && pt.Y <= rcBox.Bottom &&
				                pt.X >= rc.Left && pt.X <= rc.Right)
					return;

				// Try with the right line segment
				pt.X = rcBox.Right;
				if(pt.Y >= rcBox.Top && pt.Y <= rcBox.Bottom &&
				                pt.X >= rc.Left && pt.X <= rc.Right)
					return;
			} else {
				var a = (y1 - y2) / (x1 - x2);
				var b = (x1 * y2 - x2 * y1) / (x1 - x2);

				// Try with the top line
				pt.Y = rcBox.Top;
				pt.X = (pt.Y - b) / a;
				if(pt.X >= rcBox.Left && pt.X <= rcBox.Right &&
				                pt.Y <= rcBox.Bottom &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;

				// Try with the bottom line
				pt.Y = rcBox.Bottom;
				pt.X = (pt.Y - b) / a;
				if(pt.X >= rcBox.Left && pt.X <= rcBox.Right &&
				                pt.Y >= rcBox.Top &&
				                pt.Y >= rc.Top && pt.Y <= rc.Bottom)
					return;

				// Try with the left line
				pt.X = rcBox.Left;
				pt.Y = a * pt.X + b;
				if(pt.Y >= rcBox.Top && pt.Y <= rcBox.Bottom &&
				                pt.X <= rcBox.Right &&
				                pt.X >= rc.Left && pt.X <= rc.Right)
					return;

				// Try with the right line
				pt.X = rcBox.Right;
				pt.Y = a * pt.X + b;
				if(pt.Y >= rcBox.Top && pt.Y <= rcBox.Bottom &&
				                pt.X >= rcBox.Left &&
				                pt.X >= rc.Left && pt.X <= rc.Right)
					return;
			}
		}

		/// <summary>
		/// Calculates the percents values corresponding to the
		/// specified point relative to the specified rectangle.
		/// </summary>
		internal static Point GetRectPtPercent(Point pt, Rect rc)
		{
			var ptPer = new Point(50, 50);

			var w = rc.Right - rc.Left;
			var h = rc.Bottom - rc.Top;

			if(w != 0 && h != 0) {
				ptPer.X = (pt.X - rc.Left) * 100 / w;
				ptPer.Y = (pt.Y - rc.Top) * 100 / h;
			}

			return ptPer;
		}

		/// <summary>
		/// Rotates the specified point at the specified right angle
		/// (0, 90, 180, 270) around the center of a 100x100 rectangle.
		/// </summary>
		internal static Point GetRotatedPt(Point pt, double rotation)
		{
			var res = pt;

			if(rotation == 0)
				return res;

			if(rotation == 90) {
				res.X = 100 - pt.Y;
				res.Y = pt.X;
			} else if(rotation == 180) {
				res.X = 100 - pt.X;
				res.Y = 100 - pt.Y;
			} else {
				res.X = pt.Y;
				res.Y = 100 - pt.X;
			}

			return res;
		}

		internal static double GetScreenDpi()
		{
			double dpi = 96; //double.NaN;

			//try
			//{
			//    // Application.Current may be null (in WinForms application)
			//    if (Application.Current != null)
			//    {
			//        Window w = Application.Current.RootVisual.;
			//        dpi = GetScreenDpi(w, dpi);
			//    }

			//}
			//catch (SecurityException)
			//{
			//    // ignore security exception
			//};
			return dpi;
		}

		/// <summary>
		/// Determines whether the specified line segments intersect.
		/// </summary>
		/// <returns>
		/// true if the segments intersect, false if not.
		/// </returns>
		public static bool Intersect(Point p1, Point p2, Point p3, Point p4)
		{
			return (IsCounterClockWise(p1, p2, p3) * IsCounterClockWise(p1, p2, p4) <= 0) &&
			(IsCounterClockWise(p3, p4, p1) * IsCounterClockWise(p3, p4, p2) <= 0);
		}


		public static bool IntersectsLineSegment(this Rect rect, Point a, Point b, ref Point p)
		{
			return AreLinesIntersecting(a, b, rect.TopLeft(), rect.TopRight(), ref p)
			|| AreLinesIntersecting(a, b, rect.TopRight(), rect.BottomRight(), ref p)
			|| AreLinesIntersecting(a, b, rect.BottomRight(), rect.BottomLeft(), ref p)
			|| AreLinesIntersecting(a, b, rect.BottomLeft(), rect.TopLeft(), ref p);
		}



		public static bool IntersectsWith(this Rect rect1, Rect rect2)
		{
			rect1.Intersect(rect2);
			return !rect1.IsEmpty;
		}


		public static Matrix Invert(this Matrix m)
		{
			var det = m.M11 * m.M22 - m.M12 * m.M21;
			var det1 = 1 / det;
			var rev = new Matrix(
				                   m.M22 * det1,
				                   -m.M12 * det1,
				                   -m.M21 * det1,
				                   m.M11 * det1,
				                   (m.M21 * m.OffsetY - m.OffsetX * m.M22) * det1,
				                   (m.OffsetX * m.M12 - m.M11 * m.OffsetY) * det1);
			return rev;
		}

		/// <summary>
		/// Returns whether the given point series are ordered correctly with respect to the their relative distance.
		/// </summary>
		/// <param name="p">The first point.</param>
		/// <param name="p1">The second point.</param>
		/// <param name="p2">The third point.</param>
		/// <returns></returns>
		private static bool AreDistanceOrdered(Point p, Point p1, Point p2)
		{
			return DistanceSquared(p, p1) < DistanceSquared(p, p2);
		}

		/// <summary>
		/// Moves the given point into the rectangle by taking the rectangle's intervals as limiting values for
		/// the point's coordinates.
		/// </summary>
		/// <param name="p">Any point.</param>
		/// <param name="rectangle">A rectangle which acts as limiting container.</param>
		/// <returns></returns>
		public static Point Limit(this Point p, Rect rectangle)
		{
			return new Point(Math.Min(Math.Max(p.X, rectangle.Left), rectangle.Right), Math.Min(Math.Max(p.Y, rectangle.Top), rectangle.Bottom));
		}


		/// <summary>
		/// Returns an array of node anchor points which match current node links anchor point
		/// </summary>
		public static int[] MatchPointsMinLength(Point[] nodeAnchorPoints, int[] pointRealIndexes, Point[] fixedLinksAnchorPoints, List<int> usedAnchorPointIndexes)
		{
			if(nodeAnchorPoints == null)
				return new int[0];
			if(fixedLinksAnchorPoints == null)
				return new int[0];
			if(nodeAnchorPoints.Length == 0 || fixedLinksAnchorPoints.Length == 0 || nodeAnchorPoints.Length != pointRealIndexes.Length)
				return new int[0];

			// Collection of returned indexes of anchor points for some DiagramNode
			var resultIndexes = new List<int>(fixedLinksAnchorPoints.Length);
			// Collection of indexes of destination point which match min length with origin point.
			if(usedAnchorPointIndexes == null)
				usedAnchorPointIndexes = new List<int>(nodeAnchorPoints.Length);

			var usedPointsStartLength = usedAnchorPointIndexes.Count;
			double minLength;

			for(var i = 0; i < fixedLinksAnchorPoints.Length; i++) {
				if(i > 0 && (i % nodeAnchorPoints.Length) == 0)
					usedAnchorPointIndexes.Clear();

				// Clears usedAnchorPointIndexes if it's start length is > 0 and all point indexes are used
				if(usedPointsStartLength > 0) {
					var found = pointRealIndexes.Any(inx => !usedAnchorPointIndexes.Contains(inx));
					if(!found)
						usedAnchorPointIndexes.Clear();
				}

				minLength = Double.MaxValue;
				for(var j = 0; j < nodeAnchorPoints.Length; j++) {
					if(!usedAnchorPointIndexes.Contains(pointRealIndexes[j]))
					if(DistanceExstensions.Distance(nodeAnchorPoints[j], fixedLinksAnchorPoints[i]) < minLength) {
						minLength = DistanceExstensions.Distance(nodeAnchorPoints[j], fixedLinksAnchorPoints[i]);
						if(usedPointsStartLength > 0)
						if((i % nodeAnchorPoints.Length) < usedAnchorPointIndexes.Count && (i % nodeAnchorPoints.Length) == usedAnchorPointIndexes.Count - usedPointsStartLength - 1)
							usedAnchorPointIndexes[(i % nodeAnchorPoints.Length) + usedPointsStartLength] = pointRealIndexes[j];
						else
							usedAnchorPointIndexes.Add(pointRealIndexes[j]);
						else if((i % nodeAnchorPoints.Length) < usedAnchorPointIndexes.Count)
							usedAnchorPointIndexes[i % nodeAnchorPoints.Length] = pointRealIndexes[j];
						else
							usedAnchorPointIndexes.Add(pointRealIndexes[j]);

						if(i == resultIndexes.Count - 1)
							resultIndexes[i] = pointRealIndexes[j];
						else
							resultIndexes.Add(pointRealIndexes[j]);
					}
				}
			}

			return resultIndexes.ToArray();
		}

		public static Matrix Multiply(this Matrix m1, Matrix m2)
		{
			return new Matrix(
				m1.M11 * m2.M11 + m1.M12 * m2.M21,
				m1.M11 * m2.M12 + m1.M12 * m2.M22,
				m1.M21 * m2.M11 + m1.M22 * m2.M12,
				m1.M21 * m2.M12 + m1.M22 * m2.M22,
				m1.OffsetX * m2.M11 + m1.OffsetY * m2.M21 + m2.OffsetX,
				m1.OffsetX * m2.M12 + m1.OffsetY * m2.M22 + m2.OffsetY
			);
		}

		/// <summary>
		/// Returns the element from the specified point array nearest to the specified point.
		/// </summary>
		/// <param name="point">A Point instance.</param>
		/// <param name="points">An array for whose members to find the minimum distance to the specified point.</param>
		/// <returns>The array member nearest to the specified point.</returns>
		public static Point NearestPoint(this Point point, Point[] points)
		{
			var nearestCenter = point;
			var minDistance = Double.MaxValue;
			foreach(var centerPoint in points) {
				var distance = centerPoint.Distance(point);
				if(distance < minDistance) {
					minDistance = distance;
					nearestCenter = centerPoint;
				}
			}
			return nearestCenter;
		}


		public static Rect NewRect(Point center, double size)
		{
			return new Rect(center.X - size / 2, center.Y - size / 2, size, size);
		}

		/// <summary>
		/// Gets the point from the specified bezier curve,
		/// corresponding to the specified parameter t [0, 1].
		/// </summary>
		public static Point GetBezierPt(List<Point> points, int segment, double t)
		{
			var x0 = (points[segment * 3 + 0]).X;
			var y0 = (points[segment * 3 + 0]).Y;
			var x1 = (points[segment * 3 + 1]).X;
			var y1 = (points[segment * 3 + 1]).Y;
			var x2 = (points[segment * 3 + 2]).X;
			var y2 = (points[segment * 3 + 2]).Y;
			var x3 = (points[segment * 3 + 3]).X;
			var y3 = (points[segment * 3 + 3]).Y;

			var tt = t;

			var q0 = (1 - tt) * (1 - tt) * (1 - tt);
			var q1 = 3 * tt * (1 - tt) * (1 - tt);
			var q2 = 3 * tt * tt * (1 - tt);
			var q3 = tt * tt * tt;
			var xt = q0 * x0 + q1 * x1 + q2 * x2 + q3 * x3;
			var yt = q0 * y0 + q1 * y1 + q2 * y2 + q3 * y3;

			return new Point(xt, yt);
		}

		public static Rect NewRect(Point center, Size size)
		{
			return new Rect(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height);
		}

		public static Rect Offset(Rect rect, double x, double y)
		{
			rect.X += x;
			rect.Y += y;
			return rect;
		}

		public static Rect Offset(Rect rect, Vector2D offset)
		{
			return Offset(rect, offset.X, offset.Y);
		}

		public static Point[] Offset(Point[] points, Vector2D offset)
		{
			var n = points.Length;
			for(var i = 0; i < n; ++i)
				points[i] += offset;
			return points;
		}

		public static List<Point> Offset(List<Point> points, Vector2D offset)
		{
			var n = points.Count;
			for(var i = 0; i < n; ++i)
				points[i] += offset;
			return points;
		}

		public static void OffsetPointCollection(List<Point> points,
		                                               List<Point> originalPoints, Vector2D offset)
		{
			if(points.Count != originalPoints.Count)
				return;

			for(var i = 0; i < points.Count; ++i)
				points[i] = originalPoints[i] + offset;
		}

		public static Vector2D Perpendicular(Vector2D v)
		{
			return new Vector2D(v.Y, -v.X);
		}



		/// <summary>
		/// Checks whether the specified point is contained in
		/// the ellipse defined by the specified rectangle.
		/// </summary>
		public static bool PointInEllipse(Point pt, Rect rc)
		{
			var rct = rc;

			// Determine radii
			var a = (rct.Right - rct.Left) / 2;
			var b = (rct.Bottom - rct.Top) / 2;

			// Determine x, y
			var x = pt.X - (rct.Left + rct.Right) / 2;
			var y = pt.Y - (rct.Top + rct.Bottom) / 2;

			// Apply ellipse formula
			return ((x * x) / (a * a) + (y * y) / (b * b) <= 1);
		}

		public static bool PointInRect(Point pt, Size s)
		{
			return BetweenOrEqualSorted(pt.X, 0, s.Width)
			&& BetweenOrEqualSorted(pt.Y, 0, s.Height);
		}

		/// <summary>
		/// Checks whether the specified rectangle contains the specified point.
		/// </summary>
		public static bool PointInRect(Point pt, Rect rc)
		{
			return rc.Contains(pt);
		}



		/// <summary>
		/// Converts polar coordinates to the corresponding
		/// dekart coordinates, using the specified point as
		/// a center of the coordinate system.
		/// </summary>
		public static Point PolarToCartesean(Point coordCenter, double a, double r)
		{
			var radians = a.ToRadians();
			return new Point(coordCenter.X + Math.Cos(radians) * r, coordCenter.Y - Math.Sin(radians) * r);
		}

		/// <summary>
		/// Finds the projection (point) of the given point on the line.
		/// </summary>
		public static Point ProjectPointOnLine(Point p, Point a, Point b)
		{
			var perpendicular = Perpendicular(new Vector2D(a.X - b.X, a.Y - b.Y));
			var normalOffset = new Point(p.X + perpendicular.X, p.Y + perpendicular.Y);
			var intersection = FindLinesIntersection(a, b, p, normalOffset, true);
			return Closer(p, a, Double.IsNaN(intersection.X) ? b : Closer(p, intersection, b));
		}

		/// <summary>
		/// Determines if rectangle intersects with rect.
		/// </summary>
		/// <param name="rectangle"></param>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static bool RectIntersects(Rect rectangle, Rect rect)
		{
			rectangle.Intersect(rect);

			return !rectangle.IsEmpty;
		}

		/// <summary>
		/// Calculates the point corresponding to the specified
		/// percent values relative to the specified rectangle.
		/// </summary>
		internal static Point RectanglePointFromPercent(Point ptPer, Rect rc)
		{
			return new Point(rc.Left + ptPer.X / 100 * rc.Width,
				rc.Top + ptPer.Y / 100 * rc.Height);
		}

		internal static double ReplaceZero(double suspect, double defaultValue)
		{
			return suspect == 0 ? defaultValue : suspect;
		}


		/// <summary>
		/// Returns the nearest to percentage point that lies on the outline of rectangle.
		/// </summary>
		public static Point DistToRectPoint(Point pt, Rect r)
		{
			return new Point(
				DistToRectSelect(pt.X, r.Left, r.Right),
				DistToRectSelect(pt.Y, r.Top, r.Bottom));
		}


		public static double DistToRectSelect(double pointX, double rectX1, double rectX2)
		{
			double closer, farther;
			Closer(pointX, rectX1, rectX2, out closer, out farther);
			return BetweenOrEqual(pointX, closer, farther) ? pointX : closer;
		}

		/// <summary>
		/// Calculates the minimum distance between a given point and
		/// a given rectangle.
		/// </summary>
		/// <param name="pt"></param>
		/// <param name="rc"></param>
		/// <returns></returns>
		public static double MinDistToRect(Point pt, Rect rc)
		{
			var nearest = DistToRectPoint(pt, rc);
			return pt.Distance(nearest);
		}

		public static bool SafeEquals(object o1, object o2)
		{
			return o1 == null ? o2 == null : o1.Equals(o2);
		}

		internal static Size Scale(this Size size, double scale)
		{
			return new Size(size.Width * scale, size.Height * scale);
		}

		/// <summary>
		/// Determine whether p1 and p2 are on the same side of a line
		/// </summary>
		public static bool SameSide(Point linePoint1, Point linePoint2, Point p1, Point p2)
		{
			return IsCounterClockWise(linePoint1, linePoint2, p1) == IsCounterClockWise(linePoint1, linePoint2, p2);
		}

		/// <summary>
		/// Checks whether the segments defined by the specified
		/// point pairs intersect and returns the intersection point.
		/// </summary>
		public static bool SegmentIntersect(Point s1, Point s2, Point l1, Point l2, ref Point pt)
		{
			pt = FindLinesIntersection(s1, s2, l1, l2);

			// Do not compare to 0 but to a small double number,
			// because what's 0 in debug builds seems to result
			// in 2.XXXXXE-5 in release builds

			var p1 = (pt.X - s1.X) * (pt.X - s2.X);
			var p2 = (pt.Y - s1.Y) * (pt.Y - s2.Y);
			if(p1 > 0.0001 || p2 > 0.0001)
				return false;

			var pl1 = (pt.X - l1.X) * (pt.X - l2.X);
			var pl2 = (pt.Y - l1.Y) * (pt.Y - l2.Y);
			return pl1 <= 0.0001 && pl2 <= 0.0001;
		}

		internal static void SetLocation(ref Rect bounds, Point point)
		{
			bounds.X = point.X;
			bounds.Y = point.Y;
		}

		public static void Sort(ref double a, ref double b)
		{
			if(b >= a)
				return;
			var tmp = a;
			a = b;
			b = tmp;
		}

		internal static string Spaces(int i)
		{
			return new string(' ', i);
		}

		private static double Sqr(double d)
		{
			return d * d;
		}



		public static Point Substract(this Point p1, Point p2)
		{
			return new Point(p1.X - p2.X, p1.Y - p2.Y);
		}

		public static Vector2D Subtract(Point p1, Point p2)
		{
			return new Vector2D(p1.X - p2.X, p1.Y - p2.Y);
		}

		/// <summary>
		/// Returns the opposite of point p with respect to specified line
		/// </summary>
		/// <param name="p"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Point MirrorPoint(Point p, Point a, Point b)
		{
			var centralPoint = FindLinesIntersection(a, b, p, p + Subtract(a, b).MirrorHorizontally(), true);
			return centralPoint + Subtract(centralPoint, p);
		}


		/// <summary>
		/// Calculates the symmetric point of the specified point with
		/// respect to the specified center.
		/// </summary>
		public static Point SymmetricPt(Point pt, Point ptCenter)
		{
			var p = new Point(ptCenter.X - pt.X, ptCenter.Y - pt.Y);
			var pp = new Point(p.X + ptCenter.X, p.Y + ptCenter.Y);
			return pp;
		}



		public static Size ToSize(this Rect r)
		{
			return new Size(r.Width, r.Height);
		}



		public static Rect Transform(this Matrix m, Rect r)
		{
			return new Rect(m.Transform(r.TopLeft()), m.Transform(r.BottomRight()));
		}

		public static Rect TransformPercentToSize(Rect rect, Size size)
		{
			return new Rect(
				PointFromBarycentricPercentage(RectExtensions.TopLeft(rect), size),
				PointFromBarycentricPercentage(RectExtensions.BottomRight(rect), size));
		}

		/// <summary>
		/// Returns the smallest possible rectangle containing
		/// both of the specified rectangles, but only if the
		/// rectangles are non-empty.
		/// </summary>
		/// <param name="rc1">
		/// The first rectangle.
		/// </param>
		/// <param name="rc2">
		/// The second rectangle.
		/// </param>
		/// <returns>
		/// A .NET Rect instance that represents the union of the specified arguments.
		/// </returns>
		public static Rect UnionNonEmptyRects(Rect rc1, Rect rc2)
		{
			if(rc1.Width == 0 || rc1.Height == 0)
				return rc2;

			return UnionRects(rc1, rc2);
		}

		/// <summary>
		/// Returns the smallest possible rectangle containing
		/// both of the specified rectangles.
		/// </summary>
		/// <param name="a">
		/// The first rectangle.
		/// </param>
		/// <param name="b">
		/// The second rectangle.
		/// </param>
		/// <returns>
		/// A .NET Rect instance that represents the union of the specified arguments.
		/// </returns>
		public static Rect UnionRects(Rect a, Rect b)
		{
			var x = a;
			x.Union(b);
			return x;
		}



		public static Vector2D UnitVector(double degrees)
		{
			var rad = degrees.ToRadians();
			return new Vector2D(Math.Cos(rad), Math.Sin(rad));
		}

		public static bool ArraysEqual<T>(T[] a1, T[] a2)
		{
			if(ReferenceEquals(a1, a2))
				return true;

			if(a1 == null || a2 == null)
				return false;

			if(a1.Length != a2.Length)
				return false;

			var comparer = EqualityComparer<T>.Default;
			return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
		}

		public static bool ArraysEqual<T>(T[,] a1, T[,] a2)
		{
			if(ReferenceEquals(a1, a2))
				return true;

			if(a1 == null || a2 == null)
				return false;

			if(a1.Length != a2.Length)
				return false;
			for(var i = 0; i < 2; i++)
				if(a1.GetLength(i) != a2.GetLength(i))
					return false;
			var comparer = EqualityComparer<T>.Default;
			for(var m = 0; m < a1.GetLength(0); m++) {
				for(var n = 0; n < a1.GetLength(1); n++) {
					if(!comparer.Equals(a1[m, n], a2[m, n]))
						return false;
				}
			}
			return true;
		}
	}

	/// <summary>
	/// Defines the direction an elliptical arc is drawn.
	/// </summary>
	public enum SweepDirection
	{
		Counterclockwise,
		Clockwise,
	}
}