using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Extensions related to partial functional applications.
	/// </summary>
	public static class PartialFunction
	{
		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The type of the 1.</typeparam>
		/// <typeparam name="TDomain2">The type of the 2.</typeparam>
		/// <typeparam name="TRange">The type of the R.</typeparam>
		/// <param name="func">The functional.</param>
		/// <param name="x">The value on which the functional will be partially applied.</param>
		/// <returns></returns>
		public static Func<TDomain2, TRange> Partial<TDomain1, TDomain2, TRange>(Func<TDomain1, TDomain2, TRange> func, TDomain1 x)
		{
			return y => func(x, y);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TRange> Partial<TDomain1, TDomain2, TDomain3, TRange>(Func<TDomain1, TDomain2, TDomain3, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3) => function(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TRange> Partial<TDomain1, TDomain2, TDomain3, TRange>(Func<TDomain1, TDomain2, TDomain3, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return arg3 => function(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4) => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4) => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return arg4 => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return arg5 => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Func<TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return arg6 => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TDomain6, TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TDomain6, TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Func<TDomain6, TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Func<TDomain7, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return arg7 => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TDomain6, TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Func<TDomain6, TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Func<TDomain7, TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return (arg7, arg8) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <returns></returns>
		public static Func<TDomain8, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7)
		{
			return arg8 => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Func<TDomain6, TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Func<TDomain7, TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return (arg7, arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <returns></returns>
		public static Func<TDomain8, TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7)
		{
			return (arg8, arg9) => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <param name="arg8">The eigth argument.</param>
		/// <returns></returns>
		public static Func<TDomain9, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7, TDomain8 arg8)
		{
			return arg9 => function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2> Partial<TDomain1, TDomain2>(Action<TDomain1, TDomain2> action, TDomain1 arg1)
		{
			return arg2 => action(arg1, arg2);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3> Partial<TDomain1, TDomain2, TDomain3>(Action<TDomain1, TDomain2, TDomain3> action, TDomain1 arg1)
		{
			return (arg2, arg3) => action(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3> Partial<TDomain1, TDomain2, TDomain3>(Action<TDomain1, TDomain2, TDomain3> action, TDomain1 arg1, TDomain2 arg2)
		{
			return arg3 => action(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4> Partial<TDomain1, TDomain2, TDomain3, TDomain4>(Action<TDomain1, TDomain2, TDomain3, TDomain4> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4) => action(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4> Partial<TDomain1, TDomain2, TDomain3, TDomain4>(Action<TDomain1, TDomain2, TDomain3, TDomain4> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4) => action(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4> Partial<TDomain1, TDomain2, TDomain3, TDomain4>(Action<TDomain1, TDomain2, TDomain3, TDomain4> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return arg4 => action(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4, TDomain5> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5) => action(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4, TDomain5> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5) => action(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4, TDomain5> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5) => action(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Action<TDomain5> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return arg5 => action(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6) => action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4, TDomain5, TDomain6> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6) => action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4, TDomain5, TDomain6> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6) => action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Action<TDomain5, TDomain6> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6) => action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Action<TDomain6> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return arg6 => action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4, TDomain5, TDomain6, TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Action<TDomain5, TDomain6, TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Action<TDomain6, TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Action<TDomain7> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return arg7 => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Action<TDomain5, TDomain6, TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Action<TDomain6, TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Action<TDomain7, TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return (arg7, arg8) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <returns></returns>
		public static Action<TDomain8> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7)
		{
			return arg8 => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Action<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Action<TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6, arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Action<TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6, arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Action<TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return (arg5, arg6, arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <returns></returns>
		public static Action<TDomain6, TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5)
		{
			return (arg6, arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <returns></returns>
		public static Action<TDomain7, TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6)
		{
			return (arg7, arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <returns></returns>
		public static Action<TDomain8, TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7)
		{
			return (arg8, arg9) => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TDomain9">The data type of the ninth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <param name="arg5">The fifth argument.</param>
		/// <param name="arg6">The sixth argument.</param>
		/// <param name="arg7">The seventh argument.</param>
		/// <param name="arg8">The eigth argument.</param>
		/// <returns></returns>
		public static Action<TDomain9> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4, TDomain5 arg5, TDomain6 arg6, TDomain7 arg7, TDomain8 arg8)
		{
			return arg9 => action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}
	}
}