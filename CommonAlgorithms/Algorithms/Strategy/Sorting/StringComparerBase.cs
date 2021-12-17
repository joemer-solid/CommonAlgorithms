namespace Algorithms.Strategy.Sorting
{
    public abstract class StringComparerBase : ComparerBase<string>
    {
        #region Constructors

        public StringComparerBase(StringCompareType compareType)
        {
            CompareType = compareType;
        }

        #endregion

        #region Properties

        protected StringCompareType CompareType { get; private set; }

        #endregion

        #region IComparer implementation

        public override int Compare(string lowerPosition, string upperPosition)
        {
            CompareResult compareResult = CompareResult.Unknown;            

            if(!string.IsNullOrWhiteSpace(lowerPosition) && !string.IsNullOrWhiteSpace(upperPosition))
            {
                int lowerPositionCompare = lowerPosition.ToUpperInvariant()[0];
                int upperPositionCompare = upperPosition.ToUpperInvariant()[0];

                if (CompareType == StringCompareType.Strict)
                {
                    if (lowerPositionCompare == upperPositionCompare)
                    {
                        compareResult = CompareResult.Equal;
                    }
                    else if (lowerPositionCompare > upperPositionCompare)
                    {
                        compareResult = CompareResult.GreaterThan;
                    }
                    else if (lowerPositionCompare < upperPositionCompare)
                    {
                        compareResult = CompareResult.LessThan;
                    }
                }
            }           

            return (int)compareResult;
        }

        #endregion

        public enum CompareResult
        {
            Equal = 0,
            LessThan = -1,
            GreaterThan = 1,
            Unknown = 10
        }
    }
}
