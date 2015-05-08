using System;
using System.Globalization;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Special mathematical functions and polynomials.
	/// </summary>
	/// <seealso cref="EngineeringFunctions"/>
	/// <seealso cref="FinancialFunctions"/>
	/// <seealso cref="Trigonometry"/>
	public static class Functions
	{
		/// <summary>
		/// The factorial ln cache size.
		/// </summary>
		private const int FactorialLnCacheSize = 2 * FactorialPrecompSize;

		/// <summary>
		/// The factorial precomp size.
		/// </summary>
		private const int FactorialPrecompSize = 100;

		/// <summary>
		/// The factorial precomp.
		/// </summary>
		private static readonly double[] FactorialPrecomp = new[] {
			/*0*/1d,
			/*1*/1d,
			/*2*/2d,
			/*3*/6d,
			/*4*/24d,
			/*5*/120d,
			/*6*/720d,
			/*7*/5040d,
			/*8*/40320d,
			/*9*/362880d,
			/*10*/3628800d,
			/*11*/39916800d,
			/*12*/479001600d,
			/*13*/6227020800d,
			/*14*/87178291200d,
			/*15*/1307674368000d,
			/*16*/20922789888000d,
			/*17*/355687428096000d,
			/*18*/6402373705728000d,
			/*19*/121645100408832000d,
			/*20*/2432902008176640000d,
			/*21*/51090942171709440000d,
			/*22*/1124000727777607680000d,
			/*23*/25852016738884976640000d,
			/*24*/620448401733239439360000d,
			/*25*/15511210043330985984000000d,
			/*26*/403291461126605635584000000d,
			/*27*/10888869450418352160768000000d,
			/*28*/304888344611713860501504000000d,
			/*29*/8841761993739701954543616000000d,
			/*30*/265252859812191058636308480000000d,
			/*31*/8222838654177922817725562880000000d,
			/*32*/263130836933693530167218012160000000d,
			/*33*/8683317618811886495518194401280000000d,
			/*34*/295232799039604140847618609643520000000d,
			/*35*/10333147966386144929666651337523200000000d,
			/*36*/371993326789901217467999448150835200000000d,
			/*37*/13763753091226345046315979581580902400000000d,
			/*38*/523022617466601111760007224100074291200000000d,
			/*39*/20397882081197443358640281739902897356800000000d,
			/*40*/815915283247897734345611269596115894272000000000d,
			/*41*/33452526613163807108170062053440751665152000000000d,
			/*42*/1405006117752879898543142606244511569936384000000000d,
			/*43*/60415263063373835637355132068513997507264512000000000d,
			/*44*/2658271574788448768043625811014615890319638528000000000d,
			/*45*/119622220865480194561963161495657715064383733760000000000d,
			/*46*/5502622159812088949850305428800254892961651752960000000000d,
			/*47*/258623241511168180642964355153611979969197632389120000000000d,
			/*48*/12413915592536072670862289047373375038521486354677760000000000d,
			/*49*/608281864034267560872252163321295376887552831379210240000000000d,
			/*50*/30414093201713378043612608166064768844377641568960512000000000000d,
			/*51*/1551118753287382280224243016469303211063259720016986112000000000000d,
			/*52*/80658175170943878571660636856403766975289505440883277824000000000000d,
			/*53*/4274883284060025564298013753389399649690343788366813724672000000000000d,
			/*54*/230843697339241380472092742683027581083278564571807941132288000000000000d,
			/*55*/12696403353658275925965100847566516959580321051449436762275840000000000000d,
			/*56*/710998587804863451854045647463724949736497978881168458687447040000000000000d,
			/*57*/40526919504877216755680601905432322134980384796226602145184481280000000000000d,
			/*58*/2350561331282878571829474910515074683828862318181142924420699914240000000000000d,
			/*59*/138683118545689835737939019720389406345902876772687432540821294940160000000000000d,
			/*60*/8320987112741390144276341183223364380754172606361245952449277696409600000000000000d,
			/*61*/507580213877224798800856812176625227226004528988036003099405939480985600000000000000d,
			/*62*/31469973260387937525653122354950764088012280797258232192163168247821107200000000000000d,
			/*63*/1982608315404440064116146708361898137544773690227268628106279599612729753600000000000000d,
			/*64*/126886932185884164103433389335161480802865516174545192198801894375214704230400000000000000d,
			/*65*/8247650592082470666723170306785496252186258551345437492922123134388955774976000000000000000d,
			/*66*/544344939077443064003729240247842752644293064388798874532860126869671081148416000000000000000d,
			/*67*/36471110918188685288249859096605464427167635314049524593701628500267962436943872000000000000000d,
			/*68*/2480035542436830599600990418569171581047399201355367672371710738018221445712183296000000000000000d,
			/*69*/171122452428141311372468338881272839092270544893520369393648040923257279754140647424000000000000000d,
			/*70*/11978571669969891796072783721689098736458938142546425857555362864628009582789845319680000000000000000d,
			/*71*/850478588567862317521167644239926010288584608120796235886430763388588680378079017697280000000000000000d,
			/*72*/61234458376886086861524070385274672740778091784697328983823014963978384987221689274204160000000000000000d,
			/*73*/4470115461512684340891257138125051110076800700282905015819080092370422104067183317016903680000000000000000d,
			/*74*/330788544151938641225953028221253782145683251820934971170611926835411235700971565459250872320000000000000000d,
			/*75*/24809140811395398091946477116594033660926243886570122837795894512655842677572867409443815424000000000000000000d,
			/*76*/1885494701666050254987932260861146558230394535379329335672487982961844043495537923117729972224000000000000000000d,
			/*77*/145183092028285869634070784086308284983740379224208358846781574688061991349156420080065207861248000000000000000000d,
			/*78*/11324281178206297831457521158732046228731749579488251990048962825668835325234200766245086213177344000000000000000000d,
			/*79*/894618213078297528685144171539831652069808216779571907213868063227837990693501860533361810841010176000000000000000000d,
			/*80*/71569457046263802294811533723186532165584657342365752577109445058227039255480148842668944867280814080000000000000000000d,
			/*81*/5797126020747367985879734231578109105412357244731625958745865049716390179693892056256184534249745940480000000000000000000d,
			/*82*/475364333701284174842138206989404946643813294067993328617160934076743994734899148613007131808479167119360000000000000000000d,
			/*83*/39455239697206586511897471180120610571436503407643446275224357528369751562996629334879591940103770870906880000000000000000000d,
			/*84*/3314240134565353266999387579130131288000666286242049487118846032383059131291716864129885722968716753156177920000000000000000000d,
			/*85*/281710411438055027694947944226061159480056634330574206405101912752560026159795933451040286452340924018275123200000000000000000000d,
			/*86*/24227095383672732381765523203441259715284870552429381750838764496720162249742450276789464634901319465571660595200000000000000000000d,
			/*87*/2107757298379527717213600518699389595229783738061356212322972511214654115727593174080683423236414793504734471782400000000000000000000d,
			/*88*/185482642257398439114796845645546284380220968949399346684421580986889562184028199319100141244804501828416633516851200000000000000000000d,
			/*89*/16507955160908461081216919262453619309839666236496541854913520707833171034378509739399912570787600662729080382999756800000000000000000000d,
			/*90*/1485715964481761497309522733620825737885569961284688766942216863704985393094065876545992131370884059645617234469978112000000000000000000000d,
			/*91*/135200152767840296255166568759495142147586866476906677791741734597153670771559994765685283954750449427751168336768008192000000000000000000000d,
			/*92*/12438414054641307255475324325873553077577991715875414356840239582938137710983519518443046123837041347353107486982656753664000000000000000000000d,
			/*93*/1156772507081641574759205162306240436214753229576413535186142281213246807121467315215203289516844845303838996289387078090752000000000000000000000d,
			/*94*/108736615665674308027365285256786601004186803580182872307497374434045199869417927630229109214583415458560865651202385340530688000000000000000000000d,
			/*95*/10329978488239059262599702099394727095397746340117372869212250571234293987594703124871765375385424468563282236864226607350415360000000000000000000000d,
			/*96*/991677934870949689209571401541893801158183648651267795444376054838492222809091499987689476037000748982075094738965754305639874560000000000000000000000d,
			/*97*/96192759682482119853328425949563698712343813919172976158104477319333745612481875498805879175589072651261284189679678167647067832320000000000000000000000d,
			/*98*/9426890448883247745626185743057242473809693764078951663494238777294707070023223798882976159207729119823605850588608460429412647567360000000000000000000000d,
			/*99*/933262154439441526816992388562667004907159682643816214685929638952175999932299156089414639761565182862536979208272237582511852109168640000000000000000000000d,
			/*100*/93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000d,
		};
		/// <summary>
		/// The <see cref="FactorialLn"/> cache.
		/// </summary>
		private static double[] factorialLnCache;

		/// <summary>
		/// Returns the binomial coefficient of two integers as a double precision number.
		/// </summary>
		/// <param name="n">
		/// A number.
		/// </param>
		/// <param name="k">
		/// A number.
		/// </param>
		/// <remarks>http://en.wikipedia.org/wiki/Binomial_coefficient</remarks>
		/// <seealso cref="BinomialCoefficientLn"/>
		/// <seealso cref="BinomialDistribution"/>
		public static double BinomialCoefficient(int n, int k)
		{
			return k < 0 || n < 0 || k > n ? 0d : Math.Floor(0.5 + Math.Exp(FactorialLn(n) - FactorialLn(k) - FactorialLn(n - k)));
		}

		/// <summary>
		/// Returns the regularized lower incomplete beta function
		/// The regularized incomplete beta function (or regularized beta function for short) is defined in terms of the incomplete beta function and the complete beta function. 
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Regularized_Beta_function</remarks>
		public static double BetaRegularized(double a, double b, double x)
		{
			const int MaxIterations = 100;
			if(a < 0.0)
				throw new ArgumentOutOfRangeException("a");
			if(b < 0.0)
				throw new ArgumentOutOfRangeException("b");
			if(x < 0.0 || x > 1.0)
				throw new ArgumentOutOfRangeException("x", "0d");
			var bt = (Math.Abs(x) < Constants.Epsilon || Math.Abs(x - 1d) < Constants.Epsilon) ? 0.0 : Math.Exp(GammaLn(a + b) - GammaLn(a) - GammaLn(b) + a * Math.Log(x) + b * Math.Log(1.0 - x));
			var symmetryTransformation = x >= (a + 1.0) / (a + b + 2.0);
			var eps = Constants.RelativeAccuracy;
			var fpmin = Constants.SmallestNumberGreaterThanZero / eps;
			if(symmetryTransformation) {
				x = 1.0 - x;
				var swap = a;
				a = b;
				b = swap;
			}

			var qab = a + b;
			var qap = a + 1.0;
			var qam = a - 1.0;
			var c = 1.0;
			var d = 1.0 - qab * x / qap;
			if(Math.Abs(d) < fpmin)
				d = fpmin;
			d = 1.0 / d;
			var h = d;

			for(int m = 1, m2 = 2; m <= MaxIterations; m++, m2 += 2) {
				var aa = m * (b - m) * x / ((qam + m2) * (a + m2));
				d = 1.0 + aa * d;

				if(Math.Abs(d) < fpmin)
					d = fpmin;

				c = 1.0 + aa / c;
				if(Math.Abs(c) < fpmin)
					c = fpmin;

				d = 1.0 / d;
				h *= d * c;
				aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
				d = 1.0 + aa * d;

				if(Math.Abs(d) < fpmin)
					d = fpmin;

				c = 1.0 + aa / c;

				if(Math.Abs(c) < fpmin)
					c = fpmin;

				d = 1.0 / d;
				var del = d * c;
				h *= del;

				if(Math.Abs(del - 1.0) <= eps)
					return symmetryTransformation ? 1.0 - bt * h / a : bt * h / a;
			}
			throw new ArgumentException("a,b");
		}

		/// <summary>
		/// Returns the natural logarithm of the binomial coefficient of n and k.
		/// </summary>
		/// <param name="n">
		/// A number
		/// </param>
		/// <param name="k">
		/// A number
		/// </param>
		public static double BinomialCoefficientLn(int n, int k)
		{
			if(k < 0 || n < 0 || k > n)
				return 1.0;
			return FactorialLn(n) - FactorialLn(k) - FactorialLn(n - k);
		}

		/// <summary>
		/// Evaluates the minimum distance to the next distinguishable number near the argument value.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Relative Epsilon (positive double or NaN).
		/// </returns>
		/// <remarks>
		/// Evaluates the <b>Negative</b> epsilon. The more common positive epsilon is equal to two times this negative epsilon.
		/// </remarks>
		public static double EpsilonOf(double value)
		{
			if(double.IsInfinity(value) || double.IsNaN(value))
				return double.NaN;

			var signed64 = BitConverter.DoubleToInt64Bits(value);
			if(signed64 == 0) {
				signed64++;
				return BitConverter.Int64BitsToDouble(signed64) - value;
			}
			if(signed64-- < 0)
				return BitConverter.Int64BitsToDouble(signed64) - value;
			return value - BitConverter.Int64BitsToDouble(signed64);
		}

		/// <summary>
		/// Returns the factorial (n!) of an integer number &gt; 0. Consider using <see cref="FactorialLn"/> instead.
		/// </summary>
		/// <param name="n">
		/// The argument.
		/// </param>
		/// <returns>
		/// A value value! for value &gt; 0.
		/// </returns>
		/// <remarks>
		/// http://en.wikipedia.org/wiki/Factorial
		/// </remarks>
		/// <seealso cref="FactorialLn"/>
		public static double Factorial(int n)
		{
			if(n < 0)
				throw new ArgumentOutOfRangeException("n");
			if(n == 0)
				return 1d;
			if(n == 1)
				return 1d;
			return n >= FactorialPrecompSize ? Math.Exp(GammaLn(n + 1.0)) : FactorialPrecomp[n];
		}

		/// <summary>
		/// Returns the natural logarithm of the factorial (n!) for an integer value &gt; 0.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// A value ln(value!) for value &gt; 0.
		/// </returns>
		public static double FactorialLn(int value)
		{
			if(value < 0)
				throw new ArgumentOutOfRangeException("value");
			if(value <= 1)
				return 0d;
			if(value >= FactorialLnCacheSize)
				return GammaLn(value + 1d);
			if(factorialLnCache == null)
				factorialLnCache = new double[FactorialLnCacheSize];
			return Math.Abs(factorialLnCache[value] - 0.0) > Constants.Epsilon ? factorialLnCache[value] : (factorialLnCache[value] = GammaLn(value + 1.0));
		}

		/// <summary>
		/// Returns the natural logarithm of Gamma for a real value &gt; 0.
		/// </summary>
		/// <param name="x">
		/// The value.
		/// </param>
		/// <returns>
		/// A value ln|Gamma(value))| for value &gt; 0.
		/// </returns>
		public static double GammaLn(double x)
		{
			// if (x <= 0) throw new Exception("Input value must be > 0");

			var coef = new[] {57.1562356658629235,
				-59.5979603554754912,
				14.1360979747417471,
				-0.491913816097620199,
				0.339946499848118887E-4,
				0.465236289270485756E-4,
				-0.983744753048795646E-4,
				0.158088703224912494E-3,
				-0.210264441724104883E-3,
				0.217439618115212643E-3,
				-0.164318106536763890E-3,
				0.844182239838527433E-4,
				-0.261908384015814087E-4,
				0.368991826595316234E-5
			};

			var denominator = x;
			var series = 0.999999999999997092;
			var temp = x + 5.24218750000000000;
			temp = (x + 0.5) * Math.Log(temp) - temp;
			for(var j = 0; j < 14; j++)
				series += coef[j] / ++denominator;
			return (temp + Math.Log(2.5066282746310005 * series / x));
		}
        /// <summary>
        ///   Evaluates polynomial of degree N
        /// </summary>
        /// 
        public static double Polevl(double x, double[] coef, int n)
        {
            double ans;

            ans = coef[0];

            for (int i = 1; i <= n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

        /// <summary>
        ///   Evaluates polynomial of degree N with assumption that coef[N] = 1.0
        /// </summary>
        /// 
        public static double P1evl(double x, double[] coef, int n)
        {
            double ans;

            ans = x + coef[0];

            for (int i = 1; i < n; i++)
                ans = ans * x + coef[i];

            return ans;
        }

		/// <summary>
		/// Returns the gamma function.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Gamma_function</remarks>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Gamma(double x)
		{
			if(Math.Abs(x - 1) < Constants.Epsilon)
				return 1d;
			if(x < 0)
				throw new Exception("The Gamma function implementation does not negative arguments.");
			int n;
			if(int.TryParse(x.ToString(CultureInfo.InvariantCulture), out n) && n > 0) {
				return Factorial(n - 1);
			}
			return Math.Exp(GammaLn(x));
		}

		/// <summary>
		/// Returns the regularized lower incomplete gamma function
		/// P(a,x) = 1/Gamma(a) * int(exp(-t)t^(a-1),t=0..x) for real a &gt; 0, x &gt; 0.
		/// </summary>
		/// <param name="a">
		/// The a.
		/// </param>
		/// <param name="x">
		/// The x.
		/// </param>
		/// <remarks>Note that some packages like Mathematica define the regularized gamma function differently.</remarks>
		public static double GammaRegularized(double a, double x)
		{
			const int MaxIterations = 100;
			var eps = Constants.RelativeAccuracy;
			var fpmin = Constants.SmallestNumberGreaterThanZero / eps;

			if(a < 0.0 || x < 0.0)
				throw new ArgumentOutOfRangeException("a");

			var gln = GammaLn(a);
			if(x < a + 1.0) {
				if(x <= 0.0)
					return 0.0;
				var ap = a;
				double del, sum = del = 1.0 / a;

				for(var n = 0; n < MaxIterations; n++) {
					++ap;
					del *= x / ap;
					sum += del;
					if(Math.Abs(del) < Math.Abs(sum) * eps)
						return sum * Math.Exp(-x + a * Math.Log(x) - gln);
				}
			} else {
				// Continued fraction representation
				var b = x + 1.0 - a;
				var c = 1.0 / fpmin;
				var d = 1.0 / b;
				var h = d;

				for(var i = 1; i <= MaxIterations; i++) {
					var an = -i * (i - a);
					b += 2.0;
					d = an * d + b;
					if(Math.Abs(d) < fpmin)
						d = fpmin;

					c = b + an / c;
					if(Math.Abs(c) < fpmin)
						c = fpmin;
					d = 1.0 / d;
					var del = d * c;
					h *= del;

					if(Math.Abs(del - 1.0) <= eps)
						return 1.0 - Math.Exp(-x + a * Math.Log(x) - gln) * h;
				}
			}

			throw new ArgumentException("a");
		}

		/// <summary>
		/// Returns the inverse P^(-1) of the regularized lower incomplete gamma function
		/// P(a,x) = 1/Gamma(a) * int(exp(-t)t^(a-1),t=0..x) for real a &gt; 0, x &gt; 0,
		/// such that P^(-1)(a,P(a,x)) == x.
		/// </summary>
		public static double InverseGammaRegularized(double a, double y0)
		{
			const double Epsilon = 0.000000000000001;
			const double BigNumber = 4503599627370496.0;
			const double Threshold = 5 * Epsilon;

			// TODO: Consider to throw an out-of-range exception instead of NaN
			if(a < 0 || a.IsZero() || y0 < 0 || y0 > 1) {
				return Double.NaN;
			}

			if(y0.IsZero()) {
				return 0d;
			}

			if(y0.IsEqualTo(1)) {
				return Double.PositiveInfinity;
			}

			y0 = 1 - y0;

			var xUpper = BigNumber;
			double xLower = 0;
			double yUpper = 1;
			double yLower = 0;

			// Initial Guess
			double d = 1 / (9 * a);
			double y = 1 - d - (0.98 * Constants.Sqrt2 * ErfInverse((2.0 * y0) - 1.0) * Math.Sqrt(d));
			double x = a * y * y * y;
			double lgm = GammaLn(a);

			for(var i = 0; i < 10; i++) {
				if(x < xLower || x > xUpper) {
					d = 0.0625;
					break;
				}

				y = 1 - GammaRegularized(a, x);
				if(y < yLower || y > yUpper) {
					d = 0.0625;
					break;
				}

				if(y < y0) {
					xUpper = x;
					yLower = y;
				} else {
					xLower = x;
					yUpper = y;
				}

				d = ((a - 1) * Math.Log(x)) - x - lgm;
				if(d < -709.78271289338399) {
					d = 0.0625;
					break;
				}

				d = -Math.Exp(d);
				d = (y - y0) / d;
				if(Math.Abs(d / x) < Epsilon) {
					return x;
				}

				if((d > (x / 4)) && (y0 < 0.05)) {
					// Naive heuristics for cases near the singularity
					d = x / 10;
				}

				x -= d;
			}

			if(xUpper == BigNumber) {
				if(x <= 0) {
					x = 1;
				}

				while(xUpper == BigNumber) {
					x = (1 + d) * x;
					y = 1 - GammaRegularized(a, x);
					if(y < y0) {
						xUpper = x;
						yLower = y;
						break;
					}

					d = d + d;
				}
			}

			int dir = 0;
			d = 0.5;
			for(var i = 0; i < 400; i++) {
				x = xLower + (d * (xUpper - xLower));
				y = 1 - GammaRegularized(a, x);
				lgm = (xUpper - xLower) / (xLower + xUpper);
				if(Math.Abs(lgm) < Threshold) {
					return x;
				}

				lgm = (y - y0) / y0;
				if(Math.Abs(lgm) < Threshold) {
					return x;
				}

				if(x <= 0d) {
					return 0d;
				}

				if(y >= y0) {
					xLower = x;
					yUpper = y;
					if(dir < 0) {
						dir = 0;
						d = 0.5;
					} else {
						if(dir > 1) {
							d = (0.5 * d) + 0.5;
						} else {
							d = (y0 - yLower) / (yUpper - yLower);
						}
					}

					dir = dir + 1;
				} else {
					xUpper = x;
					yLower = y;
					if(dir > 0) {
						dir = 0;
						d = 0.5;
					} else {
						if(dir < -1) {
							d = 0.5 * d;
						} else {
							d = (y0 - yLower) / (yUpper - yLower);
						}
					}

					dir = dir - 1;
				}
			}

			return x;
		}

		/// <summary>
		/// Returns the square of the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static double Squared(double value)
		{
			return value * value;
		}

		/// <summary>
		/// Returns the square of the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static double Sqr(double value)
		{
			return value * value;
		}

		/// <summary>
		/// The sine integral function.
		/// </summary>
		/// <param name="x">A number.</param>
		public static double Si(double x)
		{
			var sum = 0.0;
			double t;

			var n = 0;
			do {
				t = Math.Pow(-1, n) * Math.Pow(x, 2 * n + 1) / (2 * n + 1) / Gamma(2 * n + 2);
				sum += t;
				n++;
			} while (Math.Abs(t) > Constants.Epsilon);
			return sum;
		}

		/// <summary>
		/// The cosine integral function.
		/// </summary>
		/// <param name="x">A number.</param>
		public static double Ci(double x)
		{
			var sum = 0.0;
			double t;
			var n = 1;
			do {
				t = Math.Pow(-1, n) * Math.Pow(x, 2 * n) / (2 * n) / Gamma(2 * n + 1);
				sum += t;
				n++;
			} while (Math.Abs(t) > Constants.Epsilon);
			return 0.57721566490153286060 + Math.Log(x) + sum;
		}

		/// <summary>
		/// Returns the error function.
		/// </summary>
		public static double Erf(double x)
		{
			return x >= 0 ? 1.0 - ErfcCheb(x) : ErfcCheb(-x) - 1.0;
		}

		/// <summary>
		/// Returns the complementary or inverse error function.
		/// </summary>
		public static double ErfC(double x)
		{
			return x >= 0 ? ErfcCheb(x) : 2.0 - ErfcCheb(-x);
		}

		/// <summary>
		/// Returns the inverse error function erf^-1(x).
		/// </summary>
		/// <remarks>
		/// <para>
		/// The algorithm uses a minimax approximation by rational functions
		/// and the result has a relative error whose absolute value is less
		/// than 1.15e-9.
		/// </para>
		/// <para>
		/// See the page <see href="http://home.online.no/~pjacklam/notes/invnorm/"/>
		/// for more details.
		/// </para>
		/// </remarks>
		public static double ErfInverse(double x)
		{
			//if(x < -1.0 || x > 1.0)
			//{
			//    throw new ArgumentOutOfRangeException("x", x, Properties.LocalStrings.ArgumentInIntervalXYInclusive(-1, 1));
			//}

			x = 0.5 * (x + 1.0);

			// Define break-points.
			const double Plow = 0.02425;
			const double Phigh = 1 - Plow;

			double q;

			// Rational approximation for lower region:
			if(x < Plow) {
				q = Math.Sqrt(-2 * Math.Log(x));
				return (((((ErfInvC[0] * q + ErfInvC[1]) * q + ErfInvC[2]) * q + ErfInvC[3]) * q + ErfInvC[4]) * q + ErfInvC[5]) /((((ErfInvD[0] * q + ErfInvD[1]) * q + ErfInvD[2]) * q + ErfInvD[3]) * q + 1) * Constants.Sqrt1Over2;
			}

			// Rational approximation for upper region:
			if(Phigh < x) {
				q = Math.Sqrt(-2 * Math.Log(1 - x));
				return -(((((ErfInvC[0] * q + ErfInvC[1]) * q + ErfInvC[2]) * q + ErfInvC[3]) * q + ErfInvC[4]) * q + ErfInvC[5]) / ((((ErfInvD[0] * q + ErfInvD[1]) * q + ErfInvD[2]) * q + ErfInvD[3]) * q + 1) * Constants.Sqrt1Over2;
			}

			// Rational approximation for central region:
			q = x - 0.5;
			var r = q * q;
			return (((((ErfInvA[0] * r + ErfInvA[1]) * r + ErfInvA[2]) * r + ErfInvA[3]) * r + ErfInvA[4]) * r + ErfInvA[5]) * q / (((((ErfInvB[0] * r + ErfInvB[1]) * r + ErfInvB[2]) * r + ErfInvB[3]) * r + ErfInvB[4]) * r + 1) * Constants.Sqrt1Over2;
		}

		private static readonly double[] ErfInvA = {
			-3.969683028665376e+01, 2.209460984245205e+02,
			-2.759285104469687e+02, 1.383577518672690e+02,
			-3.066479806614716e+01, 2.506628277459239e+00
		};

		private static readonly double[] ErfInvB = {
			-5.447609879822406e+01, 1.615858368580409e+02,
			-1.556989798598866e+02, 6.680131188771972e+01,
			-1.328068155288572e+01
		};

		private static readonly double[] ErfInvC = {
			-7.784894002430293e-03, -3.223964580411365e-01,
			-2.400758277161838e+00, -2.549732539343734e+00,
			4.374664141464968e+00, 2.938163982698783e+00
		};

		private static readonly double[] ErfInvD = {
			7.784695709041462e-03, 3.224671290700398e-01,
			2.445134137142996e+00, 3.754408661907416e+00
		};

		private static double ErfcCheb(double x)
		{
			int j;
			var d = 0.0;
			var dd = 0.0;

			if(x < 0.0)
				throw new Exception("ErfcCheb requires nonnegative argument");

			var coef = new[] {-1.3026537197817094, 
				6.4196979235649026e-1, 1.9476473204185836e-2,
				-9.561514786808631e-3, -9.46595344482036e-4,
				3.66839497852761e-4, 4.2523324806907e-5,
				-2.0278578112534e-5, -1.624290004647e-6,
				1.303655835580e-6, 1.5626441722e-8, -8.5238095915e-8,
				6.529054439e-9, 5.059343495e-9, -9.91364156e-10,
				-2.27365122e-10, 9.6467911e-11, 2.394038e-12,
				-6.886027e-12, 8.94487e-13, 3.13092e-13,
				-1.12708e-13, 3.81e-16, 7.106e-15, -1.523e-15,
				-9.4e-17, 1.21e-16, -2.8e-17
			};

			var t = 2.0 / (2.0 + x);
			var ty = 4.0 * t - 2.0;

			for(j = coef.Length - 1; j > 0; j--) {
				var tmp = d;
				d = ty * d - dd + coef[j];
				dd = tmp;
			}
			return t * Math.Exp(-x * x + 0.5 * (coef[0] + ty * d) - dd);
		}

		/// <summary>
		/// The Laguerres polynomial value for the specified number and order.
		/// </summary>
		/// <param name="x">A number.</param>
		/// <param name="order">The order of the Laguerre polynomial.</param>
		/// <remarks>http://en.wikipedia.org/wiki/Laguerre_polynomials</remarks>
		public static double Laguerre(double x, int order)
		{
			var L0 = 1.0;
			var L1 = 1.0 - x;
			var L2 = (x * x - 4 * x + 2.0) / 2.0;
			var n = 1;
			if(order < 0)
				throw new Exception("Bad Laguerre polynomial: deg < 0");
			if(order == 0)
				return L0;
			if(order == 1)
				return L1;
			while(n < order) {
				L2 = ((2.0 * n + 1.0 - x) * L1 - n * L0) / (n + 1);
				L0 = L1;
				L1 = L2;
				n++;
			}
			return L2;
		}

		/// <summary>
		/// The Hermite polynomial value for the specified number and order.
		/// </summary>
		/// <param name="x">A number.</param>
		/// <param name="order">The order of the Hermite polynomial.</param>
		/// <remarks>http://en.wikipedia.org/wiki/Hermite_polynomials</remarks>
		public static double Hermite(double x, int order)
		{
			var H_0 = 1.0;
			var H_1 = 2 * x;
			var H_2 = 4 * x * x - 2;
			var n = 1;
			if(order < 0)
				throw new Exception("The order of the Hermite polynomial cannot be less than zero.");
			if(order == 0)
				return H_0;
			if(order == 1)
				return H_1;
			if(order == 2)
				return H_2;
			while(n < order) {
				H_2 = 2d * (x * H_1 - n * H_0);
				H_0 = H_1;
				H_1 = H_2;
				n++;
			}
			return H_2;
		}

		/// <summary>
		/// The Legendre polynomial value for the specified number and order.
		/// </summary>
		/// <param name="x">A number.</param>
		/// <param name="order">The order of the Legendre polynomial.</param>
		/// <remarks>http://en.wikipedia.org/wiki/Legendre_polynomials</remarks>
		public static double Legendre(double x, int order)
		{
			var L_0 = 1d;
			var L_1 = x;
			var L_2 = (3d * x * x - 1) / 2d;
			var n = 1;
			if(order < 0)
				throw new Exception("The order of the Legendre polynomial cannot be less than zero.");
			if(order == 0)
				return L_0;
			if(order == 1)
				return L_1;
			if(order == 2)
				return L_2;

			// using the recursion relation for Legendre polynomials
			while(n < order) {
				L_2 = ((2d * n + 1) * x * L_1 - n * L_0) / (n + 1);
				L_0 = L_1;
				L_1 = L_2;
				n++;
			}
			return L_2;
		}

		/// <summary>
		/// Gets whether the value is double or infinity.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsNanOrInfinity(this double value)
		{
			return double.IsNaN(value) || double.IsInfinity(value);
		}

		/// <summary>
		/// Returns whether the given double is not infinite and an actual number (not NaN).
		/// </summary>
		/// <param name="x">The x.</param>
		/// <returns>
		///   <c>True</c> if the value is true number; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsFinitedouble(double x)
		{
			return !double.IsInfinity(x) && !double.IsNaN(x);
		}

		/// <summary>
		/// Returns the greatest common divisor of two numbers.
		/// </summary>
		/// <param name="value1">The first number.</param>
		/// <param name="value2">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		private static long GCD(long value1, long value2)
		{
			long remainder;
			do {
				remainder = value1 % value2;
				value1 = value2;
				value2 = remainder;
			} while (remainder != 0);

			return value1;
		}

		/// <summary>
		/// Returns the greatest common divisor of the given numbers.
		/// </summary>
		/// <param name="numbers">Some numbers.</param>
		/// <returns>
		/// The greatest common divisor.
		/// </returns>
		public static long GCD(params long[] numbers)
		{
			if(numbers == null)
				throw new ArgumentNullException("numbers");
			// let's assume that the GCD of one number is itself
			if(numbers.Length == 1)
				return numbers[0];
			var result = GCD(numbers[0], numbers[1]);
			if(numbers.Length > 2) {
				for(var i = 2; i < numbers.Length; i++)
					result = GCD(result, numbers[i]);
			}
			return result;
		}

		/// <summary>
		/// The Bessel function of the first kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="n">The order of the Bessel function.</param>
		public static double BesselJ(double x, double order)
		{
			int n;
			if((order % 1) == 0) // integer order
                n = (int)order;
			else {
				// if fractional we need to use another algorithm
				double res = 0d, y = 0d, jp = 0d, yp = 0d;
				bessjy(x, order, out res, out y, out jp, out yp);
				return res;
			}

			if(n == 0)
				return BesselJ0(x);
			if(n == 1)
				return BesselJ1(x);

			const double Accuracy = 40;
			const double BigNo = 1E10;
			const double BigNi = 1E-10;
			int j, jsum, m;
			double ax, bj, bjm, bjp, sum, tox, ans;

			ax = Math.Abs(x);
			if(Math.Abs(ax) < Constants.Epsilon)
				return 0.0;
			if(ax > n) {

				tox = 2.0 / ax;
				bjm = BesselJ0(ax);
				bj = BesselJ1(ax);
				for(j = 1; j < n; j++) {
					bjp = j * tox * bj - bjm;
					bjm = bj;
					bj = bjp;
				}
				ans = bj;
			} else {
				tox = 2.0 / ax;
				m = (int)(2 * ((n + (int)Math.Sqrt(Accuracy * n)) / 2));
				jsum = 0;
				bjp = ans = sum = 0.0;
				bj = 1.0;
				for(j = m; j > 0; j--) {
					bjm = j * tox * bj - bjp;
					bjp = bj;
					bj = bjm;
					if(Math.Abs(bj) > BigNo) {
						bj *= BigNi;
						bjp *= BigNi;
						ans *= BigNi;
						sum *= BigNi;
					}
					if(jsum == 1)
						sum += bj;
					jsum = jsum == 0 ? 1 : 0;
					if(j == n)
						ans = bjp;
				}
				sum = 2.0 * sum - bj;
				ans /= sum;
			}
			return x < 0.0 && (((int)n & 1) == 1)/* n is odd*/ ? -ans : ans;
		}

		public static double BesselY1(double x)
		{
			double z;
			double xx, y, ans, ans1, ans2;
			if(x < 8.0) {
				y = x * x;
				ans1 = x * (-0.4900604943e13 + y * (0.1275274390e13
				+ y * (-0.5153438139e11 + y * (0.7349264551e9
				+ y * (-0.4237922726e7 + y * 0.8511937935e4)))));
				ans2 = 0.2499580570e14 + y * (0.4244419664e12
				+ y * (0.3733650367e10 + y * (0.2245904002e8
				+ y * (0.1020426050e6 + y * (0.3549632885e3 + y)))));
				ans = (ans1 / ans2) + 0.636619772 * (BesselJ1(x) * Math.Log(x) - 1.0 / x);
			} else {
				z = 8.0 / x;
				y = z * z;
				xx = x - 2.356194491;
				ans1 = 1.0 + y * (0.183105e-2 + y * (-0.3516396496e-4
				+ y * (0.2457520174e-5 + y * (-0.240337019e-6))));
				ans2 = 0.04687499995 + y * (-0.2002690873e-3
				+ y * (0.8449199096e-5 + y * (-0.88228987e-6
				+ y * 0.105787412e-6)));
				ans = Math.Sqrt(0.636619772 / x) * (Math.Sin(xx) * ans1 + z * Math.Cos(xx) * ans2);
			}
			return ans;
		}

		public static double BesselY0(double x)
		{

			double z;
			double xx, y, ans, ans1, ans2;
			if(x < 8.0) {
				y = x * x;
				ans1 = -2957821389.0 + y * (7062834065.0 + y * (-512359803.6
				+ y * (10879881.29 + y * (-86327.92757 + y * 228.4622733))));
				ans2 = 40076544269.0 + y * (745249964.8 + y * (7189466.438
				+ y * (47447.26470 + y * (226.1030244 + y * 1.0))));
				ans = (ans1 / ans2) + 0.636619772 * BesselJ0(x) * Math.Log(x);
			} else {
				z = 8.0 / x;
				y = z * z;
				xx = x - 0.785398164;
				ans1 = 1.0 + y * (-0.1098628627e-2 + y * (0.2734510407e-4
				+ y * (-0.2073370639e-5 + y * 0.2093887211e-6)));
				ans2 = -0.1562499995e-1 + y * (0.1430488765e-3
				+ y * (-0.6911147651e-5 + y * (0.7621095161e-6
				+ y * (-0.934945152e-7))));
				ans = Math.Sqrt(0.636619772 / x) * (Math.Sin(xx) * ans1 + z * Math.Cos(xx) * ans2);
			}
			return ans;
		}

		public static double BesselJ1(double x)
		{
			double ax, z;
			double xx, y, ans, ans1, ans2;
			if((ax = Math.Abs(x)) < 8.0) {
				y = x * x;
				ans1 = x * (72362614232.0 + y * (-7895059235.0 + y * (242396853.1
				+ y * (-2972611.439 + y * (15704.48260 + y * (-30.16036606))))));
				ans2 = 144725228442.0 + y * (2300535178.0 + y * (18583304.74
				+ y * (99447.43394 + y * (376.9991397 + y * 1.0))));
				ans = ans1 / ans2;
			} else {
				z = 8.0 / ax;
				y = z * z;
				xx = ax - 2.356194491;
				ans1 = 1.0 + y * (0.183105e-2 + y * (-0.3516396496e-4
				+ y * (0.2457520174e-5 + y * (-0.240337019e-6))));
				ans2 = 0.04687499995 + y * (-0.2002690873e-3
				+ y * (0.8449199096e-5 + y * (-0.88228987e-6
				+ y * 0.105787412e-6)));
				ans = Math.Sqrt(0.636619772 / ax) * (Math.Cos(xx) * ans1 - z * Math.Sin(xx) * ans2);
				if(x < 0.0)
					ans = -ans;
			}
			return ans;
		}

		public static double BesselJ0(double x)
		{
            
			double ax, z;
			double xx, y, ans, ans1, ans2;
			if((ax = Math.Abs(x)) < 8.0) { 
				y = x * x;
				ans1 = 57568490574.0 + y * (-13362590354.0 + y * (651619640.7
				+ y * (-11214424.18 + y * (77392.33017 + y * (-184.9052456)))));
				ans2 = 57568490411.0 + y * (1029532985.0 + y * (9494680.718
				+ y * (59272.64853 + y * (267.8532712 + y * 1.0))));
				ans = ans1 / ans2;
			} else { 
				z = 8.0 / ax;
				y = z * z;
				xx = ax - 0.785398164;
				ans1 = 1.0 + y * (-0.1098628627e-2 + y * (0.2734510407e-4
				+ y * (-0.2073370639e-5 + y * 0.2093887211e-6)));
				ans2 = -0.1562499995e-1 + y * (0.1430488765e-3
				+ y * (-0.6911147651e-5 + y * (0.7621095161e-6
				- y * 0.934945152e-7)));
				ans = Math.Sqrt(0.636619772 / ax) * (Math.Cos(xx) * ans1 - z * Math.Sin(xx) * ans2);
			}
			return ans;
		}

		/// <summary>
		/// The modified Bessel function of the first kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="order">The order.</param>
		public static double BesselI(double x, double order)
		{
			int n;
			if((order % 1) == 0) // integer order
                n = (int)order;
			else {
				// if fractional we need to use another algorithm
				double res, y, jp, yp;
				bessik(x, order, out res, out y, out jp, out yp);
				return res;
			}
			if(n == 0)
				return BesselI0(x);
			if(n == 1)
				return BesselI1(x);
			const double Accuracy = 40;
			const double BigNo = 1E10;
			const double BigNi = 1E-10;
			int j;
			double ans;

			if(Math.Abs(x) < Constants.Epsilon)
				return 0.0;
			var tox = 2.0 / Math.Abs(x);
			var bip = ans = 0.0;
			var bi = 1.0;
			for(j = 2 * (n + (int)Math.Sqrt(Accuracy * n)); j > 0; j--) {
				var bim = bip + j * tox * bi;
				bip = bi;
				bi = bim;
				if(Math.Abs(bi) > BigNo) {
					ans *= BigNi;
					bi *= BigNi;
					bip *= BigNi;
				}
				if(j == n)
					ans = bip;
			}
			ans *= BesselI0(x) / bi;
			return x < 0.0 && ((n & 1) == 1)/* n is odd*/ ? -ans : ans;
		}

		private static double BesselK0(double x)
		{
			double y, ans;
			if(x <= 2.0) {
				y = x * x / 4.0;
				ans = (-Math.Log(x / 2.0) * BesselI0(x)) + (-0.57721566 + y * (0.42278420
				+ y * (0.23069756 + y * (0.3488590e-1 + y * (0.262698e-2
				+ y * (0.10750e-3 + y * 0.74e-5))))));
			} else {
				y = 2.0 / x;
				ans = (Math.Exp(-x) / Math.Sqrt(x)) * (1.25331414 + y * (-0.7832358e-1
				+ y * (0.2189568e-1 + y * (-0.1062446e-1 + y * (0.587872e-2
				+ y * (-0.251540e-2 + y * 0.53208e-3))))));
			}
			return ans;
		}

		private static double BesselK1(double x)
		{

			double y, ans;
			if(x <= 2.0) {
				y = x * x / 4.0;
				ans = (Math.Log(x / 2.0) * BesselI1(x)) + (1.0 / x) * (1.0 + y * (0.15443144
				+ y * (-0.67278579 + y * (-0.18156897 + y * (-0.1919402e-1
				+ y * (-0.110404e-2 + y * (-0.4686e-4)))))));
			} else {
				y = 2.0 / x;
				ans = (Math.Exp(-x) / Math.Sqrt(x)) * (1.25331414 + y * (0.23498619
				+ y * (-0.3655620e-1 + y * (0.1504268e-1 + y * (-0.780353e-2
				+ y * (0.325614e-2 + y * (-0.68245e-3)))))));
			}
			return ans;
		}

		private static double BesselI1(double x)
		{
			double ax, ans;
			double y;
			if((ax = Math.Abs(x)) < 3.75) {
				y = x / 3.75;
				y *= y;
				ans = ax * (0.5 + y * (0.87890594 + y * (0.51498869 + y * (0.15084934
				+ y * (0.2658733e-1 + y * (0.301532e-2 + y * 0.32411e-3))))));
			} else {
				y = 3.75 / ax;
				ans = 0.2282967e-1 + y * (-0.2895312e-1 + y * (0.1787654e-1
				- y * 0.420059e-2));
				ans = 0.39894228 + y * (-0.3988024e-1 + y * (-0.362018e-2
				+ y * (0.163801e-2 + y * (-0.1031555e-1 + y * ans))));
				ans *= (Math.Exp(ax) / Math.Sqrt(ax));
			}
			return x < 0.0 ? -ans : ans;
		}

		private static double BesselI0(double x)
		{
			double ax, ans;
			double y;
			if((ax = Math.Abs(x)) < 3.75) {
				y = x / 3.75;
				y *= y;
				ans = 1.0 + y * (3.5156229 + y * (3.0899424 + y * (1.2067492 + y * (0.2659732 + y * (0.360768e-1 + y * 0.45813e-2)))));
			} else {
				y = 3.75 / ax;
				ans = (Math.Exp(ax) / Math.Sqrt(ax)) * (0.39894228 + y * (0.1328592e-1
				+ y * (0.225319e-2 + y * (-0.157565e-2 + y * (0.916281e-2
				+ y * (-0.2057706e-1 + y * (0.2635537e-1 + y * (-0.1647633e-1
				+ y * 0.392377e-2))))))));
			}
			return ans;
		}

		/// <summary>
		/// The modified Bessel function of the second kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="order">The order.</param>
		public static double BesselK(double x, double order)
		{
			int n;
			if((order % 1) == 0) // integer order
                n = (int)order;
			else {
				// if fractional we need to use another algorithm
				double res, ink, jp, yp;
				bessik(x, order, out ink, out res, out jp, out yp);
				return res;
			}
			if(n == 0)
				return BesselK0(x);
			if(n == 1)
				return BesselK1(x);
			int j;

			var tox = 2.0 / x;
			var bkm = BesselK0(x);
			var bk = BesselK1(x);
			for(j = 1; j < n; j++) {
				var bkp = bkm + j * tox * bk;
				bkm = bk;
				bk = bkp;
			}
			return bk;
		}

		/// <summary>
		/// The Bessel function of the second kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="order">The order.</param>
		public static double BesselY(double x, double order)
		{
			int n;
			if((order % 1) == 0) // integer order
                n = (int)order;
			else {
				// if fractional we need to use another algorithm
				double res, jn, jp, yp;
				bessjy(x, order, out jn, out res, out jp, out yp);
				return res;
			}
			// Numerical recipes in C p.234
			if(n == 0)
				return BesselY0(x);
			if(n == 1)
				return BesselY1(x);

			int j;
			double bym, byp, tox;

			tox = 2.0 / x;
			double @by = BesselY1(x);
			bym = BesselY0(x);
			for(j = 1; j < n; j++) {
				byp = j * tox * by - bym;
				bym = by;
				by = byp;
			}
			return by;
		}

		/// <summary>
		/// The spherical Bessel function of the first kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="n">The order.</param>
		public static double SphericalBesselJ(double x, int n)
		{
			double sy;
			double res;
			double sjp;
			double syp;
			sphbes(n, x, out res, out sy, out sjp, out syp);
			return res;
		}

		/// <summary>
		/// The spherical Bessel function of the second kind.
		/// </summary>
		/// <remarks>http://en.wikipedia.org/wiki/Bessel_function</remarks>
		/// <param name="x">The argument.</param>
		/// <param name="n">The order.</param>
		public static double SphericalBesselY(double x, int n)
		{
			double sj;
			double res;
			double sjp;
			double syp;
			sphbes(n, x, out sj, out res, out sjp, out syp);
			return res;
		}

		/// <summary>
		/// The sign of the second argument set the sign of the returned first argument.
		/// </summary>
		/// <param name="a">A number.</param>
		/// <param name="b">A number.</param>
		static double Sign2(double a, double b)
		{
			return b >= 0.0 ? Math.Abs(a) : -Math.Abs(a);
		}

		static double chebev(double a, double b, double[] c, int m, double x)
		{

			double d = 0.0, dd = 0.0, sv, y, y2;
			int j;

			if((x - a) * (x - b) > 0.0)
				throw new Exception("Argument x in chebev is outside the computational range.");
			y2 = 2.0 * (y = (2.0 * x - a - b) / (b - a));
			for(j = m - 1; j >= 1; j--) {
				sv = d;
				d = y2 * d - dd + c[j];
				dd = sv;
			}
			return y * d - dd + 0.5 * c[0];
		}

		static void beschb(double x, out double gam1, out double gam2, out double gampl, out  double gammi)
		{
			const int NUSE1 = 5;
			const int NUSE2 = 5;
			double xx;
			var c1 = new[] {
				-1.142022680371172e0, 6.516511267076e-3,
				3.08709017308e-4, -3.470626964e-6, 6.943764e-9,
				3.6780e-11, -1.36e-13
			};
			var c2 = new[] {
				1.843740587300906e0, -0.076852840844786e0,
				1.271927136655e-3, -4.971736704e-6, -3.3126120e-8,
				2.42310e-10, -1.70e-13, -1.0e-15
			};

			xx = 8.0 * x * x - 1.0;
			gam1 = chebev(-1.0, 1.0, c1, NUSE1, xx);
			gam2 = chebev(-1.0, 1.0, c2, NUSE2, xx);
			gampl = gam2 - x * (gam1);
			gammi = gam2 + x * (gam1);
		}

		/// <summary>
		/// Bessel J and Y for fractional order.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="xnu"></param>
		/// <param name="rj"></param>
		/// <param name="ry"></param>
		/// <param name="rjp"></param>
		/// <param name="ryp"></param>
		static void bessjy(double x, double xnu, out double rj, out  double ry, out  double rjp, out double ryp)
		{
			const double XMIN = 2.0;
			const int MAXIT = 10000;
			const double FPMIN = 1.0e-30;
			const double EPS = 1.0e-10;
			int i, isign, l, nl;
			double a, b, br, bi, c, cr, ci, d, del, del1, den, di, dlr, dli, dr, e, f, fact, fact2,
			fact3, ff, gam, gam1, gam2, gammi, gampl, h, p, pimu, pimu2, q, r, rjl,
			rjl1, rjmu, rjp1, rjpl, rjtemp, ry1, rymu, rymup, rytemp, sum, sum1,
			temp, w, x2, xi, xi2, xmu, xmu2;

			if(x <= 0.0 || xnu < 0.0)
				throw new Exception("Value and order should be positive.");
			nl = (x < XMIN ? (int)(xnu + 0.5) : Math.Max(0, (int)(xnu - x + 1.5)));
			xmu = xnu - nl;
			xmu2 = xmu * xmu;
			xi = 1.0 / x;
			xi2 = 2.0 * xi;
			w = xi2 / Math.PI;
			isign = 1;
			h = xnu * xi;
			if(h < FPMIN)
				h = FPMIN;
			b = xi2 * xnu;
			d = 0.0;
			c = h;
			for(i = 1; i <= MAXIT; i++) {
				b += xi2;
				d = b - d;
				if(Math.Abs(d) < FPMIN)
					d = FPMIN;
				c = b - 1.0 / c;
				if(Math.Abs(c) < FPMIN)
					c = FPMIN;
				d = 1.0 / d;
				del = c * d;
				h = del * h;
				if(d < 0.0)
					isign = -isign;
				if(Math.Abs(del - 1.0) < EPS)
					break;
			}
			if(i > MAXIT)
				throw new Exception("x too large in bessjy; try asymptotic expansion");
			rjl = isign * FPMIN;
			rjpl = h * rjl;
			rjl1 = rjl;
			rjp1 = rjpl;
			fact = xnu * xi;
			for(l = nl; l >= 1; l--) {
				rjtemp = fact * rjl + rjpl;
				fact -= xi;
				rjpl = fact * rjtemp - rjl;
				rjl = rjtemp;
			}
			if(rjl == 0.0)
				rjl = EPS;
			f = rjpl / rjl;
			if(x < XMIN) {
				x2 = 0.5 * x;
				pimu = Math.PI * xmu;
				fact = (Math.Abs(pimu) < EPS ? 1.0 : pimu / Math.Sin(pimu));
				d = -Math.Log(x2);
				e = xmu * d;
				fact2 = (Math.Abs(e) < EPS ? 1.0 : Math.Sinh(e) / e);
				beschb(xmu, out gam1, out  gam2, out gampl, out  gammi);
				ff = 2.0 / Math.PI * fact * (gam1 * Math.Cosh(e) + gam2 * fact2 * d);
				e = Math.Exp(e);
				p = e / (gampl * Math.PI);
				q = 1.0 / (e * Math.PI * gammi);
				pimu2 = 0.5 * pimu;
				fact3 = (Math.Abs(pimu2) < EPS ? 1.0 : Math.Sin(pimu2) / pimu2);
				r = Math.PI * pimu2 * fact3 * fact3;
				c = 1.0;
				d = -x2 * x2;
				sum = ff + r * q;
				sum1 = p;
				for(i = 1; i <= MAXIT; i++) {
					ff = (i * ff + p + q) / (i * i - xmu2);
					c *= (d / i);
					p /= (i - xmu);
					q /= (i + xmu);
					del = c * (ff + r * q);
					sum += del;
					del1 = c * p - i * del;
					sum1 += del1;
					if(Math.Abs(del) < (1.0 + Math.Abs(sum)) * EPS)
						break;
				}
				if(i > MAXIT)
					throw new Exception("bessy series failed to converge");
				rymu = -sum;
				ry1 = -sum1 * xi2;
				rymup = xmu * xi * rymu - ry1;
				rjmu = w / (rymup - f * rymu);
			} else {
				a = 0.25 - xmu2;
				p = -0.5 * xi;
				q = 1.0;
				br = 2.0 * x;
				bi = 2.0;
				fact = a * xi / (p * p + q * q);
				cr = br + q * fact;
				ci = bi + p * fact;
				den = br * br + bi * bi;
				dr = br / den;
				di = -bi / den;
				dlr = cr * dr - ci * di;
				dli = cr * di + ci * dr;
				temp = p * dlr - q * dli;
				q = p * dli + q * dlr;
				p = temp;
				for(i = 2; i <= MAXIT; i++) {
					a += 2 * (i - 1);
					bi += 2.0;
					dr = a * dr + br;
					di = a * di + bi;
					if(Math.Abs(dr) + Math.Abs(di) < FPMIN)
						dr = FPMIN;
					fact = a / (cr * cr + ci * ci);
					cr = br + cr * fact;
					ci = bi - ci * fact;
					if(Math.Abs(cr) + Math.Abs(ci) < FPMIN)
						cr = FPMIN;
					den = dr * dr + di * di;
					dr /= den;
					di /= -den;
					dlr = cr * dr - ci * di;
					dli = cr * di + ci * dr;
					temp = p * dlr - q * dli;
					q = p * dli + q * dlr;
					p = temp;
					if(Math.Abs(dlr - 1.0) + Math.Abs(dli) < EPS)
						break;
				}
				if(i > MAXIT)
					throw new Exception("cf2 failed in bessjy");
				gam = (p - f) / q;
				rjmu = Math.Sqrt(w / ((p - f) * gam + q));
				rjmu = Sign2(rjmu, rjl);
				rymu = rjmu * gam;
				rymup = rymu * (p + q / gam);
				ry1 = xmu * xi * rymu - rymup;
			}
			fact = rjmu / rjl;
			rj = rjl1 * fact;
			rjp = rjp1 * fact;
			for(i = 1; i <= nl; i++) {
				rytemp = (xmu + i) * xi2 * ry1 - rymu;
				rymu = ry1;
				ry1 = rytemp;
			}
			ry = rymu;
			ryp = xnu * xi * rymu - ry1;
		}

		/// <summary>
		/// Bessel I and K for fractional order.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="xnu"></param>
		/// <param name="ri"></param>
		/// <param name="rk"></param>
		/// <param name="rip"></param>
		/// <param name="rkp"></param>
		static void bessik(double x, double xnu, out double ri, out double rk, out double rip, out  double rkp)
		{
			const double XMIN = 2.0;
			const int MAXIT = 10000;
			const double FPMIN = 1.0e-30;
			const double EPS = 1.0e-10;

			int i, l, nl;
			double a, a1, b, c, d, del, del1, delh, dels, e, f, fact, fact2, ff, gam1, gam2,
			gammi, gampl, h, p, pimu, q, q1, q2, qnew, ril, ril1, rimu, rip1, ripl,
			ritemp, rk1, rkmu, rkmup, rktemp, s, sum, sum1, x2, xi, xi2, xmu, xmu2;

			if(x <= 0.0 || xnu < 0.0)
				throw new Exception("bad arguments in bessik");
			nl = (int)(xnu + 0.5);
			xmu = xnu - nl;
			xmu2 = xmu * xmu;
			xi = 1.0 / x;
			xi2 = 2.0 * xi;
			h = xnu * xi;
			if(h < FPMIN)
				h = FPMIN;
			b = xi2 * xnu;
			d = 0.0;
			c = h;
			for(i = 1; i <= MAXIT; i++) {
				b += xi2;
				d = 1.0 / (b + d);
				c = b + 1.0 / c;
				del = c * d;
				h = del * h;
				if(Math.Abs(del - 1.0) < EPS)
					break;
			}
			if(i > MAXIT)
				throw new Exception("x too large in bessik; try asymptotic expansion");
			ril = FPMIN;
			ripl = h * ril;
			ril1 = ril;
			rip1 = ripl;
			fact = xnu * xi;
			for(l = nl; l >= 1; l--) {
				ritemp = fact * ril + ripl;
				fact -= xi;
				ripl = fact * ritemp + ril;
				ril = ritemp;
			}
			f = ripl / ril;
			if(x < XMIN) {
				x2 = 0.5 * x;
				pimu = Math.PI * xmu;
				fact = (Math.Abs(pimu) < EPS ? 1.0 : pimu / Math.Sin(pimu));
				d = -Math.Log(x2);
				e = xmu * d;
				fact2 = (Math.Abs(e) < EPS ? 1.0 : Math.Sinh(e) / e);
				beschb(xmu, out gam1, out gam2, out gampl, out gammi);
				ff = fact * (gam1 * Math.Cosh(e) + gam2 * fact2 * d);
				sum = ff;
				e = Math.Exp(e);
				p = 0.5 * e / gampl;
				q = 0.5 / (e * gammi);
				c = 1.0;
				d = x2 * x2;
				sum1 = p;
				for(i = 1; i <= MAXIT; i++) {
					ff = (i * ff + p + q) / (i * i - xmu2);
					c *= (d / i);
					p /= (i - xmu);
					q /= (i + xmu);
					del = c * ff;
					sum += del;
					del1 = c * (p - i * ff);
					sum1 += del1;
					if(Math.Abs(del) < Math.Abs(sum) * EPS)
						break;
				}
				if(i > MAXIT)
					throw new Exception("bessk series fails to converge");
				rkmu = sum;
				rk1 = sum1 * xi2;
			} else {
				b = 2.0 * (1.0 + x);
				d = 1.0 / b;
				h = delh = d;
				q1 = 0.0;
				q2 = 1.0;
				a1 = 0.25 - xmu2;
				q = c = a1;
				a = -a1;
				s = 1.0 + q * delh;
				for(i = 2; i <= MAXIT; i++) {
					a -= 2 * (i - 1);
					c = -a * c / i;
					qnew = (q1 - b * q2) / a;
					q1 = q2;
					q2 = qnew;
					q += c * qnew;
					b += 2.0;
					d = 1.0 / (b + a * d);
					delh = (b * d - 1.0) * delh;
					h += delh;
					dels = q * delh;
					s += dels;
					if(Math.Abs(dels / s) < EPS)
						break;
				}
				if(i > MAXIT)
					throw new Exception("bessik: failure to converge in cf2");
				h = a1 * h;
				rkmu = Math.Sqrt(Math.PI / (2.0 * x)) * Math.Exp(-x) / s;
				rk1 = rkmu * (xmu + x + 0.5 - h) * xi;
			}
			rkmup = xmu * xi * rkmu - rk1;
			rimu = xi / (f * rkmu - rkmup);
			ri = (rimu * ril1) / ril;
			rip = (rimu * rip1) / ril;
			for(i = 1; i <= nl; i++) {
				rktemp = (xmu + i) * xi2 * rk1 + rkmu;
				rkmu = rk1;
				rk1 = rktemp;
			}
			rk = rkmu;
			rkp = xnu * xi * rkmu - rk1;
		}

		static void sphbes(int n, double x, out double sj, out double sy, out double sjp, out double syp)
		{

			const double RTPIO2 = 1.2533141;
			double factor, order, rj, rjp, ry, ryp;

			if(n < 0 || x <= 0.0)
				throw new Exception("bad arguments in sphbes");
			order = n + 0.5;
			bessjy(x, order, out rj, out ry, out rjp, out ryp);
			factor = RTPIO2 / Math.Sqrt(x);
			sj = factor * rj;
			sy = factor * ry;
			sjp = factor * rjp - (sj) / (2.0 * x);
			syp = factor * ryp - (sy) / (2.0 * x);
		}
	}

	public interface IFunctional
	{
	
		double Compute(double x);

		double Derivative(double x);

		Vector Compute(Vector x);

		Vector Derivative(Vector x);
	}

	public abstract class Functional : IFunctional
	{

		public abstract double Compute(double x);

		public abstract double Derivative(double x);

		public Vector Compute(Vector x)
		{
			return  x.Each(Compute, true);
		}

		public Vector Derivative(Vector x)
		{
			return  x.Each(Derivative, true);
		}
	}

	public class Tanh : Functional
	{
		private static Tanh instance;

		private Tanh()
		{
		}

		public static Tanh Instance {
			get {
				if(instance == null) {
					instance = new Tanh();
				}
				return instance;
			}
		}

		/// <summary>Computes the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Compute(double x)
		{
			return Trigonometry.HyperbolicTangent(x);
		}

		/// <summary>Derivatives the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Derivative(double x)
		{
			return 1 - Math.Pow(Compute(x), 2);
		}
	}

	public class Identity : Functional
	{
		private static Identity instance;

		private Identity()
		{
		}

		public static Identity Instance {
			get {
				if(instance == null) {
					instance = new Identity();
				}
				return instance;
			}
		}

		/// <summary>Computes the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Compute(double x)
		{
			return x;
		}

		/// <summary>Derivatives the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Derivative(double x)
		{
			return 1;
		}
	}

	public class Logistic : Functional
	{
		private static Logistic instance;

		private Logistic()
		{
		}

		public static Logistic Instance {
			get {
				if(instance == null) {
					instance = new Logistic();
				}
				return instance;
			}
		}

		/// <summary>Computes the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Compute(double x)
		{
			return 1d / (1d + Math.Exp(-x));
		}

		/// <summary>Derivatives the given x coordinate.</summary>
		/// <param name="x">The Vector to process.</param>
		/// <returns>A Vector.</returns>
		public override double Derivative(double x)
		{
			var c = Compute(x);
			return c * (1d - c);
		}
	}
}