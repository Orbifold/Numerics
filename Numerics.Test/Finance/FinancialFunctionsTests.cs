using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
 
using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Finance
{
	[TestFixture]
	public class FinancialFunctionsTests
	{

		// six digits accuracy, should be enough for the generic Telerik user but would fail for a quant and similar finance market analyst...
		private const double Accuracy = 1E-6;

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void DDBGenericTest()
		{
			const double initialCost = 2400;
			const double salvageValue = 300;
			const double lifeInYears = 10;

			// First day's depreciation, using double-declining balance method. Default factor is 2.
			var firstDay = FinancialFunctions.DDB(initialCost, salvageValue, lifeInYears * 365, 1);
			Assert.AreEqual(1.315068, firstDay, Accuracy, "First day depreciation is wrong.");

			// First month's depreciation.
			var firstMonth = FinancialFunctions.DDB(initialCost, salvageValue, lifeInYears * 12, 1);
			Assert.AreEqual(40, firstMonth, Accuracy, "First month depreciation is wrong.");

			// First year's depreciation.
			var firstYear = FinancialFunctions.DDB(initialCost, salvageValue, lifeInYears, 1);
			Assert.AreEqual(480, firstYear, Accuracy, "First year depreciation is wrong.");

			// Tenth year's depreciation.
			var tenthYear = FinancialFunctions.DDB(initialCost, salvageValue, lifeInYears, 10);
			Assert.AreEqual(22.122547, tenthYear, Accuracy, "Tenth year depreciation is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PVGenericTest()
		{
			var rate = .08;
			var nper = 20.0;
			var pmt = 500000;
			var pv = FinancialFunctions.PV(rate / 12, 12 * nper, pmt);
			Assert.AreEqual(-59777145.851188, pv, Accuracy, "Wrong present value.");
			Assert.AreEqual(0, FinancialFunctions.PV(0.00666666666666667, 240, 0, 0, 0));
			Assert.AreEqual(0, FinancialFunctions.PV(0.00666666666666667, 240, 0, 0, 1));
			Assert.AreEqual(-80, FinancialFunctions.PV(0.00666666666666667, 0, 500000, 80, 0));
			Assert.AreEqual(29411.7647058824, FinancialFunctions.PV(-17, 240, 500000), Accuracy);

			var bordercases = new[] {
				Tuple.Create(-500000, 240, 500000, 0, 0), 
				Tuple.Create(100, 240, 500000, 0, 0), 
				Tuple.Create(20, 240, 500000, 0, 0), 
			};
			var result = bordercases.Aggregate(true, (b, t) => {
				try {
					Console.WriteLine(FinancialFunctions.PV(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5));
					return false;
				} catch(Exception) {
					return true;
				}
			});
			Assert.IsTrue(result, "Some border cases did not raise an error.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void FVGenericTest()
		{
			var ratePerYear = 0.06;
			var numberOfPayments = 10;
			var amount = -200;
			var presentValue = -500;
			var fv = FinancialFunctions.FV(ratePerYear / 12, numberOfPayments, amount, presentValue, 1);
			Assert.AreEqual(2581.40337406, fv, Accuracy, "Future value is wrong.");

			//0, 0.005, 10, 0, 0, 0
			fv = FinancialFunctions.FV(0.005, 10, 0, 0, 0);
			Assert.AreEqual(0, fv, Accuracy, "Future value is wrong.");

			//2581.40337406012, 0.11, 35, -2000, null, 1
			fv = FinancialFunctions.FV(0.11 / 12, 35, -2000, 0, 1);
			Assert.AreEqual(82846.246372, fv, Accuracy, "Future value is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void NPVGenericTest()
		{
			var rate = 0.1;
			var initialCost = -10000;
			var returnFirstYear = 3000;
			var returnSecondYear = 4200;
			var returnThirdYear = 6800;
			var npv = FinancialFunctions.NPV(rate, new double[] {
				initialCost,
				returnFirstYear,
				returnSecondYear,
				returnThirdYear
			});
			Assert.AreEqual(1188.44341234, npv, Accuracy, "The net present value is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void NPERGenericTest()
		{
			var rate = 0.12;
			var paymentsPerPeriod = -100;
			var presentValue = -1000;
			var futureValue = 10000;
			var periods = FinancialFunctions.NPER(rate / 12, paymentsPerPeriod, presentValue, futureValue, 1);
			Assert.AreEqual(59.67386567, periods, Accuracy, "Begin of period NPER is wrong.");
			periods = FinancialFunctions.NPER(rate / 12, paymentsPerPeriod, presentValue, futureValue, 0);
			Assert.AreEqual(60.08212285, periods, Accuracy, "End of period NPER is wrong.");
			periods = FinancialFunctions.NPER(rate / 12, paymentsPerPeriod, presentValue, 0, 0);
			Assert.AreEqual(-9.57859404, periods, Accuracy, "Zero future value NPER is wrong.");

			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, -1, -100, -1000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, 0, -1000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, 1, -1000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, 7, -1000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, 10, -1000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, -100, 10000);
			//FunctionsTestHelper.AssertFunctionError(nPer, ErrorExpressions.NumberError, 0.01, -100, -1000, -10000, 0);

			var bordercases = new[] {
				Tuple.Create(0.11d, 35d, -2000d, 0d, 1), 
				Tuple.Create(-1d, -100d, -1000d, 0d, 0), 
				Tuple.Create(0.01d, 0d, -1000d, 0d, 0),  
				Tuple.Create(0.01d, 1d, -1000d, 0d, 0),  
				Tuple.Create(0.01d, 7d, -1000d, 0d, 0),  
				Tuple.Create(0.01d, 10d, -1000d, 0d, 0), 
				Tuple.Create(0.01d, -100d, 10000d, 0d, 0),  
				Tuple.Create(0.01d, -100d, -1000d, -10000d, 0)
			};
			var result = bordercases.Aggregate(true, (b, t) => {
				try {
					FinancialFunctions.NPER(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5);
					return false;
				} catch(Exception) {
					return true;
				}
			});
			Assert.IsTrue(result, "Some border cases did not raise an error.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PMTGenericTest()
		{
			var rate = 0.08;
			var monthsToPay = 10;
			var loan = 10000;
			var monthlyPaymenst = FinancialFunctions.PMT(rate / 12, monthsToPay, loan);
			Assert.AreEqual(-1037.03208936, monthlyPaymenst, Accuracy, "PMT is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void SYDGenericTest()
		{
			var initialCost = 30000;
			var salvage = 7500;
			var lifespan = 10;
			var firstYearDepreciation = FinancialFunctions.SYD(initialCost, salvage, lifespan, 1);
			var tenYearDepreciation = FinancialFunctions.SYD(initialCost, salvage, lifespan, 10);

			Assert.AreEqual(4090.90909091, firstYearDepreciation, Accuracy, "First year SYD is wrong.");
			Assert.AreEqual(409.0909090909, tenYearDepreciation, Accuracy, "Ten year SYD is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void SLNGenericTest()
		{
			var cost = 30000;
			var salvage = 7500;
			var years = 10;
			var sln = FinancialFunctions.SLN(cost, salvage, years);
			Assert.AreEqual(2250.0, sln, Accuracy, "SLN is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void MIRRGenericTest()
		{
			var cost = -120000;
			var firstYearReturn = 39000;
			var secondYearReturn = 30000;
			var thirdYearReturn = 21000;
			var fourthYearReturn = 37000;
			var fifthYearReturn = 46000;
			var annualInterest = 0.1;
			var annualReinvestRate = 0.12;

			var fiveYearsMIRR = FinancialFunctions.MIRR(new double[] {
				cost,
				firstYearReturn,
				secondYearReturn,
				thirdYearReturn,
				fourthYearReturn,
				fifthYearReturn
			}, annualInterest, annualReinvestRate);
			Assert.AreEqual(0.12609413, fiveYearsMIRR, Accuracy, "Five years MIRR is wrong.");
			var threeYearsMIRR = FinancialFunctions.MIRR(new double[] {
				cost,
				firstYearReturn,
				secondYearReturn,
				thirdYearReturn
			}, annualInterest, annualReinvestRate);
			Assert.AreEqual(-0.04804466, threeYearsMIRR, Accuracy, "Three years MIRR is wrong.");

			var fiveYears014MIRR = FinancialFunctions.MIRR(new double[] {
				cost,
				firstYearReturn,
				secondYearReturn,
				thirdYearReturn,
				fourthYearReturn,
				fifthYearReturn
			}, annualInterest, 0.14);
			Assert.AreEqual(0.13475911, fiveYears014MIRR, Accuracy, "Five years 14% MIRR is wrong.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void RATEGenericTest()
		{
			var yearsOfLoan = 4;
			var monthly = -200;
			var loan = 8000;
			var monthlyRate = FinancialFunctions.RATE(yearsOfLoan * 12, monthly, loan);
			Assert.AreEqual(0.00770147, monthlyRate, Accuracy, "RATE is wrong.");
			var yearlyRate = FinancialFunctions.RATE(yearsOfLoan * 12, monthly, loan) * 12;
			Assert.AreEqual(0.09241767, yearlyRate, Accuracy, "Yearly RATE is wrong.");
			Assert.AreEqual(7.70147248819591E-03, FinancialFunctions.RATE(48, -200, 8000, 0, 0, 0), Accuracy);
			Assert.AreEqual(8.05298192388529E-03, FinancialFunctions.RATE(48, -200, 8000, 0, 1, 0), Accuracy);


			var bordercases = new[] {
				Tuple.Create(0, -200, 8000, 0, 0, 0), 
				Tuple.Create(-1, -200, 8000, 0, 0, 0), 
				Tuple.Create(48, 0, 8000, 0, 0, 0), 
				Tuple.Create(48, 100, 8000, 0, 0, 0), 
				Tuple.Create(48, 100, 8000, 0, 0, 0), 
			};
			var result = bordercases.Aggregate(true, (b, t) => {
				try {
					Console.WriteLine(FinancialFunctions.RATE(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6));
					return false;
				} catch(Exception) {
					return true;
				}
			});
			Assert.IsTrue(result, "Some border cases did not raise an error.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void IRRGenericTest()
		{
			var initial = -70;
			var y1 = 12;
			var y2 = 15;
			var y3 = 18;
			var y4 = 21;
			var y5 = 26;

			var fiveYears = FinancialFunctions.IRR(new double[] { initial, y1, y2, y3, y4, y5 });
			var fourYears = FinancialFunctions.IRR(new double[] { initial, y1, y2, y3, y4 });
			var twoYears = FinancialFunctions.IRR(new double[] { initial, y1, y2 }, -0.1);

			Assert.AreEqual(-0.02124485, fourYears, Accuracy);
			Assert.AreEqual(0.08663095, fiveYears, Accuracy);
			Assert.AreEqual(-0.44350694, twoYears, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void IPMTGenericTest()
		{
			var interest = 0.1;
			var period = 1;
			var years = 3;
			var presentValue = 8000;
			var firstMonth = FinancialFunctions.IPMT(interest / 12, period, years * 12, presentValue);
			var lastYear = FinancialFunctions.IPMT(interest, 3, years, presentValue);

			Assert.AreEqual(-66.66666666, firstMonth, Accuracy);
			Assert.AreEqual(-292.44712991, lastYear, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PPMTGenericTest()
		{
			var interest = 0.1;
			var years = 2;
			var loan = 2000;
			var firstMonth = FinancialFunctions.PPMT(interest / 12, 1, years * 12, loan);
			Assert.AreEqual(-75.6231860, firstMonth, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ACCRINTGenericTest()
		{
			var issueDate = new DateTime(2008, 3, 1);
			var firstInterestDate = new DateTime(2008, 8, 31);
			var settlementDate = new DateTime(2008, 9, 11);
			var rate = 0.1;
			var par = 1000;
			var frequency = 2; //twice a year
			var basis = 0;
			var accrint = FinancialFunctions.ACCRINT(issueDate, firstInterestDate, settlementDate, rate, par, frequency, basis);
			Assert.AreEqual(53.05555556, accrint, Accuracy, "ACCRINT failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ACCRINTMGenericTest()
		{
			var issueDate = new DateTime(2008, 3, 1);
			var firstInterestDate = new DateTime(2008, 8, 31);
			var rate = 0.287;
			var par = 1478;
			var accrintm = FinancialFunctions.ACCRINTM(issueDate, firstInterestDate, rate, par);
			Assert.AreEqual(212.09300000, accrintm, Accuracy, "ACCRINTM failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void AMORDEGRCGenericTest()
		{
			var cost = 58476;
			var purchase = new DateTime(2008, 8, 19);
			var firstPeriod = new DateTime(2009, 6, 14);
			var salvage = 300;
			var period = 1;
			var depreciationRate = 0.15;
			var basis = 1;
			var amordegrc = FinancialFunctions.AMORDEGRC(cost, purchase, firstPeriod, salvage, period, depreciationRate, basis);
			Assert.AreEqual(15211, amordegrc, Accuracy, "AMORDEGRC failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void AMORLINCGenericTest()
		{
			var cost = 58476;
			var purchase = new DateTime(2008, 8, 19);
			var firstPeriod = new DateTime(2009, 6, 14);
			var salvage = 300;
			var period = 1;
			var depreciationRate = 0.15;
			var basis = 1;
			var amordegrc = FinancialFunctions.AMORLINC(cost, purchase, firstPeriod, salvage, period, depreciationRate, basis);
			Assert.AreEqual(8771.4, amordegrc, Accuracy, "AMORLINC failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPDAYSBSTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, freq, basis);
			Assert.AreEqual(71, cbs, Accuracy, "COUPDAYSBS failed.");

			settlement = SpreadSheetFormatHelper.ConvertDoubleToDateTime(40568);
			maturity = SpreadSheetFormatHelper.ConvertDoubleToDateTime(40862);

			//70, 40568, 40862, 2, 0
			cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, 2, 0);
			Assert.AreEqual(70, cbs, Accuracy, "COUPDAYSBS failed.");

			//71, 40568, 40862, 2, 1
			cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, 2, 1);
			Assert.AreEqual(71, cbs, Accuracy, "COUPDAYSBS failed.");

			//71, 40568, 40862, 2, 2
			cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, 2, 2);
			Assert.AreEqual(71, cbs, Accuracy, "COUPDAYSBS failed.");

			//71, 40568, 40862, 2, 3
			cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, 2, 3);
			Assert.AreEqual(71, cbs, Accuracy, "COUPDAYSBS failed.");

			//70, 40568, 40862, 2, 4
			cbs = FinancialFunctions.COUPDAYSBS(settlement, maturity, 2, 4);
			Assert.AreEqual(70, cbs, Accuracy, "COUPDAYSBS failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPDAYSTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var cbs = FinancialFunctions.COUPDAYS(settlement, maturity, freq, basis);
			Assert.AreEqual(181, cbs, Accuracy, "COUPDAYS failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPDAYSNCTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, freq, basis);
			Assert.AreEqual(110, cbs, Accuracy, "COUPDAYSNC failed.");

			settlement = SpreadSheetFormatHelper.ConvertDoubleToDateTime(40568);
			maturity = SpreadSheetFormatHelper.ConvertDoubleToDateTime(40862);

			//294, 40568, 40862, 1, 2
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 1, 2);
			Assert.AreEqual(294, cbs, Accuracy, "COUPDAYSNC failed.");

			//294, 40568, 40862, 1, 3
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 1, 3);
			Assert.AreEqual(294, cbs, Accuracy, "COUPDAYSNC failed.");

			//110, 40568, 40862, 2, 2
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 2, 2);
			Assert.AreEqual(110, cbs, Accuracy, "COUPDAYSNC failed.");

			//110, 40568, 40862, 2, 3
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 2, 2);
			Assert.AreEqual(110, cbs, Accuracy, "COUPDAYSNC failed.");

			//21, 40568, 40862, 4,2
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 4, 2);
			Assert.AreEqual(21, cbs, Accuracy, "COUPDAYSNC failed.");

			//21, 40568, 40862, 4,3
			cbs = FinancialFunctions.COUPDAYSNC(settlement, maturity, 4, 2);
			Assert.AreEqual(21, cbs, Accuracy, "COUPDAYSNC failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPDNCDTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var date = FinancialFunctions.COUPNCD(settlement, maturity, freq, basis);
			Assert.AreEqual(new DateTime(2011, 5, 15), date, "COUPDNCD failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPNUMTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var num = FinancialFunctions.COUPNUM(settlement, maturity, freq, basis);
			Assert.AreEqual(2, num, Accuracy, "COUPNUM failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void COUPPCDTest()
		{
			var settlement = new DateTime(2011, 1, 25);
			var maturity = new DateTime(2011, 11, 15);
			var freq = 2;
			var basis = 1;
			var num = FinancialFunctions.COUPPCD(settlement, maturity, freq, basis);
			Assert.AreEqual(new DateTime(2010, 11, 15), num, "COUPPCD failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void CUMIPMTTest()
		{
			var rate = 0.09;
			var years = 30;
			var value = 125000;
			var num = FinancialFunctions.CUMIPMT(rate / 12, years * 12, value, 13, 24, 0);
			Assert.AreEqual(-11135.23213075, num, Accuracy, "CUMIPMT failed.");
			num = FinancialFunctions.CUMIPMT(rate / 12, years * 12, value, 1, 1, 0);
			Assert.AreEqual(-937.50, num, Accuracy, "CUMIPMT failed.");

			rate = 0.049;
			years = 31;
			value = 125000;
			num = FinancialFunctions.CUMIPMT(rate / 12, years * 12, value, 13, 24, 0);
			Assert.AreEqual(-5997.41116945266, num, Accuracy, "CUMIPMT failed.");

			rate = 0.987;
			years = 31;
			value = 1748541;
			num = FinancialFunctions.CUMIPMT(rate / 12, years * 12, value, 13, 24, 1);
			Assert.AreEqual(-1594650.00415715000, num, Accuracy, "CUMIPMT failed.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void CUMPRINCTest()
		{
			var rate = 0.09;
			var years = 30;
			var value = 125000;
			var num = FinancialFunctions.CUMPRINC(rate / 12, years * 12, value, 13, 24, 0);
			Assert.AreEqual(-934.107123, num, Accuracy, "CUMPRINC failed.");

			num = FinancialFunctions.CUMPRINC(rate / 12, years * 12, value, 1, 1, 0);
			Assert.AreEqual(-68.27827118, num, Accuracy, "CUMPRINC failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void DISCCTest()
		{
			var settlement = new DateTime(2007, 1, 25);
			var maturity = new DateTime(2007, 6, 15);
			var price = 97.975;
			var redemption = 100;
			var basis = 1;
			var num = FinancialFunctions.DISC(settlement, maturity, price, redemption, basis);
			Assert.AreEqual(0.0524202, num, Accuracy, "DISC failed.");

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void DBTest()
		{
			var cost = 1E6;
			var salvage = 1E5;

			var year1 = FinancialFunctions.DB(cost, salvage, 6, 1, 7);
			var year2 = FinancialFunctions.DB(cost, salvage, 6, 2, 7);
			var year3 = FinancialFunctions.DB(cost, salvage, 6, 3, 7);
			var year4 = FinancialFunctions.DB(cost, salvage, 6, 4, 7);
			var year5 = FinancialFunctions.DB(cost, salvage, 6, 5, 7);
			var year6 = FinancialFunctions.DB(cost, salvage, 6, 6, 7);

			Assert.AreEqual(186083.33333333, year1, Accuracy);
			Assert.AreEqual(259639.41666667, year2, Accuracy);
			Assert.AreEqual(176814.44275000, year3, Accuracy);
			Assert.AreEqual(120410.63551275, year4, Accuracy);
			Assert.AreEqual(81999.64278418, year5, Accuracy);
			Assert.AreEqual(55841.75673603, year6, Accuracy);

			Assert.AreEqual(0, FinancialFunctions.DB(0, 100000, 6, 1, 12));
			Assert.AreEqual(55841.7567360284000, FinancialFunctions.DB(cost, salvage, 6, 6, 7), Accuracy);
			Assert.AreEqual(15845.09847385, FinancialFunctions.DB(cost, salvage, 6, 7, 7), Accuracy);
			Assert.AreEqual(0, FinancialFunctions.DB(0, salvage, 6, 1, 7), Accuracy);
			Assert.AreEqual(0, FinancialFunctions.DB(0, salvage, 6, 7, 7), Accuracy);

			//FunctionsTestHelper.AssertFunctionResult(db, 0, 1000000, 0, 6, 7, 7);
			Assert.AreEqual(27.00606714, FinancialFunctions.DB(1000000, 6, 7, 7, 12), Accuracy);

			//FunctionsTestHelper.AssertFunctionResult(db, 46722.5182806209, 1000000, 100000, 6, 6);
			Assert.AreEqual(46722.5182806209, FinancialFunctions.DB(1000000, 100000, 6, 6, 12), Accuracy);

			//FunctionsTestHelper.AssertFunctionResult(db, 15845.0984738481, 1000000, 100000, 6, 7, 7);
			Assert.AreEqual(15845.0984738481, FinancialFunctions.DB(1000000, 100000, 6, 7, 7), Accuracy);

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void DOLLARTest()
		{
			Assert.AreEqual(1.125, FinancialFunctions.DOLLARDE(1.02, 16), Accuracy);
			Assert.AreEqual(1.3125, FinancialFunctions.DOLLARDE(1.1, 32), Accuracy);
			Assert.AreEqual(1.02, FinancialFunctions.DOLLARFR(1.125, 16), Accuracy);
			Assert.AreEqual(1.04, FinancialFunctions.DOLLARFR(1.125, 32), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void DURATIONTest()
		{
			var settlement = new DateTime(2008, 1, 1);
			var maturity = new DateTime(2016, 1, 1);
			var coupon = 0.08;
			var yield = 0.09;
			var frequency = 2;
			var basis = 1;
			var duration = FinancialFunctions.DURATION(settlement, maturity, coupon, yield, frequency, basis);
			Assert.AreEqual(5.99377496, duration, Accuracy);

			//6.14906788677325, 39448, 42370, 0.08, 0.09, 1, 0
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 1, 0);
			Assert.AreEqual(6.14906788677325, duration, Accuracy);

			//6.14906788677324, 39448, 42370, 0.08, 0.09, 1, 4
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 1, 4);
			Assert.AreEqual(6.14906788677325, duration, Accuracy);

			//5.99377495554519, 39448, 42370, 0.08, 0.09, 2, 0
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 2, 0);
			Assert.AreEqual(5.99377495554519, duration, Accuracy);

			//5.99377495554519, 39448, 42370, 0.08, 0.09, 2, 4
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 2, 4);
			Assert.AreEqual(5.99377495554519, duration, Accuracy);

			//5.91465300919712, 39448, 42370, 0.08, 0.09, 4, 0
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 4, 0);
			Assert.AreEqual(5.91465300919712, duration, Accuracy);

			//5.91465300919712, 39448, 42370, 0.08, 0.09, 4, 4
			duration = FinancialFunctions.DURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 4, 4);
			Assert.AreEqual(5.91465300919712, duration, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void EFFECTTest()
		{
			var interest = 0.0525;
			var coupPerYer = 4;
			var effect = FinancialFunctions.EFFECT(interest, coupPerYer);
			Assert.AreEqual(0.0535427, effect, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void FVSCHEDULETest()
		{
			Assert.AreEqual(1.3308900000, FinancialFunctions.FVSCHEDULE(1, new double[] { 0.09, 0.11, 0.10 }), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void INTRATETest()
		{
			var settlement = new DateTime(2008, 2, 15);
			var maturity = new DateTime(2008, 5, 15);
			var investment = 1000000;
			var redemption = 1014420;
			var basis = 0;
			var intrate = FinancialFunctions.INRATE(settlement, maturity, investment, redemption, basis);
			Assert.AreEqual(0.057680000, intrate, Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ISPMTTest()
		{
			var interest = 0.1;
			var period = 1;
			var years = 3;
			var loan = 8000000;
			Assert.AreEqual(-64814.81481481, FinancialFunctions.ISPMT(interest / 12, period, years * 12, loan), Accuracy);
			Assert.AreEqual(-533333.3333333, FinancialFunctions.ISPMT(interest, 1, years, loan), Accuracy);

			//the following test are semantic nonsense since PER should be at least one

			//-66666.6666666667, 0.00833333333333333, 0, 36, 8000000
			Assert.AreEqual(-66666.6666666667, FinancialFunctions.ISPMT(0.00833333333333333, 0, 36, 8000000), Accuracy);
			//-68518.5185185185, 0.00833333333333333, -1, 36, 8000000
			Assert.AreEqual(-68518.5185185185, FinancialFunctions.ISPMT(0.00833333333333333, -1, 36, 8000000), Accuracy);
			//-133333.333333333, 0.00833333333333333, 1, -1, 8000000
			Assert.AreEqual(-133333.333333333, FinancialFunctions.ISPMT(0.00833333333333333, 1, -1, 8000000), Accuracy);
			//3703.7037037037, 0.00833333333333333, 38, 36, 8000000
			Assert.AreEqual(3703.7037037037, FinancialFunctions.ISPMT(0.00833333333333333, 38, 36, 8000000), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void MDURATIONTest()
		{
			var settlment = new DateTime(2008, 1, 1);
			var maturity = new DateTime(2016, 1, 1);
			var coupon = 0.08;
			var yield = 0.09;
			var freq = 2;
			var basis = 1;
			Assert.AreEqual(5.735669814, FinancialFunctions.MDURATION(settlment, maturity, coupon, yield, freq, basis), Accuracy);

			//5.64134668511307, 39448, 42370, 0.08, 0.09, 1, 0
			Assert.AreEqual(5.64134668511307, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 1, 0), Accuracy);

			//5.64134668511307, 39448, 42370, 0.08, 0.09, 1, 4
			Assert.AreEqual(5.64134668511307, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 1, 4), Accuracy);

			//5.73566981391884, 39448, 42370, 0.08, 0.09, 2, 0
			Assert.AreEqual(5.73566981391884, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 2, 0), Accuracy);

			//5.73566981391884, 39448, 42370, 0.08, 0.09, 2, 4
			Assert.AreEqual(5.73566981391884, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 2, 4), Accuracy);

			//5.78450172048619, 39448, 42370, 0.08, 0.09, 4, 0
			Assert.AreEqual(5.78450172048619, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 4, 0), Accuracy);

			//5.78450172048619, 39448, 42370, 0.08, 0.09, 4, 4
			Assert.AreEqual(5.78450172048619, FinancialFunctions.MDURATION(new DateTime(2008, 1, 1), new DateTime(2016, 1, 1), 0.08, 0.09, 4, 4), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void NOMINALTest()
		{
			var rate = 0.053543;
			var coupons = 4;
			Assert.AreEqual(0.05250032, FinancialFunctions.NOMINAL(rate, coupons), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ODDFPRICETest()
		{
			var settlement = new DateTime(2008, 11, 11);
			var maturity = new DateTime(2021, 3, 1);
			var issue = new DateTime(2008, 10, 15);
			var coupon = new DateTime(2009, 3, 1);
			var rate = 0.0785;
			var yield = 0.0625;
			var redempt = 100;
			var freq = 2;
			var basis = 1;

			Assert.AreEqual(113.59771747, FinancialFunctions.ODDFPRICE(settlement, maturity, issue, coupon, rate, yield, redempt, freq, basis), Accuracy);
			Assert.AreEqual(26.62813649, FinancialFunctions.ODDFYIELD(settlement, maturity, issue, coupon, rate, yield, redempt, freq, basis), Accuracy);

			//113.650687054409, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 4, 0
			Assert.AreEqual(113.650687054409, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 4, 0), Accuracy);

			//113.650021611091, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 4, 1
			Assert.AreEqual(113.650021611091, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 4, 1), Accuracy);
			//113.650277739118, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 4, 2
			Assert.AreEqual(113.650277739118, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 4, 2), Accuracy);
			//113.649958352793, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 4, 3
			Assert.AreEqual(113.649958352793, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 4, 3), Accuracy);
			//113.650687054409, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 4, 4
			Assert.AreEqual(113.650687054409, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 4, 4), Accuracy);

			//47.4250482962375, 39763, 44256, 39736, 39873, 0, 0.0625, 100, 1, 0
			Assert.AreEqual(47.4250482962375, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0, 0.0625, 100, 1, 0), Accuracy);

			//196.598611111111, 39763, 44256, 39736, 39873, 0.0785, 0, 100, 1, 0
			Assert.AreEqual(196.598611111111, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0, 100, 1, 0), Accuracy);

			//113.498148746163, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 1, 0
			Assert.AreEqual(113.498148746163, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 1, 0), Accuracy);

			//113.494585545507, 39763, 44256, 39736, 39873, 0.0785, 0.0625, 100, 1, 3
			Assert.AreEqual(113.494585545507, FinancialFunctions.ODDFPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0785, 0.0625, 100, 1, 3), Accuracy);

		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ODDFYIELDTest()
		{
			//0.0772455415972989, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 2, 0
			Assert.AreEqual(0.0772455415972989, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 2, 0), Accuracy);

			// 0.0772500780535923, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 2, 3
			Assert.AreEqual(0.0772500780535923, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 2, 3), Accuracy);






			//0.077097558315507, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 4, 0);
			Assert.AreEqual(0.077097558315507, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 4, 0), Accuracy);

			//0.0770979399217127, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 4, 1);
			Assert.AreEqual(0.0770979399217127, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 4, 1), Accuracy);

			//0.0770970390819706, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 4, 2);
			Assert.AreEqual(0.0770970390819706, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 4, 2), Accuracy);

			//0.07709816193298, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 4, 3);
			Assert.AreEqual(0.07709816193298, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 4, 3), Accuracy);

			//0.077097558315507, 39763, 44256, 39736, 39873, 0.0575, 84.5, 100, 4, 4);
			Assert.AreEqual(0.077097558315507, FinancialFunctions.ODDFYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39763),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(44256),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39736),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39873),
				0.0575, 84.5, 100, 4, 4), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ODDLPRICETest()
		{
			var settlement = new DateTime(2008, 2, 7);
			var maturity = new DateTime(2008, 6, 14);
			var last = new DateTime(2007, 9, 15);
			var rate = 0.0375;
			var yield = 0.0405;
			var redempt = 100;
			var freq = 2;
			var basis = 0;
			Assert.AreEqual(99.87482156, FinancialFunctions.ODDLPRICE(settlement, maturity, last, rate, yield, redempt, freq, basis), Accuracy);
			Assert.AreEqual(188.922854, FinancialFunctions.ODDLYIELD(settlement, maturity, last, rate, yield, redempt, freq, basis), Accuracy);


			//99.8782860147213, 39485, 39614, 39370, 0.0375, 0.0405, 100, 4, 0);
			Assert.AreEqual(99.8782860147213, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 4, 0), Accuracy);

			//99.8785673299155, 39485, 39614, 39370, 0.0375, 0.0405, 100, 4, 1);
			Assert.AreEqual(99.8785673299155, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 4, 1), Accuracy);

			//99.8785673299155, 39485, 39614, 39370, 0.0375, 0.0405, 100, 4, 2);
			Assert.AreEqual(99.8785673299155, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 4, 2), Accuracy);

			//99.8785673299155, 39485, 39614, 39370, 0.0375, 0.0405, 100, 4, 3);
			Assert.AreEqual(99.8785673299155, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 4, 3), Accuracy);

			//99.8782860147213, 39485, 39614, 39370, 0.0375, 0.0405, 100, 4, 4);
			Assert.AreEqual(99.8782860147213, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 4, 4), Accuracy);

			//99.8791676815291, 39485, 39614, 39370, 0.0375, 0.0405, 100, 2, 3
			Assert.AreEqual(99.8791676815291, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 2, 3), Accuracy);

			//99.8791676815291, 39485, 39614, 39370, 0.0375, 0.0405, 100, 2, 1
			Assert.AreEqual(99.8791676815291, FinancialFunctions.ODDLPRICE(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39485),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39370),
				0.0375, 0.0405, 100, 2, 1), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void ODDLYIELDTest()
		{

			//0.0451922356291692, 39558, 39614, 39440, 0.0375, 99.875, 100, 4, 0);
			Assert.AreEqual(0.0451922356291692, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 4, 0), Accuracy);

			//0.0452224303408101, 39558, 39614, 39440, 0.0375, 99.875, 100, 4, 1);
			Assert.AreEqual(0.0452224303408101, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 4, 1), Accuracy);

			//0.0452224303408101, 39558, 39614, 39440, 0.0375, 99.875, 100, 4, 2);
			Assert.AreEqual(0.0452224303408101, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 4, 2), Accuracy);

			//0.0452224303408101, 39558, 39614, 39440, 0.0375, 99.875, 100, 4, 3);
			Assert.AreEqual(0.0452224303408101, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 4, 3), Accuracy);

			//0.0451922356291692, 39558, 39614, 39440, 0.0375, 99.875, 100, 4, 4);
			Assert.AreEqual(0.0451922356291692, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 4, 4), Accuracy);
			//0.045179885491872, 39558, 39614, 39440, 0.0375, 99.875, 100, 2, 1
			Assert.AreEqual(0.045179885491872, FinancialFunctions.ODDLYIELD(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39558),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39614),
				SpreadSheetFormatHelper.ConvertDoubleToDateTime(39440),
				0.0375, 99.875, 100, 2, 1), Accuracy);


		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PRICETest()
		{
			var settlement = new DateTime(2008, 2, 15);
			var maturity = new DateTime(2017, 11, 15);
			var rate = 0.0575;
			var yield = 0.06500;
			var redempt = 100;
			var freq = 2;
			var basis = 0;
			Assert.AreEqual(94.63436162, FinancialFunctions.PRICE(settlement, maturity, rate, yield, redempt, freq, basis), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PRICEDISCTest()
		{
			var settlement = new DateTime(2008, 2, 16);
			var maturity = new DateTime(2008, 3, 1);
			var rate = 0.0525;
			var redempt = 100;
			var basis = 2;
			Assert.AreEqual(99.79583333, FinancialFunctions.PRICEDISC(settlement, maturity, rate, redempt, basis), Accuracy);
		}


		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void PDURATIONTest()
		{
			Assert.AreEqual(3.859866163, FinancialFunctions.PDURATION(0.025, 2000, 2200), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void RECEIVEDTest()
		{
			var settlement = new DateTime(2008, 2, 15);
			var maturity = new DateTime(2008, 5, 15);
			var rate = 0.06;
			var invest = 1000000;
			var basis = 2;
			Assert.AreEqual(1015228.4263959, FinancialFunctions.Received(settlement, maturity, invest, rate, basis), Accuracy);
			Assert.AreEqual(1014584.6544071, FinancialFunctions.Received(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39493), SpreadSheetFormatHelper.ConvertDoubleToDateTime(39583), 1000000, 0.0575, 0), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void RRITest()
		{
			Assert.AreEqual(0.019061773, FinancialFunctions.RRI(147, 20, 321), Accuracy);
			Assert.AreEqual(0, FinancialFunctions.RRI(8, 0, 0), Accuracy);

			//FunctionsTestHelper.AssertFunctionResult(rri, 0, 8, 0, 0);
			Assert.AreEqual(0, FinancialFunctions.RRI(8, 0, 0), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void SLNTest()
		{
			Assert.AreEqual(45.8785046729, FinancialFunctions.SLN(14747, 20, 321), Accuracy);
			Assert.AreEqual(-770, FinancialFunctions.SLN(-200, 7500, 10), Accuracy);
			Assert.AreEqual(3020, FinancialFunctions.SLN(30000, -200, 10), Accuracy);
			Assert.AreEqual(-22500, FinancialFunctions.SLN(30000, 7500, -1), Accuracy);
			Assert.AreEqual(-225, FinancialFunctions.SLN(30000, 7500, -100), Accuracy);
			Assert.AreEqual(225, FinancialFunctions.SLN(-30000, -7500, -100), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void SYDTest()
		{
			Assert.AreEqual(4090.9090909090900000, FinancialFunctions.SYD(30000, 7500, 10, 1), Accuracy);
			Assert.AreEqual(409.0909090909090000, FinancialFunctions.SYD(30000, 7500, 10, 10), Accuracy);
			Assert.AreEqual(-65, FinancialFunctions.SYD(-300, 7500, 15, 15), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void TBILLEQTest()
		{
			Assert.AreEqual(0.0941514936, FinancialFunctions.TBILLEQ(new DateTime(2008, 3, 31), new DateTime(2008, 6, 1), 0.0914), Accuracy);
			Assert.AreEqual(98.4258888889, FinancialFunctions.TBILLPRICE(new DateTime(2008, 3, 31), new DateTime(2008, 6, 1), 0.0914), Accuracy);
			Assert.AreEqual(6346.9852474059, FinancialFunctions.TBILLYIELD(new DateTime(2008, 3, 31), new DateTime(2008, 6, 1), 0.0914), Accuracy);
			Assert.AreEqual(36.5, FinancialFunctions.TBILLEQ(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39538), SpreadSheetFormatHelper.ConvertDoubleToDateTime(39600), 5), Accuracy);
			Assert.AreEqual(0.554711246200608, FinancialFunctions.TBILLEQ(SpreadSheetFormatHelper.ConvertDoubleToDateTime(39538), SpreadSheetFormatHelper.ConvertDoubleToDateTime(39600), .5), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void VDBTest()
		{
			Assert.AreEqual(1.3150684932, FinancialFunctions.VDB(2400, 300, 10 * 365, 0, 1), Accuracy);
			Assert.AreEqual(40.0, FinancialFunctions.VDB(2400, 300, 10 * 12, 0, 1), Accuracy);
			Assert.AreEqual(480.0000000000, FinancialFunctions.VDB(2400, 300, 10, 0, 1), Accuracy);
			Assert.AreEqual(396.3060532648, FinancialFunctions.VDB(2400, 300, 10 * 12, 6, 18), Accuracy);
			Assert.AreEqual(311.8089366582, FinancialFunctions.VDB(2400, 300, 10 * 12, 6, 18, 1.5), Accuracy);
			Assert.AreEqual(315.0000000000, FinancialFunctions.VDB(2400, 300, 10, 0, 0.875, 1.5), Accuracy);
			Assert.AreEqual(393.44827586207, FinancialFunctions.VDB(5412, 522, 87, 1, 8, 0.33), Accuracy);
			Assert.AreEqual(570.34380334585, FinancialFunctions.VDB(456123, 522, 8787, 1, 12, 0.733), Accuracy);
			Assert.AreEqual(107.18157969762, FinancialFunctions.VDB(951156.554, 2.564, 8874.23, 11, 12, 0.733), Accuracy);

			Assert.AreEqual(0, FinancialFunctions.VDB(2400, 300, 3650, 0, 0), Accuracy);
			Assert.AreEqual(0, FinancialFunctions.VDB(2400, 300, 10, 18, 18, 1.5), Accuracy);

			var bordercases = new[] {
				Tuple.Create(2400d, 300d, 3650d, -1d, 1d), 
				Tuple.Create(300d, 10d, 6d, 18d, 1.5d), 
				Tuple.Create(2400d, 300d, 3650d, 0d, -1d), 
			};
			var result = bordercases.Aggregate(true, (b, t) => {
				try {
					Console.WriteLine(FinancialFunctions.VDB(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5));
					return false;
				} catch(Exception) {
					return true;
				}
			});
			Assert.IsTrue(result, "Some border cases did not raise an error.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void XIRRTest()
		{
			Assert.AreEqual(0.373362535, FinancialFunctions.XIRR(
				new double[] {-10000,
					2750,
					4250,
					3250,
					2750
				},
				new[] {
					new DateTime(2008, 1, 1),
					new DateTime(2008, 3, 1),
					new DateTime(2008, 10, 30),
					new DateTime(2009, 2, 15),
					new DateTime(2009, 4, 1),
				}), Accuracy);

			Assert.AreEqual(1.09924377203, FinancialFunctions.XIRR(
				new double[] {-10548,
					5532,
					6254,
					4100,
					1245
				},
				new[] {
					new DateTime(2012, 1, 1),
					new DateTime(2012, 3, 1),
					new DateTime(2012, 10, 30),
					new DateTime(2013, 2, 15),
					new DateTime(2013, 4, 1),
				}), Accuracy);

			Assert.AreEqual(1.64084802866, FinancialFunctions.XIRR(
				new double[] {-10548,
					5532,
					6254,
					4100,
					1245,
					5154
				},
				new[] {
					new DateTime(2012, 1, 1),
					new DateTime(2012, 3, 1),
					new DateTime(2012, 10, 30),
					new DateTime(2013, 2, 15),
					new DateTime(2013, 4, 1),
					new DateTime(2013, 6, 3),
				}), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void XNPVTest()
		{
			Assert.AreEqual(1994.51004065330000, FinancialFunctions.XNPV(0.1,
				new double[] {-10000,
					2750,
					4250,
					3250,
					2750
				},
				new[] {
					new DateTime(2008, 1, 1),
					new DateTime(2008, 3, 1),
					new DateTime(2008, 10, 30),
					new DateTime(2009, 2, 15),
					new DateTime(2009, 4, 1),
				}), Accuracy);

			Assert.AreEqual(7883.23632302493, FinancialFunctions.XNPV(0.24852,
				new double[] {-10548,
					5532,
					6254,
					4100,
					1245,
					5154
				},
				new[] {
					new DateTime(2012, 1, 1),
					new DateTime(2012, 3, 1),
					new DateTime(2012, 10, 30),
					new DateTime(2013, 2, 15),
					new DateTime(2013, 4, 1),
					new DateTime(2013, 6, 3),
				}), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void YEARFRACTest()
		{
			Assert.AreEqual(2.00277777778, FinancialFunctions.YEARFRAC(new DateTime(2010, 2, 1), new DateTime(2012, 2, 2)), Accuracy);
			Assert.AreEqual(49.60555555556, FinancialFunctions.YEARFRAC(new DateTime(1968, 12, 13), new DateTime(2018, 7, 21)), Accuracy);
			Assert.AreEqual(49.63561643836, FinancialFunctions.YEARFRAC(new DateTime(1968, 12, 13), new DateTime(2018, 7, 21), 3), Accuracy);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Financial")]
#else
		[Category("Financial")]
		#endif
        public void YIELDTest()
		{
			var settlement = new DateTime(2008, 2, 15);
			var maturity = new DateTime(2016, 11, 15);
			var issue = new DateTime(2005, 5, 15);
			var rate = 0.0575;
			var price = 95.04287;
			var redempt = 100;
			var freq = 2;
			var basis = 0;
			Assert.AreEqual(0.0650000069, FinancialFunctions.YIELD(settlement, maturity, rate, price, redempt, freq, basis), Accuracy);
			Assert.AreEqual(0.0059607748, FinancialFunctions.YIELDDISC(settlement, maturity, price, redempt, basis), Accuracy);
			Assert.AreEqual(0.0569799112, FinancialFunctions.YIELDMAT(settlement, maturity, issue, rate, price, basis), Accuracy);
		}

	}

	public static class SpreadSheetFormatHelper
	{
		#region Constants

		// NOTE: According to an article in MSDN: http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencypositivepattern.aspx
		internal static readonly string[] PositivePatternStrings = {
                                 "$n", "n$", "$ n", "n $" 
                               };

        // NOTE: According to an article in MSDN: http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencynegativepattern.aspx
        internal static readonly string[] NegativePatternStrings = 
                               { 
                                 "($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", 
                                 "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)" 
                               };
        internal static readonly string ZeroCountGroupName = "zeroCount";
        internal static readonly string UseThousandsSeparatorGroupName = "useThousandsSeparator";
        public static readonly DateTime StartDate = new DateTime(1900, 1, 1, 0, 0, 0);

        internal static readonly string ShortDatePattern = "M/d/yyyy";
        internal static readonly string LongTimePattern = "h:mm:ss tt";
        internal static readonly string ShortDateLongTimePattern = "M/d/yyyy h:mm:ss tt";
        internal static readonly string IsRtlRegExPattern = @"[\p{IsArabic}\p{IsHebrew}]";
        internal static readonly string GeneralFormatString = string.Empty;
        internal static readonly string TextPlaceholder = "@";
        internal static readonly double MinimumScientificValue = 100000000000;
        internal static readonly char FractionsInvisibleChar = '1';
        internal static readonly char MilliSecondsChar = 'f';
        internal static readonly string InvisibleDivider = "q";
        internal static readonly double InvisibleDividerFontsize = 0.01;

        internal static readonly char[] NumberChars = new char[] { '0', '#' };
        internal static readonly char[] DateChars = new char[] { 'y', 'm', 'd' };
        internal static readonly char[] TimeChars = new char[] { 'h', 's' };
        internal static readonly char TimeSeparator = ':';
        internal static readonly double MaxDoubleValueTranslatableToDateTime = 2958465d;

        //internal static readonly Color? DefaultFormatForeground = null;
        internal static readonly CultureInfo DefaultFormatCulture = null;
        internal static readonly Predicate<double> DefaultFormatCondition = null;

        #endregion


        #region Public Methods

        [DebuggerStepThrough]
        public static DateTime ConvertDoubleToDateTime(double doubleValue)
        {
            DateTime dateTime;
            if (doubleValue < 0) throw new Exception("Cannot convert to datetime.");

            var value = doubleValue;

            // NOTE: There is known bug in Excel - 1900 is consedered to be a leap year. 
            // That's why numbers before 3/1/1900 appear wrong and that's why
            // we need that adjustment - http://www.ozgrid.com/Excel/ExcelDateandTimes.htm
            if (value <= 60d) value += 1;

            long ticks;
            if (TryConvertDoubleDateToTicks(value, out ticks)) dateTime = new DateTime(ticks, DateTimeKind.Unspecified);
            else throw new Exception("Cannot convert to datetime.");
            Console.WriteLine(dateTime);
            return dateTime;
        }

        public static double ConvertDateTimeToDouble(DateTime dateTime)
        {
            double dateAsNumber = dateTime.ToOADate();

            if (dateAsNumber <= 60d && dateAsNumber >= 1d)
            {
                dateAsNumber--;
            }

            return dateAsNumber;
        }

        #endregion


        #region Extension Methods

        public static DateTime RoundMinutes(this DateTime dateTimeValue)
        {
            int milliseconds = dateTimeValue.Millisecond;

            DateTime result = new DateTime(dateTimeValue.Year, dateTimeValue.Month, dateTimeValue.Day,
                dateTimeValue.Hour, dateTimeValue.Minute, dateTimeValue.Second);

            if (milliseconds >= 500)
            {
                result = result.AddSeconds(1);
            }

            return result;
        }

        public static DateTime RoundMilliseconds(this DateTime dateTimeValue, int precision)
        {
            int milliseconds = dateTimeValue.Millisecond;
            string millisecondsStr = milliseconds.ToString();
            DateTime dateTimeResult = dateTimeValue;

            if (precision > 0 && millisecondsStr.Length > precision)
            {
                string formattingStr = "0." + string.Concat(Enumerable.Repeat('0', precision));
                double valToFormat = milliseconds / System.Math.Pow(10, millisecondsStr.Length);
                string result = valToFormat.ToString(formattingStr);
                int newMilliseconds = int.Parse(result.Substring(2));

                int multiplyBy = millisecondsStr.Length - newMilliseconds.ToString().Length;

                newMilliseconds *= (int)System.Math.Pow(10, multiplyBy);

                dateTimeResult = new DateTime(dateTimeResult.Year, dateTimeResult.Month, dateTimeResult.Day,
                    dateTimeResult.Hour, dateTimeResult.Minute, dateTimeResult.Second, newMilliseconds);
            }

            return dateTimeResult;
        }



        #endregion


        #region Methods

        internal static double Round(double value)
        {
            if (value > 0)
            {
                return System.Math.Floor(value);
            }

            return System.Math.Ceiling(value);
        }

        internal static string ReplaceCultureSpecificSeparatorsWithDefaults(string formatString, CultureInfo cultureInfo)
        {
            string numberGroupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            string numberDecimalSeparator = cultureInfo.NumberFormat.NumberDecimalSeparator;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < formatString.Length; i++)
            {
                if (formatString[i].ToString() == numberGroupSeparator)
                {
                    sb.Append(",");
                }
                else if (formatString[i].ToString() == numberDecimalSeparator)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(formatString[i]);
                }
            }

            return sb.ToString();
        }

        internal static CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        internal static DateTime? RoundUpValue(string formatString, double? doubleValue, DateTime? dateTimeValue)
        {
            DateTime? newDateTimeValue = dateTimeValue;
            if (!newDateTimeValue.HasValue || newDateTimeValue.Value.Millisecond == 0)
            {
                return newDateTimeValue;
            }

            if (!formatString.Contains(MilliSecondsChar))
            {
                newDateTimeValue = newDateTimeValue.Value.RoundMinutes();
            }
            else
            {
                int zerosCount = formatString.Where((c) => { return c == MilliSecondsChar; }).Count();
                newDateTimeValue = newDateTimeValue.Value.RoundMilliseconds(zerosCount);
            }

            return newDateTimeValue;
        }

        internal static string StripUnderscores(string text)
        {
            StringBuilder result = new StringBuilder();

            int i = 0;
            while (i < text.Length)
            {
                if (text[i] != '_' || i == (text.Length - 1))
                {
                    result.Append(text[i]);
                    i++;
                }
                else
                {
                    i += 2;
                }
            }

            return result.ToString();
        }

        internal static string StripBrackets(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            text = text.Trim();

            int startIndex = text.IndexOf('[');

            if (startIndex != -1)
            {
                int endIndex = text.IndexOf(']', startIndex);

                while (startIndex != -1 && startIndex < endIndex)
                {
                    text = text.Remove(startIndex, endIndex - startIndex + 1);
                    text = text.Trim();

                    startIndex = text.IndexOf('[');
                    if (startIndex != -1)
                    {
                        endIndex = text.IndexOf(']', startIndex);
                    }
                }
            }

            return text;
        }

        internal static bool ContainsTotalHoursMinutesSecondsModifiers(string formatString)
        {
            StringBuilder sb = new StringBuilder(formatString);

            if (sb.Length == 0)
            {
                return false;
            }

            sb.Replace(sb.ToString(), sb.ToString().Trim());
            int index;
            while ((index = sb.ToString().IndexOf('[')) != -1)
            {
                int index1 = sb.ToString().IndexOf(']', index);
                if (index1 == -1)
                {
                    break;
                }

                string textInBrackets = sb.ToString().Substring(index + 1, index1 - index - 1);
                if (textInBrackets == "h" || textInBrackets == "m" || textInBrackets == "s")
                {
                    return true;
                }

                sb.Remove(index, index1 - index + 1);

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    sb.Replace(sb.ToString(), sb.ToString().Trim());
                }
            }

            return false;
        }

        internal static bool TryGetDecimalPlacesForFormatString(string formatString, CultureInfo cultureInfo, out int decimalPlaces)
        {
            bool useThousandsSeparator;
            return TryGetDecimalPlacesForFormatString(formatString, cultureInfo, out decimalPlaces, out useThousandsSeparator);
        }

        internal static bool TryGetDecimalPlacesForFormatString(string formatString, CultureInfo cultureInfo,
            out int decimalPlaces, out bool useThousandsSeparator)
        {
            string pattern = string.Format(@"(?<{0}>#{1}##)?0({2}(?<{3}>0+))?",
                UseThousandsSeparatorGroupName,
                cultureInfo.NumberFormat.NumberGroupSeparator,
                cultureInfo.NumberFormat.NumberDecimalSeparator,
                ZeroCountGroupName);
            decimalPlaces = -1;
            useThousandsSeparator = false;

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(formatString);
            if (matches.Count > 0)
            {
                var match = matches[0];
                foreach (var currentMatch in matches.Cast<Match>().Where(currentMatch => currentMatch.Length > match.Length))
                {
                    match = currentMatch;
                }

                decimalPlaces = match.Groups[ZeroCountGroupName].Value.Length;
                useThousandsSeparator = match.Groups[UseThousandsSeparatorGroupName].Success;
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        private static bool TryConvertDoubleDateToTicks(double value, out long ticks)
        {
            ticks = -1;

            if (value >= 2958466 || value <= -657435) return false;
            var num1 = value * 86400000;
            double num;
            if (value >= 0)
            {
                num = 0.5;
            }
            else
            {
                num = -0.5;
            }
            var num2 = (long)(num1 + num);
            if (num2 < 0)
            {
                num2 = num2 - num2 % 86400000 * 2;
            }
            num2 = num2 + 59926435200000L;
            if (num2 < 0 || num2 >= 315537897600000L)
            {
                return false;
            }

            ticks = num2 * 10000;
            return true;
        }

        #endregion
    }
}