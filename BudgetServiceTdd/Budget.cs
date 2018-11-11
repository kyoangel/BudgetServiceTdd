﻿using System;

namespace BudgetServiceTdd
{
	public class Budget
	{
		public string YearMonth { get; set; }
		public int Amount { get; set; }

		public DateTime FirstDay()
		{
			return DateTime.ParseExact(YearMonth, "yyyyMM", null);
		}
	}
}