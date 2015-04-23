
using System;
using System.Diagnostics;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Represents a 3x3 affine transformation matrix used for transformations in two-dimensional space.
    /// </summary>
    public struct Matrix : IFormattable
    {
        private static Matrix s_identity = Matrix.CreateIdentity();
        private const int c_identityHashCode = 0;
        internal double _m11;
        internal double _m12;
        internal double _m21;
        internal double _m22;
        internal double _offsetX;
        internal double _offsetY;
        internal MatrixTypes _type;

        /// <summary>
        /// Gets or sets the value of the first row and first column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the first row and first column of this <see cref="T:System.Windows.Media.Matrix"/>. The default value is 1.
        /// </returns>
        public double M11
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 1.0;
                else
                    return this._m11;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(value, 0.0, 0.0, 1.0, 0.0, 0.0, MatrixTypes.TRANSFORM_IS_SCALING);
                }
                else
                {
                    this._m11 = value;
                    if (this._type == MatrixTypes.TRANSFORM_IS_UNKNOWN)
                        return;
                    this._type |= MatrixTypes.TRANSFORM_IS_SCALING;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the first row and second column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the first row and second column of this <see cref="T:System.Windows.Media.Matrix"/>. The default value is 0.
        /// </returns>
        public double M12
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 0.0;
                else
                    return this._m12;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(1.0, value, 0.0, 1.0, 0.0, 0.0, MatrixTypes.TRANSFORM_IS_UNKNOWN);
                }
                else
                {
                    this._m12 = value;
                    this._type = MatrixTypes.TRANSFORM_IS_UNKNOWN;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the second row and first column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the second row and first column of this <see cref="T:System.Windows.Media.Matrix"/>. The default value is 0.
        /// </returns>
        public double M21
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 0.0;
                else
                    return this._m21;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(1.0, 0.0, value, 1.0, 0.0, 0.0, MatrixTypes.TRANSFORM_IS_UNKNOWN);
                }
                else
                {
                    this._m21 = value;
                    this._type = MatrixTypes.TRANSFORM_IS_UNKNOWN;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the second row and second column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the second row and second column of this <see cref="T:System.Windows.Media.Matrix"/> structure. The default value is 1.
        /// </returns>
        public double M22
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 1.0;
                else
                    return this._m22;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(1.0, 0.0, 0.0, value, 0.0, 0.0, MatrixTypes.TRANSFORM_IS_SCALING);
                }
                else
                {
                    this._m22 = value;
                    if (this._type == MatrixTypes.TRANSFORM_IS_UNKNOWN)
                        return;
                    this._type |= MatrixTypes.TRANSFORM_IS_SCALING;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the third row and first column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the third row and first column of this <see cref="T:System.Windows.Media.Matrix"/> structure. The default value is 0.
        /// </returns>
        public double OffsetX
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 0.0;
                else
                    return this._offsetX;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(1.0, 0.0, 0.0, 1.0, value, 0.0, MatrixTypes.TRANSFORM_IS_TRANSLATION);
                }
                else
                {
                    this._offsetX = value;
                    if (this._type == MatrixTypes.TRANSFORM_IS_UNKNOWN)
                        return;
                    this._type |= MatrixTypes.TRANSFORM_IS_TRANSLATION;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the third row and second column of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the third row and second column of this <see cref="T:System.Windows.Media.Matrix"/> structure. The default value is 0.
        /// </returns>
        public double OffsetY
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return 0.0;
                else
                    return this._offsetY;
            }
            set
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                {
                    this.SetMatrix(1.0, 0.0, 0.0, 1.0, 0.0, value, MatrixTypes.TRANSFORM_IS_TRANSLATION);
                }
                else
                {
                    this._offsetY = value;
                    if (this._type == MatrixTypes.TRANSFORM_IS_UNKNOWN)
                        return;
                    this._type |= MatrixTypes.TRANSFORM_IS_TRANSLATION;
                }
            }
        }

        /// <summary>
        /// Gets an identity <see cref="T:System.Windows.Media.Matrix"/>.
        /// </summary>
        /// 
        /// <returns>
        /// An identity matrix.
        /// </returns>
        public static Matrix Identity
        {
            get
            {
                return Matrix.s_identity;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="T:System.Windows.Media.Matrix"/> structure is an identity matrix.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Windows.Media.Matrix"/> structure is an identity matrix; otherwise, false. The default is true.
        /// </returns>
        public bool IsIdentity
        {
            get
            {
                if (this._type == MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return true;
                if (this._m11 == 1.0 && this._m12 == 0.0 && (this._m21 == 0.0 && this._m22 == 1.0) && this._offsetX == 0.0)
                    return this._offsetY == 0.0;
                else
                    return false;
            }
        }

        private bool IsDistinguishedIdentity
        {
            get
            {
                return this._type == MatrixTypes.TRANSFORM_IS_IDENTITY;
            }
        }

        static Matrix()
        {
        }

        /// <summary>
        /// Initializes a <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// <param name="m11">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.M11"/> coefficient.</param><param name="m12">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.M12"/> coefficient.</param><param name="m21">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.M21"/> coefficient.</param><param name="m22">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.M22"/> coefficient.</param><param name="offsetX">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.OffsetX"/> coefficient.</param><param name="offsetY">The <see cref="T:System.Windows.Media.Matrix"/> structure's <see cref="P:System.Windows.Media.Matrix.OffsetY"/> coefficient.</param>
        public Matrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
        {
            this._m11 = m11;
            this._m12 = m12;
            this._m21 = m21;
            this._m22 = m22;
            this._offsetX = offsetX;
            this._offsetY = offsetY;
            this._type = MatrixTypes.TRANSFORM_IS_UNKNOWN;
            this.DeriveMatrixType();
        }

        /// <summary>
        /// Determines whether the two specified <see cref="T:System.Windows.Media.Matrix"/> structures are identical.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="matrix1"/> and <paramref name="matrix2"/> are identical; otherwise, false.
        /// </returns>
        /// <param name="matrix1">The first <see cref="T:System.Windows.Media.Matrix"/> structure to compare.</param><param name="matrix2">The second <see cref="T:System.Windows.Media.Matrix"/> structure to compare.</param>
        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
                return matrix1.IsIdentity == matrix2.IsIdentity;
            if (matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && (matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22) && matrix1.OffsetX == matrix2.OffsetX)
                return matrix1.OffsetY == matrix2.OffsetY;
            else
                return false;
        }

        /// <summary>
        /// Determines whether the two specified <see cref="T:System.Windows.Media.Matrix"/> structures are not identical.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="matrix1"/> and <paramref name="matrix2"/> are not identical; otherwise, false.
        /// </returns>
        /// <param name="matrix1">The first <see cref="T:System.Windows.Media.Matrix"/> structure to compare.</param><param name="matrix2">The second <see cref="T:System.Windows.Media.Matrix"/> structure to compare.</param>
        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !(matrix1 == matrix2);
        }

        internal static Matrix Create(object o)
        {
            if (o != null)
            {
                float[] numArray = (float[])o;
                if (numArray.Length == 6)
                    return new Matrix((double)numArray[0], (double)numArray[1], (double)numArray[2], (double)numArray[3], (double)numArray[4], (double)numArray[5]);
            }
            return Matrix.s_identity;
        }

        /// <summary>
        /// Creates a <see cref="T:System.String"/> representation of this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> containing the <see cref="P:System.Windows.Media.Matrix.M11"/>, <see cref="P:System.Windows.Media.Matrix.M12"/>, <see cref="P:System.Windows.Media.Matrix.M21"/>, <see cref="P:System.Windows.Media.Matrix.M22"/>, <see cref="P:System.Windows.Media.Matrix.OffsetX"/>, and <see cref="P:System.Windows.Media.Matrix.OffsetY"/> values of this <see cref="T:System.Windows.Media.Matrix"/>.
        /// </returns>
        public override string ToString()
        {
            return this.ConvertToString((string)null, (IFormatProvider)null);
        }

        /// <summary>
        /// Creates a <see cref="T:System.String"/> representation of this <see cref="T:System.Windows.Media.Matrix"/> structure with culture-specific formatting information.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> containing the <see cref="P:System.Windows.Media.Matrix.M11"/>, <see cref="P:System.Windows.Media.Matrix.M12"/>, <see cref="P:System.Windows.Media.Matrix.M21"/>, <see cref="P:System.Windows.Media.Matrix.M22"/>, <see cref="P:System.Windows.Media.Matrix.OffsetX"/>, and <see cref="P:System.Windows.Media.Matrix.OffsetY"/> values of this <see cref="T:System.Windows.Media.Matrix"/>.
        /// </returns>
        /// <param name="provider">The culture-specific formatting information.</param>
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
            if (this.IsIdentity)
                return "Identity";
            char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
            return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}{0}{4:" + format + "}{0}{5:" + format + "}{0}{6:" + format + "}", (object)numericListSeparator, (object)this._m11, (object)this._m12, (object)this._m21, (object)this._m22, (object)this._offsetX, (object)this._offsetY);
        }

        /// <summary>
        /// Transforms the specified point by the <see cref="T:System.Windows.Media.Matrix"/> and returns the result.
        /// </summary>
        /// 
        /// <returns>
        /// The result of transforming <paramref name="point"/> by this <see cref="T:System.Windows.Media.Matrix"/>.
        /// </returns>
        /// <param name="point">The point to transform.</param>
        public Point Transform(Point point)
        {
            Point point1 = point;
            this.MultiplyPoint(ref point1._x, ref point1._y);
            return point1;
        }

        //public Point Transform(Vector vector)
        //{
        //    Point point1 = new Point(vector.X, vector.Y);
        //    this.MultiplyPoint(ref point1._x, ref point1._y);
        //    return point1;
        //}

        public Vector2D Transform(Vector2D vector)
        {
            Point point1 = new Point(vector.X, vector.Y);
            this.MultiplyPoint(ref point1._x, ref point1._y);
            return new Vector2D(point1.X, point1.Y);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="T:System.Windows.Media.Matrix"/> structure.
        /// </summary>
        /// 
        /// <returns>
        /// The hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            if (this.IsDistinguishedIdentity)
                return 0;
            else
                return this.M11.GetHashCode() ^ this.M12.GetHashCode() ^ this.M21.GetHashCode() ^ this.M22.GetHashCode() ^ this.OffsetX.GetHashCode() ^ this.OffsetY.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is a <see cref="T:System.Windows.Media.Matrix"/> structure that is identical to this <see cref="T:System.Windows.Media.Matrix"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if <paramref name="o"/> is a <see cref="T:System.Windows.Media.Matrix"/> structure that is identical to this <see cref="T:System.Windows.Media.Matrix"/> structure; otherwise, false.
        /// </returns>
        /// <param name="o">The <see cref="T:System.Object"/> to compare.</param>
        public override bool Equals(object o)
        {
            if (o == null || !(o is Matrix))
                return false;
            else
                return Matrix.Equals(this, (Matrix)o);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Windows.Media.Matrix"/> structure is identical to this instance.
        /// </summary>
        /// 
        /// <returns>
        /// true if instances are equal; otherwise, false.
        /// </returns>
        /// <param name="value">The instance of <see cref="T:System.Windows.Media.Matrix"/> to compare to this instance.</param>
        public bool Equals(Matrix value)
        {
            return Matrix.Equals(this, value);
        }

        private static Matrix CreateIdentity()
        {
            Matrix matrix = new Matrix();
            matrix.SetMatrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0, MatrixTypes.TRANSFORM_IS_IDENTITY);
            return matrix;
        }

        private void SetMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY, MatrixTypes type)
        {
            this._m11 = m11;
            this._m12 = m12;
            this._m21 = m21;
            this._m22 = m22;
            this._offsetX = offsetX;
            this._offsetY = offsetY;
            this._type = type;
        }

        private void DeriveMatrixType()
        {
            this._type = MatrixTypes.TRANSFORM_IS_IDENTITY;
            if (this._m21 != 0.0 || this._m12 != 0.0)
            {
                this._type = MatrixTypes.TRANSFORM_IS_UNKNOWN;
            }
            else
            {
                if (this._m11 != 1.0 || this._m22 != 1.0)
                    this._type = MatrixTypes.TRANSFORM_IS_SCALING;
                if (this._offsetX != 0.0 || this._offsetY != 0.0)
                    this._type |= MatrixTypes.TRANSFORM_IS_TRANSLATION;
                if ((this._type & (MatrixTypes.TRANSFORM_IS_TRANSLATION | MatrixTypes.TRANSFORM_IS_SCALING)) != MatrixTypes.TRANSFORM_IS_IDENTITY)
                    return;
                this._type = MatrixTypes.TRANSFORM_IS_IDENTITY;
            }
        }

        private void MultiplyPoint(ref double x, ref double y)
        {
            switch (this._type)
            {
                case MatrixTypes.TRANSFORM_IS_IDENTITY:
                    break;
                case MatrixTypes.TRANSFORM_IS_TRANSLATION:
                    x += this._offsetX;
                    y += this._offsetY;
                    break;
                case MatrixTypes.TRANSFORM_IS_SCALING:
                    x *= this._m11;
                    y *= this._m22;
                    break;
                case MatrixTypes.TRANSFORM_IS_TRANSLATION | MatrixTypes.TRANSFORM_IS_SCALING:
                    x *= this._m11;
                    x += this._offsetX;
                    y *= this._m22;
                    y += this._offsetY;
                    break;
                default:
                    double num1 = y * this._m21 + this._offsetX;
                    double num2 = x * this._m12 + this._offsetY;
                    x *= this._m11;
                    x += num1;
                    y *= this._m22;
                    y += num2;
                    break;
            }
        }

        private static bool Equals(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
                return matrix1.IsIdentity == matrix2.IsIdentity;
            if (matrix1.M11.Equals(matrix2.M11) && matrix1.M12.Equals(matrix2.M12) && (matrix1.M21.Equals(matrix2.M21) && matrix1.M22.Equals(matrix2.M22)) && matrix1.OffsetX.Equals(matrix2.OffsetX))
                return matrix1.OffsetY.Equals(matrix2.OffsetY);
            else
                return false;
        }

        internal bool Invert()
        {
            double num = this.M11 * this.M22 - this.M12 * this.M21;
            if (num == 0.0)
                return false;
            Matrix matrix = this;
            this.M11 = matrix.M22 / num;
            this.M12 = -1.0 * matrix.M12 / num;
            this.M21 = -1.0 * matrix.M21 / num;
            this.M22 = matrix.M11 / num;
            this.OffsetX = (matrix.OffsetY * matrix.M21 - matrix.OffsetX * matrix.M22) / num;
            this.OffsetY = (matrix.OffsetX * matrix.M12 - matrix.OffsetY * matrix.M11) / num;
            return true;
        }
    }
    [Flags]
    internal enum MatrixTypes
    {
        TRANSFORM_IS_IDENTITY = 0,
        TRANSFORM_IS_TRANSLATION = 1,
        TRANSFORM_IS_SCALING = 2,
        TRANSFORM_IS_UNKNOWN = 4,
    }
}