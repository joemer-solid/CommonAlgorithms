using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Sorting
{
    public sealed class DescendingOrderStringComparer : StringComparer
    {
        public DescendingOrderStringComparer(StringCompareType compareType)
            : base(compareType) { }


        public override bool SwapValues(string x, string y)
        {
            CompareResult compareResult = (CompareResult)Compare(x, y);

            return compareResult == CompareResult.LessThan;
        }
    }
}
