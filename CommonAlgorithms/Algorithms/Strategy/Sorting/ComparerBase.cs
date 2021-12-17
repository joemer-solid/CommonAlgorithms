using System.Collections.Generic;

namespace Algorithms.Strategy.Sorting
{
    public abstract class ComparerBase<T> : IComparer<T>
    {
        public abstract int Compare(T x, T y);

        public abstract bool SwapValues(T x, T y);
    }
}
