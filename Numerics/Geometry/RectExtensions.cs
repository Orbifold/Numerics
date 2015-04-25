#if !PCL
using System;
using System.Windows;
using System.Windows.Media;
#endif
using System;
namespace Orbifold.Numerics
{
    /// <summary>
    /// Extensions on the <see cref="Rect"/> structure.
    /// </summary>
    public static class RectExtensions
    {
#if !PCL
        private static RotateTransform transform = new RotateTransform();

        /// <summary>
        /// Simple RotateTransform.
        /// </summary>
        public static RotateTransform RotateTransform
        {
            get
            {
                if (!transform.Dispatcher.CheckAccess()) transform = new RotateTransform();
                return transform;
            }
            set
            {
                transform = value;
            }
        }

        /// <summary>
        /// Rotates the given rectangle with the specified amount with respect to a rotation anchor.
        /// </summary>
        /// <param name="rect">The rectangle to rotate.</param>
        /// <param name="angle">The angle of rotation.</param>
        /// <param name="offsetVector">The anchor (fixed-point) of the rotation.</param>
        /// <returns></returns>
        public static Rect Rotate(this Rect rect, double angle, Point offsetVector = new Point())
        {
            RotateTransform.Angle = angle;
            RotateTransform.CenterX = rect.Left + offsetVector.X + (rect.Width / 2);
            RotateTransform.CenterY = rect.Top + offsetVector.Y + (rect.Height / 2);
            var rotatedRect = RotateTransform.TransformBounds(rect);
            return rotatedRect;
        }
      
#endif
        /// <summary>
        /// Gets whether the Rect is fully in bounds of the hosting rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="hostingRect">The hosting rect.</param>
        /// <returns>Returns true if the rect is fully inside the boudns fo the hosting rect.</returns>
        public static bool IsInBoundsOf(this Rect rect, Rect hostingRect)
        {
            return hostingRect.Contains(new Point(rect.Left, rect.Top)) && hostingRect.Contains(new Point(rect.Right, rect.Bottom));
        }

        /// <summary>
        /// Determines whether the specified rect is bigger.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="targetRect">The target rect.</param>
        /// <returns>
        ///   <c>true</c> if the specified rect is bigger; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBiggerThan(this Rect rect, Rect targetRect)
        {
            return (rect.Width * rect.Height) > (targetRect.Width * targetRect.Height);
        }

        /// <summary>
        /// Creates a new rectangle from the lefttop and rightbottom coordinates.
        /// </summary>
        public static Rect FromLtrd(double l, double t, double r, double b)
        {
            return new Rect(new Point(l, t), new Point(r, b));
        }

        /// <summary>
        /// Performs a rotation of the given point with respect to a point in the rectangle defined by an offset from the center.
        /// </summary>
        /// <param name="pointToRotate">The new point.</param>
        /// <param name="rect">The rectangle whose center will be offset and taken as the anchor of the rotation.</param>
        /// <param name="angle">The rotation angle in degrees.</param>
        /// <param name="offsetVector">The offset vector with respect to the center of the rectangle.</param>
        /// <returns></returns>
        public static Point GenerateTransform(Point pointToRotate, Rect rect, double angle, Point offsetVector)
        {
            var r = new Point(rect.Left + (rect.Width / 2) + offsetVector.X, rect.Top + (rect.Height / 2) + offsetVector.Y);
            return PointExtensions.RotatePoint(pointToRotate, angle*Math.PI/180d, r);
        }



        /// <summary>
        /// Returns the top-left point of the rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle.</param>
        /// <returns></returns>
        public static Point TopLeft(this Rect rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        /// <summary>
        /// Returns the top-right point of the rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle.</param>
        /// <returns></returns>
        public static Point TopRight(this Rect rect)
        {
            return new Point(rect.Right, rect.Top);
        }

        /// <summary>
        /// Returns the bottom-right corner of the rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle.</param>
        /// <returns></returns>
        public static Point BottomRight(this Rect rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Returns the bottom-left point of the rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle.</param>
        /// <returns></returns>
        public static Point BottomLeft(this Rect rect)
        {
            return new Point(rect.Left, rect.Bottom);
        }

        /// <summary>
        /// Centers the left.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Point CenterLeft(this Rect rect)
        {
            return new Point(rect.Left, rect.Top + (rect.Height / 2));
        }

        /// <summary>
        /// Centers the top.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Point CenterTop(this Rect rect)
        {
            return new Point(rect.Left + (rect.Width / 2), rect.Top);
        }

        /// <summary>
        /// Centers the right.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Point CenterRight(this Rect rect)
        {
            return new Point(rect.Right, rect.Top + (rect.Height / 2));
        }

        /// <summary>
        /// Centers the bottom.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Point CenterBottom(this Rect rect)
        {
            return new Point(rect.Left + (rect.Width / 2), rect.Bottom);
        }

        /// <summary>
        /// Returns the center of the specified rectangle.
        /// </summary>
        public static Point Center(this Rect rectangle)
        {
            return PointExtensions.MiddlePoint(rectangle.TopLeft(), rectangle.BottomRight());
        }

        /// <summary>
        /// Centers the X.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static double CenterX(this Rect rect)
        {
            return rect.Left + (rect.Width / 2);
        }

        /// <summary>
        /// Centers the Y.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static double CenterY(this Rect rect)
        {
            return rect.Top + (rect.Height / 2);
        }

        /// <summary>
        /// Tops the left.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point TopLeft(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Left, rect.Top);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Tops the right.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point TopRight(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Right, rect.Top);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Bottoms the right.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point BottomRight(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Right, rect.Bottom);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Bottoms the left.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point BottomLeft(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Left, rect.Bottom);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Centers the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point Center(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = PointExtensions.MiddlePoint(rect.TopLeft(), rect.BottomRight());
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Centers the left.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point CenterLeft(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Left, rect.Top + (rect.Height / 2));
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Centers the top.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point CenterTop(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Left + (rect.Width / 2), rect.Top);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Centers the right.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point CenterRight(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Right, rect.Top + (rect.Height / 2));
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }

        /// <summary>
        /// Centers the bottom.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Point CenterBottom(this Rect rect, double angle, Point offsetVector = new Point())
        {
            var newPoint = new Point(rect.Left + (rect.Width / 2), rect.Bottom);
            return GenerateTransform(newPoint, rect, angle, offsetVector);
        }



        /// <summary>
        /// Determines whether [contains] [the specified rect].
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="point">The point.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified rect]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this Rect rect, Point point, double angle)
        {
            var rotatedPoint = point;
            if (System.Math.Abs(angle % 180 - 0) > Constants.Epsilon) rotatedPoint = point.Rotate(rect.Center(), -angle);

            return rect.Contains(rotatedPoint);
        }

        /// <summary>
        /// Returns whether the rectangle intersects the rotated rectangle.
        /// </summary>
        /// <remarks>The rotation is with respect to the center of the rectangle.</remarks>
        /// <param name="rect">A rectangle.</param>
        /// <param name="otherRectangle">The rectangle which will be rotated before being compared for overlap.</param>
        /// <param name="angle">The angle of the rotation.</param>
        /// <returns></returns>
        public static bool IntersectsWith(this Rect rect, Rect otherRectangle, double angle)
        {
            if (System.Math.Abs(angle % 180 - 0) <= Constants.Epsilon) return rect.IntersectsWith(otherRectangle);
            var center = otherRectangle.Center();
            var isIntersecting = false;
            var topLeftPoint = otherRectangle.TopLeft().Rotate(center, angle);
            var topRightPoint = otherRectangle.TopRight().Rotate(center, angle);
            var bottomLeftPoint = otherRectangle.BottomLeft().Rotate(center, angle);
            var bottomRightPoint = otherRectangle.BottomRight().Rotate(center, angle);
            isIntersecting |= rect.Contains(topLeftPoint);
            isIntersecting |= rect.Contains(topRightPoint);
            isIntersecting |= rect.Contains(bottomLeftPoint);
            isIntersecting |= rect.Contains(bottomRightPoint);

            if (!isIntersecting)
            {
                var intersactionPoint = new Point();
                isIntersecting =  GeometryIntersections.IntersectsLineSegment (rect,topLeftPoint, topRightPoint, ref intersactionPoint);
                isIntersecting |= GeometryIntersections.IntersectsLineSegment(rect,topLeftPoint, bottomLeftPoint, ref intersactionPoint);
                isIntersecting |= GeometryIntersections.IntersectsLineSegment(rect,topRightPoint, bottomRightPoint, ref intersactionPoint);
                isIntersecting |= GeometryIntersections.IntersectsLineSegment(rect,bottomLeftPoint, bottomRightPoint, ref intersactionPoint);
            }

            return isIntersecting;
        }

        /// <summary>
        /// Returns the size of the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns></returns>
        public static Size ToSize(this Rect rect)
        {
            return new Size(rect.Width, rect.Height);
        }

        /// <summary>
        /// Return a rectangle located a (0.0) with the specified size.
        /// </summary>
        /// <param name="size">The s.</param>
        /// <returns>A rectangle located at the (0.0).</returns>
        public static Rect ToRect(this Size size)
        {
            return new Rect(0, 0, size.Width, size.Height);
        }

        /// <summary>
        /// Creates a new rectangle based on the middle point rather than the LT origin.
        /// </summary>
        /// <param name="center">The center of the rectangle.</param>
        /// <param name="size">The size of the new rectangle.</param>
        /// <returns></returns>
        public static Rect NewRect(Point center, double size)
        {
            return new Rect(center.X - (size / 2), center.Y - (size / 2), size, size);
        }

        /// <summary>
        /// News the rect.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static Rect NewRect(Point center, Size size)
        {
            return new Rect(center.X - (size.Width / 2), center.Y - (size.Height / 2), size.Width, size.Height);
        }

        /// <summary>
        /// Offsets the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Rect Offset(this Rect rect, Vector2D offsetVector)
        {
            return Offset(rect, offsetVector.X, offsetVector.Y);
        }

        /// <summary>
        /// Offsets the current rectangle with the specified values.
        /// </summary>
        /// <param name="rect">The rectangle to offset.</param>
        /// <param name="x">The horizontal offset.</param>
        /// <param name="y">The vertical offset.</param>
        /// <returns></returns>
        public static Rect Offset(Rect rect, double x, double y)
        {
            rect.X += x;
            rect.Y += y;
            return rect;
        }

        /// <summary>
        /// Inflates the given rectangle with the specified amount.
        /// </summary>
        public static Rect Inflate(this Rect rect, double deltaX, double deltaY)
        {
            ////border case when the delta is negative and larger than the actual rectangle size
            if (rect.Width + (2 * deltaX) < 0) deltaX = -rect.Width / 2;
            if (rect.Height + (2 * deltaY) < 0) deltaY = -rect.Height / 2;
            return new Rect(rect.X - deltaX, rect.Y - deltaY, rect.Width + (2 * deltaX), rect.Height + (2 * deltaY));
        }

        /// <summary>
        /// Inflates the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static Rect Inflate(this Rect rect, Size size)
        {
            return Inflate(rect, size.Width, size.Height, size.Width, size.Height);
        }

        internal static bool Contains(this Rect r1, Rect r2)
        {
            return r1.Contains(r2.BottomLeft()) && r1.Contains(r2.BottomRight()) && r1.Contains(r2.TopRight()) && r1.Contains(r2.TopLeft());
        }

        internal static void SetLocation(ref Rect bounds, Point point)
        {
            bounds.X = point.X;
            bounds.Y = point.Y;
        }

        /// <summary>
        /// Determines whether [contains] [the specified rectangle].
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="p">The p.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified rectangle]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this Rect rectangle, Point p)
        {
            return rectangle.Contains(p);
        }

        /// <summary>
        /// Returns the center point of the specified rectangle.
        /// </summary>
        public static Point MiddlePoint(Rect rect)
        {
            return new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2));
        }

        /// <summary>
        /// Unions the specified a.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static Rect Union(Rect a, Rect b)
        {
            var x = a;
            x.Union(b);
            return x;
        }

        /// <summary>
        /// Inflates the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <returns></returns>
        public static Rect Inflate(this Rect rect, double left, double top, double right, double bottom)
        {
            var width = rect.Width + left + right;
            var height = rect.Height + top + bottom;
            var x = rect.X - left;
            if (width < 0)
            {
                x = x + (width / 2);
                width = 0;
            }
            var y = rect.Y - top;
            if (height < 0)
            {
                y = y + (height / 2);
                height = 0;
            }
            return new Rect(x, y, width, height);
        }
    }
}