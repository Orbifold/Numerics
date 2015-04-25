using System;
using System.Globalization;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Utility class related to <c>DateTime</c>.
	/// </summary>
	internal sealed class DateTimeUtil
	{
		public static long DateDiff(DateIntervalType intervalType,DateTime startDate, DateTime endDate)
		{
			return DateDiff(intervalType, startDate, endDate, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
		}

		private static int GetQuarter(int nMonth)
		{
			if (nMonth <= 3)
				return 1;
			if (nMonth <= 6)
				return 2;
			if (nMonth <= 9)
				return 3;
			return 4;
		}

		private static long DateDiff(DateIntervalType intervalType, DateTime startDate, DateTime endDate, DayOfWeek firstDayOfWeek)
		{

			if (intervalType == DateIntervalType.Year) return endDate.Year - startDate.Year;
			if (intervalType == DateIntervalType.Month) return (endDate.Month - startDate.Month) + (12 * (endDate.Year - startDate.Year));
			var ts = endDate - startDate;
			if (intervalType == DateIntervalType.Day || intervalType == DateIntervalType.DayOfYear) return Round(ts.TotalDays);
			if (intervalType == DateIntervalType.Hour) return Round(ts.TotalHours); 
			if (intervalType == DateIntervalType.Minute) return Round(ts.TotalMinutes);
			if (intervalType == DateIntervalType.Second) return Round(ts.TotalSeconds);
			if (intervalType == DateIntervalType.Weekday) {return Round(ts.TotalDays / 7.0);}
			if (intervalType == DateIntervalType.WeekOfYear)
			{
				while (endDate.DayOfWeek != firstDayOfWeek) endDate = endDate.AddDays(-1);
				while (startDate.DayOfWeek != firstDayOfWeek) startDate = startDate.AddDays(-1);
				ts = endDate - startDate;
				return Round(ts.TotalDays / 7.0);
			}

			if (intervalType == DateIntervalType.Quarter)
			{
				var d1Quarter = GetQuarter(startDate.Month);
				var d2Quarter = GetQuarter(endDate.Month);
				var d1 = d2Quarter - d1Quarter;
				var d2 = (4 * (endDate.Year - startDate.Year));
				return Round(d1 + d2);
			}
			return 0;
		}

		private static long Round(double dVal)
		{
			if (dVal >= 0) return (long)System.Math.Floor(dVal);
			return (long)System.Math.Ceiling(dVal);
		}
	}
}