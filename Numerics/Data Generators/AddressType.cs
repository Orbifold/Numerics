using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A flag enumeration of option when generating a random address.
	/// </summary>
	/// <seealso cref="DataGenerator.RandomAddress"/>
	[Flags]
	public enum AddressType
	{
		/// <summary>
		/// Include a random company name.
		/// </summary>
		CompanyName,

		/// <summary>
		/// Include a random state name.
		/// </summary>
		StateName,

		/// <summary>
		/// Include a random country name.
		/// </summary>
		CountryName
	}
}