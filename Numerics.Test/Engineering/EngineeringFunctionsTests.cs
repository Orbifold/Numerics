#if SILVERLIGHT
using Microsoft.Silverlight.Testing;
#endif
using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Engineering
{
	[TestFixture]
	public class EngineeringFunctionsTests
	{
		private const double Accuracy = 1E-6;

		[Test]
#if SILVERLIGHT 
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Oct2HexTest()
		{
			Assert.AreEqual("0000000F2C", EngineeringFunctions.OCT2HEX("7454", 10));
			Assert.AreEqual("6A53", EngineeringFunctions.OCT2HEX("65123", 4));
			Assert.AreEqual("0002EA53", EngineeringFunctions.OCT2HEX("565123", 8));
			Assert.AreEqual("FFFFFFFFFF", EngineeringFunctions.OCT2HEX("7777777777"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Oct2DecTest()
		{
			Assert.AreEqual(134217563, EngineeringFunctions.OCT2DEC("777777533"));
			Assert.AreEqual(44, EngineeringFunctions.OCT2DEC("54"));
			Assert.AreEqual(-28, EngineeringFunctions.OCT2DEC("7777777744"));
			Assert.AreEqual(-78451228, EngineeringFunctions.OCT2DEC("7324566744"));
			Assert.AreEqual(-14745628, EngineeringFunctions.OCT2DEC("7707577744"));
			Assert.AreEqual(4128740, EngineeringFunctions.OCT2DEC("17577744"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Oct2BinTest()
		{
			Assert.AreEqual("011", EngineeringFunctions.OCT2BIN("3", 3));
			Assert.AreEqual("1000000000", EngineeringFunctions.OCT2BIN("7777777000"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Dec2BinTest()
		{
			Assert.AreEqual("1110011100", EngineeringFunctions.DEC2BIN(-100));
			Assert.AreEqual("00000", EngineeringFunctions.DEC2BIN(0, 5));
			Assert.AreEqual("111110110", EngineeringFunctions.DEC2BIN(502.784512));
			Assert.AreEqual("1000011011", EngineeringFunctions.DEC2BIN(-485.123456));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Bin2DecTest()
		{
			Assert.AreEqual(100, EngineeringFunctions.BIN2DEC("1100100"));
			Assert.AreEqual(-1, EngineeringFunctions.BIN2DEC("1111111111"));
			Assert.AreEqual(-257, EngineeringFunctions.BIN2DEC("1011111111"));
			Assert.AreEqual(383, EngineeringFunctions.BIN2DEC("101111111"));
			Assert.AreEqual(-293, EngineeringFunctions.BIN2DEC("1011011011"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Bin2HexTest()
		{
			Assert.AreEqual("00FB", EngineeringFunctions.BIN2HEX("11111011", 4));
			Assert.AreEqual("E", EngineeringFunctions.BIN2HEX("1110"));
			Assert.AreEqual("FFFFFFFFFF", EngineeringFunctions.BIN2HEX("1111111111"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Bin2OctTest()
		{
			Assert.AreEqual("011", EngineeringFunctions.BIN2OCT("1001", 3));
			Assert.AreEqual("144", EngineeringFunctions.BIN2OCT("1100100"));
			Assert.AreEqual("7777777777", EngineeringFunctions.BIN2OCT("1111111111"));
			Assert.AreEqual("677", EngineeringFunctions.BIN2OCT("110111111"));
			Assert.AreEqual("7777777573", EngineeringFunctions.BIN2OCT("1101111011"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Dec2OctTest()
		{
			Assert.AreEqual("0000000034", EngineeringFunctions.DEC2OCT(28, 10));
			Assert.AreEqual("7777777744", EngineeringFunctions.DEC2OCT(-28, 10));
			Assert.AreEqual("7324566744", EngineeringFunctions.DEC2OCT(-78451228, 10));
			Assert.AreEqual("0000000005", EngineeringFunctions.DEC2OCT(5.332211, 10));
			Assert.AreEqual("0000000021", EngineeringFunctions.DEC2OCT(17.889966, 10));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Hex2OctTest()
		{
			Assert.AreEqual("7777777400", EngineeringFunctions.HEX2OCT("FFFFFFFF00", 7));
			Assert.AreEqual("0000035516", EngineeringFunctions.HEX2OCT("3B4E", 10));
			Assert.AreEqual("0002536336", EngineeringFunctions.HEX2OCT("ABCDE", 10));
			Assert.AreEqual("2536336", EngineeringFunctions.HEX2OCT("ABCDE"));
			Assert.AreEqual("1623447116", EngineeringFunctions.HEX2OCT("e4e4e4e", 10));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Hex2BinTest()
		{
			Assert.AreEqual("00001111", EngineeringFunctions.HEX2BIN("F", 8));
			Assert.AreEqual("10110111", EngineeringFunctions.HEX2BIN("B7"));
			Assert.AreEqual("1111111111", EngineeringFunctions.HEX2BIN("FFFFFFFFFF"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Dec2HexTest()
		{
			Assert.AreEqual("0064", EngineeringFunctions.DEC2HEX(100, 4));
			Assert.AreEqual("FFFFFFFFCA", EngineeringFunctions.DEC2HEX(-54));
			Assert.AreEqual("1C", EngineeringFunctions.DEC2HEX(28));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void Hex2DecTest()
		{
			Assert.AreEqual(165, EngineeringFunctions.HEX2DEC("A5"));
			Assert.AreEqual(-165, EngineeringFunctions.HEX2DEC("FFFFFFFF5B"));
			Assert.AreEqual(1034160313, EngineeringFunctions.HEX2DEC("3DA408B9"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void BitorTest()
		{
			Assert.AreEqual(44, EngineeringFunctions.BITOR(12, 44));
			Assert.AreEqual(31, EngineeringFunctions.BITOR(23, 10));
			Assert.AreEqual(127559, EngineeringFunctions.BITOR(123456, 111111));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void BitxorTest()
		{
			Assert.AreEqual(6, EngineeringFunctions.BITXOR(5, 3));
			Assert.AreEqual(20551, EngineeringFunctions.BITXOR(123456, 111111));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void BitshiftsTest()
		{
			Assert.AreEqual(1802240, EngineeringFunctions.BITLSHIFT(55, 15));
			Assert.AreEqual(4016640, EngineeringFunctions.BITLSHIFT(7845, 9));
			Assert.AreEqual(241, EngineeringFunctions.BITLSHIFT(123456, -9));

			Assert.AreEqual(0, EngineeringFunctions.BITRSHIFT(55, 15));
			Assert.AreEqual(15, EngineeringFunctions.BITRSHIFT(7845, 9));
			Assert.AreEqual(64726499328, EngineeringFunctions.BITRSHIFT(123456, -19));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void BitAndTest()
		{
			Assert.AreEqual(39, EngineeringFunctions.BITAND(111, 951));
			Assert.AreEqual(34834, EngineeringFunctions.BITAND(101010, 445566));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Engineering")]
#else
		[Category("Engineering")]
		#endif
        public void ConvertTest()
		{
			// area
			Assert.AreEqual(-1.50864292791368E-05, EngineeringFunctions.CONVERT(-420.585510015488, "ft2", "mi2"), Accuracy);
			Assert.AreEqual(-733.939129183053, EngineeringFunctions.CONVERT(-971.950699269772, "mi2", "Nmi2"), Accuracy);
			Assert.AreEqual(169.476211062459, EngineeringFunctions.CONVERT(418.784837841988, "uk_acre", "ha"), Accuracy);
			Assert.AreEqual(249045.775389244, EngineeringFunctions.CONVERT(726.101300179958, "Nmi2", "ha"), Accuracy);
			Assert.AreEqual(2.84422879234204E-08, EngineeringFunctions.CONVERT(924.868279457092, "Pica2", "uk_acre"), Accuracy);
			Assert.AreEqual(1.00559591061956E-25, EngineeringFunctions.CONVERT(900.062854111195, "ha", "ly2"), Accuracy);
			Assert.AreEqual(2165029.45659285, EngineeringFunctions.CONVERT(534.990429759026, "uk_acre", "m2"), Accuracy);
			Assert.AreEqual(-5.77216603066395, EngineeringFunctions.CONVERT(-62.1310780644417, "ft2", "m2"), Accuracy);
			Assert.AreEqual(296.290197134018, EngineeringFunctions.CONVERT(296.290197134018, "ar", "ar"), Accuracy);

			// distance
			Assert.AreEqual(-971.948755368373, EngineeringFunctions.CONVERT(-971.950699269772, "mi", "survey_mi"), Accuracy);
			Assert.AreEqual(4.18784837841988E-08, EngineeringFunctions.CONVERT(418.784837841988, "ang", "m"), Accuracy);
			Assert.AreEqual(1168549.10793503, EngineeringFunctions.CONVERT(726.101300179958, "survey_mi", "m"), Accuracy);
			Assert.AreEqual(2.91690505700008E-14, EngineeringFunctions.CONVERT(900.062854111195, "m", "parsec"), Accuracy);
			Assert.AreEqual(1.26375692069061E-05, EngineeringFunctions.CONVERT(534.990429759026, "ang", "pica"), Accuracy);
			Assert.AreEqual(296.290197134018, EngineeringFunctions.CONVERT(296.290197134018, "in", "in"), Accuracy);

			// energy
			Assert.AreEqual(0.529191514796743, EngineeringFunctions.CONVERT(411.80057066679, "flb", "BTU"), Accuracy);
			Assert.AreEqual(411.80057066679, EngineeringFunctions.CONVERT(411.80057066679, "HPh", "HPh"), Accuracy);
			Assert.AreEqual(-1298.77865645604, EngineeringFunctions.CONVERT(-420.585510015488, "cal", "flb"), Accuracy);
			Assert.AreEqual(-1.24902221971545, EngineeringFunctions.CONVERT(-971.950699269772, "flb", "BTU"), Accuracy);
			Assert.AreEqual(235.134512059509, EngineeringFunctions.CONVERT(726.101300179958, "flb", "cal"), Accuracy);
			Assert.AreEqual(975787.691143795, EngineeringFunctions.CONVERT(924.868279457092, "BTU", "J"), Accuracy);
			Assert.AreEqual(1.40374584899715E-03, EngineeringFunctions.CONVERT(900.062854111195, "cal", "HPh"), Accuracy);
			Assert.AreEqual(1.99287217785775E-04, EngineeringFunctions.CONVERT(534.990429759026, "J", "HPh"), Accuracy);
			Assert.AreEqual(-7.22101196171178E-02, EngineeringFunctions.CONVERT(-62.1310780644417, "c", "Wh"), Accuracy);
			Assert.AreEqual(296.290197134018, EngineeringFunctions.CONVERT(296.290197134018, "c", "c"), Accuracy);

			// force
			Assert.AreEqual(1.4560056195937, EngineeringFunctions.CONVERT(660.433039724827, "pond", "lbf"), Accuracy);
			Assert.AreEqual(9.54356131400595, EngineeringFunctions.CONVERT(973.172420144081, "pond", "N"), Accuracy);
			Assert.AreEqual(3.83367572104245, EngineeringFunctions.CONVERT(390.926128804684, "pond", "N"), Accuracy);
			Assert.AreEqual(6962.62592177329, EngineeringFunctions.CONVERT(68.2800354957581, "N", "pond"), Accuracy);
			Assert.AreEqual(79.363842706788, EngineeringFunctions.CONVERT(353.027960598469, "N", "lbf"), Accuracy);
			Assert.AreEqual(-81556.4410620474, EngineeringFunctions.CONVERT(-799.795472741127, "N", "pond"), Accuracy);
			Assert.AreEqual(-43075497.2279072, EngineeringFunctions.CONVERT(-430.754972279072, "N", "dyn"), Accuracy);
			Assert.AreEqual(-0.24024166526257, EngineeringFunctions.CONVERT(-235.596592664719, "dyn", "pond"), Accuracy);
			Assert.AreEqual(960.638571321964, EngineeringFunctions.CONVERT(960.638571321964, "dyn", "dyn"), Accuracy);
			Assert.AreEqual(-152.635498306354, EngineeringFunctions.CONVERT(-678.95652282238, "N", "lbf"), Accuracy);

			// mass
			Assert.AreEqual(-200.976703557968, EngineeringFunctions.CONVERT(-179.443485319614, "uk_cwt", "cwt"), Accuracy);
			Assert.AreEqual(-157515.112019822, EngineeringFunctions.CONVERT(-347.261379241943, "lbm", "g"), Accuracy);
			Assert.AreEqual(-530878.929292649, EngineeringFunctions.CONVERT(-83.5991212129593, "stone", "g"), Accuracy);
			Assert.AreEqual(28.5604824870825, EngineeringFunctions.CONVERT(571.20964974165, "uk_cwt", "uk_ton"), Accuracy);
			Assert.AreEqual(839.673554182053, EngineeringFunctions.CONVERT(839.673554182053, "lbm", "lbm"), Accuracy);
			Assert.AreEqual(-2.03798302156585E-02, EngineeringFunctions.CONVERT(-142.658811509609, "grain", "lbm"), Accuracy);
			Assert.AreEqual(55666.4426837649, EngineeringFunctions.CONVERT(389.665098786354, "ton", "stone"), Accuracy);
			Assert.AreEqual(-2.98349677491933E-02, EngineeringFunctions.CONVERT(-954.718967974186, "ozm", "ton"), Accuracy);
			Assert.AreEqual(-222475.949859619, EngineeringFunctions.CONVERT(-139.047468662262, "cwt", "ozm"), Accuracy);

			// power
			Assert.AreEqual(27.9887369275093, EngineeringFunctions.CONVERT(27.9887369275093, "PS", "PS"), Accuracy);
			Assert.AreEqual(-187.328677235149, EngineeringFunctions.CONVERT(-189.926863312721, "PS", "HP"), Accuracy);
			Assert.AreEqual(411.80057066679, EngineeringFunctions.CONVERT(411.80057066679, "PS", "PS"), Accuracy);
			Assert.AreEqual(-313630.560807913, EngineeringFunctions.CONVERT(-420.585510015488, "HP", "W"), Accuracy);
			Assert.AreEqual(-971.950699269772, EngineeringFunctions.CONVERT(-971.950699269772, "W", "W"), Accuracy);
			Assert.AreEqual(424.5932434275, EngineeringFunctions.CONVERT(418.784837841988, "HP", "PS"), Accuracy);
			Assert.AreEqual(0.987223024077143, EngineeringFunctions.CONVERT(726.101300179958, "W", "PS"), Accuracy);
			Assert.AreEqual(1.24026879271771, EngineeringFunctions.CONVERT(924.868279457092, "W", "HP"), Accuracy);
			Assert.AreEqual(900.062854111195, EngineeringFunctions.CONVERT(900.062854111195, "PS", "PS"), Accuracy);
			Assert.AreEqual(542.410568024825, EngineeringFunctions.CONVERT(534.990429759026, "HP", "PS"), Accuracy);
			Assert.AreEqual(-62.9928153296279, EngineeringFunctions.CONVERT(-62.1310780644417, "HP", "PS"), Accuracy);
			Assert.AreEqual(296.290197134018, EngineeringFunctions.CONVERT(296.290197134018, "HP", "HP"), Accuracy);

			// pressure
			Assert.AreEqual(660.434864760722, EngineeringFunctions.CONVERT(660.433039724827, "Torr", "mmHg"), Accuracy);
			Assert.AreEqual(1.28049002650537, EngineeringFunctions.CONVERT(973.172420144081, "Torr", "atm"), Accuracy);
			Assert.AreEqual(0.514376485269321, EngineeringFunctions.CONVERT(390.926128804684, "Torr", "atm"), Accuracy);
			Assert.AreEqual(0.512142383190487, EngineeringFunctions.CONVERT(68.2800354957581, "Pa", "Torr"), Accuracy);
			Assert.AreEqual(2.64793477894473, EngineeringFunctions.CONVERT(353.027960598469, "Pa", "mmHg"), Accuracy);
			Assert.AreEqual(-0.116000526013236, EngineeringFunctions.CONVERT(-799.795472741127, "Pa", "psi"), Accuracy);
			Assert.AreEqual(-4.25122104395827E-03, EngineeringFunctions.CONVERT(-430.754972279072, "Pa", "atm"), Accuracy);
			Assert.AreEqual(-179053.410425186, EngineeringFunctions.CONVERT(-235.596592664719, "atm", "Torr"), Accuracy);
			Assert.AreEqual(1.26399462724685, EngineeringFunctions.CONVERT(960.638571321964, "mmHg", "atm"), Accuracy);
			Assert.AreEqual(-9.84743180873272E-02, EngineeringFunctions.CONVERT(-678.95652282238, "Pa", "psi"), Accuracy);

			// speed
			Assert.AreEqual(-4.98454125887818E-02, EngineeringFunctions.CONVERT(-179.443485319614, "m/h", "m/s"), Accuracy);
			Assert.AreEqual(-675.02211947678, EngineeringFunctions.CONVERT(-347.261379241943, "m/s", "kn"), Accuracy);
			Assert.AreEqual(-0.338761260054233, EngineeringFunctions.CONVERT(-627.786946952343, "m/h", "admkn"), Accuracy);
			Assert.AreEqual(-72.6456501778341, EngineeringFunctions.CONVERT(-83.5991212129593, "mph", "kn"), Accuracy);
			Assert.AreEqual(571.20964974165, EngineeringFunctions.CONVERT(571.20964974165, "kn", "kn"), Accuracy);
			Assert.AreEqual(839.673554182053, EngineeringFunctions.CONVERT(839.673554182053, "m/s", "m/s"), Accuracy);
			Assert.AreEqual(-264373.026948624, EngineeringFunctions.CONVERT(-142.658811509609, "admkn", "m/h"), Accuracy);
			Assert.AreEqual(389.665098786354, EngineeringFunctions.CONVERT(389.665098786354, "mph", "mph"), Accuracy);
			Assert.AreEqual(-0.593234863381717, EngineeringFunctions.CONVERT(-954.718967974186, "m/h", "mph"), Accuracy);
			Assert.AreEqual(-500570.887184143, EngineeringFunctions.CONVERT(-139.047468662262, "m/s", "m/h"), Accuracy);

			// temperature
			Assert.AreEqual(-409.290273530483, EngineeringFunctions.CONVERT(27.9887369275093, "K", "F"), Accuracy);
			Assert.AreEqual(-123.292701840401, EngineeringFunctions.CONVERT(-189.926863312721, "F", "C"), Accuracy);
			Assert.AreEqual(-640.082281604409, EngineeringFunctions.CONVERT(-512.065825283527, "Reau", "C"), Accuracy);
			Assert.AreEqual(133.690664593379, EngineeringFunctions.CONVERT(-219.026803731918, "F", "K"), Accuracy);
			Assert.AreEqual(-1699.00274344206, EngineeringFunctions.CONVERT(-688.518190801144, "K", "F"), Accuracy);
			Assert.AreEqual(-15.0175001859665, EngineeringFunctions.CONVERT(258.132499814034, "K", "C"), Accuracy);
			Assert.AreEqual(487.7938222554, EngineeringFunctions.CONVERT(878.028880059719, "Rank", "K"), Accuracy);
			Assert.AreEqual(98.070098991394, EngineeringFunctions.CONVERT(-218.666611671448, "C", "Rank"), Accuracy);
			Assert.AreEqual(-44.5881984962357, EngineeringFunctions.CONVERT(-80.2587572932243, "Rank", "K"), Accuracy);
			Assert.AreEqual(1231.32754094601, EngineeringFunctions.CONVERT(666.293078303337, "C", "F"), Accuracy);

			// time
			Assert.AreEqual(-311200.080603495, EngineeringFunctions.CONVERT(-852.019385635853, "yr", "d"), Accuracy);
			Assert.AreEqual(-6516398.80133772, EngineeringFunctions.CONVERT(-743.371982812881, "yr", "hr"), Accuracy);
			Assert.AreEqual(1133161.115098, EngineeringFunctions.CONVERT(314.766976416111, "hr", "sec"), Accuracy);
			Assert.AreEqual(-439771450.185885, EngineeringFunctions.CONVERT(-836.130979895592, "yr", "min"), Accuracy);
			Assert.AreEqual(-0.249500662557561, EngineeringFunctions.CONVERT(-91.1301169991493, "d", "yr"), Accuracy);
			Assert.AreEqual(0.113748871617847, EngineeringFunctions.CONVERT(409.495937824249, "sec", "hr"), Accuracy);
			Assert.AreEqual(-13.6771262357632, EngineeringFunctions.CONVERT(-820.627574145794, "min", "hr"), Accuracy);
			Assert.AreEqual(-3.15789141754309, EngineeringFunctions.CONVERT(-75.7893940210342, "hr", "d"), Accuracy);
			Assert.AreEqual(-2982179.12703359, EngineeringFunctions.CONVERT(-340.198394596577, "yr", "hr"), Accuracy);
			Assert.AreEqual(-2.0916773597076E-05, EngineeringFunctions.CONVERT(-660.083174467087, "sec", "yr"), Accuracy);

			// volume
			Assert.AreEqual(22485.5190127118, EngineeringFunctions.CONVERT(292.548612654209, "uk_qt", "tbs"), Accuracy);
			Assert.AreEqual(-0.691813814054211, EngineeringFunctions.CONVERT(-629.009572386742, "tspm", "uk_gal"), Accuracy);
			Assert.AreEqual(118.363288581371, EngineeringFunctions.CONVERT(946.90630865097, "us_pt", "gal"), Accuracy);
			Assert.AreEqual(3.09605241321354E-04, EngineeringFunctions.CONVERT(115.559537112713, "Pica3", "in3"), Accuracy);
			Assert.AreEqual(-286.845099450283, EngineeringFunctions.CONVERT(-688.9732016325, "us_pt", "uk_qt"), Accuracy);
			Assert.AreEqual(346324.113600034, EngineeringFunctions.CONVERT(514.816096305847, "yd3", "uk_qt"), Accuracy);
			Assert.AreEqual(44156674.6382332, EngineeringFunctions.CONVERT(946.430783569813, "yd3", "in3"), Accuracy);
			Assert.AreEqual(-5.12039246294541E-07, EngineeringFunctions.CONVERT(-753.7112839818, "GRT", "mi3"), Accuracy);
			Assert.AreEqual(362721.130157101, EngineeringFunctions.CONVERT(393.266303777695, "uk_gal", "tsp"), Accuracy);
			Assert.AreEqual(-4.8781304512643, EngineeringFunctions.CONVERT(-187.468492865562, "tbs", "uk_pt"), Accuracy);
			Assert.AreEqual(-7.24031395933627E-06, EngineeringFunctions.CONVERT(-624.261954784393, "Pica3", "gal"), Accuracy);
			Assert.AreEqual(-7.86369202782462E-12, EngineeringFunctions.CONVERT(-105.566543221474, "us_pt", "Nmi3"), Accuracy);
			Assert.AreEqual(221.476040065289, EngineeringFunctions.CONVERT(221.476040065289, "gal", "gal"), Accuracy);
			Assert.AreEqual(8.98545929625445, EngineeringFunctions.CONVERT(722.039120912552, "bushel", "GRT"), Accuracy);
			Assert.AreEqual(6.24461821878925E-12, EngineeringFunctions.CONVERT(34.9020391106606, "uk_qt", "Nmi3"), Accuracy);
			Assert.AreEqual(-1156.80055928904, EngineeringFunctions.CONVERT(-481.61898124218, "uk_pt", "cup"), Accuracy);
			Assert.AreEqual(-1.50696232007003E-03, EngineeringFunctions.CONVERT(-305.73874861002, "tsp", "m3"), Accuracy);
			Assert.AreEqual(4.58089013937495E-49, EngineeringFunctions.CONVERT(682.615026473999, "uk_pt", "ly3"), Accuracy);
			Assert.AreEqual(-145.162909137435, EngineeringFunctions.CONVERT(-189.865916907787, "yd3", "m3"), Accuracy);
			Assert.AreEqual(-0.136638859482503, EngineeringFunctions.CONVERT(-124.234510540962, "tspm", "uk_gal"), Accuracy);

			// multipliers
			Assert.AreEqual(48.11898513, EngineeringFunctions.CONVERT(44000000000000, "pm", "yd"), Accuracy);

		}
	}
}