namespace Algorithms.Strategy.Sorting
{
    public sealed class DescendingOrderStringComparer : StringComparerBase
    {
        public DescendingOrderStringComparer(StringCompareType compareType)
            : base(compareType) { }


        public override bool SwapValues(string lowPosition, string highPosition)
        {
            CompareResult compareResult = (CompareResult)Compare(lowPosition, highPosition);

            return compareResult == CompareResult.LessThan;
        }
    }
}
