using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Geometric intersection and overlap methods.
    /// </summary>
    public static class GeometryIntersections
    {
        /// <summary>
        /// Returns whether the two rectangles intersect.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool AreIntersecting(Rect rectangle, Rect rect)
        {
            rectangle.Intersect(rect);
            return !rectangle.IsEmpty;
        }

        /// <summary>
        /// Returns whether the specified rectangle and circle intersect.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public static bool AreIntersecting(Rect rectangle, Point center, double radius)
        {
            var leftX = rectangle.Left - center.X;
            var rightX = rectangle.Right - center.X;
            var topY = rectangle.Top - center.Y;
            var bottomY = rectangle.Bottom - center.Y;

            if (rightX < 0)
            {
                // SW
                if (topY > 0) return (rightX * rightX) + (topY * topY) < radius * radius;
                // NW
                if (bottomY < 0) return (rightX * rightX) + (bottomY * bottomY) < radius * radius;
                return System.Math.Abs(rightX) < radius;
            }
            if (leftX > 0)
            {
                // SE
                if (topY > 0) return (leftX * leftX) + (topY * topY) < radius * radius;
                // NE
                if (bottomY < 0) return (leftX * leftX) + (bottomY * bottomY) < radius * radius;
                return System.Math.Abs(leftX) < radius;
            }
            if (topY > 0) return System.Math.Abs(topY) < radius;
            if (bottomY < 0) return System.Math.Abs(bottomY) < radius;
            return true;
        }

        /// <summary>
        /// Returns whether the specified point is inside the ellipse defined by the specified rectangle.
        /// </summary>
        public static bool IsPointInEllipse(Point point, Rect rectangle)
        {
            var rct = rectangle;
            var width = (rct.Right - rct.Left) / 2;
            var height = (rct.Bottom - rct.Top) / 2;
            var x = point.X - ((rct.Left + rct.Right) / 2);
            var y = point.Y - ((rct.Top + rct.Bottom) / 2);
            ////basic geometry again
            return ((x * x) / (width * width)) + ((y * y) / (height * height)) <= 1;
        }

        /// <summary>
        /// Determines whether [is point in rectangle] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        ///   <c>true</c> if [is point in rectangle] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPointInRectangle(Point point, Size size)
        {
            return Utils.BetweenOrEqualSorted(point.X, 0, size.Width) && Utils.BetweenOrEqualSorted(point.Y, 0, size.Height);
        }

        /// <summary>
        /// Returns whether the specified rectangle and circle intersect.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public static bool IntersectsCircle(this Rect rectangle, Point center, double radius)
        {
            return AreIntersecting(rectangle, center, radius);
        }

        /// <summary>
        /// Returns whether the line (line segments) intersect and returns in the crossingPoint the actual crossing
        /// point if they do.
        /// </summary>
        /// <param name="a">The first point of the first line.</param>
        /// <param name="b">The second point of the first line.</param>
        /// <param name="c">The first point of the second line.</param>
        /// <param name="d">The second point of the second line.</param>
        /// <param name="intersectionPoint">The crossing point, if any.</param>
        /// <returns></returns>
        public static bool AreLinesIntersecting(Point a, Point b, Point c, Point d, ref Point intersectionPoint)
        {
            var tangensdiff = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            if (tangensdiff.IsEqualTo(0)) return false;
            var num1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            var num2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));
            var r = num1 / tangensdiff;
            var s = num2 / tangensdiff;

            if (r < 0 || r > 1 || s < 0 || s > 1) return false;
            intersectionPoint = new Point(a.X + (r * (b.X - a.X)), a.Y + (r * (b.Y - a.Y)));
            return true;
        }
        /// <summary>
        /// Returns whether the rectangle and the segment intersect.
        /// </summary>
        /// <param name="rect">A rectangle.</param>
        /// <param name="a">An endpoint of the segment.</param>
        /// <param name="b">The complementary endpoint of the segment.</param>
        /// <param name="intersectionPoint">The crossing point, if any.</param>
        /// <returns></returns>
        public static bool IntersectsLineSegment(this Rect rect, Point a, Point b, ref Point intersectionPoint)
        {
            return AreLinesIntersecting(a, b, rect.TopLeft(), rect.TopRight(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.TopRight(), rect.BottomRight(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.BottomRight(), rect.BottomLeft(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.BottomLeft(), rect.TopLeft(), ref intersectionPoint);
        }

        /// <summary>
        /// Returns whether the polyline segment intersect the rectangle.
        /// </summary>
        /// <param name="rect">A rectangle</param>
        /// <param name="polyline">A polyline.</param>
        /// <returns></returns>
        /// <seealso cref="IntersectsLineSegment"/>
        public static bool IntersectsLine(this Rect rect, IList polyline)
        {
            var intersectsWithRect = false;
            for (var s = 0; s < polyline.Count - 1; ++s)
            {
                var p1 = (Point)polyline[s];
                var p2 = (Point)polyline[s + 1];
                var intersectionPoint = new Point();
                intersectsWithRect = GeometryIntersections.IntersectsLineSegment(rect, p1, p2, ref intersectionPoint);
                if (intersectsWithRect) break;
            }
            return intersectsWithRect;
        }

        /// <summary>
        /// Returns whether the given rectangle intersects the current one.
        /// </summary>
        /// <param name="r1">The first rectangle.</param>
        /// <param name="r2">The queried rectangle which potentially intersects.</param>
        /// <returns></returns>
        public static bool IntersectsWith(this Rect r1, Rect r2)
        {
            r1.Intersect(r2);
            return !r1.IsEmpty;
        }

        /// <summary>
        /// Checks whether the segments defined by the specified
        /// point pairs intersect and returns the intersection point.
        /// </summary>
        public static bool SegmentIntersect(Point s1, Point s2, Point l1, Point l2, ref Point p)
        {
            p = FindLinesIntersection(s1, s2, l1, l2);
            var p1 = (p.X - s1.X) * (p.X - s2.X);
            var p2 = (p.Y - s1.Y) * (p.Y - s2.Y);
            if (p1 > Constants.Epsilon || p2 > Constants.Epsilon) return false;
            var pl1 = (p.X - l1.X) * (p.X - l2.X);
            var pl2 = (p.Y - l1.Y) * (p.Y - l2.Y);
            return pl1 <= Constants.Epsilon && pl2 <= Constants.Epsilon;
        }

        /// <summary>
        /// Finds the intersection point of the lines defined by the point pairs.
        /// </summary>
        /// <returns>
        /// The intersection point. If acceptNaN is <c>true</c> a <c>double.NaN</c> is returned if they don't intersect.
        /// </returns>
        public static Point FindLinesIntersection(Point a, Point b, Point c, Point d, bool acceptNaN = false)
        {
            var pt = acceptNaN ? new Point(Double.NaN, Double.NaN) : new Point(Double.MinValue, Double.MinValue);
            if (a.X.IsEqualTo(b.X) && c.X.IsEqualTo(d.X)) return pt;

            //// Check if the first line is vertical
            if (a.X.IsEqualTo(b.X))
            {
                pt.X = a.X;
                pt.Y = ((c.Y - d.Y) / (c.X - d.X) * pt.X) + (((c.X * d.Y) - (d.X * c.Y)) / (c.X - d.X));
                return pt;
            }
            //// Check if the second line is vertical
            if (c.X.IsEqualTo(d.X))
            {
                pt.X = c.X;
                pt.Y = ((a.Y - b.Y) / (a.X - b.X) * pt.X) + (((a.X * b.Y) - (b.X * a.Y)) / (a.X - b.X));
                return pt;
            }
            var a1 = (a.Y - b.Y) / (a.X - b.X);
            var b1 = ((a.X * b.Y) - (b.X * a.Y)) / (a.X - b.X);
            var a2 = (c.Y - d.Y) / (c.X - d.X);
            var b2 = ((c.X * d.Y) - (d.X * c.Y)) / (c.X - d.X);
            if (a1.IsNotEqualTo(a2) || acceptNaN)
            {
                pt.X = (b2 - b1) / (a1 - a2);
                pt.Y = (a1 * (b2 - b1) / (a1 - a2)) + b1;
            }

            return pt;
        }

        /// <summary>
        /// Calculate the intersection point between an ellipse and a line segment.
        /// </summary>
        public static Point IntersectionPoint(Rect rectangle, Point pt1, Point pt2)
        {
            var rc = new Rect(pt1, pt2);
            var pointOfIntersection = new Point();

            var x1 = pt1.X;
            var y1 = pt1.Y;
            var x2 = pt2.X;
            var y2 = pt2.Y;

            if (System.Math.Abs(x1 - x2) > Constants.Epsilon)
            {
                var cx = (rectangle.Left + rectangle.Right) / 2;
                var cy = (rectangle.Top + rectangle.Bottom) / 2;
                var ea = (rectangle.Right - rectangle.Left) / 2;
                var eb = (rectangle.Bottom - rectangle.Top) / 2;
                var a = (y1 - y2) / (x1 - x2);
                var b = ((x1 * y2) - (x2 * y1)) / (x1 - x2);

                var alpha = (eb * eb) + (a * a * ea * ea);
                var beta = (2 * a * (b - cy) * ea * ea) - (2 * cx * eb * eb);
                var rho = (eb * eb * cx * cx) + (ea * ea * (b - cy) * (b - cy)) - (ea * ea * eb * eb);

                var d = System.Math.Sqrt((beta * beta) - (4 * alpha * rho));
                var p1X = (-beta + d) / (2 * alpha);
                var p2X = (-beta - d) / (2 * alpha);
                var p1Y = (a * p1X) + b;
                var p2Y = (a * p2X) + b;

                pointOfIntersection.X = p1X;
                pointOfIntersection.Y = p1Y;
                if (pointOfIntersection.X >= rc.Left && pointOfIntersection.X <= rc.Right && pointOfIntersection.Y >= rc.Top && pointOfIntersection.Y <= rc.Bottom) return pointOfIntersection;

                pointOfIntersection.X = p2X;
                pointOfIntersection.Y = p2Y;
                if (pointOfIntersection.X >= rc.Left && pointOfIntersection.X <= rc.Right && pointOfIntersection.Y >= rc.Top && pointOfIntersection.Y <= rc.Bottom) return pointOfIntersection;
            }
            else
            {
                var cx = (rectangle.Left + rectangle.Right) / 2;
                var cy = (rectangle.Top + rectangle.Bottom) / 2;
                var ea = (rectangle.Right - rectangle.Left) / 2;
                var eb = (rectangle.Bottom - rectangle.Top) / 2;

                var x = x1;
                var mu = cy - System.Math.Sqrt(1 - ((x - cx) * (x - cx) / (ea * ea) * eb * eb));
                var nu = cy + System.Math.Sqrt(1 - ((x - cx) * (x - cx) / (ea * ea) * eb * eb));

                pointOfIntersection.X = x;
                pointOfIntersection.Y = mu;
                if (pointOfIntersection.X >= rc.Left && pointOfIntersection.X <= rc.Right && pointOfIntersection.Y >= rc.Top && pointOfIntersection.Y <= rc.Bottom) return pointOfIntersection;

                pointOfIntersection.X = x;
                pointOfIntersection.Y = nu;
                if (pointOfIntersection.X >= rc.Left && pointOfIntersection.X <= rc.Right && pointOfIntersection.Y >= rc.Top && pointOfIntersection.Y <= rc.Bottom) return pointOfIntersection;
            }

            return pointOfIntersection;
        }

        /// <summary>
        /// Calculate the intersection point between a polyline and a line segment.
        /// </summary>
        public static bool IntersectionPointOnEllipse(Collection<Point> points, Point org, Point end, ref Point result)
        {
            var intersection = new Point();
            var d = Double.PositiveInfinity;
            for (var i = 0; i < points.Count; i++)
            {
                if (!SegmentIntersect(points[i], points[(i + 1) % points.Count], org, end, ref intersection)) continue;
                var currentDistanceSq = DistanceExstensions.DistanceSquared(intersection, end);
                if (currentDistanceSq >= d) continue;
                d = currentDistanceSq;
                result = intersection;
            }
            return !Double.IsPositiveInfinity(d);
        }

        /// <summary>
        /// Calculates the intersection point between the specified
        /// rectangle and the line segment defined by the specified
        /// points.
        /// </summary>
        public static void IntersectionPointOnRectangle(Rect rectangle, Point pt1, Point pt2, ref Point intersectionPoint)
        {
            var rc = RectExtensions.FromLtrd(pt1.X, pt1.Y, pt2.X, pt2.Y);

            var x1 = pt1.X;
            var y1 = pt1.Y;
            var x2 = pt2.X;
            var y2 = pt2.Y;

            if (x1 == x2)
            {
                intersectionPoint.X = x1;

                // try with the top line
                intersectionPoint.Y = rectangle.Top;
                if (intersectionPoint.X >= rectangle.Left && intersectionPoint.X <= rectangle.Right &&
                    intersectionPoint.Y >= rc.Top && intersectionPoint.Y <= rc.Bottom) return;

                // try with the bottom line
                intersectionPoint.Y = rectangle.Bottom;
                if (intersectionPoint.X >= rectangle.Left && intersectionPoint.X <= rectangle.Right &&
                    intersectionPoint.Y >= rc.Top && intersectionPoint.Y <= rc.Bottom) return;
            }
            else if (y1 == y2)
            {
                intersectionPoint.Y = y1;

                // Try with the left line segment
                intersectionPoint.X = rectangle.Left;
                if (intersectionPoint.Y >= rectangle.Top && intersectionPoint.Y <= rectangle.Bottom &&
                    intersectionPoint.X >= rc.Left && intersectionPoint.X <= rc.Right) return;

                // Try with the right line segment
                intersectionPoint.X = rectangle.Right;
                if (intersectionPoint.Y >= rectangle.Top && intersectionPoint.Y <= rectangle.Bottom &&
                    intersectionPoint.X >= rc.Left && intersectionPoint.X <= rc.Right) return;
            }
            else
            {
                var a = (y1 - y2) / (x1 - x2);
                var b = ((x1 * y2) - (x2 * y1)) / (x1 - x2);

                ////TOP
                intersectionPoint.Y = rectangle.Top;
                intersectionPoint.X = (intersectionPoint.Y - b) / a;
                if (intersectionPoint.X >= rectangle.Left && intersectionPoint.X <= rectangle.Right &&
                    intersectionPoint.Y <= rectangle.Bottom &&
                    intersectionPoint.Y >= rc.Top && intersectionPoint.Y <= rc.Bottom) return;

                //// BOTTOM
                intersectionPoint.Y = rectangle.Bottom;
                intersectionPoint.X = (intersectionPoint.Y - b) / a;
                if (intersectionPoint.X >= rectangle.Left && intersectionPoint.X <= rectangle.Right &&
                    intersectionPoint.Y >= rectangle.Top &&
                    intersectionPoint.Y >= rc.Top && intersectionPoint.Y <= rc.Bottom) return;

                ////LEFT
                intersectionPoint.X = rectangle.Left;
                intersectionPoint.Y = (a * intersectionPoint.X) + b;
                if (intersectionPoint.Y >= rectangle.Top && intersectionPoint.Y <= rectangle.Bottom &&
                    intersectionPoint.X <= rectangle.Right &&
                    intersectionPoint.X >= rc.Left && intersectionPoint.X <= rc.Right) return;

                ////RIGHT
                intersectionPoint.X = rectangle.Right;
                intersectionPoint.Y = (a * intersectionPoint.X) + b;
                if (intersectionPoint.Y >= rectangle.Top && intersectionPoint.Y <= rectangle.Bottom &&
                    intersectionPoint.X >= rectangle.Left &&
                    intersectionPoint.X >= rc.Left && intersectionPoint.X <= rc.Right) return;
            }
        }
    }
}