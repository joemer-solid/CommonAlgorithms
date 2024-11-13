using CommonAlgorithms.PMInvestmentWatcherUtilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonAlgorithms.PMInvestmentWatcherUtilities
{
    public sealed class CurrentBusinessDayForDateTranslator : IStrategyOperation<DateTime, DateTime>
    {
        DateTime IStrategyOperation<DateTime, DateTime>.Execute(DateTime p)
        {
            if (IsDateDayWeekDay(p)) { return p; }

            return GetLastBusinessDayForWeekendDate(p);
        }

        private static bool IsDateDayWeekDay(DateTime date)
        {
            bool result = true;

            DayOfWeek dayOfWeek = date.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                result = false;
            }

            return result;
        }

        private static DateTime GetLastBusinessDayForWeekendDate(DateTime date)
        {
            int daysToSubtract = 0;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                daysToSubtract = -1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                daysToSubtract = -2;
            }

            return date.AddDays(daysToSubtract);
        }
    }
}
