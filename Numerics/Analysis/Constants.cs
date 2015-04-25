namespace Orbifold.Numerics
{
	/// <summary>
	/// Mathematical, physical and framework related constants.
	/// </summary>
	public static class Constants
	{
		/// <summary>
		/// The epsilon aka infinitesimal; a small value to compare floating numbers.
		/// </summary>
		public const double Epsilon = 1E-8;

		/// <summary>
		/// The Catalan number.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Catalan_number .</remarks>
		public const double Catalan = 0.9159655941772190150546035149323841107741493742816721342664981196217630197762547694794d;

		/// <summary>
		/// The Euler constant.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/E_%28mathematical_constant%29 .</remarks>
		public const double E = 2.7182818284590452353602874713526624977572470937000d;

		/// <summary>
		/// The Euler-Mascheroni constant.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Euler_constant .</remarks>
		public const double EulerGamma = 0.5772156649015328606065120900824024310421593359399235988057672348849d;

		/// <summary>
		/// Glaisher-Kinkelin constant.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Glaisher%E2%80%93Kinkelin_constant .</remarks>
		public const double Glaisher = 1.2824271291006226368753425688697917277676889273250011920637400217404063088588264611297d;

		/// <summary>
		/// The golden ratio; (1+Sqrt[5])/2.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Golden_ratio .</remarks>
		public const double GoldenRatio = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072d;

		/// <summary>
		/// The inverse of the Euler constant; 1/[e].
		/// </summary>
		public const double InverseE = 0.36787944117144232159552377016146086744581113103176d;

		/// <summary>
		/// The inverse of Pi; 1/[Pi].
		/// </summary>
		public const double InversePi = 0.31830988618379067153776752674502872406891929148091d;

		/// <summary>
		/// The inverse of the square root of twice Pi; 1/Sqrt[2*Pi].
		/// </summary>
		public const double InvSqrt2Pi = 0.39894228040143267793994605993438186847585863116492d;

		/// <summary>
		/// The inverse of the square root of Pi; 1/Sqrt[Pi].
		/// </summary>
		public const double InvSqrtPi = 0.56418958354775628694807945156077258584405062932899d;

		/// <summary>
		/// The Khinchin constant.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Khinchin_constant .</remarks>
		public const double Khinchin = 2.6854520010653064453097148354817956938203822939944629530511523455572188595371520028011d;

		/// <summary>
		/// 2^(-53)
		/// </summary>
		public static readonly double RelativeAccuracy = Functions.EpsilonOf(1.0);

		/// <summary>
		/// 2^(-1074)
		/// </summary>
		public const double SmallestNumberGreaterThanZero = double.Epsilon;

		/// <summary>
		/// The logorithm of <c>E</c> in base 2; log[2](e).
		/// </summary>
		public const double Log2E = 1.4426950408889634073599246810018921374266459541530d;

		/// <summary>
		/// The logorithm of 2 in base <c>e</c>; log[e](2).
		/// </summary>
		public const double Ln2 = 0.69314718055994530941723212145817656807550013436026d;

		/// <summary>
		/// The logorithm of <c>10</c> in base e; log[e](10).
		/// </summary>
		public const double Ln10 = 2.3025850929940456840179914546843642076011014886288d;

		/// <summary>
		/// The logorithm of <c>Pi</c> in base e; log[e](10).
		/// </summary>
		public const double LnPi = 1.1447298858494001741434273513530587116472948129153d;

		/// <summary>
		/// The well-know Pi constants.
		/// </summary>
		/// <remarks>This one has a higher precision than the .Net system one.</remarks>
		public const double Pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940813d;

		/// <summary>
		/// Pi over 180; Pi/180.
		/// </summary>
		/// <seealso cref="Trigonometry.DegreesToRadians"/>
		/// <seealso cref="Trigonometry.RadiansToDegrees"/>
		public const double PiOver180 = 0.017453292519943295769236907684886127134428718885417d;

		/// <summary>
		/// Pi over 2; [Pi]/2.
		/// </summary>
		public const double PiOver2 = 1.5707963267948966192313216916397514420985846996876d;

		/// <summary>
		/// Pi over 4; [Pi]/4.
		/// </summary>
		public const double PiOver4 = 0.78539816339744830961566084581987572104929234984378d;

		/// <summary>
		/// The square root of 1/2.
		/// </summary>
		public const double Sqrt1Over2 = 0.70710678118654752440084436210484903928483593768845d;

		/// <summary>The square root of two.</summary>
		public const double Sqrt2 = 1.4142135623730950488016887242096980785696718753769d;

		/// <summary>
		/// The square root of two time Pi; Sqrt[2*Pi].
		/// </summary>
		public const double Sqrt2Pi = 2.5066282746310005024157652848110452530069867406099d;

		/// <summary>The square root of 3, divided by two; Sqrt[3]/2.
		/// </summary>
		public const double Sqrt3DividedBy2 = 0.86602540378443864676372317075293618347140262690520d;

		/// <summary>
		/// The square root of Pi; Sqrt[Pi].
		/// </summary>
		public const double SqrtPi = 1.7724538509055160272981674833411451827975494561224d;

		/// <summary>
		/// The speed of light (in vacuum) in [m s^-1].
		/// See http://en.wikipedia.org/wiki/Speed_of_light .
		/// </summary>
		public const double SpeedOfLight = 2.99792458e8;

		/// <summary>
		/// The Planck's constant in [J s = m^2 kg s^-1].
		/// See http://en.wikipedia.org/wiki/Planck_constant .
		/// </summary>
		public const double PlancksConstant = 6.62606896e-34;

		/// <summary>
		/// The Planck length in [h_bar/(m_p*c_0)].
		/// See http://en.wikipedia.org/wiki/Plancks_length .
		/// </summary>
		public const double PlancksLength = 1.616253e-35;

		/// <summary>
		/// The Newtonian constant in [m^3 kg^-1 s^-2].
		/// See http://en.wikipedia.org/wiki/Gravitational_Constant .
		/// </summary>
		public const double GravitationalConstant = 6.67429e-11;

		/// <summary>
		/// The magnetic permeability (in vacuum) in [kg m s^-2 A^-2].
		/// See http://en.wikipedia.org/wiki/Magnetic_Permeability .
		/// </summary>
		public const double MagneticPermeability = 1.2566370614359172953850573533118011536788677597500e-6;

		/// <summary>
		/// The electric permittivity (in vacuum) in [s^4 kg^-1 A^2 m^-3].
		/// See http://en.wikipedia.org/wiki/Electric_Permittivity .
		/// </summary>
		public const double ElectricPermittivity = 8.8541878171937079244693661186959426889222899381429e-12;
	}
}