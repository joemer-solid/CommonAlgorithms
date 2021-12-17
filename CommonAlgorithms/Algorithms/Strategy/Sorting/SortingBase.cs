using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Sorting
{
    public abstract class SortingBase<P> : AlgorithmBase, IComparable<P>
    {
        public abstract int CompareTo(P other);

    }


    public abstract class SortParams<T>
    {
        internal SortDirection Direction { get; set; }

        internal IEnumerable<T> ItemsToSort { get; set; }

        internal enum SortDirection
        {
            Ascending,
            Descending
        }
    }
}
