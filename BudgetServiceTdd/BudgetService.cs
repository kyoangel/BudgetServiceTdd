using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetServiceTdd
{
	public class BudgetService
	{
		private readonly IBudgetRepository _budgetRepository;

		public BudgetService(IBudgetRepository budgetRepository)
		{
			_budgetRepository = budgetRepository;
		}

		public double TotalAmount(DateTime start, DateTime end)
		{
			var period = new Period(start, end);
			return _budgetRepository.GetAll().Sum(x => x.DailyAmount() * period.OverlappingDays(x.CreatePeriod()));
		}
	}
}