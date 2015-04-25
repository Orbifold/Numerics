using System;
using System.Collections.Generic;
using System.Reflection;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Memoization extensions.
    /// </summary>
    /// <remarks>See http://en.wikipedia.org/wiki/Memoization. </remarks>
    public static class Memoization
    {
#if !PCL
        /// <summary>
		/// Memoizes the specified functional.
		/// </summary>
		/// <typeparam name="TDomain">The data type of the domain. </typeparam>
		/// <typeparam name="TRange">The data type of the range.</typeparam>
		/// <param name="f">The f.</param>
		/// <returns></returns>
		public static Func<TDomain, TRange> Memoize<TDomain, TRange>(this Func<TDomain, TRange> f)
		{
			return Memoize(f, GetKeyForMethod(f.Method));
		}
#endif
        /// <summary>
        /// Describes a memoization memory.
        /// </summary>
        /// <typeparam name="TDomain">The type of the domain.</typeparam>
        /// <typeparam name="TRange">The type of the range.</typeparam>
        private interface IMemory<in TDomain, TRange>
        {
            /// <summary>
            /// Determines whether there is already a result for the given value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <returns>
            ///   <c>true</c> if [has result for] [the specified val]; otherwise, <c>false</c>.
            /// </returns>
            bool HasResultFor(TDomain val);

            /// <summary>
            /// Remembers the specified value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <param name="result">The result.</param>
            void Remember(TDomain val, TRange result);

            /// <summary>
            /// Returns the result for the given value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <returns></returns>
            TRange ResultFor(TDomain val);
        }



        /// <summary>
        /// Memoizes the specified functional and stores the values at the speicified key.
        /// </summary>
        /// <typeparam name="TDomain">The data type of the domain.</typeparam>
        /// <typeparam name="TRange">The data type of the range.</typeparam>
        /// <param name="f">The f.</param>
        /// <param name="memoryKey">The memory key to use when storing the values.</param>
        /// <returns></returns>
        public static Func<TDomain, TRange> Memoize<TDomain, TRange>(this Func<TDomain, TRange> f, string memoryKey)
        {
            return value =>
                {
                    var memory = Memoizer<TDomain, TRange>.GetMemory(memoryKey);
                    if (!memory.HasResultFor(value))
                    {
                        memory.Remember(value, f(value));
                    }
                    return memory.ResultFor(value);
                };
        }

        /// <summary>
        /// Gets a memory keyed using the methodinfo.
        /// </summary>
        /// <param name="info">The info of the functional.</param>
        /// <returns></returns>
        private static string GetKeyForMethod(MethodInfo info)
        {
            if (info.DeclaringType != null)
            {
                return string.Format("{0}+{1}", info.DeclaringType.FullName, info.Name);
            }
            throw new Exception("The declaring type could not be found for the given method, use a key memoization instead.");
        }

        /// <summary>
        /// Memoization of functionals.
        /// </summary>
        /// <remarks>See http://en.wikipedia.org/wiki/Memoization .</remarks>
        /// <typeparam name="TDomain">The type of the domain.</typeparam>
        /// <typeparam name="TRange">The type of the range.</typeparam>
        private static class Memoizer<TDomain, TRange>
        {
            private static readonly object MemoryListLock = new object();

            private static Dictionary<string, IMemory<TDomain, TRange>> memories;

            private static Dictionary<string, IMemory<TDomain, TRange>> Memories
            {
                get
                {
                    lock (MemoryListLock)
                    {
                        return memories ?? (memories = new Dictionary<string, IMemory<TDomain, TRange>>());
                    }
                }
            }

            /// <summary>
            /// Gets the memory for the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns></returns>
            public static IMemory<TDomain, TRange> GetMemory(string key)
            {
                return !Memories.ContainsKey(key) ? CreateMemory(key) : Memories[key];
            }

            /// <summary>
            /// Creates the memory for the key.
            /// </summary>
            /// <typeparam name="T">The data type.</typeparam>
            /// <param name="key">The key.</param>
            /// <returns></returns>
            private static T CreateMemory<T>(string key) where T : IMemory<TDomain, TRange>, new()
            {
                lock (MemoryListLock)
                {
                    if (Memories.ContainsKey(key))
                    {
                        throw new InvalidOperationException("The memory key '" + key + "' is already in use.");
                    }
                    var memory = new T();
                    memories[key] = memory;
                    return memory;
                }
            }

            /// <summary>
            /// Creates a memory and stores it at the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns></returns>
            private static IMemory<TDomain, TRange> CreateMemory(string key)
            {
                return CreateMemory<Memory<TDomain, TRange>>(key);
            }
        }

        /// <summary>
        /// The memoization memory.
        /// </summary>
        /// <typeparam name="TDomain">The type of the domain.</typeparam>
        /// <typeparam name="TRange">The type of the range.</typeparam>
        private sealed class Memory<TDomain, TRange> : IMemory<TDomain, TRange>
        {
            private readonly Dictionary<TDomain, TRange> storage = new Dictionary<TDomain, TRange>();

            /// <summary>
            /// Determines whether there is already a result for the given value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <returns>
            ///   <c>true</c> if [has result for] [the specified val]; otherwise, <c>false</c>.
            /// </returns>
            public bool HasResultFor(TDomain val)
            {
                return this.storage.ContainsKey(val);
            }

            /// <summary>
            /// Remembers the specified value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <param name="result">The result.</param>
            public void Remember(TDomain val, TRange result)
            {
                this.storage[val] = result;
            }

            /// <summary>
            /// Returns the result for the given value.
            /// </summary>
            /// <param name="val">The val.</param>
            /// <returns></returns>
            public TRange ResultFor(TDomain val)
            {
                return this.storage[val];
            }
        }
    }
}