using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommonAlgorithms.PMInvestmentWatcherUtilities.Extensions
{
    public interface IStrategyOperation<T, P>
    {
        T Execute(P p);
    }
    public static class SpotPriceExtensions
    {
        private readonly static Lazy<IStrategyOperation<DateTime, DateTime>> _currentBusinessDayForDateTranslator =
            new Lazy<IStrategyOperation<DateTime, DateTime>>(() => new CurrentBusinessDayForDateTranslator());
        
        private readonly static Lazy<IStrategyOperation<DateTime, DateTime>> _previousSpotPriceDateTranslator = 
            new Lazy<IStrategyOperation<DateTime, DateTime>>(() => new PreviousSpotPriceDateTranslator(new CurrentBusinessDayForDateTranslator()));
     
        public static DateTime ToPreviousSpotPriceBusinessDay(this DateTime input)
            => _previousSpotPriceDateTranslator.Value.Execute(input);
    }
}
