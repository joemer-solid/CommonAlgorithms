namespace Algorithms.Strategy.Search
{
    public class PerformNumericSearch : NumericSearch
    {
        public override int? Execute(NumericSearchParams p)
        {
            int[] numericList = GetSortedListAsArray(p.Items);
            int? searchResultIndex = null;

            int begin = 0;
            int last = numericList.Length - 1;
            int midpoint = 0;

            while (begin <= last)
            {
                // get or refresh the search list mid point
                midpoint = GetSearchListMidPoint(begin, last);

                // if midpoint value is greater than search item
                // increase begin point of search window to midpoint + 1
                if(numericList[midpoint] < p.SearchItem)
                {
                    begin = midpoint + 1;
                }
                else if (numericList[midpoint] > p.SearchItem)
                {
                    // if midpoint is greater decrease the top of the  search window to midpoint - 1
                    last = midpoint - 1;
                }
                else
                {
                    searchResultIndex = midpoint;
                    break;
                }
            }


            return searchResultIndex;
        }
    }
}
