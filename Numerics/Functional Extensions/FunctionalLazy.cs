using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A lazy construct geared towards functional programming.
	/// </summary>
	/// <typeparam name="T">The data type of the list.</typeparam>
	public sealed class FunctionalLazy<T>
	{
		private readonly object forceLock = new object();

		private readonly Func<T> function;

		private Exception exception;

		private bool forced;

		private T value;

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="function">The function.</param>
		public FunctionalLazy(Func<T> function)
		{
			this.function = function;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FunctionalLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public FunctionalLazy(T value)
		{
			this.value = value;
		}

		/// <summary>
		/// Gets the exception which was raised during instantiation, if any.
		/// </summary>
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		/// <summary>
		/// Gets whether the instantiation raised an exception.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has exception; otherwise, <c>false</c>.
		/// </value>
		public bool HasException
		{
			get
			{
				return this.exception != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is forced.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is forced; otherwise, <c>false</c>.
		/// </value>
		public bool IsForced
		{
			get
			{
				return this.forced;
			}
		}

		/// <summary>
		/// Gets the value and induces an instantiation.
		/// </summary>
		public T Value
		{
			get
			{
				return this.Force();
			}
		}

		/// <summary>
		/// Forces an instantiation.
		/// </summary>
		/// <returns></returns>
		public T Force()
		{
			lock (this.forceLock)
			{
				return this.UnsynchronizedForce();
			}
		}

		/// <summary>
		/// Unsynchronizeds the force.
		/// </summary>
		/// <returns></returns>
		public T UnsynchronizedForce()
		{
			if (this.exception != null)
			{
				throw this.exception;
			}
			if (this.function != null && !this.forced)
			{
				try
				{
					this.value = this.function();
					this.forced = true;
				}
				catch (Exception ex)
				{
					this.exception = ex;
					throw;
				}
			}
			return this.value;
		}
	}
}