using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Common financial, actuarial and investment functions.
    /// </summary>
    public sealed partial class FinancialFunctions
    {
        /// <summary>
        /// Returns the accrued interest for a security that pays periodic interest.
        /// </summary>
        /// <param name="issue">The security's issue date.</param>
        /// <param name="firstInterestData">The security's first interest date.</param>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer..</param>
        /// <param name="rate">The security's annual coupon rate.</param>
        /// <param name="par">The security's par value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list>
        /// </param>
        public static double ACCRINT(DateTime issue, DateTime firstInterestData, DateTime settlement, double rate, double par, double frequency = 1, int basis = 0)
        {
            return AccrInt(issue, firstInterestData, settlement, rate, par, frequency, (DayCountBasis)basis, AccrIntCalcMethod.FromIssueToSettlement);
        }

        /// <summary>
        /// Returns the accrued interest for a security that pays interest at maturity.
        /// </summary>
        /// <param name="issue">The security's issue date.</param>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer..</param>
        /// <param name="rate">The security's annual coupon rate.</param>
        /// <param name="par">The security's par value.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list>
        /// </param>
        public static double ACCRINTM(DateTime issue, DateTime settlement, double rate, double par, int basis = 0)
        {
            return AccrIntM(issue, settlement, rate, par, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the depreciation for each accounting period. This function is provided for the French accounting system. 
        /// If an asset is purchased in the middle of the accounting period, the prorated depreciation is taken into account. 
        /// The function is similar to AMORLINC, except that a depreciation coefficient is applied in the calculation depending on the life of the assets.
        /// </summary>
        /// <param name="cost">The cost of the asset.</param>
        /// <param name="datePurchased">The date of the purchase of the asset.</param>
        /// <param name="firstPeriod">The date of the end of the first period.</param>
        /// <param name="salvage">The salvage value at the end of the life of the asset.</param>
        /// <param name="period">The period.</param>
        /// <param name="rate">The rate of depreciation.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list>
        /// </param>
        public static double AMORDEGRC(double cost, DateTime datePurchased, DateTime firstPeriod, double salvage, double period, double rate, int basis)
        {
            return Depreciation.AmorDegrc(cost, datePurchased, firstPeriod, salvage, period, rate, (DayCountBasis)basis, true);
        }

        /// <summary>
        /// Returns the depreciation for each accounting period. 
        /// This function is provided for the French accounting system. 
        /// If an asset is purchased in the middle of the accounting period, the prorated depreciation is taken into account.
        /// </summary>
        /// <param name="cost">The cost of the asset.</param>
        /// <param name="datePurchased">The date of the purchase of the asset.</param>
        /// <param name="firstPeriod">The date of the end of the first period.</param>
        /// <param name="salvage">The salvage value at the end of the life of the asset.</param>
        /// <param name="period">The period.</param>
        /// <param name="rate">The rate of depreciation.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list>
        /// </param>
        public static double AMORLINC(double cost, DateTime datePurchased, DateTime firstPeriod, double salvage, double period, double rate, int basis)
        {
            return Depreciation.AmorLinc(cost, datePurchased, firstPeriod, salvage, period, rate, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the depreciation for each accounting period.
        /// This function is provided for the French accounting system.
        /// If an asset is purchased in the middle of the accounting period, the prorated depreciation is taken into account.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double COUPDAYS(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupDays(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the number of days from the beginning of a coupon period until its settlement date.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double COUPDAYSBS(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupDaysBS(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the number of days from the settlement date to the next coupon date.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double COUPDAYSNC(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupDaysNC(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns a number that represents the next coupon date after the settlement date.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static DateTime COUPNCD(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupNCD(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the number of coupons payable between the settlement date and maturity date.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double COUPNUM(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupNum(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the previous coupon date before the settlement date.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static DateTime COUPPCD(DateTime settlement, DateTime maturity, int frequency, int basis)
        {
            return DayCount.CoupPCD(settlement, maturity, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the cumulative interest paid between two periods.
        /// </summary>
        /// <param name="rate">The interest rate.</param>
        /// <param name="nper">The total number of payment periods.</param>
        /// <param name="pv">The present value.</param>
        /// <param name="startPeriod">The first period in the calculation. Payment periods are numbered beginning with 1.</param>
        /// <param name="endPeriod">The last period in the calculation.</param>
        /// <param name="typ"> The timing of the payment:
        ///<list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>Payment at the end of the period.</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Payment at the beginning of the period.</description>
        /// </item>
        /// </list> 
        /// </param>
        public static double CUMIPMT(double rate, double nper, double pv, double startPeriod, double endPeriod, int typ)
        {
            return CumIpmtInternal(rate, nper, pv, startPeriod, endPeriod, (PaymentDue)typ);
        }

        /// <summary>
        /// Returns the cumulative principal paid on a loan between two periods.
        /// </summary>
        /// <param name="rate">The interest rate.</param>
        /// <param name="nper">The total number of payment periods.</param>
        /// <param name="pv">The present value.</param>
        /// <param name="startPeriod">The first period in the calculation. Payment periods are numbered beginning with 1.</param>
        /// <param name="endPeriod">The last period in the calculation.</param>
        /// <param name="typ"> The timing of the payment:
        ///<list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>Payment at the end of the period.</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Payment at the beginning of the period.</description>
        /// </item>
        /// </list> 
        /// </param>
        /// <returns></returns>
        public static double CUMPRINC(double rate, double nper, double pv, double startPeriod, double endPeriod, int typ)
        {
            return CumPrinc(rate, nper, pv, startPeriod, endPeriod, (PaymentDue)typ);
        }

        /// <summary>
        /// Returns the depreciation of an asset for a specified period by using the fixed-declining balance method.
        /// </summary>
        /// <param name="cost">The initial cost of the asset.</param>
        /// <param name="salvage">The value at the end of the depreciation (sometimes called the salvage value of the asset).</param>
        /// <param name="life">The number of periods over which the asset is being depreciated (sometimes called the useful life of the asset).</param>
        /// <param name="period">The period for which you want to calculate the depreciation. Period must use the same units as life.</param>
        /// <param name="month"> The number of months in the first year. If month is omitted, it is assumed to be 12.</param>
        public static double DB(double cost, double salvage, double life, double period, double month)
        {
            return Depreciation.Db(cost, salvage, life, period, month);
        }

        /// <summary>
        /// Returns the depreciation of an asset for a specified period using the double-declining balance method.
        /// </summary>
        /// <param name="cost">The initial cost of the asset.</param>
        /// <param name="salvage">The value at the end of the depreciation (sometimes called the salvage value of the asset). This should be positive or zero otherwise an exception will be thrown.</param>
        /// <param name="life">The number of periods over which the asset is being depreciated.</param>
        /// <param name="period">The period for which you want to calculate the depreciation.</param>
        /// <remarks>It's crucial to use for the <paramref name="life"/> and the <paramref name="period"/> parameters the same unit (years, days... and so on). If not a result will be returned but obviously have an incorrect meaning.</remarks>
        public static double DDB(double cost, double salvage, double life, double period)
        {
            return Depreciation.Ddb(cost, salvage, life, period, 2);
        }

        /// <summary>
        /// Returns the discount rate for a security.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="pr">The pr.</param>
        /// <param name="redemption"> The security's redemption value per $100 face value.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double DISC(DateTime settlement, DateTime maturity, double pr, double redemption, int basis)
        {
            return Disc(settlement, maturity, pr, redemption, (DayCountBasis)basis);
        }

        /// <summary>
        /// Converts a dollar price, expressed as a fraction, into a dollar price, expressed as a decimal number.
        /// </summary>
        /// <param name="fractionalDollar">The fractional dollar.</param>
        /// <param name="fraction">The integer to use in the denominator of a fraction.</param>
        public static double DOLLARDE(double fractionalDollar, double fraction)
        {
            return DollarDe(fractionalDollar, fraction);
        }

        /// <summary>
        /// Converts a dollar price, expressed as a decimal number, into a dollar price, expressed as a fraction.
        /// </summary>
        /// <param name="fractionalDollar">The fractional dollar.</param>
        /// <param name="fraction">The integer to use in the denominator of a fraction.</param>
        public static double DOLLARFR(double decimalDollar, double fraction)
        {
            return DollarFr(decimalDollar, fraction);
        }

        /// <summary>
        /// Returns the Macauley duration for an assumed par value of $100. 
        /// Duration is defined as the weighted average of the present value of the cash flows and is used as a measure of a bond price's response to changes in yield.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="coupon"> The security's annual coupon rate.</param>
        /// <param name="yld">The security's annual yield.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double DURATION(DateTime settlement, DateTime maturity, double coupon, double yld, int frequency, int basis)
        {
            return Duration(settlement, maturity, coupon, yld, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the effective annual interest rate.
        /// </summary>
        /// <param name="nominalRate">The nominal interest rate.</param>
        /// <param name="npery">The number of compounding periods per year.</param>
        public static double EFFECT(double nominalRate, double npery)
        {
            return Effect(nominalRate, npery);
        }

        /// <summary>
        /// Returns the future value of an investment based on periodic, constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="nPer">Nper is the total number of payment periods in an annuity.</param>
        /// <param name="pmt">Pmt is the payment made each period; it cannot change over the life of the annuity. Typically, pmt contains principal and interest but no other fees or taxes. If pmt is omitted, you must include the pv argument.</param>
        /// <param name="pv">Pv is the present value, or the lump-sum amount that a series of future payments is worth right now. If pv is omitted, it is assumed to be 0 (zero), and you must include the pmt argument.</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double FV(double rate, double nPer, double pmt, double pv = 0d, int due = 0)
        {
            return Fv(rate, nPer, pmt, pv, (PaymentDue)due);
        }

        /// <summary>
        /// Returns the future value of an initial principal after applying a series of compound interest rates.
        /// </summary>
        /// <param name="principal"> The present value.</param>
        /// <param name="schedule">An array of interest rates to apply.</param>
        public static double FVSCHEDULE(double principal, IEnumerable<double> schedule)
        {
            return FvSchedule(principal, schedule);
        }

        /// <summary>
        /// Returns the interest payment for a given period for an investment based on periodic, constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="per">Per is the period for which you want to find the interest and must be in the range 1 to nper</param>
        /// <param name="nPer">Nper   is the total number of payment periods in an annuity.</param>
        /// <param name="pv">Pv is the present value, or the lump-sum amount that a series of future payments is worth right now.</param>
        /// <param name="fv">Fv is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (the future value of a loan, for example, is 0).</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double IPMT(double rate, double per, double nPer, double pv, double fv = 0d, int due = 0)
        {
            return Ipmt(rate, per, nPer, pv, fv, (PaymentDue)due);
        }

        /// <summary>
        /// Returns the internal rate of return for a series of cash flows represented by the numbers in values. These cash flows do not have to be even, as they would be for an annuity. However, the cash flows must occur at regular intervals, such as monthly or annually. The internal rate of return is the interest rate received for an investment consisting of payments (negative values) and income (positive values) that occur at regular periods.
        /// </summary>
        /// <param name="values">
        /// Values is an array or a reference to cells that contain numbers for which you want to calculate the internal rate of return.
        /// <list type="bullet">
        ///<item>Values must contain at least one positive value and one negative value to calculate the internal rate of return.</item>
        /// <item>
        /// IRR uses the order of values to interpret the order of cash flows. Be sure to enter your payment and income values in the sequence you want.
        /// </item>
        /// <item>If an array or reference argument contains text, logical values, or empty cells, those values are ignored.</item> 
        /// </list>
        /// </param>
        /// <param name="guess">
        /// Guess  is a number that you guess is close to the result of IRR.
        /// <list type="bullet">
        /// <item>An iterative technique is used for calculating IRR. Starting with guess, IRR cycles through the calculation until the result is accurate within 0.00001 percent. If IRR can't find a result that works after 20 tries, the #NUM! error value is returned.</item>
        /// <item>In most cases you do not need to provide guess for the IRR calculation. If guess is omitted, it is assumed to be 0.1 (10 percent).</item>
        /// <item>If IRR gives the #NUM! error value, or if the result is not close to what you expected, try again with a different value for guess.</item>
        /// </list>
        ///  </param>
        public static double IRR(double[] values, double guess = 0.1)
        {
            return Irr(values, guess);
        }

        /// <summary>
        /// Calculates the interest paid during a specific period of an investment.
        /// </summary>
        /// <param name="rate">The interest rate for the investment.</param>
        /// <param name="per">The period for which you want to find the interest, and must be between 1 and nper.</param>
        /// <param name="nper"> The total number of payment periods for the investment.</param>
        /// <param name="pv">The present value of the investment. For a loan, pv is the loan amount.</param>
        public static double ISPMT(double rate, double per, double nper, double pv)
        {
            return Ispmt(rate, per, nper, pv);
        }

        /// <summary>
        /// Returns the interest rate for a fully invested security.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="investment">The amount invested in the security.</param>
        /// <param name="redemption">The amount to be received at maturity.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double INRATE(DateTime settlement, DateTime maturity, double investment, double redemption, int basis)
        {
            return IntRate(settlement, maturity, investment, redemption, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the Macauley modified duration for a security with an assumed par value of $100.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="coupon">The security's annual coupon rate.</param>
        /// <param name="yld"> The security's annual yield.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double MDURATION(DateTime settlement, DateTime maturity, double coupon, double yld, int frequency, int basis)
        {
            return MDuration(settlement, maturity, coupon, yld, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the modified internal rate of return for a series of periodic cash flows. MIRR considers both the cost of the investment and the interest received on reinvestment of cash.
        /// </summary>
        /// <param name="values">
        /// Values is an array or a reference to cells that contain numbers. These numbers represent a series of payments (negative values) and income (positive values) occurring at regular periods.
        /// <list type="bullet">
        /// <item>
        /// Values must contain at least one positive value and one negative value to calculate the modified internal rate of return. Otherwise, MIRR returns the #DIV/0! error value.
        /// </item>
        /// <item>
        /// If an array or reference argument contains text, logical values, or empty cells, those values are ignored; however, cells with the value zero are included.
        ///  </item>
        /// </list>
        /// </param>
        /// <param name="financeRate">Finance-rate is the interest rate you pay on the money used in the cash flows.</param>
        /// <param name="reinvestRate">Reinvest-rate is the interest rate you receive on the cash flows as you reinvest them.</param>
        public static double MIRR(double[] values, double financeRate, double reinvestRate)
        {
            return Mirr(values, financeRate, reinvestRate);
        }

        /// <summary>
        /// Returns the number of periods for an investment based on periodic, constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="pmt">Pmt is the payment made each period; it cannot change over the life of the annuity. Typically, pmt contains principal and interest but no other fees or taxes.</param>
        /// <param name="pv">The Pv is the present value, or the lump-sum amount that a series of future payments is worth right now.</param>
        /// <param name="fv">Fv  is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (the future value of a loan, for example, is 0).</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double NPER(double rate, double pmt, double pv, double fv = 0d, int due = 0)
        {
            return RaiseIfNonsense(Nper(rate, pmt, pv, fv, (PaymentDue)due));
        }

        /// <summary>
        /// Returns the net present value of an investment by using a discount rate and a series of future payments (negative values) and income (positive values).
        /// </summary>
        /// <param name="rate">Rate is the rate of discount over the length of one period.</param>
        /// <param name="values">The array of payments equally spaced in time and occurring at the end of a period.</param>
        public static double NPV(double rate, double[] values)
        {
            return Npv(rate, values);
        }

        /// <summary>
        /// Returns the annual nominal interest rate.
        /// </summary>
        /// <param name="effectRate">The effective interest rate.</param>
        /// <param name="npery">The number of compounding periods per year.</param>
        public static double NOMINAL(double effectRate, double npery)
        {
            return Nominal(effectRate, npery);
        }

        /// <summary>
        /// Returns the price per $100 face value of a security with an odd first period.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="issue">The security's issue date.</param>
        /// <param name="firstCoupon">The security's first coupon date.</param>
        /// <param name="rate">The security's interest rate.</param>
        /// <param name="yld">The security's annual yield.</param>
        /// <param name="redemption"> The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double ODDFPRICE(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double yld, double redemption, int frequency, int basis)
        {
            return OddFPrice(settlement, maturity, issue, firstCoupon, rate, yld, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the yield of a security with an odd first period.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="issue">The security's issue date.</param>
        /// <param name="firstCoupon">The security's first coupon date.</param>
        /// <param name="rate">The security's interest rate.</param>
        /// <param name="pr">The security's price.</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double ODDFYIELD(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, int frequency, int basis)
        {
            return OddFYield(settlement, maturity, issue, firstCoupon, rate, pr, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the price per $100 face value of a security with an odd last period.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="lastInterest"> The security's last coupon date.</param>
        /// <param name="rate">The security's interest rate.</param>
        /// <param name="yld">The security's annual yield.</param>
        /// <param name="redemption"> The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double ODDLPRICE(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double yld, double redemption, int frequency, int basis)
        {
            return OddLPrice(settlement, maturity, lastInterest, rate, yld, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the yield of a security with an odd last period.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="lastInterest">The last interest.</param>
        /// <param name="rate">The security's interest rate</param>
        /// <param name="pr">The security's price.</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double ODDLYIELD(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double pr, double redemption, int frequency, int basis)
        {
            return OddLYield(settlement, maturity, lastInterest, rate, pr, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the number of periods required by an investment to reach a specified value.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="pv">Pv is the present value of the investment.</param>
        /// <param name="fv"> Fv is the desired future value of the investment.</param>
        public static double PDURATION(double rate, double pv, double fv)
        {
            return (Math.Log(fv) - Math.Log(pv)) / Math.Log(1 + rate);
        }

        /// <summary>
        /// Calculates the payment for a loan based on constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="nPer">Nper is the total number of payments for the loan.</param>
        /// <param name="pv">Pv is the present value, or the total amount that a series of future payments is worth now; also known as the principal.</param>
        /// <param name="fv">Fv is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (zero), that is, the future value of a loan is 0.</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double PMT(double rate, double nPer, double pv, double fv = 0d, int due = 0)
        {
            return Pmt(rate, nPer, pv, fv, (PaymentDue)due);
        }

        /// <summary>
        /// Returns the payment on the principal for a given period for an investment based on periodic, constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period.</param>
        /// <param name="per">Per is the period for which you want to find the interest and must be in the range 1 to nper</param>
        /// <param name="nPer">Nper   is the total number of payment periods in an annuity.</param>
        /// <param name="pv">Pv is the present value, or the lump-sum amount that a series of future payments is worth right now.</param>
        /// <param name="fv">Fv is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (the future value of a loan, for example, is 0).</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double PPMT(double rate, double per, double nPer, double pv, double fv = 0d, int due = 0)
        {
            return Ppmt(rate, per, nPer, pv, fv, (PaymentDue)due);
        }

        /// <summary>
        /// Returns the present value of an investment. The present value is the total amount that a series of future payments is worth now. For example, when you borrow money, the loan amount is the present value to the lender.
        /// </summary>
        /// <param name="rate">Rate is the interest rate per period. For example, if you obtain an automobile loan at a 10 percent annual interest rate and make monthly payments, your interest rate per month is 10%/12, or 0.83%. You would enter 10%/12, or 0.83%, or 0.0083, into the formula as the rate.</param>
        /// <param name="nPer">Nper is the total number of payment periods in an annuity. For example, if you get a four-year car loan and make monthly payments, your loan has 4*12 (or 48) periods. You would enter 48 into the formula for nper.</param>
        /// <param name="pmt">Pmt is the payment made each period and cannot change over the life of the annuity. Typically, pmt includes principal and interest but no other fees or taxes. For example, the monthly payments on a $10,000, four-year car loan at 12 percent are $263.33. You would enter -263.33 into the formula as the pmt. If pmt is omitted, you must include the fv argument.</param>
        /// <param name="fv">Fv is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (the future value of a loan, for example, is 0). For example, if you want to save $50,000 to pay for a special project in 18 years, then $50,000 is the future value. You could then make a conservative guess at an interest rate and determine how much you must save each month. If fv is omitted, you must include the pmt argument.</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        public static double PV(double rate, double nPer, double pmt, double fv = 0d, int due = 0)
        {
            return RaiseIfNonsense(Pv(rate, nPer, pmt, fv, (PaymentDue)due));
        }

        /// <summary>
        /// Returns the price per $100 face value of a security that pays periodic interest.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="rate">The security's annual coupon rate.</param>
        /// <param name="yld">The security's annual yield.</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double PRICE(DateTime settlement, DateTime maturity, double rate, double yld, double redemption, int frequency, int basis)
        {
            return Price(settlement, maturity, rate, yld, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        ///Returns the price per $100 face value of a discounted security
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="discount">The security's discount rate.</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double PRICEDISC(DateTime settlement, DateTime maturity, double discount, double redemption, int basis)
        {
            return PriceDisc(settlement, maturity, discount, redemption, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the interest rate per period of an annuity. RATE is calculated by iteration and can have zero or more solutions. If the successive results of RATE do not converge to within 0.0000001 after 20 iterations, RATE returns the #NUM! error value.
        /// </summary>
        /// <param name="nPer">Nper is the total number of payment periods in an annuity.</param>
        /// <param name="pmt">Pmt is the payment made each period and cannot change over the life of the annuity. Typically, pmt includes principal and interest but no other fees or taxes. If pmt is omitted, you must include the fv argument</param>
        /// <param name="pv">Pv is the present value — the total amount that a series of future payments is worth now.</param>
        /// <param name="fv">Fv is the future value, or a cash balance you want to attain after the last payment is made. If fv is omitted, it is assumed to be 0 (the future value of a loan, for example, is 0).</param>
        /// <param name="due">The due parameter indicates when payments are due.</param>
        /// <param name="guess">
        ///Guess is your guess for what the rate will be.
        /// <list type="bullet">
        ///<item>
        ///If you omit guess, it is assumed to be 10 percent.
        /// </item> 
        /// <item>If RATE does not converge, try different values for guess. RATE usually converges if guess is between 0 and 1. </item>
        /// </list>
        /// </param>
        public static double RATE(double nPer, double pmt, double pv, double fv = 0d, int due = 0, double guess = 0.1)
        {
            return RaiseIfNonsense(Rate(nPer, pmt, pv, fv, (PaymentDue)due, 0.1));
        }

        /// <summary>
        /// Returns an equivalent interest rate for the growth of an investment.
        /// </summary>
        /// <param name="nper">Nper is the number of periods for the investment.</param>
        /// <param name="pv"> Pv is the present value of the investment.</param>
        /// <param name="fv">Fv is the future value of the investment.</param>
        /// <returns></returns>
        public static double RRI(double nper, double pv, double fv)
        {
            if (pv == 0) return 0d;
            if (nper == 0) return 0d;
            return (Math.Pow(fv / pv, 1 / nper)) - 1;
        }

        /// <summary>
        /// Returns the amount received at maturity for a fully invested security.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="investment">The amount invested in the security.</param>
        /// <param name="discount">The security's discount rate.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double Received(DateTime settlement, DateTime maturity, double investment, double discount, int basis)
        {
            return Received(settlement, maturity, investment, discount, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the straight-line depreciation of an asset for one period.
        /// </summary>
        /// <param name="cost">Cost is the initial cost of the asset.</param>
        /// <param name="salvage">Salvage is the value at the end of the depreciation (sometimes called the salvage value of the asset).</param>
        /// <param name="life">Life is the number of periods over which the asset is depreciated (sometimes called the useful life of the asset).</param>

        /// <exception cref="System.ArgumentException"></exception>
        public static double SLN(double cost, double salvage, double life)
        {
            return Depreciation.Sln(cost, salvage, life);
        }

        /// <summary>
        /// Returns the sum-of-years' digits depreciation of an asset for a specified period.
        /// </summary>
        /// <param name="cost">Cost is the initial cost of the asset.</param>
        /// <param name="salvage">Salvage is the value at the end of the depreciation (sometimes called the salvage value of the asset).</param>
        /// <param name="life">Life is the number of periods over which the asset is depreciated (sometimes called the useful life of the asset).</param>
        /// <param name="period">Per is the period and must use the same units as life.</param>

        /// <exception cref="System.ArgumentException"></exception>
        public static double SYD(double cost, double salvage, double life, double period)
        {
            return Depreciation.Syd(cost, salvage, life, period);
        }

        /// <summary>
        /// Returns the bond-equivalent yield for a Treasury bill.
        /// </summary>
        /// <param name="settlement"> The Treasury bill's settlement date. The security settlement date is the date after the issue date when the Treasury bill is traded to the buyer.</param>
        /// <param name="maturity">The Treasury bill's maturity date. The maturity date is the date when the Treasury bill expires.</param>
        /// <param name="discount">The Treasury bill's discount rate.</param>
        public static double TBILLEQ(DateTime settlement, DateTime maturity, double discount)
        {
            return BillEq(settlement, maturity, discount);
        }

        /// <summary>
        /// Returns the price per $100 face value for a Treasury bill.
        /// </summary>
        /// <param name="settlement"> The Treasury bill's settlement date. The security settlement date is the date after the issue date when the Treasury bill is traded to the buyer.</param>
        /// <param name="maturity">The Treasury bill's maturity date. The maturity date is the date when the Treasury bill expires.</param>
        /// <param name="discount">The Treasury bill's discount rate.</param>
        public static double TBILLPRICE(DateTime settlement, DateTime maturity, double discount)
        {
            return BillPrice(settlement, maturity, discount);
        }

        /// <summary>
        /// Returns the yield for a Treasury bill.
        /// </summary>
        /// <param name="settlement"> The Treasury bill's settlement date. The security settlement date is the date after the issue date when the Treasury bill is traded to the buyer.</param>
        /// <param name="maturity">The Treasury bill's maturity date. The maturity date is the date when the Treasury bill expires.</param>
        /// <param name="pr">The Treasury bill's price per $100 face value.</param>
        public static double TBILLYIELD(DateTime settlement, DateTime maturity, double pr)
        {
            return BillYield(settlement, maturity, pr);
        }

        /// <summary>
        /// Returns the depreciation of an asset for a specified or partial period by using a declining balance method.
        /// </summary>
        /// <param name="cost">The initial cost of the asset.</param>
        /// <param name="salvage">The value at the end of the depreciation (sometimes called the salvage value of the asset). This value can be 0.</param>
        /// <param name="life">The number of periods over which the asset is depreciated (sometimes called the useful life of the asset).</param>
        /// <param name="startPeriod">The starting period for which you want to calculate the depreciation. Start_period must use the same units as life.</param>
        /// <param name="endPeriod">The ending period for which you want to calculate the depreciation. End_period must use the same units as life.</param>
        /// <param name="factor">The rate at which the balance declines. If factor is omitted, it is assumed to be 2 (the double-declining balance method). Change factor if you do not want to use the double-declining balance method. For a description of the double-declining balance method, see <see cref="DDB"/>.
        /// </param>
        /// <param name="noSwitch">A logical value specifying whether to switch to straight-line depreciation when depreciation is greater than the declining balance calculation:
        /// <list type="bullet">
        /// <item>
        ///<term>0: </term> 
        /// <description>
        /// switches to straight-line depreciation when depreciation is greater than the declining balance calculation
        /// </description>
        /// </item>
        /// <item>
        ///<term>1: </term> 
        /// <description>
        /// does not switch to straight-line depreciation even when the depreciation is greater than the declining balance calculation.
        /// </description>
        /// </item>
        /// </list>
        /// </param>
        public static double VDB(double cost, double salvage, double life, double startPeriod, double endPeriod, double factor, int noSwitch)
        {
            return Depreciation.Vdb(cost, salvage, life, startPeriod, endPeriod, factor, (VdbSwitch)noSwitch);
        }

        /// <summary>
        /// Returns the depreciation of an asset for a specified or partial period by using a declining balance method.
        /// </summary>
        /// <param name="cost">The initial cost of the asset.</param>
        /// <param name="salvage">The value at the end of the depreciation (sometimes called the salvage value of the asset). This value can be 0.</param>
        /// <param name="life">The number of periods over which the asset is depreciated (sometimes called the useful life of the asset).</param>
        /// <param name="startPeriod">The starting period for which you want to calculate the depreciation. Start_period must use the same units as life.</param>
        /// <param name="endPeriod">The ending period for which you want to calculate the depreciation. End_period must use the same units as life.</param>
        /// <param name="factor">The rate at which the balance declines. If factor is omitted, it is assumed to be 2 (the double-declining balance method). Change factor if you do not want to use the double-declining balance method. For a description of the double-declining balance method, see <see cref="DDB"/>.
        /// </param>
        public static double VDB(double cost, double salvage, double life, double startPeriod, double endPeriod, double factor)
        {
            return Depreciation.Vdb(cost, salvage, life, startPeriod, endPeriod, factor, VdbSwitch.SwitchToStraightLine);
        }

        /// <summary>
        /// Returns the depreciation of an asset for a specified or partial period by using a declining balance method.
        /// </summary>
        /// <param name="cost">The cost.</param>
        /// <param name="salvage">The salvage.</param>
        /// <param name="life">The life.</param>
        /// <param name="startPeriod">The first period in the calculation. Payment periods are numbered beginning with 1.</param>
        /// <param name="endPeriod">The last period in the calculation.</param>
        public static double VDB(double cost, double salvage, double life, double startPeriod, double endPeriod)
        {
            return RaiseIfNonsense(Depreciation.Vdb(cost, salvage, life, startPeriod, endPeriod, 2, VdbSwitch.SwitchToStraightLine));
        }

        /// <summary>
        /// Returns the internal rate of return for a schedule of cash flows that is not necessarily periodic.
        /// </summary>
        /// <param name="values">A series of cash flows that corresponds to a schedule of payments in dates. 
        /// The first payment is optional and corresponds to a cost or payment that occurs at the beginning of the investment. If the first value is a cost or payment, it must be a negative value. 
        /// All succeeding payments are discounted based on a 365-day year. The series of values must contain at least one positive value and one negative value.</param>
        /// <param name="dates">A schedule of payment dates that corresponds to the cash flow payments. 
        /// The first payment date indicates the beginning of the schedule of payments. 
        /// All other dates must be later than this date, but they may occur in any order.</param>
        /// <param name="guess">A number that you guess is close to the result of XIRR.</param>
        public static double XIRR(IEnumerable<double> values, IEnumerable<DateTime> dates, double guess)
        {
            return Xirr(values, dates, guess);
        }

        /// <summary>
        /// Returns the internal rate of return for a schedule of cash flows that is not necessarily periodic.
        /// </summary>
        /// <param name="values">A series of cash flows that corresponds to a schedule of payments in dates. 
        /// The first payment is optional and corresponds to a cost or payment that occurs at the beginning of the investment. If the first value is a cost or payment, it must be a negative value. 
        /// All succeeding payments are discounted based on a 365-day year. The series of values must contain at least one positive value and one negative value.</param>
        /// <param name="dates">A schedule of payment dates that corresponds to the cash flow payments. 
        /// The first payment date indicates the beginning of the schedule of payments. 
        /// All other dates must be later than this date, but they may occur in any order.</param>
        public static double XIRR(IEnumerable<double> values, IEnumerable<DateTime> dates)
        {
            return Xirr(values, dates, 0.1);
        }

        /// <summary>
        /// Returns the net present value for a schedule of cash flows that is not necessarily periodic.
        /// </summary>
        /// <param name="rate">TThe discount rate to apply to the cash flows.</param>
        /// <param name="values">A series of cash flows that corresponds to a schedule of payments in dates. 
        /// The first payment is optional and corresponds to a cost or payment that occurs at the beginning of the investment. If the first value is a cost or payment, it must be a negative value. 
        /// All succeeding payments are discounted based on a 365-day year. The series of values must contain at least one positive value and one negative value.</param>
        /// <param name="dates">A schedule of payment dates that corresponds to the cash flow payments. 
        /// The first payment date indicates the beginning of the schedule of payments. 
        /// All other dates must be later than this date, but they may occur in any order.</param>
        public static double XNPV(double rate, IEnumerable<double> values, IEnumerable<DateTime> dates)
        {
            return Xnpv(rate, values, dates);
        }

        /// <summary>
        /// Returns the yield on a security that pays periodic interest.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="rate">The security's annual coupon rate.</param>
        /// <param name="pr">The security's price per $100 face value..</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="frequency">The number of coupon payments per year:
        /// <list type="bullet">
        /// <item>
        /// <term>1: </term>
        /// <description>annual payments</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>semi-annual payments</description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>quarterly payments.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double YIELD(DateTime settlement, DateTime maturity, double rate, double pr, double redemption, int frequency, int basis)
        {
            return Yield(settlement, maturity, rate, pr, redemption, (Frequency)frequency, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the annual yield for a discounted security; for example, a Treasury bill.
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="pr">The security's price per $100 face value..</param>
        /// <param name="redemption">The security's redemption value per $100 face value.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double YIELDDISC(DateTime settlement, DateTime maturity, double pr, double redemption, int basis)
        {
            return YieldDisc(settlement, maturity, pr, redemption, (DayCountBasis)basis);
        }

        /// <summary>
        /// Returns the annual yield of a security that pays interest at maturity.h
        /// </summary>
        /// <param name="settlement">The security's settlement date. The security settlement date is the date after the issue date when the security is traded to the buyer.</param>
        /// <param name="maturity">The security's maturity date. The maturity date is the date when the security expires.</param>
        /// <param name="issue">The security's issue date.</param>
        /// <param name="rate">TThe security's interest rate at date of issue.</param>
        /// <param name="pr">The security's price per $100 face value.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double YIELDMAT(DateTime settlement, DateTime maturity, DateTime issue, double rate, double pr, int basis)
        {
            return YieldMat(settlement, maturity, issue, rate, pr, (DayCountBasis)basis);
        }

        /// <summary>
        /// Calculates the fraction of the year represented by the number of whole days between two dates. Use the YEARFRAC worksheet function to identify the proportion of a whole year's benefits or obligations to assign to a specific term.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="basis">The type of day count basis to use:
        /// <list type="bullet">
        /// <item>
        /// <term>0: </term>
        /// <description>US (NASD) 30/360</description>
        /// </item>
        /// <item>
        /// <term>1: </term>
        /// <description>Actual/actual</description>
        /// </item>
        /// <item>
        /// <term>2: </term>
        /// <description>Actual/360.</description>
        /// </item>
        /// <item>
        /// <term>3: </term>
        /// <description> Actual/365 </description>
        /// </item>
        /// <item>
        /// <term>4: </term>
        /// <description>European 30/360.</description>
        /// </item>
        /// </list></param>
        public static double YEARFRAC(DateTime startDate, DateTime endDate, int basis = 0)
        {
            return DayCount.YearFrac(startDate, endDate, (DayCountBasis)basis);
        }
    }
}