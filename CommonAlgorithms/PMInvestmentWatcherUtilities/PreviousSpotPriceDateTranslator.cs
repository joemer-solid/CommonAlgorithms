using CommonAlgorithms.PMInvestmentWatcherUtilities.Extensions;
using System;

namespace CommonAlgorithms.PMInvestmentWatcherUtilities
{
    public sealed class PreviousSpotPriceDateTranslator : IStrategyOperation<DateTime, DateTime>
    {
        private readonly IStrategyOperation<DateTime, DateTime> _currentBusinessDayForDateTranslator;

        public PreviousSpotPriceDateTranslator(IStrategyOperation<DateTime, DateTime> currentBusinessDayForDateTranslator)
        {
            _currentBusinessDayForDateTranslator = currentBusinessDayForDateTranslator ?? throw new ArgumentNullException(nameof(currentBusinessDayForDateTranslator));
        }

        DateTime IStrategyOperation<DateTime, DateTime>.Execute(DateTime p)
        {          
            // from this date-time - go back to 12:00 pm or 1200 hours of the previous business day
            return GetPreviousMidDayDateTimeFromCurrentBusinessDay(p, _currentBusinessDayForDateTranslator);
        }

        private static DateTime GetCurrentBusinessDayFromDate(DateTime p, IStrategyOperation<DateTime, DateTime> currentBusinessDayForDateTranslator)
            => currentBusinessDayForDateTranslator.Execute(p);

        private static DateTime GetPreviousMidDayDateTimeFromCurrentBusinessDay(DateTime currentBusinessDay, IStrategyOperation<DateTime, DateTime> currentBusinessDayForDateTranslator)
            => GetCurrentBusinessDayFromDate(GetMidDayAdjustedDateTime(currentBusinessDay), currentBusinessDayForDateTranslator);

        private static DateTime GetMidDayAdjustedDateTime(DateTime currentBusinessDay)
        {
            const int adjustedTimeSpanMinutes = 0;
            const int adjustedTimeSpanSeconds = 0;
            
            int adjustedTimeSpanHour = CalculateAdjustedTimeSpanHour(currentBusinessDay.Hour);
            TimeSpan adjustedSpan = new TimeSpan(adjustedTimeSpanHour, adjustedTimeSpanMinutes, adjustedTimeSpanSeconds);
            DateTime adjustedCurrentBusinessDay = new DateTime(currentBusinessDay.Year,
                currentBusinessDay.Month,
                currentBusinessDay.Day,
                currentBusinessDay.Hour,
                adjustedTimeSpanMinutes,
                adjustedTimeSpanSeconds);           

            return adjustedCurrentBusinessDay.Subtract(adjustedSpan);
        }

        private static int CalculateAdjustedTimeSpanHour(int currentBusinessDayHour)
        {
            const int midDayHour = 12;

            return midDayHour + currentBusinessDayHour;          
        }
    }
}
