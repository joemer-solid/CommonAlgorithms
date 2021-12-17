using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Sorting
{
    public interface ISort<T, P>
    {
        T Execute(P p);
    }

    public interface INumericSort : ISort<IEnumerable<int>, IEnumerable<int>> { }

    public interface ITextualSort : ISort<IEnumerable<string>, TextualSortParams> { }
}
