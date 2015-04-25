using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
    public sealed partial class FinancialFunctions
    {
        private static double AccrInt(DateTime issue, DateTime firstInterestData, DateTime settlement, double rate, double par, double frequency, DayCountBasis basis, AccrIntCalcMethod calcMethod)
        {
            DateTime pdc;
            var dc = DayCount.DayCounterFactory(basis);

            var numMonths = DayCount.Freq2Months(frequency);
            var numMonthsNeg = -numMonths;
            var endMonthBond = Common.LastDayOfMonth(firstInterestData.Year, firstInterestData.Month, firstInterestData.Day);
            if (settlement > firstInterestData && calcMethod == AccrIntCalcMethod.FromIssueToSettlement) pdc = DayCount.FindPcdNcd(firstInterestData, settlement, numMonths, basis, endMonthBond).Item1;
            else pdc = dc.ChangeMonth(firstInterestData, numMonthsNeg, endMonthBond);
            var firstDate = issue > pdc ? issue : pdc;

            var days = dc.DaysBetween(firstDate, settlement, NumDenumPosition.Numerator);
            var dayCount2 = dc;
            var dateTime5 = pdc;
            var dateTime6 = firstInterestData;

            var coupDays = dayCount2.CoupDays(dateTime5, dateTime6, frequency);
            var aggFunction = FAccrInt(issue, basis, calcMethod, dc, frequency);
            var tuple1 = DayCount.DatesAggregate1(pdc, issue, numMonthsNeg, basis, aggFunction, days / coupDays, endMonthBond);
            var a = tuple1.Item3;
            return par * rate / frequency * a;
        }

        private static double AccrIntM(DateTime issue, DateTime settlement, double rate, double par, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var days = dc.DaysBetween(issue, settlement, NumDenumPosition.Numerator);
            var daysInYear = dc.DaysInYear(issue, settlement);
            return par * rate * days / daysInYear;
        }

        private static double Disc(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return DiscInternal(settlement, maturity, pr, redemption, basis);
        }

        private static double Duration(DateTime settlement, DateTime maturity, double coupon, double yld, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("coupon must be more than 0", coupon >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            return DurationInternal(settlement, maturity, coupon, yld, (double)frequency, basis, false);
        }

        private static double IntRate(DateTime settlement, DateTime maturity, double investment, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", investment > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return IntRateInternal(settlement, maturity, investment, redemption, basis);
        }

        private static double MDuration(DateTime settlement, DateTime maturity, double coupon, double yld, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("coupon must be more than 0", coupon >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            return DurationInternal(settlement, maturity, coupon, yld, (double)frequency, basis, true);
        }

        private static double Price(DateTime settlement, DateTime maturity, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("yld must be more than 0", yld > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return PriceInternal(settlement, maturity, rate, yld, redemption, (double)frequency, basis);
        }

        private static double PriceDisc(DateTime settlement, DateTime maturity, double discount, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", discount > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return PriceDiscInternal(settlement, maturity, discount, redemption, basis);
        }

        private static double Received(DateTime settlement, DateTime maturity, double investment, double discount, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dim = dc.DaysBetween(settlement, maturity, NumDenumPosition.Numerator);
            var b = dc.DaysInYear(settlement, maturity);
            var discountFactor = discount * dim / b;
            Common.Ensure("discount * dim / b must be different from 1", discountFactor < 1);
            Common.Ensure("maturity must be after settlement", maturity > settlement);
            Common.Ensure("investment must be more than 0", investment > 0);
            Common.Ensure("redemption must be more than 0", discount > 0);
            return ReceivedInternal(settlement, maturity, investment, discount, basis);
        }

        private static double Yield(DateTime settlement, DateTime maturity, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("pr must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);

            var frq = (double)frequency;
            var priceYieldFactors = GetPriceYieldFactors(settlement, maturity, frq, basis);

            var n = priceYieldFactors.Item1;
            var e = priceYieldFactors.Item4;
            var dsr = priceYieldFactors.Item5;
            var a = priceYieldFactors.Item3;
            if (n > 1) return Common.FindRoot(FYield(settlement, maturity, rate, pr, redemption, frq, basis), 0.05);
            const double Par = 100;
            var firstNum = redemption / 100 + rate / frq - (Par / 100 + a / e * rate / frq);
            var firstDen = Par / 100 + a / e * rate / frq;
            return firstNum / firstDen * frq * e / dsr;
        }

        private static double YieldDisc(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("investment must be more than 0", pr > 0);
            Common.Ensure("redemption must be more than 0", redemption > 0);
            return YieldDiscInternal(settlement, maturity, pr, redemption, basis);
        }

        private static double YieldMat(DateTime settlement, DateTime maturity, DateTime issue, double rate, double pr, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be after issue", (maturity > issue));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate > 0);
            Common.Ensure("price must be more than 0", pr > 0);
            //return yieldMat(settlement, maturity, issue, rate, pr, basis);
            var matFactors = GetMatFactors(settlement, maturity, issue, basis);
            var dsm = matFactors.Item4;
            var dim = matFactors.Item2;
            var b = matFactors.Item1;
            var a = matFactors.Item3;
            var term1 = dim / b * rate + 1 - pr / 100 - a / b * rate;
            var term2 = pr / 100 + a / b * rate;
            var term3 = b / dsm;
            return term1 / term2 * term3;
        }

        private static double DiscInternal(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (-pr / redemption + 1) * b / dim;
        }

        private static double DurationInternal(DateTime settlement, DateTime maturity, double coupon, double yld, double frequency, DayCountBasis basis, bool isMDuration)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dbc = dc.CoupDaysBS(settlement, maturity, frequency);
            var e = dc.CoupDays(settlement, maturity, frequency);
            var n = dc.CoupNum(settlement, maturity, frequency);
            var dsc = e - dbc;
            var x1 = dsc / e;
            var x2 = x1 + n - 1;
            var x3 = yld / frequency + 1;
            var x4 = Common.Pow(x3, x2);
            Common.Ensure("(yld / frequency + 1)^((dsc / e) + n -1) must be different from 0)", Math.Abs(x4) > Constants.Epsilon);
            var term1 = x2 * 100 / x4;
            var term3 = 100 / x4;
            var aggr = FDuration(coupon, frequency, x1, x3);
            var tuple = Common.AggrBetween(1, (int)n, aggr, new Tuple<double, double>(0, 0));
            var term4 = tuple.Item2;
            var term2 = tuple.Item1;
            var term5 = term1 + term2;
            var term6 = term3 + term4;
            Common.Ensure("term6 must be different from 0)", Math.Abs(term6) > Constants.Epsilon);
            return isMDuration ? term5 / term6 / frequency / x3 : term5 / term6 / frequency;
        }

        private static Tuple<double, double> GetCommonFactors(DateTime settlement, DateTime maturity, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;
            var dim = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
            var dayCount1 = dc;
            var dateTime2 = settlement;
            var dateTime3 = maturity;
            var b = dayCount1.DaysInYear(dateTime2, dateTime3);
            return new Tuple<double, double>(dim, b);
        }

        private static double IntRateInternal(DateTime settlement, DateTime maturity, double investment, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (redemption - investment) / investment * b / dim;
        }

        private static double PriceDiscInternal(DateTime settlement, DateTime maturity, double discount, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return redemption - discount * redemption * dim / b;
        }

        private static double ReceivedInternal(DateTime settlement, DateTime maturity, double investment, double discount, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            var discountFactor = discount * dim / b;
            return investment / (1 - discountFactor);
        }

        private static double YieldDiscInternal(DateTime settlement, DateTime maturity, double pr, double redemption, DayCountBasis basis)
        {
            var commonFactors = GetCommonFactors(settlement, maturity, basis);
            var dim = commonFactors.Item1;
            var b = commonFactors.Item2;
            return (redemption - pr) / pr * b / dim;
        }

        private static Tuple<double, double, double, double> GetMatFactors(DateTime settlement, DateTime maturity, DateTime issue, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var b = dc.DaysInYear(issue, settlement);
            var dim = dc.DaysBetween(issue, maturity, NumDenumPosition.Numerator);
            var a = dc.DaysBetween(issue, settlement, NumDenumPosition.Numerator);
            var dsm = dim - a;
            return new Tuple<double, double, double, double>(b, dim, a, dsm);
        }

        private static Tuple<double, DateTime, double, double, double> GetPriceYieldFactors(DateTime settlement, DateTime maturity, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var n = dc.CoupNum(settlement, maturity, frequency);
            var pcd = dc.CoupPCD(settlement, maturity, frequency);
            var a = dc.DaysBetween(pcd, settlement, NumDenumPosition.Numerator);
            var e = dc.CoupDays(settlement, maturity, frequency);
            var dsc = e - a;
            return new Tuple<double, DateTime, double, double, double>(n, pcd, a, e, dsc);
        }

        private static double PriceInternal(DateTime settlement, DateTime maturity, double rate, double yld, double redemption, double frequency, DayCountBasis basis)
        {
            var priceYieldFactors = GetPriceYieldFactors(settlement, maturity, frequency, basis);
            var n = priceYieldFactors.Item1;
            var e = priceYieldFactors.Item4;
            var dsc = priceYieldFactors.Item5;
            var a = priceYieldFactors.Item3;
            var coupon = 100 * rate / frequency;
            var accrInt = 100 * rate / frequency * a / e;
            var pvFactor = FPrice(yld, frequency, e, dsc);
            var pvOfRedemption = redemption / pvFactor(n);
            double pvOfCoupons = 0;
            var k = 1;
            var num = (int)n;
            if (num >= k)
            {
                do
                {
                    pvOfCoupons = pvOfCoupons + coupon / pvFactor(k);
                    k++;
                }
                while (k != num + 1);
            }
            return n != 1 ? pvOfRedemption + pvOfCoupons - accrInt : (redemption + coupon) / (1 + dsc / e * yld / frequency) - accrInt;
        }

        private static Func<double, double> FYield(DateTime settlement, DateTime maturity, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            return yld => PriceInternal(settlement, maturity, rate, yld, redemption, frequency, basis) - pr;
        }

        private static Func<DateTime, DateTime, double> FAccrInt(DateTime issue, DayCountBasis basis, AccrIntCalcMethod calcMethod, DayCount.IDayCounter dc, double freq)
        {
            return (pcd, ncd) =>
                {
                    double days;
                    var firstDate = issue > pcd ? issue : pcd;
                    if (basis == DayCountBasis.UsPsa30_360)
                    {
                        var psaMethod = issue > pcd ? DayCount.Method360Us.ModifyStartDate : DayCount.Method360Us.ModifyBothDates;
                        days = (double)DayCount.DateDiff360US(firstDate, ncd, psaMethod);
                    }
                    else days = dc.DaysBetween(firstDate, ncd, NumDenumPosition.Numerator);

                    double coupDays;
                    if (basis == DayCountBasis.UsPsa30_360) coupDays = (double)DayCount.DateDiff360US(pcd, ncd, DayCount.Method360Us.ModifyBothDates);
                    else
                    {
                        if (basis == DayCountBasis.Actual365) coupDays = 365 / freq;
                        else coupDays = dc.DaysBetween(pcd, ncd, NumDenumPosition.Denumerator);
                    }
                    return issue < pcd ? (double)calcMethod : days / coupDays;
                };
        }

        private static Func<Tuple<double, double>, int, Tuple<double, double>> FDuration(double coupon, double frequency, double x1, double x3)
        {
            return (acc, index) =>
                {
                    var x5 = (double)index - 1 + x1;
                    var x6 = Common.Pow(x3, x5);
                    Common.Ensure("x6 must be different from 0)", Math.Abs(x6) > Constants.Epsilon);
                    var x7 = 100 * coupon / frequency / x6;
                    var tuple = acc;
                    var b = tuple.Item2;
                    var a = tuple.Item1;
                    return new Tuple<double, double>(a + x7 * x5, b + x7);
                };
        }

        private static Func<double, double> FPrice(double yld, double frequency, double e, double dsc)
        {
            return k => Common.Pow(1 + yld / frequency, k - 1 + dsc / e);
        }

        private static double DollarDe(double fractionalDollar, double fraction)
        {
            Common.Ensure("fraction must be more than 0", fraction > 0);
            return Dollar(fractionalDollar, fraction, DollarDeInternal);
        }

        private static double CoupNumber(DateTime d1, DateTime d2, int numMonths, DayCountBasis basis, bool isWholeNumber)
        {
            var mat = d1;
            var tuple = Common.ToTuple(mat);
            var my = tuple.Item1;
            var mm = tuple.Item2;
            var md = tuple.Item3;
            var settl = d2;
            var tuple1 = Common.ToTuple(settl);
            var sy = tuple1.Item1;
            var sm = tuple1.Item2;
            var sd = tuple1.Item3;
            double num = (!isWholeNumber ? 1 : 0);
            var couponsTemp = num;
            var endOfMonthTemp = Common.LastDayOfMonth(my, mm, md);
            var flag = (!endOfMonthTemp && mm != 2);
            var flag1 = (flag && md > 28);
            var flag2 = (flag1 && md < Common.DaysOfMonth(my, mm));
            var flag3 = (!flag2 ? endOfMonthTemp : Common.LastDayOfMonth(sy, sm, sd));
            var endOfMonth = flag3;
            var startDate = DayCount.ChangeMonth(settl, 0, basis, endOfMonth);
            var num1 = (!(settl < startDate) ? couponsTemp : couponsTemp + 1);
            var coupons = num1;
            var date = DayCount.ChangeMonth(startDate, numMonths, basis, endOfMonth);
            var tuple2 = DayCount.DatesAggregate1(date, mat, numMonths, basis, (time, dateTime) => 1, coupons, endOfMonth);
            var result = tuple2.Item3;
            return result;
        }

        private static double DaysBetweenNotNeg(DayCount.IDayCounter dc, DateTime startDate, DateTime endDate)
        {
            var dayCount = dc;
            var dateTime = startDate;
            var dateTime1 = endDate;

            var result = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
            return result <= 0 ? 0 : result;
        }

        private static double DaysBetweenNotNegPsaHack(DateTime startDate, DateTime endDate)
        {
            var result = (double)DayCount.DateDiff360US(startDate, endDate, DayCount.Method360Us.ModifyBothDates);
            return result <= 0 ? 0 : result;
        }

        private static double DaysBetweenNotNegWithHack(DayCount.IDayCounter dc, DateTime startDate, DateTime endDate, DayCountBasis basis)
        {
            return basis != DayCountBasis.UsPsa30_360 ? DaysBetweenNotNeg(dc, startDate, endDate) : DaysBetweenNotNegPsaHack(startDate, endDate);
        }

        private static Func<Tuple<double, double>, int, Tuple<double, double>> FOddFPrice(DateTime settlement, DateTime issue, DayCountBasis basis, DayCount.IDayCounter dc, int numMonthsNeg, double e, DateTime lateCoupon)
        {
            return (t, index) =>
                {
                    var acc = t;
                    var earlyCoupon = DayCount.ChangeMonth(lateCoupon, numMonthsNeg, basis, false);
                    var num = (basis != DayCountBasis.ActualActual ? e : DaysBetweenNotNeg(dc, earlyCoupon, lateCoupon));
                    var dci = (index <= 1 ? DaysBetweenNotNeg(dc, issue, lateCoupon) : num);
                    var startDate = (!(issue > earlyCoupon) ? earlyCoupon : issue);
                    var endDate = (!(settlement < lateCoupon) ? lateCoupon : settlement);
                    var a = DaysBetweenNotNeg(dc, startDate, endDate);
                    lateCoupon = earlyCoupon;
                    var tuple = acc;
                    var dcnl = tuple.Item1;
                    var anl = tuple.Item2;
                    return Tuple.Create(dcnl + dci / num, anl + a / num);
                };
        }

        private static Func<double, int, double> FOddFPriceNegative(double rate, double m, double y, double p1)
        {
            return (acc, index) => acc + 100 * rate / m / Common.Pow(p1, (double)index - 1 + y);
        }

        private static Func<double, int, double> FOddFPricePositive(double rate, double m, double nq, double y, double p1)
        {
            return (acc, index) => acc + 100 * rate / m / Common.Pow(p1, index + nq + y);
        }

        private static Func<double, double> FOddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            return yld => pr - OddFPrice(settlement, maturity, issue, firstCoupon, rate, yld, redemption, frequency, basis);
        }

        private static Func<Tuple<double, double, double>, int, Tuple<double, double, double>> FOddLFunc(DateTime settlement, DateTime maturity, DayCountBasis basis, DayCount.IDayCounter dc, int numMonths, double nc, DateTime earlyCoupon)
        {
            return (t, index) =>
                {
                    double a;
                    var acc = t;
                    var lateCoupon = DayCount.ChangeMonth((earlyCoupon), numMonths, basis, false);
                    var nl = DaysBetweenNotNegWithHack(dc, (earlyCoupon), lateCoupon, basis);
                    var num = (index >= (int)nc ? DaysBetweenNotNegWithHack(dc, (earlyCoupon), maturity, basis) : nl);
                    var dci = num;
                    if (!(lateCoupon < settlement)) a = (!(earlyCoupon < settlement) ? 0 : DaysBetweenNotNeg(dc, earlyCoupon, settlement));
                    else a = dci;

                    var dateTime = (!(settlement > earlyCoupon) ? earlyCoupon : settlement);
                    var startDate = dateTime;
                    var dateTime1 = !(maturity < lateCoupon) ? lateCoupon : maturity;
                    var endDate = dateTime1;
                    var dsc = DaysBetweenNotNeg(dc, startDate, endDate);
                    earlyCoupon = lateCoupon;
                    var tuple = acc;
                    var dscnl = tuple.Item3;
                    var dcnl = tuple.Item1;
                    var anl = tuple.Item2;
                    return new Tuple<double, double, double>(dcnl + dci / nl, anl + a / nl, dscnl + dsc / nl);
                };
        }

        private static double OddFPrice(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double yld, double redemption, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);

            var numMonths = DayCount.Freq2Months(frequency);
            var numMonthsNeg = -numMonths;
            var num1 = frequency;
            var e = dc.CoupDays(settlement, firstCoupon, num1);

            var n = dc.CoupNum(settlement, maturity, frequency);
            var m = frequency;
            var dfc = DaysBetweenNotNeg(dc, issue, firstCoupon);
            if (dfc >= e)
            {
                var dayCount2 = dc;
                var nc = dayCount2.CoupNum(issue, firstCoupon, frequency);
                var lateCoupon = firstCoupon;
                var aggrFunction = FOddFPrice(settlement, issue, basis, dc, numMonthsNeg, e, lateCoupon);
                var tuple1 = Common.AggrBetween((int)nc, 1, aggrFunction.Invoke, new Tuple<double, double>(0, 0));
                var dcnl = tuple1.Item1;
                var anl = tuple1.Item2;

                double num;
                if (basis == DayCountBasis.Actual360 || basis == DayCountBasis.Actual365)
                {
                    var date = dc.CoupNCD(settlement, firstCoupon, frequency);
                    num = DaysBetweenNotNeg(dc, settlement, date);
                }
                else
                {
                    var date = dc.CoupPCD(settlement, firstCoupon, frequency);
                    var a = dc.DaysBetween(date, settlement, NumDenumPosition.Numerator);
                    num = e - a;
                }
                var nq = CoupNumber(firstCoupon, settlement, numMonths, basis, true);
                n = dc.CoupNum(firstCoupon, maturity, frequency);
                var x = yld / m + 1;
                var y = num / e;
                var term1 = redemption / Common.Pow(x, y + nq + n);
                var term2 = 100 * rate / m * dcnl / Common.Pow(x, nq + y);
                var term3 = Common.AggrBetween<double>(1, (int)n, FOddFPricePositive(rate, m, nq, y, x).Invoke, 0);
                var term4 = 100 * rate / m * anl;
                return term1 + term2 + term3 - term4;
            }
            else
            {
                var dsc = DaysBetweenNotNeg(dc, settlement, firstCoupon);
                var a = DaysBetweenNotNeg(dc, issue, settlement);
                var x = yld / m + 1;
                var y = dsc / e;
                var p1 = x;
                var term1 = redemption / Common.Pow(p1, n - 1 + y);
                var term2 = 100 * rate / m * dfc / e / Common.Pow(p1, y);
                var term3 = Common.AggrBetween<double>(2, (int)n, FOddFPriceNegative(rate, m, y, p1).Invoke, 0);
                var p2 = rate / m;
                var term4 = a / e * p2 * 100;
                return term1 + term2 + term3 - term4;
            }
        }

        private static double OddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, double frequency, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;

            var years = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);

            var px = pr - 100;
            var num = rate * years * 100 - px;
            var denum = px / 4 + years * px / 2 + years * 100;
            var guess = num / denum;
            return Common.FindRoot(FOddFYield(settlement, maturity, issue, firstCoupon, rate, pr, redemption, frequency, basis).Invoke, guess);
        }

        private static double OddLFunc(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double prOrYld, double redemption, double frequency, DayCountBasis basis, bool isLPrice)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var m = frequency;
            var numMonths = (int)(12 / frequency);
            var lastCoupon = lastInterest;
            var dayCount = dc;
            var dateTime = lastCoupon;
            var dateTime1 = maturity;
            var num = frequency;
            var nc = dayCount.CoupNum(dateTime, dateTime1, num);
            var earlyCoupon = lastCoupon;
            var aggrFunction = FOddLFunc(settlement, maturity, basis, dc, numMonths, nc, earlyCoupon);
            var tuple = Common.AggrBetween(1, (int)nc, aggrFunction.Invoke, new Tuple<double, double, double>(0, 0, 0));
            var dscnl = tuple.Item3;
            var dcnl = tuple.Item1;
            var anl = tuple.Item2;
            var x = 100 * rate / m;
            var term1 = dcnl * x + redemption;
            if (!isLPrice)
            {
                var term2 = anl * x + prOrYld;
                var term3 = m / dscnl;
                return (term1 - term2) / term2 * term3;
            }
            else
            {
                var term2 = dscnl * prOrYld / m + 1;
                var term3 = anl * x;
                return term1 / term2 - term3;
            }
        }

        private static double OddFPrice(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            var tuple = Common.ToTuple(maturity);
            var my = tuple.Item1;
            var mm = tuple.Item2;
            var md = tuple.Item3;
            var endMonth = Common.LastDayOfMonth(my, mm, md);
            var numMonths = (int)(12 / (double)frequency);
            var numMonthsNeg = -numMonths;
            var tuple1 = DayCount.FindPcdNcd(DayCount.ChangeMonth(maturity, numMonthsNeg, basis, endMonth), firstCoupon, numMonthsNeg, basis, endMonth);
            var pcd = tuple1.Item1;

            Common.Ensure("maturity and firstCoupon must have the same month and day (except for February when leap years are considered)", pcd == firstCoupon);
            Common.Ensure("maturity must be after firstCoupon", (maturity > firstCoupon));
            Common.Ensure("firstCoupon must be after settlement", (firstCoupon > settlement));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddFPrice(settlement, maturity, issue, firstCoupon, rate, yld, redemption, (double)frequency, basis);
        }

        private static double OddFYield(DateTime settlement, DateTime maturity, DateTime issue, DateTime firstCoupon, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after firstCoupon", (maturity > firstCoupon));
            Common.Ensure("firstCoupon must be after settlement", (firstCoupon > settlement));
            Common.Ensure("settlement must be after issue", (settlement > issue));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("pr must be more than 0", pr >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddFYield(settlement, maturity, issue, firstCoupon, rate, pr, redemption, (double)frequency, basis);
        }

        private static double OddLPrice(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double yld, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("settlement must be after lastInterest", (settlement > lastInterest));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("yld must be more than 0", yld >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddLFunc(settlement, maturity, lastInterest, rate, yld, redemption, (double)frequency, basis, true);
        }

        private static double OddLYield(DateTime settlement, DateTime maturity, DateTime lastInterest, double rate, double pr, double redemption, Frequency frequency, DayCountBasis basis)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("settlement must be after lastInterest", (settlement > lastInterest));
            Common.Ensure("rate must be more than 0", rate >= 0);
            Common.Ensure("pr must be more than 0", pr >= 0);
            Common.Ensure("redemption must be more than 0", redemption >= 0);
            return OddLFunc(settlement, maturity, lastInterest, rate, pr, redemption, (double)frequency, basis, false);
        }

        private static double CumIpmtInternal(double r, double nper, double pv, double startPeriod, double endPeriod, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, startPeriod - 1));
            Common.Ensure("pv must be more than 0", pv > 0);
            Common.Ensure("r must be more than 0", r > 0);
            Common.Ensure("nper must be more than 0", nper > 0);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", Math.Abs(AnnuityCertainPvFactor(r, nper, pd)) > Constants.Epsilon);
            Common.Ensure("startPeriod must be less or equal to endPeriod", startPeriod <= endPeriod);
            Common.Ensure("startPeriod must be more or equal to 1", startPeriod >= 1);
            Common.Ensure("startPeriod and endPeriod must be less or equal to nper", endPeriod <= nper);
            return Common.AggrBetween<double>((int)Common.Ceiling(startPeriod), (int)endPeriod, FCumIpmt(r, nper, pv, pd).Invoke, 0);
        }

        private static double CumPrinc(double r, double nper, double pv, double startPeriod, double endPeriod, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, startPeriod - 1));
            Common.Ensure("pv must be more than 0", pv > 0);
            Common.Ensure("r must be more than 0", r > 0);
            Common.Ensure("nper must be more than 0", nper > 0);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", Math.Abs(AnnuityCertainPvFactor(r, nper, pd)) > Constants.Epsilon);
            Common.Ensure("startPeriod must be less or equal to endPeriod", startPeriod <= endPeriod);
            Common.Ensure("startPeriod must be more or equal to 1", startPeriod >= 1);
            Common.Ensure("startPeriod and endPeriod must be less or equal to nper", endPeriod <= nper);
            return Common.AggrBetween<double>((int)Common.Ceiling(startPeriod), (int)endPeriod, FCumPrinc(r, nper, pv, pd).Invoke, 0);
        }

        private static double Ipmt(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, per - 1));
            Common.Ensure("fv or pv need to be different from 0", Math.Abs(fv) < Constants.Epsilon ? pv != 0 : true);
            Common.Ensure("r cannot be -100% when nper is <= 0", r != -1 || (!(!(r != -1 ? false : per > 1) ? false : nper > 1) ? false : pd == PaymentDue.EndOfPeriod));
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            Common.Ensure("per must be in the range 1 to nper", per < 1 ? false : per <= nper);
            Common.Ensure("nper must be more than 0", nper > 0);
            return !((int)per != 1 ? false : pd == PaymentDue.BeginningOfPeriod) ? (r != -1 ? IpmtInternal(r, per, nper, pv, fv, pd) : -fv) : 0;
        }

        private static double Ispmt(double r, double per, double nper, double pv)
        {
            // constraints dropped here in favor of RadSpreadsheet, meaning that some semantic imaginary situations can be calculated

            //Common.Ensure("per must be in the range 1 to nper", !(per < 1) && per <= nper);
            //Common.Ensure("nper must be more than 0", nper > 0);
            return IspmtInternal(r, per, nper, pv);
        }

        private static double Ppmt(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer)", Common.Raisable(r, nper));
            Common.Ensure("r is not raisable to (per - 1) (r is negative and nper not an integer)", Common.Raisable(r, per - 1));
            Common.Ensure("fv or pv need to be different from 0", fv == 0 ? pv != 0 : true);
            Common.Ensure("r cannot be -100% when nper is <= 0", r != -1 || (!(!(r != -1 ? false : per > 1) ? false : nper > 1) ? false : pd == PaymentDue.EndOfPeriod));
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            Common.Ensure("per must be in the range 1 to nper", !(per < 1) && per <= nper);
            Common.Ensure("nper must be more than 0", nper > 0);
            return !((int)per == 1 && pd == PaymentDue.BeginningOfPeriod) ? (r != -1 ? PpmtInternal(r, per, nper, pv, fv, pd) : 0) : PmtInternal(r, nper, pv, fv, pd);
        }

        private static double IpmtInternal(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            var result = -(pv * FvFactor(r, per - 1) * r + PmtInternal(r, nper, pv, fv, PaymentDue.EndOfPeriod) * (FvFactor(r, per - 1) - 1));
            return pd != PaymentDue.EndOfPeriod ? result / (1 + r) : result;
        }

        private static double IspmtInternal(double r, double per, double nper, double pv)
        {
            var coupon = -pv * r;
            return coupon - coupon / nper * per;
        }

        private static double PpmtInternal(double r, double per, double nper, double pv, double fv, PaymentDue pd)
        {
            return PmtInternal(r, nper, pv, fv, pd) - IpmtInternal(r, per, nper, pv, fv, pd);
        }

        private static Func<double, int, double> FCumIpmt(double r, double nper, double pv, PaymentDue pd)
        {
            return (acc, per) => acc + Ipmt(r, per, nper, pv, 0, pd);
        }

        private static Func<double, int, double> FCumPrinc(double r, double nper, double pv, PaymentDue pd)
        {
            return (acc, per) => acc + Ppmt(r, per, nper, pv, 0, pd);
        }

        private static double AnnuityCertainFvFactor(double r, double nper, PaymentDue pd)
        {
            return AnnuityCertainPvFactor(r, nper, pd) * FvFactor(r, nper);
        }

        private static double AnnuityCertainPvFactor(double r, double nper, PaymentDue pd)
        {
            return r != 0 ? (1 + r * (double)pd) * (1 - PvFactor(r, nper)) / r : nper;
        }

        private static double Fv(double r, double nper, double pmt, double pv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer", Common.Raisable(r, nper));
            var flag = r != -1 || (r != -1 ? false : nper > 0);
            Common.Ensure("r cannot be -100% when nper is <= 0", flag);
            //Common.Ensure("pmt or pv need to be different from 0", pmt == 0 ? pv != 0 : true);
            if (pmt == 0 && pv == 0) return 0;
            if (r != -1 ? false : pd == PaymentDue.BeginningOfPeriod) return -(pv * FvFactor(r, nper));
            return !(r != -1 ? false : pd == PaymentDue.EndOfPeriod) ? FvInternal(r, nper, pmt, pv, pd) : -(pv * FvFactor(r, nper) + pmt);
        }

        private static double FvSchedule(double pv, IEnumerable<double> interests)
        {
            var result = pv;
            var nums = interests;
            var enumerator = nums.GetEnumerator();

            using (enumerator)
            {
                while (enumerator.MoveNext()) result = result * (enumerator.Current + 1);
            }
            return result;
        }

        private static double Nper(double r, double pmt, double pv, double fv, PaymentDue pd)
        {
            return (r == 0 ? pmt != 0 : false) ? -(fv + pv) / pmt : NperInternal(r, pmt, pv, fv, pd);
        }

        private static double Pmt(double r, double nper, double pv, double fv, PaymentDue pd)
        {
            bool flag2;
            Common.Ensure("r is not raisable to nper (r is negative and nper not an integer", Common.Raisable(r, nper));
            Common.Ensure("fv or pv need to be different from 0", fv == 0 ? pv != 0 : true);
            if (r == -1)
            {
                flag2 = (!(r != -1 ? false : nper > 0) ? false : pd == PaymentDue.EndOfPeriod);
            }
            else flag2 = true;
            Common.Ensure("r cannot be -100% when nper is <= 0", flag2);
            Common.Ensure("1 * pd + 1 - (1 / (1 + r)^nper) / nper has to be <> 0", AnnuityCertainPvFactor(r, nper, pd) != 0);
            return r != -1 ? PmtInternal(r, nper, pv, fv, pd) : -fv;
        }

        private static double Pv(double r, double nper, double pmt, double fv, PaymentDue pd)
        {
            Common.Ensure("r is not raisable to nper (r is less than -1 and nper not an integer", Common.Raisable(r, nper));

            //Common.Ensure("pmt or fv need to be different from 0", pmt == 0 ? fv != 0 : true);
            Common.Ensure("r cannot be -100%", r != -1);
            // due to data type constraints in Excel it seems the rate cannot go beyond 19.45 see here http://stackoverflow.com/questions/17745852/excels-present-value-pv-function
            if (Math.Abs(r) > 19.45) throw new Exception();
            return PvInternal(r, nper, pmt, fv, pd);
        }

        private static double Rate(double nper, double pmt, double pv, double fv, PaymentDue pd, double guess)
        {
            Common.Ensure("pmt or pv need to be different from 0", pmt == 0 ? pv != 0 : true);
            Common.Ensure("nper needs to be more than 0", nper > 0);
            Common.Ensure("There must be at least a change in sign in pv, fv and pmt", CheckRate(pmt, pv, fv));
            if (fv != 0 ? false : pv == 0) return pmt >= 0 ? 1 : -1;
            var f = FRate(nper, pmt, pv, fv, pd);
            return Common.FindRoot(f.Invoke, guess);
        }

        private static double FvInternal(double r, double nper, double pmt, double pv, PaymentDue pd)
        {
            return -(pv * FvFactor(r, nper) + pmt * AnnuityCertainFvFactor(r, nper, pd));
        }

        private static double FvFactor(double r, double nper)
        {
            return Math.Pow(1 + r, nper);
        }

        private static double NperInternal(double r, double pmt, double pv, double fv, PaymentDue pd)
        {
            var x = NperFactor(r, pmt, -fv, pd) / NperFactor(r, pmt, pv, pd);
            //if (x <= 0 || (r + 1) <= 0) throw new Exception();
            return Common.Ln(x) / Common.Ln(r + 1);
        }

        private static double NperFactor(double r, double pmt, double v, PaymentDue pd)
        {
            return v * r + pmt * (1 + r * (double)pd);
        }

        private static double PmtInternal(double r, double nper, double pv, double fv, PaymentDue pd)
        {
            return -(pv + fv * PvFactor(r, nper)) / AnnuityCertainPvFactor(r, nper, pd);
        }

        private static double PvInternal(double r, double nper, double pmt, double fv, PaymentDue pd)
        {
            return -(fv * PvFactor(r, nper) + pmt * AnnuityCertainPvFactor(r, nper, pd));
        }

        private static double PvFactor(double r, double nper)
        {
            return Math.Pow(1 + r, -nper);
        }

        private static double RaiseIfNonsense(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new Exception("Not a number.");
            return value;
        }

        private static Func<double, double> FRate(double nper, double pmt, double pv, double fv, PaymentDue pd)
        {
            return r => Fv(r, nper, pmt, pv, pd) - fv;
        }

        private static bool CheckRate(double x, double y, double z)
        {
            var flag1 = (Common.Sign(x) != Common.Sign(y) || Common.Sign(y) != Common.Sign(z)) && (Common.Sign(x) != Common.Sign(y) ? true : z == 0);
            var flag2 = flag1 && (Common.Sign(x) != Common.Sign(z) ? true : y == 0);
            return flag2 && (Common.Sign(y) != Common.Sign(z) ? true : x == 0);
        }

        private static double DollarFr(double fractionalDollar, double fraction)
        {
            Common.Ensure("fraction must be more than 0", fraction > 0);
            Common.Ensure("10^(ceiling (log10 (floor fraction))) must be different from 0", Common.Pow(10, Common.Ceiling(Common.Log10(Common.Floor(fraction)))) != 0);
            return Dollar(fractionalDollar, fraction, DollarFrInternal);
        }

        private static double Effect(double nominalRate, double npery)
        {
            Common.Ensure("nominal rate must be more than zero", nominalRate > 0);
            Common.Ensure("npery must be more or equal to one", npery >= 1);
            return EffectInternal(nominalRate, npery);
        }

        private static double Nominal(double effectRate, double npery)
        {
            Common.Ensure("effective rate must be more than zero", effectRate > 0);
            Common.Ensure("npery must be more or equal to one", npery >= 1);
            var periods = Common.Floor(npery);
            return (Common.Pow(effectRate + 1, 1 / periods) - 1) * periods;

        }

        private static T Dollar<T>(double fractionalDollar, double fraction, Func<double, double, double, double, T> f)
        {
            var aBase = Common.Floor(fraction);
            var num = (fractionalDollar <= 0 ? Common.Ceiling(fractionalDollar) : Common.Floor(fractionalDollar));
            var dollar = num;
            var remainder = fractionalDollar - dollar;
            var digits = Common.Pow(10, Common.Ceiling(Common.Log10(aBase)));
            return f.Invoke(aBase, dollar, remainder, digits);
        }

        private static double DollarDeInternal(double aBase, double dollar, double remainder, double digits)
        {
            return remainder * digits / aBase + dollar;
        }

        private static double DollarFrInternal(double aBase, double dollar, double remainder, double digits)
        {
            var absDigits = Math.Abs(digits);
            return remainder * aBase / absDigits + dollar;
        }

        private static double EffectInternal(double nominalRate, double npery)
        {
            var periods = Common.Floor(npery);
            return Common.Pow(nominalRate / periods + 1, periods) - 1;
        }

        private static double BillPriceInternal(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            return 100 * (1 - discount * dsm / 360);
        }

        private static double BillYieldInternal(DateTime settlement, DateTime maturity, double pr)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            return (100 - pr) / pr * 360 / dsm;
        }

        private static double BillEq(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.Actual360);
            var price = (100 - discount * 100 * dsm / 360) / 100;
            var days = dsm != 366 ? 365 : 366;
            var tempTerm2 = Common.Pow(dsm / days, 2) - (2 * dsm / days - 1) * (1 - 1 / price);
            //Common.Ensure("(dsm / days)^2 - (2. * dsm / days - 1.) * (1. - 1. / (100. - discount * 100. * dsm / 360.) / 100.) must be positive", tempTerm2 >= 0);
            Common.Ensure("2. * dsm / days - 1. must be different from 0", 2 * dsm / days - 1 != 0);
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("investment must be more than 0", discount > 0);

            if (dsm <= 182) return 365 * discount / (360 - discount * dsm);
            return 2 * (Common.Sqr(tempTerm2) - dsm / days) / (2 * dsm / days - 1);
        }

        private static double BillPrice(DateTime settlement, DateTime maturity, double discount)
        {
            var dsm = GetDsm(settlement, maturity, DayCountBasis.ActualActual);
            Common.Ensure("a result less than zero triggers an exception", 100 * (1 - discount * dsm / 360) > 0);
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("discount must be more than 0", discount > 0);
            return BillPriceInternal(settlement, maturity, discount);
        }

        private static double BillYield(DateTime settlement, DateTime maturity, double pr)
        {
            Common.Ensure("maturity must be after settlement", (maturity > settlement));
            Common.Ensure("maturity must be less than one year after settlement", (maturity <= Common.AddYears(settlement, 1)));
            Common.Ensure("pr must be more than 0", pr > 0);
            return BillYieldInternal(settlement, maturity, pr);
        }

        private static double GetDsm(DateTime settlement, DateTime maturity, DayCountBasis basis)
        {
            var dc = DayCount.DayCounterFactory(basis);
            var dayCount = dc;
            var dateTime = settlement;
            var dateTime1 = maturity;
            return dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
        }

        private static double Irr(IEnumerable<double> cfs, double guess)
        {
            Common.Ensure("There must be one positive and one negative cash flow", ValidCfs(cfs));
            return Common.FindRoot(d => NpvInternal(d, cfs), guess);
        }

        private static double Mirr(IEnumerable<double> cfs, double financeRate, double reinvestRate)
        {
            Common.Ensure("financeRate cannot be -100%", financeRate != -1);
            Common.Ensure("reinvestRate cannot be -100%", reinvestRate != -1);
            Common.Ensure("cfs must contain more than one cashflow", cfs.Count() != 1);
            Common.Ensure("The NPV calculated using financeRate and the negative cashflows in cfs must be different from zero", NpvInternal(financeRate, cfs.Map(cf => cf >= 0 ? 0 : cf)) != 0);
            return MirrInternal(cfs, financeRate, reinvestRate);
        }

        private static double Npv(double r, IEnumerable<double> cfs)
        {
            Common.Ensure("r cannot be -100%", r != -1);
            return NpvInternal(r, cfs);
        }

        private static double Xirr(IEnumerable<double> cfs, IEnumerable<DateTime> dates, double guess)
        {
            Common.Ensure("There must be one positive and one negative cash flow", ValidCfs(cfs));
            Common.Ensure("In dates, one date is less than the first date", dates.ToList().Any(time => (time <= dates.First())));
            Common.Ensure("cfs and dates must have the same length", cfs.Count() == dates.Count());
            return Common.FindRoot(r => XnpvInternal(r, cfs, dates), guess);
        }

        private static double Xnpv(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
        {
            Common.Ensure("r cannot be -100%", r != -1);
            Common.Ensure("In dates, one date is less than the first date", dates.ToList().Any(time => (time <= dates.First())));
            Common.Ensure("cfs and dates must have the same length", cfs.Count() == dates.Count());
            return XnpvInternal(r, cfs, dates);
        }

        private static double MirrInternal(IEnumerable<double> cfs, double financeRate, double reinvestRate)
        {
            var n = (double)cfs.Count();
            var cfsl = cfs.ToList();
            var positives = cfsl.Map(input => Math.Max(input, 0));
            var negatives = cfsl.Map(input => Math.Min(input, 0));
            return Math.Pow(-NpvInternal(reinvestRate, positives) * Math.Pow(1 + reinvestRate, n) / (NpvInternal(financeRate, negatives) * (1 + financeRate)), 1 / (n - 1)) - 1;
        }

        private static double NpvInternal(double r, IEnumerable<double> cfs)
        {
            double nums = 0;
            var cfsl = cfs.ToList();
            for (var i = 0; i < cfsl.Count(); i++) nums += cfsl[i] * PvFactor(r, (double)(i + 1));
            return nums;
        }

        private static bool ValidCfs(IEnumerable<double> cfs)
        {
            return ValidCfs(cfs, false, false);
        }

        private static bool ValidCfs(IEnumerable<double> cfs, bool pos, bool neg)
        {
            if (pos && neg) return true;
            if (cfs == null || !cfs.Any()) return false;
            var l = new FunctionalList<double>(cfs);
            var h = l.Head;
            var t = l.Tail;
            return h > 0 ? ValidCfs(t, true, neg) : ValidCfs(t, pos, true);
        }

        private static double XnpvInternal(double r, IEnumerable<double> cfs, IEnumerable<DateTime> dates)
        {
            var dateTimes = dates as IList<DateTime> ?? dates.ToList();
            var d0 = dateTimes.First();
            var mapped = dateTimes.Zip(cfs, (d, cf) => cf / Math.Pow(1 + r, (double)Common.Days(d, d0) / 365));
            return mapped.Sum();
        }

        static class Depreciation
        {
            private static double DeprBetweenPeriods(double cost, double salvage, double life, double startPeriod, double endPeriod, double factor, bool straightLine)
            {
                return TotalDepr(cost, salvage, life, endPeriod, factor, straightLine) - TotalDepr(cost, salvage, life, startPeriod, factor, straightLine);
            }

            private static double DeprCoeff(double assetLife)
            {
                Func<double, double, bool> between = (x1, x2) => x1 <= assetLife && assetLife <= x2;
                if (between.Invoke(3, 4)) return 1.5;
                if (between.Invoke(5, 6)) return 2;
                return assetLife > 6d ? 2.5 : 1;
            }

            private static Func<double, double, double> FDb(double cost, double life, double period, double month, double rate)
            {
                return (totDepr, per) =>
                    {
                        var months = (int)month;
                        var req = (int)period - 1;
                        var lastIndex = months == 12 ? (int)life - 1 : (int)life;
                        while (true)
                        {
                            var i = (int)per;

                            if (i == 0)
                            {
                                var depr = cost * rate * month / 12;
                                if (req == 0) return depr;
                                per += 1;
                                totDepr = depr;
                            }
                            else
                            {
                                if (req == lastIndex)
                                {
                                    if (i < lastIndex)
                                    {
                                        per += 1;
                                        totDepr += DeprForPeriod(cost, totDepr, rate);
                                    }
                                    else
                                    {
                                        if (months == 12) return DeprForPeriod(cost, totDepr, rate);
                                        else return (cost - totDepr) * rate * (12 - month) / 12;
                                    }
                                }
                                else
                                {
                                    if (i == req) return DeprForPeriod(cost, totDepr, rate);
                                    else
                                    {
                                        per += 1;
                                        totDepr += DeprForPeriod(cost, totDepr, rate);
                                    }

                                }

                            }
                            /*  if (i == (((int)period) - 1))
                              {
                                  if (month == 12)
                                  {
                                      if (period == life) //the last segment in a normal situation
                                          return (cost - totDepr) * rate * (12 - month) / 12;
                                      else
                                          return DeprForPeriod(cost, totDepr, rate); // an intermediate segment
                                  }
                                  else // there is an extra segment
                                  {
                                      var last = life + 1;
                                      if (i < last)
                                      {
                                          return DeprForPeriod(cost, totDepr, rate); // an intermediate segment
                                      }
                                      else
                                          return (cost - totDepr) * rate * (12 - month) / 12;
                                  }

                              }
                              else
                              {
                                  per += 1;
                                  totDepr += DeprForPeriod(cost, totDepr, rate);
                              }*/
                        }
                    };
            }

            private static Func<double, double, double> FDepr(double cost, double salvage, double life)
            {
                return (totDepr, aPeriod) => SLNInternal(cost - totDepr, salvage, life - aPeriod);
            }

            private static double DaysInYear(DateTime date, DayCountBasis basis)
            {
                if (basis != DayCountBasis.ActualActual)
                {
                    var dc = DayCount.DayCounterFactory(basis);
                    var dayCount = dc;
                    var dateTime = date;
                    var dateTime1 = date;
                    return dayCount.DaysInYear(dateTime, dateTime1);
                }
                return !Common.IsLeapYear(date) ? 365 : 366;
            }

            private static double DDBInternal(double cost, double salvage, double life, double period, double factor)
            {
                return period < 2 ? TotalDepr(cost, salvage, life, period, factor, false) : DeprBetweenPeriods(cost, salvage, life, period - 1, period, factor, false);
            }

            private static double DeprForPeriod(double cost, double totDepr, double rate)
            {
                return (cost - totDepr) * rate;
            }

            private static double DeprRate(double cost, double salvage, double life)
            {
                return Math.Abs(cost) < Constants.Epsilon ? 0 : Math.Round(1 - Math.Pow(salvage / cost, 1 / life), 3);
            }

            private static Tuple<double, double> FirstDeprLinc(double cost, DateTime datePurch, DateTime firstP, double salvage, double rate, double assLife, DayCountBasis basis)
            {
                var fix29February = FFirstDeprLinc(basis);
                var dc = DayCount.DayCounterFactory(basis);
                var daysInYr = DaysInYear(datePurch, basis);
                var tuple = new Tuple<DateTime, DateTime>(fix29February.Invoke(datePurch), fix29February.Invoke(firstP));
                var firstPeriod = tuple.Item2;
                var datePurchased = tuple.Item1;
                var dayCount = dc;
                var dateTime = datePurchased;
                var dateTime1 = firstPeriod;

                var firstLen = dayCount.DaysBetween(dateTime, dateTime1, NumDenumPosition.Numerator);
                var firstDeprTemp = firstLen / daysInYr * rate * cost;
                var firstDepr = (firstDeprTemp != 0 ? firstDeprTemp : cost * rate);
                var d = (firstDeprTemp != 0 ? assLife + 1 : assLife);
                var availDepr = cost - salvage;
                return firstDepr <= availDepr ? new Tuple<double, double>(firstDepr, d) : new Tuple<double, double>(availDepr, d);
            }

            private static Func<double, double, double, double, double> FAmorDegrc(double salvage, double period, bool excelComplaint, double assetLife)
            {
                return (countedPeriod, depr, deprRate, remainCost) =>
                {
                    while (countedPeriod <= period)
                    {
                        var num3 = countedPeriod + 1;
                        var calcT = assetLife - num3;
                        var deprTemp = (!Common.AreEqual(calcT, 2) ? deprRate * remainCost : remainCost * 0.5);
                        var num4 = (!Common.AreEqual(calcT, 2) ? deprRate : 1);
                        var num2 = remainCost >= salvage ? deprTemp : (remainCost - salvage >= 0 ? remainCost - salvage : 0);
                        var num5 = num2;
                        var num6 = remainCost - num5;
                        remainCost = num6;
                        deprRate = num4;
                        depr = num5;
                        countedPeriod = num3;
                    }
                    return Common.Round(excelComplaint, depr);
                };
            }

            private static Func<double, double, double> FTotalDepr(double cost, double salvage, double life, double period, double factor, bool straightLine)
            {
                return (totDepr, per) =>
                {
                    double frac;

                    double newTotalDepr;
                    double num1;

                    Func<double, double, double> slnDeprFormula;
                    Func<double, double> ddbDeprFormula;
                    while (true)
                    {
                        frac = Common.Rest(period);
                        ddbDeprFormula = d => Common.Min((cost - d) * factor / life, cost - salvage - d);
                        slnDeprFormula = FDepr(cost, salvage, life);
                        var tuple = new Tuple<double, double>(ddbDeprFormula.Invoke(totDepr), slnDeprFormula(totDepr, per));
                        var slnDepr = tuple.Item2;
                        var ddbDepr = tuple.Item1;
                        var flag = (straightLine && ddbDepr < slnDepr);
                        var isSln = flag;
                        var num = (!isSln ? ddbDepr : slnDepr);
                        var depr = num;
                        newTotalDepr = totDepr + depr;
                        if ((int)period == 0) return newTotalDepr * frac;
                        if ((int)per == (int)period - 1) break;
                        per = per + 1;
                        totDepr = newTotalDepr;
                    }
                    var ddbDeprNextPeriod = ddbDeprFormula.Invoke(newTotalDepr);
                    var slnDeprNextPeriod = slnDeprFormula.Invoke(newTotalDepr, per + 1);
                    var flag1 = (straightLine && ddbDeprNextPeriod < slnDeprNextPeriod);
                    var isSlnNextPeriod = flag1;
                    if (!isSlnNextPeriod) num1 = ddbDeprNextPeriod;
                    else num1 = ((int)period != (int)life ? slnDeprNextPeriod : 0);
                    var deprNextPeriod = num1;
                    return newTotalDepr + deprNextPeriod * frac;
                };
            }

            private static double SLNInternal(double cost, double salvage, double life)
            {
                return (cost - salvage) / life;
            }


            private static double TotalDepr(double cost, double salvage, double life, double period, double factor, bool straightLine)
            {
                var ddb = FTotalDepr(cost, salvage, life, period, factor, straightLine);
                return ddb.Invoke(0, 0);
            }

            internal static double AmorDegrc(double cost, DateTime datePurchased, DateTime firstPeriod, double salvage, double period, double rate, DayCountBasis basis, bool excelComplaint)
            {
                var assetLife = 1 / rate;
                var between = FAmorDegrc(assetLife);
                Common.Ensure("Assset life cannot be between 0 and 3", !between.Invoke(0, 3));
                Common.Ensure("Assset life cannot be between 4. and 5.", !between.Invoke(4, 5));
                Common.Ensure("Cost must be 0 or more", cost >= 0);
                Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                Common.Ensure("Salvage must be less than cost", salvage < cost);
                Common.Ensure("Period must be 0 or more", period >= 0);
                Common.Ensure("Rate must be 0 or more", rate >= 0);
                Common.Ensure("DatePurchased must be less than FirstPeriod", datePurchased < firstPeriod);
                Common.Ensure("basis cannot be Actual360", basis != DayCountBasis.Actual360);
                var assLife = Common.Ceiling(1d / rate);
                if (cost == salvage || period > assLife) return 0d;
                var deprCoeff = DeprCoeff(assLife);
                var deprR = rate * deprCoeff;
                var tuple = FirstDeprLinc(cost, datePurchased, firstPeriod, salvage, deprR, assLife, basis);
                var firstDeprLinc = tuple.Item1;

                var assetL = tuple.Item2;
                var firstDepr = Common.Round(excelComplaint, firstDeprLinc);
                var findDepr = FAmorDegrc(salvage, period, excelComplaint, assetL);

                return period == 0 ? firstDepr : findDepr.Invoke(1, 0, deprR, cost - firstDepr);

            }

            internal static double AmorLinc(double cost, DateTime datePurchased, DateTime firstPeriod, double salvage, double period, double rate, DayCountBasis basis)
            {
                var assetLifeTemp = Common.Ceiling(1 / rate);
                var findDepr = FAmorLinc(period);

                if (cost != salvage ? period > assetLifeTemp : true) return 0;
                var tuple = FirstDeprLinc(cost, datePurchased, firstPeriod, salvage, rate, assetLifeTemp, basis);
                var firstDepr = tuple.Item1;
                return period != 0 ? findDepr.Invoke(1, rate * cost, cost - salvage - firstDepr) : firstDepr;
            }

            internal static double Db(double cost, double salvage, double life, double period, double month)
            {
                Common.Ensure("Cost must be 0 or more", cost >= 0);
                Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                Common.Ensure("Life must be 0 or more", life > 0);
                Common.Ensure("Month must be 0 or more", month > 0);
                Common.Ensure("Period must be less than life", period <= life + 1);
                Common.Ensure("Period must be more than 0", period > 0);
                Common.Ensure("Month must be less or equal to 12", month <= 12);
                var rate = DeprRate(cost, salvage, life);
                var fdb = FDb(cost, life, period, month, rate);
                return fdb.Invoke(0, 0);
            }

            internal static double Ddb(double cost, double salvage, double life, double period, double factor)
            {
                Common.Ensure("Cost must be 0 or more", cost >= 0);
                Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                Common.Ensure("Life must be 0 or more", life > 0);
                Common.Ensure("Month must be 0 or more", factor > 0);
                Common.Ensure("Period must be less than life", period <= life);
                Common.Ensure("Period must be more than 0", period > 0);
                return (int)period != 0 ? DDBInternal(cost, salvage, life, period, factor) : Common.Min(cost * factor / life, cost - salvage);
            }

            internal static double Sln(double cost, double salvage, double life)
            {
                //Common.Ensure("Cost must be 0 or more", cost >= 0);
                //Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                //Common.Ensure("Life must be 0 or more", life > 0);
                return SLNInternal(cost, salvage, life);
            }

            internal static double Syd(double cost, double salvage, double life, double period)
            {
                // Common.Ensure("Cost must be 0 or more", cost >= 0);
                Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                Common.Ensure("Life must be 0 or more", life > 0);
                Common.Ensure("Period must be less than life", period <= life);
                Common.Ensure("Period must be more than 0", period > 0);
                return (cost - salvage) * (life - period + 1) * 2 / (life * (life + 1));
            }

            internal static double Vdb(double cost, double salvage, double life, double startPeriod, double endPeriod, double factor, VdbSwitch bflag)
            {
                Common.Ensure("Cost must be 0 or more", cost >= 0);
                Common.Ensure("Salvage must be 0 or more", salvage >= 0);
                Common.Ensure("Life must be 0 or more", life > 0);
                Common.Ensure("Month must be 0 or more", factor > 0);
                Common.Ensure("Start period must be 1 or more", startPeriod >= 0);
                Common.Ensure("End period must be 1 or more", endPeriod >= 0);
                //Common.Ensure("StartPeriod must be less than life", startPeriod <= life);
                // Common.Ensure("EndPeriod must be less than life", endPeriod <= life);
                // Common.Ensure("StartPeriod must be less than endPeriod", startPeriod <= endPeriod);
                // Common.Ensure("EndPeriod must be more than 0", endPeriod > 0);
                Common.Ensure("If bflag is set to SwitchToStraightLine, then life, startPeriod and endPeriod cannot all have the same value", bflag == VdbSwitch.DontSwitchToStraightLine || !(life == startPeriod && startPeriod == endPeriod));
                return bflag != VdbSwitch.DontSwitchToStraightLine ? DeprBetweenPeriods(cost, salvage, life, startPeriod, endPeriod, factor, true) : DeprBetweenPeriods(cost, salvage, life, startPeriod, endPeriod, factor, false);
            }

            private static Func<double, double, bool> FAmorDegrc(double assetLife)
            {
                return (x1, x2) => x1 <= assetLife && assetLife <= x2;
            }

            private static Func<double, double, double, double> FAmorLinc(double period)
            {
                return (countedPeriod, depr, availDepr) =>
                {
                    while (countedPeriod <= period)
                    {
                        var num = (depr <= availDepr ? depr : availDepr);
                        var num2 = num;
                        var availDeprTemp = availDepr - num2;
                        var num1 = (availDeprTemp >= 0 ? availDeprTemp : 0);
                        var num3 = num1;
                        availDepr = num3;
                        depr = num2;
                        countedPeriod = countedPeriod + 1;
                    }
                    return depr;
                };
            }
            private static Func<DateTime, DateTime> FFirstDeprLinc(DayCountBasis basis)
            {
                return dateTime =>
                {
                    var tuple = Common.ToTuple(dateTime);
                    var y = tuple.Item1;
                    var m = tuple.Item2;
                    var d = tuple.Item3;
                    var flag = basis == DayCountBasis.ActualActual || basis == DayCountBasis.Actual365;
                    var flag1 = flag && Common.IsLeapYear(dateTime);
                    var flag2 = flag1 && m == 2;
                    var flag3 = flag2 && d >= 28;
                    return !flag3 ? dateTime : Common.Date(y, m, 28);
                };
            }
        }

        /// <summary>
        /// Utilities related to counting days and the diverse convention around it.
        /// See for example http://en.wikipedia.org/wiki/Day_count_convention.
        /// </summary>
        static class DayCount
        {
            private static readonly Func<DateTime, DateTime, double> FPcdNcd = (d1, d2) => 0;

            public enum Method360Us
            {
                ModifyStartDate,

                ModifyBothDates
            }

            /// <summary>
            /// Defines the common methods of the day counting modules for each <see cref="DayCountBasis"/>.
            /// </summary>
            public interface IDayCounter
            {
                DateTime ChangeMonth(DateTime dateTime, int num, bool flag);

                double CoupDays(DateTime settlement, DateTime maturity, double num);

                double CoupDaysBS(DateTime settlement, DateTime maturity, double num);

                double CoupDaysNC(DateTime settlement, DateTime maturity, double num);

                DateTime CoupNCD(DateTime settlement, DateTime maturity, double num);

                double CoupNum(DateTime settlement, DateTime maturity, double num);

                DateTime CoupPCD(DateTime settlement, DateTime maturity, double num);

                double DaysBetween(DateTime from, DateTime t, NumDenumPosition numDenumPosition);

                double DaysInYear(DateTime a, DateTime b);
            }

            public static IDayCounter DayCounterFactory(DayCountBasis basis)
            {
                switch (basis)
                {
                    case DayCountBasis.UsPsa30_360:
                        {
                            return new UsPsa30360();
                        }
                    case DayCountBasis.ActualActual:
                        {
                            return new ActualActual();
                        }
                    case DayCountBasis.Actual360:
                        {
                            return new Actual360();
                        }
                    case DayCountBasis.Actual365:
                        {
                            return new Actual365();
                        }
                    case DayCountBasis.Europ30_360:
                        {
                            return new Europ30360();
                        }
                }
                throw new Exception("Should not get here.");
            }

            private static double ActualCoupDays(DateTime settl, DateTime mat, double freq)
            {
                var pcd = FindPreviousCouponDate(settl, mat, freq, DayCountBasis.ActualActual);
                var ncd = FindNextCouponDate(settl, mat, freq, DayCountBasis.ActualActual);
                return Common.Days(ncd, pcd);
            }

            public static DateTime ChangeMonth(DateTime orgDate, int numMonths, DayCountBasis basis, bool returnLastDay)
            {
                var newDate = orgDate.AddMonths(numMonths);
                var tuple1 = new Tuple<int, int, int>(newDate.Year, newDate.Month, newDate.Day);
                var year = tuple1.Item1;
                var month = tuple1.Item2;

                return !returnLastDay ? newDate : new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }

            private static bool ConsiderAsBisestile(DateTime d1, DateTime d2)
            {
                var tuple = Common.ToTuple(d1);
                var item1 = tuple.Item1;
                var tuple1 = Common.ToTuple(d2);
                var num = tuple1.Item1;
                var item21 = tuple1.Item2;
                var item31 = tuple1.Item3;
                var flag = item1 == num && Common.IsLeapYear(d1);
                var flag1 = flag || item21 == 2 && item31 == 29;
                return flag1 || IsFeb29BetweenConsecutiveYears(d1, d2);
            }

            private static bool LessOrEqualToAYearApart(DateTime d1, DateTime d2)
            {
                var tuple = Common.ToTuple(d1);
                var item1 = tuple.Item1;
                var item2 = tuple.Item2;
                var item3 = tuple.Item3;

                var tuple1 = Common.ToTuple(d2);
                var num = tuple1.Item1;
                var item21 = tuple1.Item2;
                var item31 = tuple1.Item3;
                if (item1 != num)
                {
                    if (num != item1 + 1) return false;
                    if (item2 <= item21) return item2 == item21 && item3 >= item31;
                    return true;
                }
                return true;
            }

            internal static double CoupDays(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                Common.Ensure("settlement must be before maturity", maturity > settlement);
                var dc = DayCounterFactory(basis);
                var dayCount = dc;
                var dateTime = settlement;
                var dateTime1 = maturity;
                var num = (double)frequency;
                return dayCount.CoupDays(dateTime, dateTime1, num);
            }

            internal static double CoupDaysBS(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                var dc = DayCounterFactory(basis);
                return dc.CoupDaysBS(settlement, maturity, (double)frequency);
            }

            internal static double CoupDaysNC(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                Common.Ensure("settlement must be before maturity", maturity > settlement);
                var dc = DayCounterFactory(basis);
                return dc.CoupDaysNC(settlement, maturity, (double)frequency);
            }

            internal static DateTime CoupNCD(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                Common.Ensure("settlement must be before maturity", maturity > settlement);
                var dc = DayCounterFactory(basis);
                var dayCount = dc;
                var dateTime = settlement;
                var dateTime1 = maturity;
                var num = (double)frequency;
                return dayCount.CoupNCD(dateTime, dateTime1, num);
            }

            internal static double CoupNum(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                Common.Ensure("settlement must be before maturity", maturity > settlement);
                var dc = DayCounterFactory(basis);
                var dayCount = dc;
                var dateTime = settlement;
                var dateTime1 = maturity;
                var num = (double)frequency;
                return dayCount.CoupNum(dateTime, dateTime1, num);
            }

            internal static DateTime CoupPCD(DateTime settlement, DateTime maturity, Frequency frequency, DayCountBasis basis)
            {
                Common.Ensure("settlement must be before maturity", maturity > settlement);
                var dc = DayCounterFactory(basis);
                var dayCount = dc;
                var dateTime = settlement;
                var dateTime1 = maturity;
                var num = (double)frequency;
                return dayCount.CoupPCD(dateTime, dateTime1, num);
            }

            private static int DateDiff360(int sd, int sm, int sy, int ed, int em, int ey)
            {
                return (ey - sy) * 360 + (em - sm) * 30 + ed - sd;
            }

            private static int DateDiff360Eu(DateTime arg2, DateTime arg1)
            {
                var startDate = arg2;
                var tuple = Common.ToTuple(startDate);
                var sy = tuple.Item1;
                var sm = tuple.Item2;
                var sd = tuple.Item3;
                var endDate = arg1;
                var tuple1 = Common.ToTuple(endDate);
                var ey = tuple1.Item1;
                var em = tuple1.Item2;
                var ed = tuple1.Item3;
                var tuple2 = new Tuple<int, int, int, int, int, int, DateTime, Tuple<DateTime>>(sd, sm, sy, ed, em, ey, startDate, new Tuple<DateTime>(endDate));
                var sy1 = tuple2.Item3;

                var sm1 = tuple2.Item2;
                var sd1 = tuple2.Item1;
                var ey1 = tuple2.Item6;

                var em1 = tuple2.Item5;
                var ed1 = tuple2.Item4;
                var num = (sd1 != 31 ? sd1 : 30);
                sd1 = num;
                var num1 = (ed1 != 31 ? ed1 : 30);
                ed1 = num1;
                return DateDiff360(sd1, sm1, sy1, ed1, em1, ey1);
            }

            public static int DateDiff360US(DateTime startDate, DateTime endDate, Method360Us method360)
            {

                var tuple = Common.ToTuple(startDate);
                var sy = tuple.Item1;
                var sm = tuple.Item2;
                var sd = tuple.Item3;

                var tuple1 = Common.ToTuple(endDate);
                var ey = tuple1.Item1;
                var em = tuple1.Item2;
                var ed = tuple1.Item3;

                var tuple2 = new Tuple<int, int, int, int, int, int, DateTime, Tuple<DateTime>>(sd, sm, sy, ed, em, ey, startDate, new Tuple<DateTime>(endDate));
                var sy1 = tuple2.Item3;
                var startDate1 = tuple2.Item7;
                var sm1 = tuple2.Item2;
                var sd1 = tuple2.Item1;
                var ey1 = tuple2.Item6;

                var endDate1 = tuple2.Rest.Item1;
                var em1 = tuple2.Item5;
                var ed1 = tuple2.Item4;
                var flag = Common.LastDayOfFebruary(endDate1) && (Common.LastDayOfFebruary(startDate1) || method360 == Method360Us.ModifyBothDates);
                if (flag) ed1 = 30;
                var flag1 = ed1 == 31 && (sd1 >= 30 || method360 == Method360Us.ModifyBothDates);
                if (flag1) ed1 = 30;
                if (sd1 == 31) sd1 = 30;
                if (Common.LastDayOfFebruary(startDate1)) sd1 = 30;
                return DateDiff360(sd1, sm1, sy1, ed1, em1, ey1);
            }

            private static int DateDiff365(DateTime arg2, DateTime arg1)
            {
                var startDate = arg2;
                var tuple = Common.ToTuple(startDate);
                var sy = tuple.Item1;
                var sm = tuple.Item2;
                var sd = tuple.Item3;
                var endDate = arg1;
                var tuple1 = Common.ToTuple(endDate);
                var ey = tuple1.Item1;
                var em = tuple1.Item2;
                var ed = tuple1.Item3;
                var tuple2 = new Tuple<int, int, int, int, int, int, DateTime, Tuple<DateTime>>(sd, sm, sy, ed, em, ey, startDate, new Tuple<DateTime>(endDate));
                var sy1 = tuple2.Item3;

                var sm1 = tuple2.Item2;
                var sd1 = tuple2.Item1;
                var ey1 = tuple2.Item6;

                var em1 = tuple2.Item5;
                var ed1 = tuple2.Item4;
                var flag = sd1 > 28 && sm1 == 2;
                if (flag) sd1 = 28;
                var flag1 = (ed1 > 28 && em1 == 2);
                if (flag1) ed1 = 28;
                var tuple3 = new Tuple<DateTime, DateTime>(Common.Date(sy1, sm1, sd1), Common.Date(ey1, em1, ed1));
                var startd = tuple3.Item1;
                var endd = tuple3.Item2;
                return (ey1 - sy1) * 365 + Common.Days(endd, startd);
            }

            internal static Tuple<DateTime, DateTime, double> DatesAggregate1(DateTime startDate, DateTime endDate, int numMonths, DayCountBasis basis, Func<DateTime, DateTime, double> f, double acc, bool returnLastMonth)
            {
                var iter = FDatesAggregator(endDate, numMonths, basis, f, returnLastMonth);
                return iter.Invoke(startDate, endDate, acc);
            }
            internal static double YearFrac(DateTime startDate, DateTime endDate, DayCountBasis basis)
            {
                Common.Ensure("startDate must be before endDate", startDate < endDate);
                var dc = DayCounterFactory(basis);
                return dc.DaysBetween(startDate, endDate, NumDenumPosition.Numerator) / dc.DaysInYear(startDate, endDate);
            }
            private static Tuple<DateTime, DateTime> FindCouponDates(DateTime settl, DateTime mat, double freq, DayCountBasis basis)
            {
                var tuple = Common.ToTuple(mat);
                var my = tuple.Item1;
                var mm = tuple.Item2;
                var md = tuple.Item3;
                var endMonth = Common.LastDayOfMonth(my, mm, md);
                var numMonths = -Freq2Months(freq);
                return FindPcdNcd(mat, settl, numMonths, basis, endMonth);
            }

            private static DateTime FindNextCouponDate(DateTime settl, DateTime mat, double freq, DayCountBasis basis)
            {
                return FindCouponDates(settl, mat, freq, basis).Item2;
            }

            internal static Tuple<DateTime, DateTime> FindPcdNcd(DateTime startDate, DateTime endDate, int numMonths, DayCountBasis basis, bool returnLastMonth)
            {
                var tuple = DatesAggregate1(startDate, endDate, numMonths, basis, FPcdNcd, 0, returnLastMonth);
                var pcd = tuple.Item1;
                var ncd = tuple.Item2;
                return new Tuple<DateTime, DateTime>(pcd, ncd);
            }

            private static DateTime FindPreviousCouponDate(DateTime settl, DateTime mat, double freq, DayCountBasis basis)
            {
                return FindCouponDates(settl, mat, freq, basis).Item1;
            }

            internal static int Freq2Months(double freq)
            {
                return 12 / (int)freq;
            }

            private static bool IsFeb29BetweenConsecutiveYears(DateTime arg2, DateTime arg1)
            {
                var date1 = arg2;
                var tuple = Common.ToTuple(date1);
                var y1 = tuple.Item1;
                var m1 = tuple.Item2;

                var date2 = arg1;
                var tuple1 = Common.ToTuple(date2);
                var y2 = tuple1.Item1;
                var m2 = tuple1.Item2;


                if (y1 == y2 && Common.IsLeapYear(date1))
                {
                    var flag1 = m1 <= 2 && m2 > 2;
                    return flag1;
                }
                if (y1 != y2)
                {
                    if (y2 != y1 + 1) throw new Exception("isFeb29BetweenConsecutiveYears: function called with non consecutive years");
                    if (!Common.IsLeapYear(date1)) return Common.IsLeapYear(date2) && m2 > 2;
                    return m1 <= 2;
                }
                return false;
            }

            private static double NumberOfCoupons(DateTime settl, DateTime mat, double freq, DayCountBasis basis)
            {
                var tuple = Common.ToTuple(mat);
                var my = tuple.Item1;
                var mm = tuple.Item2;

                var pcdate = FindPreviousCouponDate(settl, mat, freq, basis);
                var tuple1 = Common.ToTuple(pcdate);
                var pcy = tuple1.Item1;
                var pcm = tuple1.Item2;

                var months = (double)((my - pcy) * 12 + mm - pcm);
                return months * freq / 12;
            }

            private sealed class UsPsa30360 : IDayCounter
            {
                public DateTime ChangeMonth(DateTime dateTime, int num, bool flag)
                {
                    return DayCount.ChangeMonth(dateTime, num, DayCountBasis.UsPsa30_360, flag);
                }

                public double CoupDays(DateTime settlement, DateTime maturity, double num)
                {
                    return 360 / num;
                }

                public double CoupDaysBS(DateTime settlement, DateTime maturity, double num)
                {
                    return DateDiff360US(this.CoupPCD(settlement, maturity, num), settlement, Method360Us.ModifyStartDate);
                }

                public double CoupDaysNC(DateTime settlement, DateTime maturity, double num)
                {
                    var pdc = FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.UsPsa30_360);
                    var ndc = FindNextCouponDate(settlement, maturity, num, DayCountBasis.UsPsa30_360);
                    var totDaysInCoup = DateDiff360US(pdc, ndc, Method360Us.ModifyBothDates);
                    var daysToSettl = DateDiff360US(pdc, settlement, Method360Us.ModifyStartDate);
                    return totDaysInCoup - daysToSettl;
                }

                public DateTime CoupNCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindNextCouponDate(settlement, maturity, num, DayCountBasis.UsPsa30_360);
                }

                public double CoupNum(DateTime settlement, DateTime maturity, double num)
                {
                    return NumberOfCoupons(settlement, maturity, num, DayCountBasis.UsPsa30_360);
                }

                public DateTime CoupPCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.UsPsa30_360);
                }

                public double DaysBetween(DateTime settlement, DateTime maturity, NumDenumPosition numDenumPosition)
                {
                    return DateDiff360US(settlement, maturity, Method360Us.ModifyStartDate);
                }

                public double DaysInYear(DateTime a, DateTime b)
                {
                    return 360;
                }
            }

            /// <summary>
            /// Day count convention for calculating interest accrued on U.S. Treasury bills and other money market instruments. 
            /// Uses actual number of days in a month and 360 days in a year for calculating interest payments. 
            /// </summary>
            private sealed class Actual360 : IDayCounter
            {
                public DateTime ChangeMonth(DateTime dateTime, int num, bool flag)
                {
                    return DayCount.ChangeMonth(dateTime, num, DayCountBasis.Actual360, flag);
                }

                public double CoupDays(DateTime settlement, DateTime maturity, double num)
                {
                    return 360 / num;
                }

                public double CoupDaysBS(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(settlement, this.CoupPCD(settlement, maturity, num));
                }

                public double CoupDaysNC(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(this.CoupNCD(settlement, maturity, num), settlement);
                }

                public DateTime CoupNCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindNextCouponDate(settlement, maturity, num, DayCountBasis.Actual360);
                }

                public double CoupNum(DateTime settlement, DateTime maturity, double num)
                {
                    return NumberOfCoupons(settlement, maturity, num, DayCountBasis.Actual360);
                }

                public DateTime CoupPCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.Actual360);
                }

                public double DaysBetween(DateTime issue, DateTime settlement, NumDenumPosition numDenumPosition)
                {
                    return numDenumPosition == NumDenumPosition.Numerator ? Common.Days(settlement, issue) : DateDiff360US(settlement, issue, Method360Us.ModifyStartDate);
                }

                public double DaysInYear(DateTime a, DateTime b)
                {
                    return 360;
                }
            }

            /// <summary>
            /// Each month is treated normally and the year is assumed to be 365 days.
            /// </summary>
            private sealed class Actual365 : IDayCounter
            {
                public DateTime ChangeMonth(DateTime dateTime, int num, bool flag)
                {
                    return DayCount.ChangeMonth(dateTime, num, DayCountBasis.Actual365, flag);
                }

                public double CoupDays(DateTime settlement, DateTime maturity, double num)
                {
                    return 365 / num;
                }

                public double CoupDaysBS(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(settlement, this.CoupPCD(settlement, maturity, num));
                }

                public double CoupDaysNC(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(this.CoupNCD(settlement, maturity, num), settlement);
                }

                public DateTime CoupNCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindNextCouponDate(settlement, maturity, num, DayCountBasis.Actual365);
                }

                public double CoupNum(DateTime settlement, DateTime maturity, double num)
                {
                    return NumberOfCoupons(settlement, maturity, num, DayCountBasis.Actual365);
                }

                public DateTime CoupPCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.Actual365);
                }

                public double DaysBetween(DateTime maturity, DateTime settlement, NumDenumPosition numDenumPosition)
                {
                    return numDenumPosition == NumDenumPosition.Numerator ? Common.Days(settlement, maturity) : DateDiff365(maturity, settlement);
                }

                public double DaysInYear(DateTime settlement, DateTime dateTime)
                {
                    return 365;
                }
            }

            /// <summary>
            /// "Actual/Actual AFB/FBF Master Agreement" has the DiY equal to 365 (if the calculation period does not contain 29 February) or 366 (if 29 February falls within the Calculation Period or Compounding Period).
            /// </summary>
            private sealed class ActualActual : IDayCounter
            {
                public DateTime ChangeMonth(DateTime dateTime, int num, bool flag)
                {
                    return DayCount.ChangeMonth(dateTime, num, DayCountBasis.ActualActual, flag);
                }

                public double CoupDays(DateTime settlement, DateTime maturity, double num)
                {
                    return ActualCoupDays(settlement, maturity, num);
                }

                public double CoupDaysBS(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(settlement, this.CoupPCD(settlement, maturity, num));
                }

                public double CoupDaysNC(DateTime settlement, DateTime maturity, double num)
                {
                    return Common.Days(this.CoupNCD(settlement, maturity, num), settlement);
                }

                public DateTime CoupNCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindNextCouponDate(settlement, maturity, num, DayCountBasis.ActualActual);
                }

                public double CoupNum(DateTime settlement, DateTime maturity, double num)
                {
                    return NumberOfCoupons(settlement, maturity, num, DayCountBasis.ActualActual);
                }

                public DateTime CoupPCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.ActualActual);
                }

                public double DaysBetween(DateTime startDate, DateTime endDate, NumDenumPosition numDenumPosition)
                {
                    return Common.Days(endDate, startDate);
                }

                public double DaysInYear(DateTime a, DateTime b)
                {
                    if (LessOrEqualToAYearApart(a, b)) return !ConsiderAsBisestile(a, b) ? 365 : 366;
                    var totDays = Common.Days(Common.Date(a.Year + 1, 1, 1), Common.Date(b.Year, 1, 1));
                    return totDays / (double)(a.Year - b.Year + 1);
                }
            }

            /// <summary>
            /// The 360-day calendar is a method of measuring durations used in financial markets, in computer models, in ancient literature, and in prophetic literary genres. 
            /// It is based on merging the three major calendar systems into one complex clock, with the 360-day year as the average year of the lunar and the solar: 365.24 (solar) + 354.37(lunar) = 719.61 ÷ 2 = 359.8 days rounded to 360. 
            /// It is a simplification to a 360-day year, consisting of 12 months of 30 days each. To derive such a calendar from the standard Gregorian calendar, certain days are skipped.
            /// </summary>
            private sealed class Europ30360 : IDayCounter
            {
                public DateTime ChangeMonth(DateTime settlement, int num, bool flag)
                {
                    return DayCount.ChangeMonth(settlement, num, DayCountBasis.Europ30_360, flag);
                }

                public double CoupDays(DateTime settlement, DateTime maturity, double num)
                {
                    return 360 / num;
                }

                public double CoupDaysBS(DateTime settlement, DateTime maturity, double num)
                {
                    return DateDiff360Eu(this.CoupPCD(settlement, maturity, num), settlement);
                }

                public double CoupDaysNC(DateTime settlement, DateTime maturity, double num)
                {
                    return DateDiff360Eu(settlement, this.CoupNCD(settlement, maturity, num));
                }

                public DateTime CoupNCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindNextCouponDate(settlement, maturity, num, DayCountBasis.Europ30_360);
                }

                public double CoupNum(DateTime settlement, DateTime maturity, double num)
                {
                    return NumberOfCoupons(settlement, maturity, num, DayCountBasis.Europ30_360);
                }

                public DateTime CoupPCD(DateTime settlement, DateTime maturity, double num)
                {
                    return FindPreviousCouponDate(settlement, maturity, num, DayCountBasis.Europ30_360);
                }

                public double DaysBetween(DateTime settlement, DateTime maturity, NumDenumPosition numDenumPosition)
                {
                    return DateDiff360Eu(settlement, maturity);
                }

                public double DaysInYear(DateTime settlement, DateTime maturity)
                {
                    return 360;
                }
            }
            private static Func<DateTime, DateTime, double, Tuple<DateTime, DateTime, double>> FDatesAggregator(DateTime endDate, int numMonths, DayCountBasis basis, Func<DateTime, DateTime, double> f, bool returnLastMonth)
            {
                return (frontDate, trailingDate, acc) =>
                {
                    while (true)
                    {
                        if (numMonths <= 0 ? frontDate <= endDate : frontDate >= endDate) break;
                        var dateTime = frontDate;
                        var dateTime1 = ChangeMonth(frontDate, numMonths, basis, returnLastMonth);
                        var num = acc + f(dateTime1, dateTime);
                        acc = num;
                        trailingDate = dateTime;
                        frontDate = dateTime1;
                    }
                    return new Tuple<DateTime, DateTime, double>(frontDate, trailingDate, acc);
                };
            }

        }

        /// <summary>
        /// Calculational utilities.
        /// </summary>
        static class Common
        {
            internal static DateTime AddYears(DateTime d, int n)
            {
                return d.AddYears(n);
            }
            /// <summary>
            /// Aggregates in step of one from start to end using the given collector function.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <param name="collector">The collector.</param>
            /// <param name="seed">The seed.</param>
            /// <returns></returns>
            public static T AggrBetween<T>(int start, int end, Func<T, int, T> collector, T seed)
            {
                IEnumerable<int> nums = Range.Create(start, end);
                return nums.Aggregate(seed, collector);
            }

            public static Tuple<int, int, int> ToTuple(DateTime d1)
            {
                return new Tuple<int, int, int>(d1.Year, d1.Month, d1.Day);
            }

            internal static bool AreEqual(double x, double y)
            {
                return Math.Abs(x - y) < Constants.Epsilon;
            }

            private static double Bisection(Func<double, double> f, double a, double b, int count, double precision)
            {

                var d = FBisection();
                return d.Invoke(f, a, b, count, precision);
            }

            internal static double Ceiling(double x)
            {
                return Math.Ceiling(x);
            }

            internal static DateTime Date(int y, int m, int d)
            {
                return new DateTime(y, m, d);
            }

            internal static int Days(DateTime after, DateTime before)
            {
                var timeSpan = after - before;
                return timeSpan.Days;
            }

            internal static int DaysOfMonth(int y, int m)
            {
                return DateTime.DaysInMonth(y, m);
            }

            internal static void Ensure(string s, bool c)
            {
                if (c) return;
                throw new Exception(s);
            }

            private static Tuple<double, double> FindBounds(Func<double, double> f, double guess, double minBound, double maxBound, double precision)
            {
                if (guess <= minBound || guess >= maxBound) throw new Exception(string.Format("guess needs to be between {0} and {1}", minBound, maxBound));
                const double Shift = 0.01;
                const double Factor = 1.6;
                const int MaxTries = 60;
                var adjValueToMin = FAdjustMin(minBound, precision);
                var adjValueToMax = FAdjustMax(maxBound, precision);
                var rfindBounds = new FindBoundsWorker(f, Factor, MaxTries, adjValueToMin.Invoke, adjValueToMax.Invoke);
                var low = adjValueToMin.Invoke(guess - Shift);
                var high = adjValueToMax.Invoke(guess + Shift);
                return rfindBounds.Invoke(low, high, MaxTries);
            }

            internal static double FindRoot(Func<double, double> f, double guess)
            {
                const double Precision = 1E-07;
                var newtValue = Newton(f, guess, 0, Precision);

                if (newtValue.HasValue && Sign(guess) == Sign(newtValue.Value)) return newtValue.Value;
                var tuple = FindBounds(f, guess, -1, double.MaxValue, Precision);
                var upper = tuple.Item2;
                var lower = tuple.Item1;
                return Bisection(f, lower, upper, 0, Precision);
            }

            internal static double Floor(double x)
            {
                return Math.Floor(x);
            }

            internal static bool IsLeapYear(DateTime d)
            {
                var tuple = ToTuple(d);
                var y = tuple.Item1;
                return DateTime.IsLeapYear(y);
            }

            internal static bool LastDayOfFebruary(DateTime d)
            {

                var tuple = ToTuple(d);
                var item1 = tuple.Item1;
                var item2 = tuple.Item2;
                var item3 = tuple.Item3;
                return item2 == 2 && LastDayOfMonth(item1, item2, item3);
            }

            internal static bool LastDayOfMonth(int y, int m, int d)
            {
                return DateTime.DaysInMonth(y, m) == d;
            }

            internal static double Ln(double x)
            {
                return Math.Log(x);
            }

            internal static double Log10(double x)
            {
                return Math.Log(x, 10);
            }

            internal static double Min(double x, double y)
            {
                return Math.Min(x, y);
            }

            /// <summary>
            /// The well-know Newton iterator.
            /// </summary>
            /// <param name="f">The underlying function.</param>
            /// <param name="x">The argumentx.</param>
            /// <param name="count">The count.</param>
            /// <param name="precision">The required precision.</param>
            /// <returns></returns>
            private static double? Newton(Func<double, double> f, double x, int count, double precision)
            {
                var d = FNewtonIterator();
                return d.Invoke(f, x, count, precision);
            }

            internal static double Pow(double x, double y)
            {
                return Math.Pow(x, y);
            }

            internal static bool Raisable(double b, double p)
            {
                return !((1d + b) < 0d && (Math.Abs((p - (int)p)) > Constants.Epsilon));
            }

            internal static double Rest(double x)
            {
                return x - (int)x;
            }

            internal static double Round(bool excelComplaint, double x)
            {
#if SILVERLIGHT

				if (!excelComplaint) return DecimalExtensions.Round(x, MidpointRounding.AwayFromZero);
				var k = DecimalExtensions.Round(x, 13, MidpointRounding.AwayFromZero);
				return DecimalExtensions.Round(k, MidpointRounding.AwayFromZero);
#else

                if (!excelComplaint) return Math.Round(x, MidpointRounding.AwayFromZero);
                var k = Math.Round(x, 13, MidpointRounding.AwayFromZero);
                return Math.Round(k, MidpointRounding.AwayFromZero);
#endif
            }

            internal static int Sign(double x)
            {
                return Math.Sign(x);
            }

            internal static double Sqr(double x)
            {
                return Math.Sqrt(x);
            }

            private static Func<double, double> FAdjustMin(double minBound, double precision)
            {
                return value => value > minBound ? value : minBound + precision;
            }
            private static Func<double, double> FAdjustMax(double maxBound, double precision)
            {
                return value => value < maxBound ? value : maxBound - precision;
            }
            private static Func<Func<double, double>, double, double> Derivative(double precision)
            {
                return (f, x) => (f.Invoke(x + precision) - f.Invoke(x - precision)) / (2 * precision);
            }

            private static Func<Func<double, double>, double, int, double, double?> FNewtonIterator(int maxIterations = 20)
            {
                return (f, x, count, precision) =>
                {
                    while (true)
                    {
                        var d = Derivative(precision);
                        var fx = f.Invoke(x);
                        var Fx = d.Invoke(f, x);
                        var newX = x - fx / Fx;
                        if (Math.Abs(newX - x) < precision) return newX;
                        if (count > maxIterations) break;
                        count++;
                        x = newX;
                    }
                    return null;
                };
            }


            private static Func<Func<double, double>, double, double, int, double, double> FBisection(int maxIterations = 200)
            {
                return (f, a, b, count, precision) =>
                {
                    while (true)
                    {
                        if (a == b) throw new Exception(string.Format("(a=b={0}) impossible to start Bisection", a));
                        var fa = f.Invoke(a);
                        if (Math.Abs(fa) < precision) return a;

                        var fb = f.Invoke(b);
                        if (Math.Abs(fb) < precision) return b;

                        var newCount = count + 1;
                        if (newCount > maxIterations) throw new Exception(string.Format("No root found in {0} iterations", maxIterations));

                        if (fa * fb > 0) throw new Exception(string.Format("({0},{1}) don't bracket the root", a, b));

                        var midvalue = a + 0.5 * (b - a);
                        var fmid = f.Invoke(midvalue);
                        if (Math.Abs(fmid) < precision) return midvalue;

                        if (fa * fmid >= 0)
                        {
                            if (fa * fmid <= 0) break;
                            count = newCount;
                            a = midvalue;
                        }
                        else
                        {
                            count = newCount;
                            b = midvalue;
                        }
                    }
                    throw new Exception("Bisection: It should never get here");
                };
            }

            class FindBoundsWorker
            {
                private readonly Func<double, double> adjValueToMax;

                private readonly Func<double, double> adjValueToMin;

                private readonly Func<double, double> f;

                private readonly double factor;

                private readonly int maxTries;

                public FindBoundsWorker(Func<double, double> f, double factor, int maxTries, Func<double, double> adjValueToMin, Func<double, double> adjValueToMax)
                {
                    this.f = f;
                    this.factor = factor;
                    this.maxTries = maxTries;
                    this.adjValueToMin = adjValueToMin;
                    this.adjValueToMax = adjValueToMax;
                }

                public Tuple<double, double> Invoke(double low, double up, int tries)
                {
                    while (true)
                    {
                        var num = tries - 1;
                        if (num == 0) throw new Exception(string.Format("Not found an interval comprising the root after {0} tries, last tried was ({1}, {2})", this.maxTries, low, up));
                        var lower = this.adjValueToMin.Invoke(low);
                        var upper = this.adjValueToMax.Invoke(up);

                        var x = this.f.Invoke(lower);
                        var y = this.f.Invoke(upper);
                        if (Math.Abs(x * y) < Constants.Epsilon) return new Tuple<double, double>(lower, upper);
                        if (x * y < 0) return new Tuple<double, double>(lower, upper);
                        if (x * y > 0)
                        {
                            tries = num;
                            up = upper + this.factor * (upper - lower);
                            low = lower + this.factor * (lower - upper);
                        }
                        else throw new Exception(string.Format("FindBounds: one of the values {0}, {1}) cannot be used to evaluate the objective function", lower, upper));
                    }
                }
            }


        }
    }

#if SILVERLIGHT

	public enum MidpointRounding
	{
		ToEven,
		AwayFromZero
	}

	public static class DecimalExtensions
	{
		public static double Round(this double d, MidpointRounding mode)
		{
			return d.Round(0, mode);
		}

		/// <summary>
		/// Rounds using arithmetic (5 rounds up) symmetrical (up is away from zero) rounding
		/// </summary>
		/// <param name="d">A Decimal number to be rounded.</param>
		/// <param name="decimals">The number of significant fractional digits (precision) in the return value.</param>
		/// <returns>The number nearest d with precision equal to decimals. If d is halfway between two numbers, then the nearest whole number away from zero is returned.</returns>
		public static double Round(this double d, int decimals, MidpointRounding mode)
		{
			if (mode == MidpointRounding.ToEven)
			{
				return (double)decimal.Round((decimal)d, decimals);
			}
			else
			{
				var  factor = Convert.ToDouble(Math.Pow(10, decimals));
				var sign = Math.Sign(d);
				return (double) Decimal.Truncate((decimal)(d * factor + 0.5d * sign)) / factor;
			}
		}
	}
#endif
}