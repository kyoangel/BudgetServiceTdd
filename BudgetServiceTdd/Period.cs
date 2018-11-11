using System;

namespace BudgetServiceTdd
{
	public class Period
	{
		public Period(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}

		public DateTime Start { get; private set; }
		public DateTime End { get; private set; }

		public double OverlappingDays(Period another)
		{
			if (InvalidPeriod() || NoOverlappingDays(another))
			{
				return 0;
			}

			var validStart = another.Start > Start ? another.Start : Start;
			var validEnd = another.End < End ? another.End : End;

			return validEnd.Subtract(validStart).Days + 1;
		}

		private bool InvalidPeriod()
		{
			return Start > End;
		}

		private bool NoOverlappingDays(Period another)
		{
			return Start > another.End || End < another.Start;
		}
	}
}