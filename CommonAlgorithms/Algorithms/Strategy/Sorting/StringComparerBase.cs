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
                compareResult = CharacterPositionCompare(lowerPosition, upperPosition);              
            }           

            return (int)compareResult;
        }

        #endregion

        protected CompareResult CharacterPositionCompare(string lowerPosition, string upperPosition, int characterPosition = 0)
        {
            CompareResult compareResult = CompareResult.Unknown;
          
            (int lowerPositionCompare, int upperPositionCompare) = 
                StringComparerBase.GetOrCoalesceComparePositions(lowerPosition, upperPosition, characterPosition);

            if (CompareType == StringCompareType.Strict)
            {
                if (lowerPositionCompare == upperPositionCompare)
                {
                    compareResult = CompareResult.Equal;
                    if(characterPosition < lowerPosition.Length - 1)
                    {
                        CharacterPositionCompare(lowerPosition, upperPosition, characterPosition + 1);
                    }
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

            return compareResult;
        }

        private static (int,int) GetOrCoalesceComparePositions(string lowerPosition, string upperPosition, int characterPosition)
        {
            const int lowerPositionCoalescedShortCircuitValue = 0;
            const int upperPositionCoalescedShortCircuitValue = 1;

            // invariant guard in case both words have no more characters left to compare - they are equal
            (int lowerPositionCompare, int higherPositionCompare) positionCompareTuple = 
                (lowerPositionCoalescedShortCircuitValue, upperPositionCoalescedShortCircuitValue);

            bool coalesceCompareShortCircuit = (characterPosition > lowerPosition.Length - 1 && characterPosition > upperPosition.Length - 1);

            if (!coalesceCompareShortCircuit)
            {
                positionCompareTuple =
                (
                    characterPosition < lowerPosition.Length - 1 ? lowerPosition.ToUpperInvariant()[characterPosition] : 0,
                    characterPosition < upperPosition.Length - 1 ? upperPosition.ToUpperInvariant()[characterPosition] : 0
                );
            }

            return positionCompareTuple;
        }

        public enum CompareResult
        {
            Equal = 0,
            LessThan = -1,
            GreaterThan = 1,
            Unknown = 10
        }
    }
}
