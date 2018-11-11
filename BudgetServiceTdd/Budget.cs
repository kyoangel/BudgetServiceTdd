using System;

namespace BudgetServiceTdd
{
	public class Budget
	{
		public string YearMonth { get; set; }
		public int Amount { get; set; }

		private DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

		private DateTime LastDay => new DateTime(FirstDay.Year, FirstDay.Month,
			DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month));

		private int DaysInMonth()
		{
			return DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);
		}

		public int DailyAmount()
		{
			var dailyAmount = Amount / DaysInMonth();
			return dailyAmount;
		}

		public Period CreatePeriod()
		{
			return new Period(FirstDay, LastDay);
		}
	}
}