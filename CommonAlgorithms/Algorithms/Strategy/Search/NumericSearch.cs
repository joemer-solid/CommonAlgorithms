using Algorithms.Strategy.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Strategy.Search
{
    public abstract class NumericSearch : SearchBase<int>, INumericSearch
    {
        public abstract int? Execute(NumericSearchParams p);

        protected override IEnumerable<int> GetSortedList(IEnumerable<int> listToSort)
        {
            INumericSort numericSort = new NumericSort();

            return numericSort.Execute(listToSort);
        }

        protected int GetSearchListMidPoint(int begin, int last)
        {
            return (begin + last) / 2;
        }

        protected int[] GetSortedListAsArray(IEnumerable<int> unsortedList)
        {
            IEnumerable<int> sortedList = GetSortedList(unsortedList);
            return sortedList.ToArray<int>();
        }
    }

     

    public sealed class NumericSearchParams : SearchParams<int> { }
}
