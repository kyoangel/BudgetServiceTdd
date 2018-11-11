using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetServiceTdd.Tests
{
	[TestClass()]
	public class BudgetServiceTests
	{
		private BudgetService _budgetService;
		private FakeBudgetRepository _fakeRepository;

		[TestInitialize]
		public void Init()
		{
			_fakeRepository = new FakeBudgetRepository();
			_budgetService = new BudgetService(_fakeRepository);
		}

		[TestMethod()]
		public void No_budget()
		{
			TotalAmountShouldBe(0, new DateTime(), new DateTime());
		}

		[TestMethod()]
		public void Period_inside_budget_month()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(1, new DateTime(2018, 4, 1), new DateTime(2018, 4, 1));
		}

		[TestMethod()]
		public void Period_no_overlap_before_budget_firstDay()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(0, new DateTime(2018, 3, 31), new DateTime(2018, 3, 31));
		}

		[TestMethod()]
		public void Period_no_overlap_after_budget_lastDay()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(0, new DateTime(2018, 5, 1), new DateTime(2018, 5, 1));
		}

		[TestMethod()]
		public void Period_overlap_budget_firstDay()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(1, new DateTime(2018, 3, 31), new DateTime(2018, 4, 1));
		}

		[TestMethod()]
		public void Period_overlap_budget_lastDay()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(1, new DateTime(2018, 4, 30), new DateTime(2018, 5, 1));
		}

		[TestMethod()]
		public void Invalid_period()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 30, YearMonth = "201804" });
			TotalAmountShouldBe(0, new DateTime(2018, 4, 30), new DateTime(2018, 4, 1));
		}

		[TestMethod()]
		public void Daily_amount_is_10()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 300, YearMonth = "201804" });
			TotalAmountShouldBe(40, new DateTime(2018, 4, 1), new DateTime(2018, 4, 4));
		}

		[TestMethod()]
		public void Multiple_budget()
		{
			_fakeRepository.SetBudget(new Budget() { Amount = 300, YearMonth = "201804" }
				, new Budget() { Amount = 31, YearMonth = "201805" });
			TotalAmountShouldBe(12, new DateTime(2018, 4, 30), new DateTime(2018, 5, 2));
		}

		private void TotalAmountShouldBe(int expected, DateTime start, DateTime end)
		{
			Assert.AreEqual(expected, _budgetService.TotalAmount(start, end));
		}
	}

	public class FakeBudgetRepository : IBudgetRepository
	{
		private Budget[] _budgets = new Budget[] { };

		public List<Budget> GetAll()
		{
			return _budgets.ToList();
		}

		public void SetBudget(params Budget[] budgets)
		{
			_budgets = budgets;
		}
	}
}