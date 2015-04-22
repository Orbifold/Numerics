using System;


namespace Orbifold.Numerics
{
	/// <summary>
	/// Defines an object which can be cloned.
	/// </summary>
	public interface ICloneable
	{
		/// <summary>
		/// Returns a (shallow) clone of this object.
		/// </summary>
		object Clone();
	}

}