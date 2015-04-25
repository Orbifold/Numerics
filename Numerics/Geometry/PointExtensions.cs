using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if !PCL
using System.Windows;
using System.Windows.Media;
#endif
namespace Orbifold.Numerics
{
    /// <summary>
    /// Extensions for the Point class.
    /// </summary>
    public static class PointExtensions
    {
#if SILVERLIGHT
		public static void Offset(this Point p,double x, double y)
		{
			p.X += x;
			p.Y += y;
		}
#endif

#if !PCL
        /// <summary>
		/// Rotates the transform.
		/// </summary>
		/// <param name="center">The center.</param>
		/// <param name="angle">The angle.</param>
		/// <returns></returns>
		public static RotateTransform RotateTransform(Point center, double angle)
		{
			return new RotateTransform { Angle = angle, CenterX = center.X, CenterY = center.Y };
		}

        /// <summary>
		/// Rotatates the specified point with respect to an achor point.
		/// </summary>
		/// <param name="point">The point to rotate.</param>
		/// <param name="anchorPoint">The anchor point of the rotation.</param>
		/// <param name="angle">The rotation angle.</param>
		/// <returns></returns>
		public static Point Rotate(this Point point, Point anchorPoint, double angle)
		{
			RectExtensions.RotateTransform.Angle = angle;
			RectExtensions.RotateTransform.CenterX = anchorPoint.X;
			RectExtensions.RotateTransform.CenterY = anchorPoint.Y;

			return RectExtensions.RotateTransform.Transform(point);
		}

        /// <summary>
		/// Rotates the point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="pivot">The pivot.</param>
		/// <param name="angle">The angle.</param>
		/// <returns></returns>
		public static Point RotatePoint(Point point, Point pivot, double angle)
		{
			return RotateTransform(pivot, angle).Transform(point);
		}

		/// <summary>
		/// Rotates the points.
		/// </summary>
		public static void RotatePointsAt(Point[] points, Point pivot, double angle)
		{
			var rotation = RotateTransform(pivot, angle);
			var n = points.Length;
			for (var i = 0; i < n; ++i)
				points[i] = rotation.Transform(points[i]);
		}
#else

        /// <summary>
        /// Rotatates the specified point with respect to an achor point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="anchorPoint">The anchor point of the rotation.</param>
        /// <param name="angle">The rotation angle.</param>
        /// <returns></returns>
        public static Point Rotate(this Point point, Point anchorPoint, double angle)
        {
            return RotatePoint(point, angle, anchorPoint);
        }
#endif
        /// <summary>
        /// Rotates the given point.
        /// </summary>
        /// <param name="p">The point to rotate.</param>
        /// <param name="angle">The angle is radians.</param>
        /// <param name="referencePoint">The optional reference point.</param>
        /// <returns></returns>
        public static Point RotatePoint(Point p, double angle, Point? referencePoint = null)
        {
            var o = new Point(0, 0);
            if (referencePoint.HasValue)
            {
                o = referencePoint.Value;
            }
            //p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
            //p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy
            return new Point(Math.Cos(angle) * (p.X - o.X) - Math.Sin(angle) * (p.Y - o.Y) + o.X,  Math.Sin(angle) * (p.X - o.X) + Math.Cos(angle) * (p.Y - o.Y) + o.Y);
        }
        /// <summary>
        /// Adds the specified point and vector together.
        /// </summary>
        /// <seealso cref="Vector">The Vector struct and its operations.</seealso>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>The augmented point.</returns>
        public static Point Add(Point point, Vector2D vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y);
        }

        /// <summary>
        /// Adds the specified points together.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="p2">The p2.</param>
        /// <returns>
        /// The augmented point.
        /// </returns>
        /// <seealso cref="Vector">The Vector struct and its operations.</seealso>
        public static Point Add(this Point point, Point p2)
        {
            return new Point(point.X + p2.X, point.Y + p2.Y);
        }

        /// <summary>
        /// Swaps the values of the two points.
        /// </summary>
        /// <param name="p">A point.</param>
        /// <param name="q">Another point.</param>
        public static void Swap(ref Point p, ref Point q)
        {
            var t = p;
            p = q;
            q = t;
        }

        /// <summary>
        /// Offsets the specified points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static IEnumerable<Point> Offset(this IEnumerable<Point> points, Vector2D offsetVector)
        {
            if (points == null) throw new ArgumentNullException("points");
            points.ForEach(p => p += offsetVector);
            return points;
        }

        /// <summary>
        /// Snaps a point by changing the X and Y coordinates to the closest value dividable by the snapping value.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="snapX">The horizontal snapping value.</param>
        /// /// <param name="snapY">The vertical snapping value.</param>
        /// <returns>Returns the snapped point.</returns>
        public static Point Snap(this Point point, int snapX, int snapY)
        {
            var moduleX = point.X % snapX;
            var moduleY = point.Y % snapY;
            var newX = point.X - moduleX + (moduleX > snapX / 2d ? snapX : 0);
            var newY = point.Y - moduleY + (moduleY > snapY / 2d ? snapY : 0);
            return new Point(newX, newY);
        }

        /// <summary>
        /// Returns the barycentric coordinates as percentages with respect to the given rectangle.
        /// </summary>
        /// <param name="realPoint">The real point.</param>
        /// <param name="rectangle">The rectangle which acts as a barycentric coordinate system.</param>
        /// <returns>The percentages wrapped in a Point.</returns>
        /// <remarks>The right side of the rectangle corresponds to a value of 100. If the position is left of the left side of rectanlge the value will be negative.</remarks>
        /// <see cref="PointFromBarycentricPercentage(System.Windows.Point,System.Windows.Size)">The complementary method.</see>        
        public static Point BarycentricPercentageFromPoint(Point realPoint, Rect rectangle)
        {
            var percentage = new Point(50, 50);
            var w = rectangle.Right - rectangle.Left;
            var h = rectangle.Bottom - rectangle.Top;
            if (w.IsNotEqualTo(0) && h.IsNotEqualTo(0))
            {
                percentage.X = (realPoint.X - rectangle.Left) * 100 / w;
                percentage.Y = (realPoint.Y - rectangle.Top) * 100 / h;
            }
            return percentage;
        }

        /// <summary>
        /// Given a percentage and a rectangle this will return the coordinates corresponding to the percentages given.
        /// </summary>
        /// <param name="percentage">A couple of values in percentage, e.g. a value of (50,50) will return the center of the rectangle.</param>
        /// <param name="size">The size from which the point will be extracted.</param>
        /// <returns>The point corresponding to the barycentric coordinates.</returns> 
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
        /// <returns>The point corresponding to the barycentric coordinates.</returns>         
        public static Point PointFromBarycentricPercentage(Point percentage, Rect rectangle)
        {
            return new Point(rectangle.Left + (percentage.X / 100D * rectangle.Width), rectangle.Top + (percentage.Y / 100D * rectangle.Height));
        }


        /// <summary>
        /// Determines whether the given point is horizontally between two points.
        /// </summary>
        /// <remarks>The interval includes the endpoints.</remarks>
        /// <param name="point">The point.</param>
        /// <param name="firstPoint">The first point.</param>
        /// <param name="secondPoint">The second point.</param>
        /// <returns>
        ///   <c>true</c> if the X-coordinate is located in the interval of the X-coordinates of the other points; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsXBetween(this Point point, Point firstPoint, Point secondPoint)
        {
            return (firstPoint.X <= point.X && point.X <= secondPoint.X) || (secondPoint.X <= point.X && point.X <= firstPoint.X);
        }

        /// <summary>
        /// Determines whether the given point is vertically between two points.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="firstPoint">The first point.</param>
        /// <param name="secondPoint">The second point.</param>
        /// <returns>
        ///   <c>true</c> if the Y-coordinate is located in the interval of the Y-coordinates of the other points; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsYBetween(this Point point, Point firstPoint, Point secondPoint)
        {
            return (firstPoint.Y <= point.Y && point.Y <= secondPoint.Y) || (secondPoint.Y <= point.Y && point.Y <= firstPoint.Y);
        }



        /// <summary>
        /// Determines whether another point sits in a delta neighborhood.
        /// </summary>
        /// <param name="originPoint">The point which determines the neighborhood.</param>
        /// <param name="point">The point which is tested for being contained in the neighborhood of <see cref="originPoint"/>.</param>
        /// <param name="delta">The size of the square around the <see cref="originPoint"/> which acts as a neighborhood around the point.</param>
        /// <returns>
        ///   <c>true</c> if <see cref="point"/> is in the neghborhood of <see cref="originPoint"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsInNeighborhood(this Point originPoint, Point point, double delta)
        {
            var x = new Rect(originPoint.X - delta, originPoint.Y - delta, 2 * delta, 2 * delta);
            return x.Contains(point);
        }

        /// <summary>
        /// Attempts to convert the given string to a Point.
        /// </summary>
        /// <param name="s">The string representation of the point.</param>
        /// <returns>The Point corresponding to the serialized form.</returns>
        public static Point? ToPoint(string s)
        {
            if (String.IsNullOrEmpty(s)) return null;
            var parts = s.Split(';');
            if (parts.Length < 2) return null;
            double x, y;
            if (Double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out x)
                &&
                Double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out y))
            {
                return new Point(x, y);
            }

            return null;
        }



        /// <summary>
        /// Converts the point to a string representation.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <returns></returns>
        public static string ToInvariantString(this Point p)
        {
            return String.Format("{0};{1}", p.X.ToString(CultureInfo.InvariantCulture), p.Y.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns the opposite of point p with respect to specified line.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point MirrorPoint(Point p, Point a, Point b)
        {
            var centralPoint = GeometryIntersections.FindLinesIntersection(a, b, p, p + a.Subtract(b).MirrorHorizontally(), true);
            return centralPoint + centralPoint.Subtract(p);
        }

        /// <summary>
        /// Returns <c>true</c> if for three given points <c>p, u, v </c>one has <c>|p-u| &lt; |p-v|</c>.
        /// </summary>
        /// <param name="p">A point.</param>
        /// <param name="u">The second point.</param>
        /// <param name="v">The third point.</param>
        public static bool AreDistanceOrdered(Point p, Point u, Point v)
        {
            return DistanceExstensions.DistanceSquared(p, u) < DistanceExstensions.DistanceSquared(p, v);
        }

        /// <summary>
        /// Subtracts the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static Point Minus(this Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        /// <summary>
        /// Mirrors the point with respect to the given center.
        /// </summary>
        public static Point MirrorPoint(Point point, Point center)
        {
            var p = new Point(center.X - point.X, center.Y - point.Y);
            return new Point(p.X + center.X, p.Y + center.Y);
        }

        /// <summary>
        /// Returs the middle point between the given points.
        /// </summary>
        /// <param name="p1">A point.</param>
        /// <param name="p2">Another point.</param>
        /// <returns>Halfway between the two given points.</returns>
        public static Point MiddlePoint(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        /// <summary>
        /// Gets a point from the minimum X and Y values from the specified points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns></returns>
        public static Point GetTopLeftPoint(IEnumerable<Point> points)
        {
            return new Point(points.Min(p => p.X), points.Min(p => p.Y));
        }

        /// <summary>
        /// Gets a point from the maximum X and Y values from the specified points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns></returns>
        public static Point GetBottomRightPoint(IEnumerable<Point> points)
        {
            return new Point(points.Max(p => p.X), points.Max(p => p.Y));
        }

        /// <summary>
        /// Substracts the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static Point Substract(this Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        /// <summary>
        /// Subtracts the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static Vector2D Subtract(this Point p1, Point p2)
        {
            return new Vector2D(p1.X - p2.X, p1.Y - p2.Y);
        }

        /// <summary>
        /// Determine whether p1 and p2 are on the same side of a line.
        /// </summary>
        public static bool SameSide(Point linePoint1, Point linePoint2, Point p1, Point p2)
        {
            return IsCounterClockWise(linePoint1, linePoint2, p1) == IsCounterClockWise(linePoint1, linePoint2, p2);
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
            return ((p1.X - p0.X) * (p2.Y - p0.Y) > (p1.Y - p0.Y) * (p2.X - p0.X)) ? 1 : -1;
        }

        /// <summary>
        /// Dots the specified v1.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public static double Dot(Vector2D v1, Vector2D v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        /// <summary>
        /// Dots the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static double Dot(Point p1, Point p2)
        {
            return (p1.X * p2.X) + (p1.Y * p2.Y);
        }

        /// <summary>
        /// Determinants the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static double Determinant(Point p1, Point p2)
        {
            return (p1.X * p2.Y) - (p1.Y * p2.X);
        }

        /// <summary>
        /// Calculates the point of the specified line segment which determines the distance from the specified point to the line segment.
        /// That is, the perpendicular projection onto the segment.
        /// </summary>
        /// <param name="p">The point outside the segment.</param>
        /// <param name="a">The first point defining the segment.</param>
        /// <param name="b">The second point defining the segment.</param>
        /// <returns></returns>
        public static Point DistancePoint(Point p, Point a, Point b)
        {
            if (a == b) return a;
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            var dotProduct = ((p.X - a.X) * dx) + ((p.Y - a.Y) * dy);
            if (dotProduct < 0) return a;
            dotProduct = ((b.X - p.X) * dx) + ((b.Y - p.Y) * dy);
            if (dotProduct < 0) return b;
            var lf = new Vector2D(a.X - b.X, a.Y - b.Y).MirrorHorizontally();
            var n = new Point(p.X + lf.X, p.Y + lf.Y);
            return GeometryIntersections.FindLinesIntersection(a, b, p, n, true);
        }

        /// <summary>
        /// Finds the projection (point) of the given point on the line.
        /// </summary>
        public static Point ProjectPointOnLine(Point p, Point a, Point b)
        {
            var perpendicular = VectorExtensions.Perpendicular(new Vector2D(a.X - b.X, a.Y - b.Y));
            var normalOffset = new Point(p.X + perpendicular.X, p.Y + perpendicular.Y);
            var intersection = GeometryIntersections.FindLinesIntersection(a, b, p, normalOffset, true);
            return DistanceExstensions.Closer(p, a, Double.IsNaN(intersection.X) ? b : DistanceExstensions.Closer(p, intersection, b));
        }



        /// <summary>
        /// Linear interpolation between the given points.
        /// </summary>
        /// <param name="p">A point.</param>
        /// <param name="q">Another point.</param>
        /// <param name="fraction">A value in the [0,1] interval. At zero the interpolation returns the first point, at one it results in the second point.</param>
        /// <returns></returns>
        public static Point Lerp(Point p, Point q, double fraction)
        {
            return new Point(Utils.Lerp(p.X, q.X, fraction), Utils.Lerp(p.Y, q.Y, fraction));
        }

        /// <summary>
        /// Normals the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static Vector2D Normal(Point p1, Point p2)
        {
            return new Vector2D(p1.Y - p2.Y, p2.X - p1.X).Normalized();
        }

        /// <summary>
        /// Inverts the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Point InvertPoint(this Point point)
        {
            return new Point(-point.X, -point.Y);
        }

        /// <summary>
        /// Nearests the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="points">The points.</param>
        /// <returns></returns>
        public static Point NearestPoint(this Point point, IEnumerable<Point> points)
        {
            if (points == null) throw new ArgumentNullException("points");
            var nearestCenter = point;
            var minDistance = Double.MaxValue;
            foreach (var centerPoint in points)
            {
                var distance = centerPoint.Distance(point);
                if (distance >= minDistance) continue;
                minDistance = distance;
                nearestCenter = centerPoint;
            }
            return nearestCenter;
        }
    }
}