using System;
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
			var budgets = _budgetRepository.GetAll();
			if (budgets.Any())
			{
				if (budgets[0].FirstDay() > start)
				{
					return 0;
				}
				return end.Subtract(start).Days + 1;
			}

			return 0;
		}
	}
}