using System;
using System.Numerics;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Complex Fast (FFT) Implementation of the Discrete Fourier Transform (DFT).
    /// </summary>
    /// <remarks>Note that there is some arbitraryness where to put the normalization factor. It was put in the inverse transform in both approaches.</remarks>
    public class DiscreteFourierTransform
    {

        /// <summary>
        /// Plain forward discrete Fourier transformation.
        /// </summary>
        /// <remarks>Forward means that the k-space vector is returned.</remarks>
        /// <returns>Corresponding frequency-space vector.</returns>
        public static Complex[] PlainForward(Complex[] samples)
        {
            if (samples.Length == 0) return new Complex[] { };
            var w0 = -2 * Constants.Pi / samples.Length;
            var spectrum = new Complex[samples.Length];
            for (var i = 0; i < samples.Length; i++)
            {
                var sum = Complex.Zero;
                for (var n = 0; n < samples.Length; n++)
                {
                    sum += samples[n] * new Complex(Math.Cos(n * w0 * i), Math.Sin(n * w0 * i));
                }
                spectrum[i] = sum;
            }
            return spectrum;
        }

        /// <summary>
        /// Plain inverse discrete Fourier transformation.
        /// </summary>
        /// <remarks>Forward means that the k-space vector is returned.</remarks>
        public static Complex[] PlainInverse(Complex[] samples)
        {
            if (samples.Length == 0) return new Complex[] { };
            var w0 = 2 * Constants.Pi / samples.Length;
            var spectrum = new Complex[samples.Length];
            for (var i = 0; i < samples.Length; i++)
            {
                var sum = Complex.Zero;
                for (var n = 0; n < samples.Length; n++)
                {
                    sum += samples[n] * new Complex(Math.Cos(n * w0 * i), Math.Sin(n * w0 * i));
                }
                spectrum[i] = sum / samples.Length;
            }
            return spectrum;
        }

        /// <summary>
        /// Forward Fourier transformation.
        /// <remarks>
        /// The radix-2 decimation-in-time (DIT) FFT is the simplest and most common form of the Cooley–Tukey algorithm.
        /// Radix-2 DIT divides a DFT of size N into two interleaved DFTs (hence the name "radix-2") of size N/2 with each recursive stage.
        /// </remarks> 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Complex[] Radix2Forward(Complex[] x)
        {
            var length = x.Length;
            // 
            if (length == 1) return new[] { x[0] };

            // Cooley-Tukey FFT
            if (length % 2 != 0) throw new Exception("N is not a power of 2");

            // even Radix2Forward
            var even = new Complex[length / 2];
            for (var k = 0; k < length / 2; k++) even[k] = x[2 * k];
            var q = Radix2Forward(even);

            // odd Radix2Forward;
            var odd = even;
            for (var k = 0; k < length / 2; k++) odd[k] = x[2 * k + 1];
            var r = Radix2Forward(odd);

            // combine
            var y = new Complex[length];
            for (var k = 0; k < length / 2; k++)
            {
                var value = -2 * k * Math.PI / length;
                var wk = new Complex(Math.Cos(value), Math.Sin(value));
                y[k] = q[k] + (wk * (r[k]));
                y[k + length / 2] = q[k] - (wk * (r[k]));
            }
            return y;
        }
        /// <summary>
        /// Backward Fourier transform.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <seealso cref="Radix2Forward"/>
        public static Complex[] Radix2Inverse(Complex[] x)
        {
            var length = x.Length;
            var y = new Complex[length];

            // Cooley-Tukey FFT
            if (length % 2 != 0) throw new Exception("N is not a power of 2");

            //conjugate
            for (var i = 0; i < length; i++) y[i] = new Complex(x[i].Real, -x[i].Imaginary);

            // compute forward FFT
            y = Radix2Forward(y);

            // take conjugate again
            for (var i = 0; i < length; i++) y[i] = new Complex(y[i].Real, -y[i].Imaginary);

            // divide by N
            for (var i = 0; i < length; i++) y[i] = y[i] / length;

            return y;
        }
        public static Complex[,] Radix2Forward2D(Complex[,] input)
        {
            var output = (Complex[,])input.Clone();
            // Rows first:
            var x = new Complex[output.GetLength(1)];
            for (var h = 0; h < output.GetLength(0); h++)
            {
                for (var i = 0; i < output.GetLength(1); i++)
                {
                    x[i] = output[h, i];
                }
                x = Radix2Forward(x);
                for (var i = 0; i < output.GetLength(1); i++)
                {
                    output[h, i] = x[i];
                }
            }
            //Columns last
            var y = new Complex[output.GetLength(0)];
            for (var h = 0; h < output.GetLength(1); h++)
            {
                for (var i = 0; i < output.GetLength(0); i++)
                {
                    y[i] = output[i, h];
                }
                y = Radix2Forward(y);
                for (var i = 0; i < output.GetLength(0); i++)
                {
                    output[i, h] = y[i];
                }
            }
            return output;
        }
        public static Complex[,] Radix2Inverse2D(Complex[,] input)
        {
            var output = (Complex[,])input.Clone();
            // Rows first:
            var x = new Complex[output.GetLength(1)];
            for (var h = 0; h < output.GetLength(0); h++)
            {
                for (var i = 0; i < output.GetLength(1); i++) x[i] = output[h, i];
                x = Radix2Inverse(x);
                for (var i = 0; i < output.GetLength(1); i++) output[h, i] = x[i];
            }
            //Columns last
            var y = new Complex[output.GetLength(0)];
            for (var h = 0; h < output.GetLength(1); h++)
            {
                for (var i = 0; i < output.GetLength(0); i++) y[i] = output[i, h];
                y = Radix2Inverse(y);
                for (var i = 0; i < output.GetLength(0); i++) output[i, h] = y[i];
            }
            return output;
        }

    }
}