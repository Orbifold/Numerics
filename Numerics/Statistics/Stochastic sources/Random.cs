using System;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Static wrapper of the <see cref="System.Random"/> class.
    /// </summary>
    public static class Random
    {
        private static readonly System.Random rand = new System.Random(Environment.TickCount);

        /// <summary>
        /// Returns a random double.
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
        {
            return rand.NextDouble();
        }

        /// <summary>
        /// Returns a random integer.
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return rand.Next();
        }
    }
}