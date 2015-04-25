using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Currying extension methods.
	/// </summary>
	/// <remarks>See http://en.wikipedia.org/wiki/Currying .</remarks>
	public static class Currying
	{
		/// <summary>
		/// Two-parameter currying to a one-parameter functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range.</typeparam>
		/// <param name="func">The functional to curry.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, TRange>> Curry<TDomain1, TDomain2, TRange>(this Func<TDomain1, TDomain2, TRange> func)
		{
			return x => y => func(x, y);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the third parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, TRange>>> Curry<TDomain1, TDomain2, TDomain3, TRange>(this Func<TDomain1, TDomain2, TDomain3, TRange> func)
		{
			return p1 => p2 => p3 => func(p1, p2, p3);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, TRange>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> func)
		{
			return p1 => p2 => p3 => p4 => func(p1, p2, p3, p4);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, TRange>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> func)
		{
			return p1 => p2 => p3 => p4 => p5 => func(p1, p2, p3, p4, p5);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, TRange>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> func)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => func(p1, p2, p3, p4, p5, p6);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, TRange>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> func)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => func(p1, p2, p3, p4, p5, p6, p7);
		}

		/// <summary>
		/// Curries the given function.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, TRange>>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> func)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => p8 => func(p1, p2, p3, p4, p5, p6, p7, p8);
		}

		/// <summary>
		/// Curries the given function.
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
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being curried.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, Func<TDomain9, TRange>>>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> func)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => p8 => p9 => func(p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Action<TDomain2>> Curry<TDomain1, TDomain2>(this Action<TDomain1, TDomain2> action)
		{
			return p1 => p2 => action(p1, p2);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Action<TDomain3>>> Curry<TDomain1, TDomain2, TDomain3>(this Action<TDomain1, TDomain2, TDomain3> action)
		{
			return p1 => p2 => p3 => action(p1, p2, p3);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Action<TDomain4>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4>(this Action<TDomain1, TDomain2, TDomain3, TDomain4> action)
		{
			return p1 => p2 => p3 => p4 => action(p1, p2, p3, p4);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Action<TDomain5>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(this Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> action)
		{
			return p1 => p2 => p3 => p4 => p5 => action(p1, p2, p3, p4, p5);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Action<TDomain6>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(this Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> action)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => action(p1, p2, p3, p4, p5, p6);
		}

		/// <summary>
		/// Curries the specified functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Action<TDomain7>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(this Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> action)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => action(p1, p2, p3, p4, p5, p6, p7);
		}

		/// <summary>
		/// Curries the specified functional.
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
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Action<TDomain8>>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(this Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> action)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => p8 => action(p1, p2, p3, p4, p5, p6, p7, p8);
		}

		/// <summary>
		/// Curries the specified functional.
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
		/// <returns>The curried functional.</returns>
		public static Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, Action<TDomain9>>>>>>>>> Curry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(this Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> action)
		{
			return p1 => p2 => p3 => p4 => p5 => p6 => p7 => p8 => p9 => action(p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TRange> Uncurry<TDomain1, TDomain2, TRange>(this Func<TDomain1, Func<TDomain2, TRange>> func)
		{
			return (p1, p2) => func(p1)(p2);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, TRange>>> func)
		{
			return (p1, p2, p3) => func(p1)(p2)(p3);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, TRange>>>> func)
		{
			return (p1, p2, p3, p4) => func(p1)(p2)(p3)(p4);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, TRange>>>>> func)
		{
			return (p1, p2, p3, p4, p5) => func(p1)(p2)(p3)(p4)(p5);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, TRange>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6) => func(p1)(p2)(p3)(p4)(p5)(p6);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, TRange>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, TRange>>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7)(p8);
		}

		/// <summary>
		/// Uncurries the given functional.
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
		/// <typeparam name="TRange">The data type of the range parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TRange>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, Func<TDomain9, TRange>>>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7)(p8)(p9);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2> Uncurry<TDomain1, TDomain2>(this Func<TDomain1, Action<TDomain2>> func)
		{
			return (p1, p2) => func(p1)(p2);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3> Uncurry<TDomain1, TDomain2, TDomain3>(this Func<TDomain1, Func<TDomain2, Action<TDomain3>>> func)
		{
			return (p1, p2, p3) => func(p1)(p2)(p3);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Action<TDomain4>>>> func)
		{
			return (p1, p2, p3, p4) => func(p1)(p2)(p3)(p4);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Action<TDomain5>>>>> func)
		{
			return (p1, p2, p3, p4, p5) => func(p1)(p2)(p3)(p4)(p5);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Action<TDomain6>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6) => func(p1)(p2)(p3)(p4)(p5)(p6);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Action<TDomain7>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7);
		}

		/// <summary>
		/// Uncurries the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Action<TDomain8>>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7)(p8);
		}

		/// <summary>
		/// Uncurries the given functional.
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
		/// <param name="func">The functional being uncurried.</param>
		/// <returns>The uncurried functional.</returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Uncurry<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9>(this Func<TDomain1, Func<TDomain2, Func<TDomain3, Func<TDomain4, Func<TDomain5, Func<TDomain6, Func<TDomain7, Func<TDomain8, Action<TDomain9>>>>>>>>> func)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func(p1)(p2)(p3)(p4)(p5)(p6)(p7)(p8)(p9);
		}
	}
}