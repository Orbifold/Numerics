namespace Orbifold.Numerics
{
	/// <summary>
	/// Defines a type of generated person data, see <see cref="DataGenerator.RandomPersonName"/>.
	/// </summary>
	public enum PersonDataType
	{
		/// <summary>
		/// A male first name.
		/// </summary>
		MaleFirstName,

		/// <summary>
		/// A female first name.
		/// </summary>
		FemalFirstName,

		/// <summary>
		/// A random male or female first name.
		/// </summary>
		FirstName,

		/// <summary>
		/// A random family name.
		/// </summary>
		FamilyName,

		/// <summary>
		/// A random male or female first name with family name.
		/// </summary>
		FullName,

		/// <summary>
		/// A random male or female first name with middle initial and family name.
		/// </summary>
		FullNameWithMiddleInitial
	}
}