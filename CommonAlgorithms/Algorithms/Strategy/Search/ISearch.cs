using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Search
{
    public interface ISearch<T, P>
    {
        T Execute(P p);
    }

    public abstract class SearchParams<T>
    {
        public T SearchItem { get; set; }

        public IEnumerable<T> Items { get; set; }
    }

    public interface INumericSearch : ISearch<int?, NumericSearchParams> { }
}
