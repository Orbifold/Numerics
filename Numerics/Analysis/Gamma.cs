using System;

namespace Orbifold.Numerics
{
    class Gamma
    {
        /// <summary>
        ///   Gamma function as computed by Stirling's formula.
        /// </summary>
        /// 
        public static double Stirling(double x)
        {
            double[] STIR =
            {
                 7.87311395793093628397E-4,
                -2.29549961613378126380E-4,
                -2.68132617805781232825E-3,
                 3.47222221605458667310E-3,
                 8.33333333333482257126E-2,
            };

            double MAXSTIR = 143.01608;

            double w = 1.0 / x;
            double y = Math.Exp(x);

            w = 1.0 + w * Functions.Polevl(w, STIR, 4);

            if (x > MAXSTIR)
            {
                double v = Math.Pow(x, 0.5 * x - 0.25);

                if (Double.IsPositiveInfinity(v) && Double.IsPositiveInfinity(y))
                {
                    // lim x -> inf { (x^(0.5*x - 0.25)) * (x^(0.5*x - 0.25) / exp(x))  }
                    y = Double.PositiveInfinity;
                }
                else
                {
                    y = v * (v / y);
                }
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }

            y = Constants.Sqrt2Pi * y * w;
            return y;
        }
        /// <summary>
        ///   Lower incomplete regularized gamma function P
        ///   (a.k.a. the incomplete Gamma function).
        /// </summary>
        /// 
        /// <remarks>
        ///   This function is equivalent to P(x) = γ(s, x) / Γ(s).
        /// </remarks>
        /// 
        public static double LowerIncomplete(double a, double x)
        {
            if (a <= 0)
                return 1.0;

            if (x <= 0)
                return 0.0;

            if (x > 1.0 && x > a)
                return 1.0 - UpperIncomplete(a, x);

            double ax = a * Math.Log(x) - x - Log(a);

            if (ax < -Constants.LogMax)
                return 0.0;

            ax = Math.Exp(ax);

            double r = a;
            double c = 1.0;
            double ans = 1.0;

            do
            {
                r += 1.0;
                c *= x / r;
                ans += c;
            } while (c / ans > Constants.DoubleEpsilon);

            return ans * ax / a;
        }
        /// <summary>
        ///   Random Gamma-distribution number generation 
        ///   based on Marsaglia's Simple Method (2000).
        /// </summary>
        /// 
        public static double Random(double d, double c)
        {
            var g = new GaussianDistribution(0,1);

            // References:
            //
            // - Marsaglia, G. A Simple Method for Generating Gamma Variables, 2000
            //

            while (true)
            {
                // 2. Generate v = (1+cx)^3 with x normal
                double x, t, v;

                do
                {
                    x = g.NextDouble();
                    t = (1.0 + c * x);
                    v = t * t * t;
                } while (v <= 0);


                // 3. Generate uniform U
                double U = Numerics.Random.NextDouble();

                // 4. If U < 1-0.0331*x^4 return d*v.
                double x2 = x * x;
                if (U < 1 - 0.0331 * x2 * x2)
                    return d * v;

                // 5. If log(U) < 0.5*x^2 + d*(1-v+log(v)) return d*v.
                if (Math.Log(U) < 0.5 * x2 + d * (1.0 - v + Math.Log(v)))
                    return d * v;

                // 6. Goto step 2
            }
        }
        /// <summary>
        ///   Natural logarithm of the gamma function.
        /// </summary>
        /// 
        public static double Log(double x)
        {
            if (x == 0)
                return Double.PositiveInfinity;

            double p, q, w, z;

            double[] A =
            {
                 8.11614167470508450300E-4,
                -5.95061904284301438324E-4,
                 7.93650340457716943945E-4,
                -2.77777777730099687205E-3,
                 8.33333333333331927722E-2
            };

            double[] B =
            {
                -1.37825152569120859100E3,
                -3.88016315134637840924E4,
                -3.31612992738871184744E5,
                -1.16237097492762307383E6,
                -1.72173700820839662146E6,
                -8.53555664245765465627E5
            };

            double[] C =
            {
                -3.51815701436523470549E2,
                -1.70642106651881159223E4,
                -2.20528590553854454839E5,
                -1.13933444367982507207E6,
                -2.53252307177582951285E6,
                -2.01889141433532773231E6
            };

            if (x < -34.0)
            {
                q = -x;
                w = Log(q);
                p = Math.Floor(q);

                if (p == q)
                    throw new OverflowException();

                z = q - p;
                if (z > 0.5)
                {
                    p += 1.0;
                    z = p - q;
                }
                z = q * Math.Sin(Math.PI * z);

                if (z == 0.0)
                    throw new OverflowException();

                z = Constants.LnPi - Math.Log(z) - w;
                return z;
            }

            if (x < 13.0)
            {
                z = 1.0;
                while (x >= 3.0)
                {
                    x -= 1.0;
                    z *= x;
                }
                while (x < 2.0)
                {
                    if (x == 0.0)
                        throw new OverflowException();

                    z /= x;
                    x += 1.0;
                }

                if (z < 0.0)
                    z = -z;

                if (x == 2.0)
                    return Math.Log(z);

                x -= 2.0;

                p = x * Functions.Polevl(x, B, 5) / Functions.P1evl(x, C, 6);

                return (Math.Log(z) + p);
            }

            if (x > 2.556348e305)
                throw new OverflowException();

            q = (x - 0.5) * Math.Log(x) - x + 0.91893853320467274178;

            if (x > 1.0e8)
                return (q);

            p = 1.0 / (x * x);

            if (x >= 1000.0)
            {
                q += ((7.9365079365079365079365e-4 * p
                    - 2.7777777777777777777778e-3) * p
                    + 0.0833333333333333333333) / x;
            }
            else
            {
                q += Functions.Polevl(p, A, 4) / x;
            }

            return q;
        }

        /// <summary>
        ///   Upper incomplete regularized Gamma function Q
        ///   (a.k.a the incomplete complemented Gamma function)
        /// </summary>
        /// 
        /// <remarks>
        ///   This function is equivalent to Q(x) = Γ(s, x) / Γ(s).
        /// </remarks>
        /// 
        public static double UpperIncomplete(double a, double x)
        {
            const double big = 4.503599627370496e15;
            const double biginv = 2.22044604925031308085e-16;
            double ans, ax, c, yc, r, t, y, z;
            double pk, pkm1, pkm2, qk, qkm1, qkm2;

            if (x <= 0 || a <= 0)
                return 1.0;

            if (x < 1.0 || x < a)
                return 1.0 - LowerIncomplete(a, x);

            if (Double.IsPositiveInfinity(x))
                return 0;

            ax = a * Math.Log(x) - x - Log(a);

            if (ax < -Constants.LogMax)
                return 0.0;

            ax = Math.Exp(ax);

            // continued fraction
            y = 1.0 - a;
            z = x + y + 1.0;
            c = 0.0;
            pkm2 = 1.0;
            qkm2 = x;
            pkm1 = x + 1.0;
            qkm1 = z * x;
            ans = pkm1 / qkm1;

            do
            {
                c += 1.0;
                y += 1.0;
                z += 2.0;
                yc = y * c;
                pk = pkm1 * z - pkm2 * yc;
                qk = qkm1 * z - qkm2 * yc;
                if (qk != 0)
                {
                    r = pk / qk;
                    t = Math.Abs((ans - r) / r);
                    ans = r;
                }
                else
                    t = 1.0;

                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if (Math.Abs(pk) > big)
                {
                    pkm2 *= biginv;
                    pkm1 *= biginv;
                    qkm2 *= biginv;
                    qkm1 *= biginv;
                }
            } while (t > Constants.DoubleEpsilon);

            return ans * ax;
        }

        /// <summary>
        ///   Gamma function of the specified value.
        /// </summary>
        /// 
        public static double Function(double x)
        {
            double[] P =
            {
                1.60119522476751861407E-4,
                1.19135147006586384913E-3,
                1.04213797561761569935E-2,
                4.76367800457137231464E-2,
                2.07448227648435975150E-1,
                4.94214826801497100753E-1,
                9.99999999999999996796E-1
            };

            double[] Q =
            {
               -2.31581873324120129819E-5,
                5.39605580493303397842E-4,
               -4.45641913851797240494E-3,
                1.18139785222060435552E-2,
                3.58236398605498653373E-2,
               -2.34591795718243348568E-1,
                7.14304917030273074085E-2,
                1.00000000000000000320E0
            };

            double p, z;

            double q = Math.Abs(x);

            if (q > 33.0)
            {
                if (x < 0.0)
                {
                    p = Math.Floor(q);

                    if (p == q)
                        throw new OverflowException();

                    z = q - p;
                    if (z > 0.5)
                    {
                        p += 1.0;
                        z = q - p;
                    }
                    z = q * Math.Sin(Math.PI * z);

                    if (z == 0.0)
                        throw new OverflowException();

                    z = Math.Abs(z);
                    z = Math.PI / (z * Stirling(q));

                    return -z;
                }
                else
                {
                    return Stirling(x);
                }
            }

            z = 1.0;
            while (x >= 3.0)
            {
                x -= 1.0;
                z *= x;
            }

            while (x < 0.0)
            {
                if (x == 0.0)
                {
                    throw new ArithmeticException();
                }
                else if (x > -1.0E-9)
                {
                    return (z / ((1.0 + 0.5772156649015329 * x) * x));
                }
                z /= x;
                x += 1.0;
            }

            while (x < 2.0)
            {
                if (x == 0.0)
                {
                    throw new ArithmeticException();
                }
                else if (x < 1.0E-9)
                {
                    return (z / ((1.0 + 0.5772156649015329 * x) * x));
                }

                z /= x;
                x += 1.0;
            }

            if ((x == 2.0) || (x == 3.0))
                return z;

            x -= 2.0;
            p = Functions.Polevl(x, P, 6);
            q = Functions.Polevl(x, Q, 7);
            return z * p / q;

        }
    }
}
