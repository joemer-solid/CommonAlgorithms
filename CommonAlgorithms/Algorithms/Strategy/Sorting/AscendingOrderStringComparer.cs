namespace Algorithms.Strategy.Sorting
{

    public sealed class AscendingOrderStringComparer : StringComparerBase
    {
        public AscendingOrderStringComparer(StringCompareType compareType) : base(compareType) { }      

        public override bool SwapValues(string lowPosition, string highPosition)
        {
            CompareResult compareResult = (CompareResult) Compare(lowPosition, highPosition);

            // values will be swapped only if the lowPosition value is greater than the highPosition value
            return compareResult == CompareResult.GreaterThan;
        }
    }
}
