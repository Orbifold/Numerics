using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Math
{
	/// <summary>
	/// Unit tests of diverse special functions and polynomials.
	/// </summary>
	[TestFixture]
	public class MathTests
	{
		private const double Accuracy = 1E-6;

		[Test]
		[Category("Diverse functions")]
		public void GammaTest()
		{
			// gamma
			Assert.AreEqual(2.41605085099579, Functions.Gamma(3.19672372937202), Accuracy);
			Assert.AreEqual(13.8825126879496, Functions.Gamma(4.62595878839493), Accuracy);
			Assert.AreEqual(2.13271882732642, Functions.Gamma(0.415676707029343), Accuracy);
			Assert.AreEqual(3.69810387443817, Functions.Gamma(3.59550366401672), Accuracy);
			Assert.AreEqual(1.77273235949519, Functions.Gamma(2.86533065438271), Accuracy);
			Assert.AreEqual(0.948430702927698, Functions.Gamma(1.85917609930038), Accuracy);
			Assert.AreEqual(4.55022977456423, Functions.Gamma(3.77391051650047), Accuracy);
			Assert.AreEqual(5.44572548650429, Functions.Gamma(3.92214500904083), Accuracy);
			Assert.AreEqual(0.901097590334103, Functions.Gamma(1.65637829899788), Accuracy);
			Assert.AreEqual(0.918635851663489, Functions.Gamma(1.74811812639236), Accuracy);

			// gamma negative values
//			Assert.AreEqual(2.9452661069313324820932987, Functions.Gamma(-1.3478), Accuracy);
//			Assert.AreEqual(-1.39822215681271E-03, Functions.Gamma(-6.66904813051224), Accuracy);
//			Assert.AreEqual(-0.976666670015353, Functions.Gamma(-2.73450392484665), Accuracy);
//			Assert.AreEqual(0.277448639195839, Functions.Gamma(-3.76867443323135), Accuracy);
//			Assert.AreEqual(-5.29530743048645, Functions.Gamma(-0.225512206554413), Accuracy);
//			Assert.AreEqual(3.10564420824069, Functions.Gamma(-1.79224115610123), Accuracy);
//			Assert.AreEqual(0.289605666043831, Functions.Gamma(-3.78734856843948), Accuracy);
//			Assert.AreEqual(-1.97041451487914E-03, Functions.Gamma(-6.43006724119186), Accuracy);

			// GammaLn
			Assert.AreEqual(1.50856818610322, Functions.GammaLn(3.76835145950317), Accuracy);
			Assert.AreEqual(1.52395510070524, Functions.GammaLn(3.78128070831299), Accuracy);
			Assert.AreEqual(3.51639004045872, Functions.GammaLn(5.22110624313355), Accuracy);
			Assert.AreEqual(1.05593856001418, Functions.GammaLn(3.36578979492187), Accuracy);
			Assert.AreEqual(2.93885210191772, Functions.GammaLn(4.83925867080688), Accuracy);
			Assert.AreEqual(0.513590205904634, Functions.GammaLn(2.79629344940186), Accuracy);
			Assert.AreEqual(0.429146817643342, Functions.GammaLn(2.69286489486694), Accuracy);
			Assert.AreEqual(2.59403131257292, Functions.GammaLn(4.60012321472168), Accuracy);
			Assert.AreEqual(9.01512217041147E-02, Functions.GammaLn(2.18743028640747), Accuracy);
			Assert.AreEqual(1.78957799295296, Functions.GammaLn(3.9982629776001), Accuracy);

			Assert.AreEqual(39.3398841872, Functions.GammaLn(20), Accuracy);
			Assert.AreEqual(365.123, Functions.GammaLn(101.3), 0.01);
			Assert.AreEqual(1.82781, Functions.GammaLn(0.15), 0.01);

			Assert.AreEqual(0.864664716763, Functions.GammaRegularized(1, 2), Accuracy);
			Assert.AreEqual(0.999477741950, Functions.GammaRegularized(3, 12), Accuracy);
			Assert.AreEqual(0.714943499683, Functions.GammaRegularized(5, 6), Accuracy);

			Assert.AreEqual(6227020800, Functions.Gamma(14), Accuracy); // equal 13!
			Assert.AreEqual(7.1099858780486345E74, Functions.Gamma(57), Accuracy); // equal 56!
			Assert.AreEqual(9.3326215443944153E155, Functions.Gamma(100), Accuracy); // equal 99!
			Assert.AreEqual(3.0414093201713378E64, Functions.Gamma(51), Accuracy); // equal 50!
		}

		[Test]
		[Category("Diverse functions")]
		public void BinomialTest()
		{
			Assert.AreEqual(225792840, Functions.BinomialCoefficient(32, 12), Accuracy);
			Assert.AreEqual(792, Functions.BinomialCoefficient(12, 5), Accuracy);
			Assert.AreEqual(53130, Functions.BinomialCoefficient(25, 5), Accuracy);
			Assert.AreEqual(53130, Functions.BinomialCoefficient(25, 5), Accuracy);
			Assert.AreEqual(170544, Functions.BinomialCoefficient(22, 7), Accuracy);
			Assert.AreEqual(0, Functions.BinomialCoefficient(-5, 3), Accuracy);
			Assert.AreEqual(0, Functions.BinomialCoefficient(5, -43), Accuracy);
			Assert.AreEqual(0, Functions.BinomialCoefficient(5, 43), Accuracy);
			Assert.AreEqual(6.67456139181, Functions.BinomialCoefficientLn(12, 5), Accuracy);
			Assert.AreEqual(9.64885333413, Functions.BinomialCoefficientLn(20, 15), Accuracy);

		}

		[Test]
		[Category("Diverse functions")]
		public void BetaRegularizedTest()
		{
			Assert.AreEqual(0.651473, Functions.BetaRegularized(0.1, 0.22, 0.33), Accuracy);
			Assert.AreEqual(0.470091, Functions.BetaRegularized(0.55, 0.77, 0.33), Accuracy);
		}

		[Test]
		[Category("Diverse functions")]
		public void FactorialTest()
		{
			Assert.AreEqual(479001600, Functions.Factorial(12), Accuracy);
			Assert.AreEqual(355687428096000, Functions.Factorial(17), Accuracy);
			Assert.AreEqual(40320, Functions.Factorial(8), Accuracy);

			Assert.AreEqual(19.9872144957, Functions.FactorialLn(12), Accuracy);
			Assert.AreEqual(932.555207148, Functions.FactorialLn(213), Accuracy);
			Assert.AreEqual(8.52516136107, Functions.FactorialLn(7), Accuracy);

		}

		[Test]
		[Category("Diverse functions")]
		public void GCDTest()
		{
			Assert.AreEqual(2, Functions.GCD(1128, 314), Accuracy);
			Assert.AreEqual(1, Functions.GCD(978, 455), Accuracy);
			Assert.AreEqual(1, Functions.GCD(361, 1104), Accuracy);
			Assert.AreEqual(1, Functions.GCD(787, 754), Accuracy);
			Assert.AreEqual(2, Functions.GCD(534, 118), Accuracy);
			Assert.AreEqual(3, Functions.GCD(699, 834), Accuracy);
			Assert.AreEqual(2, Functions.GCD(1138, 1002), Accuracy);
			Assert.AreEqual(1, Functions.GCD(29, 653), Accuracy);
			Assert.AreEqual(1, Functions.GCD(1141, 517), Accuracy);
			Assert.AreEqual(1, Functions.GCD(845, 603), Accuracy);

			// more than two values
			Assert.AreEqual(20, Functions.GCD(120, 400, 500), Accuracy);
			Assert.AreEqual(10, Functions.GCD(120, 400, 500, 630, 1210), Accuracy);

		}

		[Test]
		[Category("Diverse functions")]
		public void ErfTest()
		{
			// Erf
			Assert.AreEqual(0.157299, Functions.ErfC(1), Accuracy);
			Assert.AreEqual(0.974041238799887, Functions.Erf(1.57460528612137), Accuracy);
			Assert.AreEqual(0.179576632289644, Functions.Erf(0.160513579845428), Accuracy);
			Assert.AreEqual(0.953246471325595, Functions.Erf(1.40610033273697), Accuracy);
			Assert.AreEqual(0.991755233573447, Functions.Erf(1.86809009313583), Accuracy);
			Assert.AreEqual(0.736936058170946, Functions.Erf(0.791378796100616), Accuracy);
			Assert.AreEqual(0.999569347389669, Functions.Erf(2.48940485715866), Accuracy);
			Assert.AreEqual(0.987566883153486, Functions.Erf(1.76748901605606), Accuracy);
			Assert.AreEqual(0.999888862657955, Functions.Erf(2.73289293050766), Accuracy);
			Assert.AreEqual(0.996813252870384, Functions.Erf(2.08534651994705), Accuracy);
			Assert.AreEqual(0.699290496068478, Functions.Erf(0.731794059276581), Accuracy);

			//ErfC
			Assert.AreEqual(0.651781770756059, Functions.ErfC(0.31910902261734), Accuracy);
			Assert.AreEqual(4.12069564569917E-03, Functions.ErfC(2.02852767705917), Accuracy);
			Assert.AreEqual(1.46751412783641E-02, Functions.ErfC(1.72555142641068), Accuracy);
			Assert.AreEqual(0.662047513039235, Functions.ErfC(0.309067904949188), Accuracy);
			Assert.AreEqual(0.227452330753622, Functions.ErfC(0.853440821170807), Accuracy);
			Assert.AreEqual(0.209530338576434, Functions.ErfC(0.887318551540375), Accuracy);
			Assert.AreEqual(0.201634140967401, Functions.ErfC(0.902911484241486), Accuracy);
			Assert.AreEqual(3.22363767814562E-05, Functions.ErfC(2.93948811292648), Accuracy);
			Assert.AreEqual(0.237745018524273, Functions.ErfC(0.834839880466461), Accuracy);
			Assert.AreEqual(0.489695316985242, Functions.ErfC(0.488464772701263), Accuracy);
		}

		[Test]
		[Category("Diverse functions")]
		public void SiCiTest()
		{
			// Si
			Assert.AreEqual(0.946083, Functions.Si(1), Accuracy);
			Assert.AreEqual(1.658347594, Functions.Si(10), Accuracy);
			Assert.AreEqual(-1.47509, Functions.Si(-7.2), Accuracy);

			//Ci
			Assert.AreEqual(.0959571, Functions.Ci(7.2), Accuracy);
		}

		[Test]
		[Category("Diverse functions")]
		public void LaguerreTest()
		{

			Assert.AreEqual(0.663492, Functions.Laguerre(2, 7), Accuracy);
			Assert.AreEqual(-0.114667, Functions.Laguerre(2.2, 3), Accuracy);
			Assert.AreEqual(3465.09, Functions.Laguerre(23.3, 8), 0.01);
		}

		[Test]
		[Category("Diverse functions")]
		public void LegendreTest()
		{

			Assert.AreEqual(73, Functions.Legendre(7, 2), Accuracy);
			Assert.AreEqual(2199.125000, Functions.Legendre(2, 7), Accuracy);
			Assert.AreEqual(23.32, Functions.Legendre(2.2, 3), Accuracy);
			Assert.AreEqual(4.352028145736411E+12, Functions.Legendre(23.3, 8), 0.01);
		}

		[Test]
		[Category("Diverse functions")]
		public void HermiteTest()
		{

			Assert.AreEqual(194, Functions.Hermite(7, 2), Accuracy);
			Assert.AreEqual(-3104, Functions.Hermite(2, 7), Accuracy);
			Assert.AreEqual(58.78400000000001, Functions.Hermite(2.2, 3), Accuracy);
			Assert.AreEqual(2.166806362005386E+13, Functions.Hermite(23.3, 8), 0.1);
		}
        
        [Test]
		[Category("Diverse functions")]
		public void ErfInverseTest()
        {
            Assert.AreEqual(0.2724627147267544, Functions.ErfInverse(0.3), Accuracy);
            Assert.AreEqual(0.6040031879352371, Functions.ErfInverse(0.607), Accuracy);
            Assert.AreEqual(0.1418558907268814, Functions.ErfInverse(0.159), Accuracy);
            Assert.AreEqual(1.701751973779214, Functions.ErfInverse(0.9839), Accuracy);

        }

		[Test]
		[Category("Diverse functions")]
		public void BesselTest()
		{
			Assert.AreEqual(-0.2459357645, Functions.BesselJ0(10), Accuracy);
			Assert.AreEqual(0.110798, Functions.BesselJ0(12.3), Accuracy);
			Assert.AreEqual(-0.155559, Functions.BesselJ0(2.73), Accuracy);
			Assert.AreEqual(0.0142751, Functions.BesselJ0(209.44), Accuracy);

			Assert.AreEqual(-0.0267446, Functions.BesselY0(809.44), Accuracy);
			Assert.AreEqual(0.0140024, Functions.BesselY0(79.17), Accuracy);
			Assert.AreEqual(0.244426, Functions.BesselY0(9.05), Accuracy);

			Assert.AreEqual(0.02628796642, Functions.BesselJ1(901), Accuracy);
			Assert.AreEqual(-0.0943526, Functions.BesselJ1(61.66), Accuracy);
			Assert.AreEqual(0.443286, Functions.BesselJ1(1.01), Accuracy);

			Assert.AreEqual(0.0707866, Functions.BesselY1(111.1), Accuracy);
			Assert.AreEqual(-0.966882, Functions.BesselY1(0.81), Accuracy);
			Assert.AreEqual(-0.0596379, Functions.BesselY1(45.8), Accuracy);

			Assert.AreEqual(0.0505855, Functions.BesselY(45.8, 3), Accuracy);
			Assert.AreEqual(-0.0556135, Functions.BesselY(55.5, 34), Accuracy);

			Assert.AreEqual(0.207486106634021, Functions.BesselJ(10, 10), Accuracy);
			Assert.AreEqual(0.207486106634021, Functions.BesselJ(10, 10.5.Truncate()), Accuracy);
			Assert.AreEqual(0.207486106634021, Functions.BesselJ(10, 10.855.Truncate()), Accuracy);
			Assert.AreEqual(0.207486106634021, Functions.BesselJ(10, 10.01855.Truncate()), Accuracy);
			Assert.AreEqual(0.364831233515002, Functions.BesselJ(5, 3), Accuracy);
			Assert.AreEqual(0.213408926326772, Functions.BesselJ(5.66, 3), Accuracy);
			Assert.AreEqual(0.234061686267552, Functions.BesselJ(-10, 5.22.Truncate()), Accuracy);
			// Assert.AreEqual(2.49757730211234E-04, Functions.BesselJ(1, 5), Accuracy);


			// first kind
			Assert.AreEqual(-272.546574254071, Functions.BesselY(50.8402637243271, 66.204491019249.Truncate()), Accuracy);
			Assert.AreEqual(-8.52769135257866E-03, Functions.BesselY(46.6838082075119, 26.3440624475479.Truncate()), Accuracy);
			Assert.AreEqual(-9.40174306165309E-02, Functions.BesselY(58.0795556306839, 56.0648009777069.Truncate()), Accuracy);
			Assert.AreEqual(0.074994290525709, Functions.BesselY(31.5206769704819, 24.7440747022629.Truncate()), Accuracy);
			Assert.AreEqual(-7.91404283286247E-02, Functions.BesselY(73.4053198099136, 71.0911808013916.Truncate()), Accuracy);
			Assert.AreEqual(-13162504.974304, Functions.BesselY(55.5980623960495, 84.8179188966751.Truncate()), Accuracy);
			Assert.AreEqual(-4.56293634715995E-02, Functions.BesselY(79.4113589525223, 20.2836105823517.Truncate()), Accuracy);
			Assert.AreEqual(-5670.83700260085, Functions.BesselY(63.4385474920273, 84.3002752065659.Truncate()), Accuracy);
			Assert.AreEqual(-5956.65770853918, Functions.BesselY(30.0509201288223, 46.3792119026184.Truncate()), Accuracy);

			// second kind
			Assert.AreEqual(-272.546574254071, Functions.BesselY(50.8402637243271, 66.204491019249.Truncate()), Accuracy);
			Assert.AreEqual(-8.52769135257866E-03, Functions.BesselY(46.6838082075119, 26.3440624475479.Truncate()), Accuracy);
			Assert.AreEqual(-9.40174306165309E-02, Functions.BesselY(58.0795556306839, 56.0648009777069.Truncate()), Accuracy);
			Assert.AreEqual(0.074994290525709, Functions.BesselY(31.5206769704819, 24.7440747022629.Truncate()), Accuracy);
			Assert.AreEqual(-7.91404283286247E-02, Functions.BesselY(73.4053198099136, 71.0911808013916.Truncate()), Accuracy);
			Assert.AreEqual(-13162504.974304, Functions.BesselY(55.5980623960495, 84.8179188966751.Truncate()), Accuracy);
			Assert.AreEqual(-4.56293634715995E-02, Functions.BesselY(79.4113589525223, 20.2836105823517.Truncate()), Accuracy);
			Assert.AreEqual(-5670.83700260085, Functions.BesselY(63.4385474920273, 84.3002752065659.Truncate()), Accuracy);
			Assert.AreEqual(-5956.65770853918, Functions.BesselY(30.0509201288223, 46.3792119026184.Truncate()), Accuracy);

			// problem cases of Deyan
			Assert.AreEqual("2.23438690749371E+25", Functions.BesselI(100, 90).ToString());
			Assert.AreEqual("1.617319483915E+24", Functions.BesselI(70, 40).ToString());
			Assert.AreEqual("1.20158844027E+29", Functions.BesselI(70, 0).ToString());
			Assert.AreEqual("5.85925129038407E+28", Functions.BesselI(70, 10).ToString());

			Assert.AreEqual(0.00222052182115523, Functions.BesselJ(80, 90), Accuracy);
			Assert.AreEqual(4.60655306482347E-06, Functions.BesselJ(-80, 100), Accuracy);

			Assert.AreEqual(-0.1139486674, Functions.BesselJ(70, 50), Accuracy);
			Assert.AreEqual(-0.124230136902855, Functions.BesselJ(70, 60), Accuracy);

			Assert.AreEqual(-0.124230136902855, Functions.BesselJ(-70, 60), Accuracy);
			Assert.AreEqual(-0.1139486674, Functions.BesselJ(-70, 50), Accuracy);
			Assert.AreEqual(-0.0914718040406352, Functions.BesselJ(60, 0), Accuracy);
			Assert.AreEqual(0.0558123275994173, Functions.BesselJ(50, 0), Accuracy);

			Assert.AreEqual(0.000174459, Functions.BesselK(8.05547511577606, 2), Accuracy);


			// modified of the first kind
			Assert.AreEqual(-36.2867133167814, Functions.BesselI(-7.25505954027176, 5.8989474773407.Truncate()), Accuracy);
			Assert.AreEqual(-757.642823453682, Functions.BesselI(-9.97638005018234, 5.74459171295166.Truncate()), Accuracy);
			Assert.AreEqual(-46.3021169622632, Functions.BesselI(-8.65552049875259, 7.28751873970032.Truncate()), Accuracy);
			Assert.AreEqual(16.7107111103458, Functions.BesselI(-4.95308727025986, 2.56302213668823.Truncate()), Accuracy);
			Assert.AreEqual(8.47578514068768, Functions.BesselI(-5.42086428403854, 4.90471458435059.Truncate()), Accuracy);
			Assert.AreEqual(244.988171836858, Functions.BesselI(-10.6041207909584, 8.83995270729065.Truncate()), Accuracy);
			Assert.AreEqual(0.440387388712755, Functions.BesselI(-6.02466958761215, 8.53688097000122.Truncate()), Accuracy);
			Assert.AreEqual(-10077.3191262523, Functions.BesselI(-11.7561413645744, 3.10368633270264.Truncate()), Accuracy);

			// modified of the second kind
			Assert.AreEqual(1.06647166273871E-03, Functions.BesselK(8.05547511577606, 6.33424019813538.Truncate()), Accuracy);
			Assert.AreEqual(9.76053018739238E-04, Functions.BesselK(6.79518616199493, 3.89562463760376.Truncate()), Accuracy);
			Assert.AreEqual(5.4480877288023, Functions.BesselK(4.01948010921478, 8.74740099906921.Truncate()), Accuracy);
			Assert.AreEqual(215663.892022097, Functions.BesselK(1.14017641544342, 8.6072359085083.Truncate()), Accuracy);
			Assert.AreEqual(1.05301707327901E-03, Functions.BesselK(9.14490020275116, 8.09037899971008.Truncate()), Accuracy);
			Assert.AreEqual(52.0039071955581, Functions.BesselK(1.45352756977081, 5.14032697677612.Truncate()), Accuracy);
			Assert.AreEqual(5.5102547538128E-04, Functions.BesselK(9.62619340419769, 8.90480017662048.Truncate()), Accuracy);
			Assert.AreEqual(17.9663705301776, Functions.BesselK(4.73536169528961, 10.6195316314697.Truncate()), Accuracy);
			Assert.AreEqual(2.51991462316546E-05, Functions.BesselK(9.71445834636688, 1.56236863136292.Truncate()), Accuracy);
			Assert.AreEqual(2.17828016795605E-05, Functions.BesselK(10.4955664873123, 4.64018678665161.Truncate()), Accuracy);

			// spherical of the first kind
			Assert.AreEqual(0.00009256115861, Functions.SphericalBesselJ(1, 5), Accuracy);
			Assert.AreEqual(0.003112791813, Functions.SphericalBesselJ(11, 15), Accuracy);
			Assert.AreEqual(0.170869, Functions.SphericalBesselJ(3.2, 3), Accuracy);

			// spherical of the second kind
			Assert.AreEqual(-999.4403434, Functions.SphericalBesselY(1, 5), 0.0001); // function diverges near zero and six digits accuracy doesn't work there
			Assert.AreEqual(-1.353530885, Functions.SphericalBesselY(11, 15), Accuracy);
			Assert.AreEqual(-0.433651, Functions.SphericalBesselY(3.2, 3), Accuracy);
		}

		[Test]
		[Category("Diverse functions")]
		public void MersenneTest()
		{
			// the question is: how to test a random MachineLearningModelCreator...?
			var rand = new MersenneSource();
			var trail = new List<int>();
			Range.Create(1, 1000).ForEach(i => {
				var v = rand.Next();

				Assert.IsFalse(trail.Contains(v));
				trail.Add(v);
			});
			var traild = new List<double>();
			Range.Create(1, 1000).ForEach(i => {
				var v = rand.NextDouble();
				Assert.IsFalse(traild.Contains(v));
				traild.Add(v);
			});

		}

		[Test]
		[Category("Complex numbers")]
		public void Complex2Test()
		{
			Assert.AreEqual(0.92729522, EngineeringFunctions.IMARGUMENT("3+4i"), Accuracy);
			Assert.AreEqual(new Complex(3, -4), EngineeringFunctions.IMCONJUGATE("3+4j"));
			var zln2 = EngineeringFunctions.IMLOG2("3+4j");
			Assert.AreEqual(2.32192809488736, zln2.Real, Accuracy);
			Assert.AreEqual(1.33780421245098, zln2.Imaginary, Accuracy);
		}

		[Test]
		[Category("Complex numbers")]
		public void Complex1Test()
		{
			var z = "4.3+j7".ToComplex();
			Assert.AreEqual(4.3, z.Real);
			Assert.AreEqual(7, z.Imaginary);

			z = "44+77j".ToComplex();
			Assert.AreEqual(44, z.Real);
			Assert.AreEqual(77, z.Imaginary);

			z = "77E+44j".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(77E44, z.Imaginary);

			z = "-77E+44j".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(-77E44, z.Imaginary);

			z = "0.2 +i.87".ToComplex();
			Assert.AreEqual(.2, z.Real);
			Assert.AreEqual(.87, z.Imaginary);

			z = "0.2E-12".ToComplex();
			Assert.AreEqual(.2E-12, z.Real);
			Assert.AreEqual(0, z.Imaginary);

			z = "88E-12i".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(88E-12, z.Imaginary);

			z = "i".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(1, z.Imaginary);

			z = "-i".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(-1, z.Imaginary);

			z = "j".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(1, z.Imaginary);

			z = "-j".ToComplex();
			Assert.AreEqual(0, z.Real);
			Assert.AreEqual(-1, z.Imaginary);


			z = ("2 +  i3").ToComplex();
			Assert.AreEqual(2, z.Real, "Real part is wrong.");
			Assert.AreEqual(3, z.Imaginary, "Imaginary part is wrong.");
			Assert.AreEqual(System.Math.Sqrt(13), z.Norm(), "Norm is wrong.");

			z = z.NegateRealNumberPart();
			Assert.AreEqual(-2, z.Real, "Inverse of the real is wrong");
			z = z.NegateImaginaryNumberPart();
			Assert.AreEqual(-3, z.Imaginary, "Inverse of the imaginary is wrong");
			var sum = z + new Complex(22, 0);
			Assert.AreEqual(20, sum.Real);
			Assert.AreEqual(-3, sum.Imaginary);
			sum += new Complex(0, 22);
			Assert.AreEqual(20, sum.Real);
			Assert.AreEqual(19, sum.Imaginary);
			var w = new Complex(2, 5);
			w = 2 * w;
			Assert.AreEqual(4, w.Real, "Real part is wrong after multiplication.");
			Assert.AreEqual(10, w.Imaginary, "Imaginary part is wrong after multiplication.");
			var u = new Complex(1, 1);
			Assert.AreEqual(System.Math.PI / 4, u.PolarFormAngle(), "Polar angle is wrong.");

		}

		[Test]
		[Category("Diverse functions")]
		public void IsXBetweenTest()
		{
			var p = new Point(0, 10);
			var a = new Point(-5, 77);
			var b = new Point(5, 55);
			Assert.IsTrue(p.IsXBetween(a, b));
			p.Offset(5, 0);
			Assert.IsTrue(p.IsXBetween(a, b));
			p.Offset(2, 0);
			Assert.IsFalse(p.IsXBetween(a, b));

		}

		[Test]
		[Category("Diverse functions")]
		[Ignore]
		public void BenchmarkRandomizerTest()
		{

			var amount = 1E7;
			var defaultSource = new DefaultSource(Environment.TickCount);
			var sw = new Stopwatch();
			sw.Start();
			for(var i = 0; i < amount; i++) {
				var r = defaultSource.Next();
			}
			sw.Stop();
			var defaultTime = sw.ElapsedMilliseconds;


//			var cryptoSource = new CryptoSource();
//			sw.Reset();
//			sw.Start();
//			for(var i = 0; i < amount; i++) {
//				var r = cryptoSource.Next();
//			}
//			sw.Stop();
//			var cryptoTime = sw.ElapsedMilliseconds;


			var mSource = new MersenneSource(Environment.TickCount);
			sw.Reset();
			sw.Start();
			for(var i = 0; i < amount; i++) {
				var r = mSource.Next();
			}
			sw.Stop();
			var mTime = sw.ElapsedMilliseconds;

			Console.WriteLine(string.Format("Default: {0} ms", defaultTime));
			//Console.WriteLine(string.Format("Crypto: {0} ms", cryptoTime));
			Console.WriteLine(string.Format("Mersenne: {0} ms", mTime));

			Assert.IsTrue(defaultTime < mTime, "Not so much faster after all...");
			//Assert.IsTrue(mTime < cryptoTime, "Not so much faster after all...");

		}

		[Test]
		[Category("Fourier transform")]
		public void Radix2Test()
		{
			var size = 5.PowerOfTwo();
			var x = new Complex[size];
            var myRand = new System.Random();
			for(var i = 0; i < x.Length; i++)
				x[i] = new Complex(-2 * myRand.NextDouble() + 1, 0);
			var y = DiscreteFourierTransform.Radix2Forward(x);
			var z = DiscreteFourierTransform.Radix2Inverse(y);
		    var u = DiscreteFourierTransform.PlainForward(x);
		    var v = DiscreteFourierTransform.PlainInverse(u);
		    for (var i = 0; i < x.Length; i++)
		    {
                if ((x[i] - z[i]).IsNotZero())
                    Assert.Fail("Radix: item [{0}] failed to be mapped correctly; differs by [{1}].", i, x[i] - z[i]);
                if ((x[i] - v[i]).IsNotZero())
                    Assert.Fail("Plain: item [{0}] failed to be mapped correctly; differs by [{1}].", i, x[i] - v[i]);
		    }
				

			var input = new Complex[16, 32];
			for(var i = 0; i < input.GetLength(0); i++)
				for(var k = 0; k < input.GetLength(1); k++)
					input[i, k] = new Complex(-2 * myRand.NextDouble() + 1, 0);
			var output1 = DiscreteFourierTransform.Radix2Forward2D(input);
			var output2 = DiscreteFourierTransform.Radix2Inverse2D(output1);
			for(var i = 0; i < input.GetLength(0); i++) {
				for(var j = 0; j < input.GetLength(1); j++) {
					if((output2[i, j] - input[i, j]).IsNotZero())
						Assert.Fail(string.Format("Item [{0},{1}] failed to be mapped correctly.", i, j));
				}
			}
		}

	}
}