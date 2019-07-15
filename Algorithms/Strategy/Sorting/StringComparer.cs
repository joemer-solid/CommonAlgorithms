namespace Algorithms.Strategy.Sorting
{
    public abstract class StringComparer : ComparerBase<string>
    {
        #region Constructors

        public StringComparer(StringCompareType compareType)
        {
            CompareType = compareType;
        }

        #endregion

        #region Properties

        protected StringCompareType CompareType { get; private set; }

        #endregion

        #region IComparer implementation

        public override int Compare(string x, string y)
        {
            CompareResult compareResult = CompareResult.Unknown;

            if (CompareType == StringCompareType.Strict)
            {
                if (x[0] == y[0])
                {
                    compareResult = CompareResult.Equal;
                }
                else if (x[0] > y[0])
                {
                    compareResult = CompareResult.GreaterThan;
                }
                else if (x[0] < y[0])
                {
                    compareResult = CompareResult.LessThan;
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
