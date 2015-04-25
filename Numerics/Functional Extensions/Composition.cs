using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Composition of functionals extensions.
	/// </summary>
	public static class Composition
	{
		/// <summary>
		/// Composes two functions together.
		/// </summary>
		/// <remarks>This is the pipe-forward |> operation in F#.</remarks>
		/// <typeparam name="TDomain">The type of the source.</typeparam>
		/// <typeparam name="TIntermediate">The type of the intermediate result.</typeparam>
		/// <typeparam name="TTarget">The type of the end result.</typeparam>
		/// <param name="fun1">The first function.</param>
		/// <param name="fun2">The second function.</param>
		/// <returns>The function composition.</returns>
		public static Func<TDomain, TTarget> Compose<TDomain, TIntermediate, TTarget>(this Func<TDomain, TIntermediate> fun1, Func<TIntermediate, TTarget> fun2)
		{
			return sourceParam => fun2(fun1(sourceParam));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TEndResult> Compose<TDomain1, TDomain2, TIntermediateResult, TEndResult>(this Func<TDomain1, TDomain2, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2) => func2(func1(p1, p2));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3) => func2(func1(p1, p2, p3));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4) => func2(func1(p1, p2, p3, p4));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4, p5) => func2(func1(p1, p2, p3, p4, p5));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4, p5, p6) => func2(func1(p1, p2, p3, p4, p5, p6));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func2(func1(p1, p2, p3, p4, p5, p6, p7));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func2(func1(p1, p2, p3, p4, p5, p6, p7, p8));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TSource, TEndResult> Compose<TSource, TIR1, TIR2, TEndResult>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return sourceParam => func3(func2(func1(sourceParam)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TEndResult> Compose<TDomain1, TDomain2, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2) => func3(func2(func1(p1, p2)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3) => func3(func2(func1(p1, p2, p3)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4) => func3(func2(func1(p1, p2, p3, p4)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4, p5) => func3(func2(func1(p1, p2, p3, p4, p5)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4, p5, p6) => func3(func2(func1(p1, p2, p3, p4, p5, p6)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func3(func2(func1(p1, p2, p3, p4, p5, p6, p7)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8)));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TEndResult> func3)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TSource, TEndResult> Compose<TSource, TIR1, TIR2, TIR3, TEndResult>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return sourceParam => func4(func3(func2(func1(sourceParam))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TEndResult> Compose<TDomain1, TDomain2, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2) => func4(func3(func2(func1(p1, p2))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3) => func4(func3(func2(func1(p1, p2, p3))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4) => func4(func3(func2(func1(p1, p2, p3, p4))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4, p5) => func4(func3(func2(func1(p1, p2, p3, p4, p5))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4, p5, p6) => func4(func3(func2(func1(p1, p2, p3, p4, p5, p6))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8))));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2, TIR3, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TEndResult> func4)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TSource, TEndResult> Compose<TSource, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return sourceParam => func5(func4(func3(func2(func1(sourceParam)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TEndResult> Compose<TDomain1, TDomain2, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2) => func5(func4(func3(func2(func1(p1, p2)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3) => func5(func4(func3(func2(func1(p1, p2, p3)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4) => func5(func4(func3(func2(func1(p1, p2, p3, p4)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4, p5) => func5(func4(func3(func2(func1(p1, p2, p3, p4, p5)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4, p5, p6) => func5(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => func5(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => func5(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8)))));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="func5">The fifth functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2, TIR3, TIR4, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Func<TIR4, TEndResult> func5)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => func5(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TSource> Compose<TSource, TIntermediateResult>(
		  this Func<TSource, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return sourceParam => action(func1(sourceParam));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2> Compose<TDomain1, TDomain2, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2) => action(func1(p1, p2));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3> Compose<TDomain1, TDomain2, TDomain3, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3) => action(func1(p1, p2, p3));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4) => action(func1(p1, p2, p3, p4));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4, p5) => action(func1(p1, p2, p3, p4, p5));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4, p5, p6) => action(func1(p1, p2, p3, p4, p5, p6));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => action(func1(p1, p2, p3, p4, p5, p6, p7));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => action(func1(p1, p2, p3, p4, p5, p6, p7, p8));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIntermediateResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIntermediateResult> func1, Action<TIntermediateResult> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => action(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TSource> Compose<TSource, TIR1, TIR2>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return sourceParam => action(func2(func1(sourceParam)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2> Compose<TDomain1, TDomain2, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2) => action(func2(func1(p1, p2)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3) => action(func2(func1(p1, p2, p3)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4) => action(func2(func1(p1, p2, p3, p4)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4, p5) => action(func2(func1(p1, p2, p3, p4, p5)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4, p5, p6) => action(func2(func1(p1, p2, p3, p4, p5, p6)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => action(func2(func1(p1, p2, p3, p4, p5, p6, p7)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => action(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8)));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Action<TIR2> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => action(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9)));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TSource> Compose<TSource, TIR1, TIR2, TIR3>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return sourceParam => action(func3(func2(func1(sourceParam))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2> Compose<TDomain1, TDomain2, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2) => action(func3(func2(func1(p1, p2))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3) => action(func3(func2(func1(p1, p2, p3))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4) => action(func3(func2(func1(p1, p2, p3, p4))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4, p5) => action(func3(func2(func1(p1, p2, p3, p4, p5))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4, p5, p6) => action(func3(func2(func1(p1, p2, p3, p4, p5, p6))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => action(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => action(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8))));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2, TIR3>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Action<TIR3> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => action(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TSource> Compose<TSource, TIR1, TIR2, TIR3, TIR4>(
		  Func<TSource, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return sourceParam => action(func4(func3(func2(func1(sourceParam)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2> Compose<TDomain1, TDomain2, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2) => action(func4(func3(func2(func1(p1, p2)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3> Compose<TDomain1, TDomain2, TDomain3, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3) => action(func4(func3(func2(func1(p1, p2, p3)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4) => action(func4(func3(func2(func1(p1, p2, p3, p4)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4, p5) => action(func4(func3(func2(func1(p1, p2, p3, p4, p5)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4, p5, p6) => action(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7) => action(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7)))));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TDomain7">The data type of the seventh parameter.</typeparam>
		/// <typeparam name="TDomain8">The data type of the eigth parameter.</typeparam>
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8) => action(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8)))));
		}

		/// <summary>
		/// Composition of the given functionals.
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
		/// <typeparam name="TIR1">The type of the I r1.</typeparam>
		/// <typeparam name="TIR2">The type of the I r2.</typeparam>
		/// <typeparam name="TIR3">The type of the I r3.</typeparam>
		/// <typeparam name="TIR4">The type of the I r4.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <param name="func3">The third functional.</param>
		/// <param name="func4">The fourth functional.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static Action<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1, TIR2, TIR3, TIR4>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TDomain7, TDomain8, TDomain9, TIR1> func1, Func<TIR1, TIR2> func2, Func<TIR2, TIR3> func3, Func<TIR3, TIR4> func4, Action<TIR4> action)
		{
			return (p1, p2, p3, p4, p5, p6, p7, p8, p9) => action(func4(func3(func2(func1(p1, p2, p3, p4, p5, p6, p7, p8, p9)))));
		}
	}
}