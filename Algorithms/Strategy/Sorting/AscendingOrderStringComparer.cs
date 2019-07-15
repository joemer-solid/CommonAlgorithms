using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Sorting
{

    public sealed class AscendingOrderStringComparer : StringComparer
    {
        public AscendingOrderStringComparer(StringCompareType compareType) : base(compareType) { }

        public override bool SwapValues(string x, string y)
        {
            CompareResult compareResult = (CompareResult) Compare(x, y);

            return compareResult == CompareResult.GreaterThan;
        }
    }
}
