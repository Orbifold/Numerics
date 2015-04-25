namespace Orbifold.Numerics
{
	/// <summary>
	/// A logical value specifying whether to switch to straight-line depreciation when depreciation is greater than the declining balance calculation.
	/// </summary>
	public enum VdbSwitch
	{
		/// <summary>
		/// Switches to straight-line depreciation when depreciation is greater than the declining balance calculation.
		/// </summary>
		SwitchToStraightLine = 0,

		/// <summary>
		/// Does not switch to straight-line depreciation even when the depreciation is greater than the declining balance calculation.
		/// </summary>
		DontSwitchToStraightLine = 1
	}
}