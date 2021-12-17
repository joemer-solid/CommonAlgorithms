using System.Collections.Generic;
using System.Linq;


namespace Algorithms.Strategy.Sorting
{
    public sealed class TextualSort : ITextualSort
    {
        private ComparerBase<string> _sortComparer;

        IEnumerable<string> ISort<IEnumerable<string>, TextualSortParams>.Execute(TextualSortParams p)
        {
            _sortComparer = GetSortComparer(p);

            string[] textList = p.ItemsToSort.ToArray<string>();

            for (int i = 0; i <= textList.Length - 1; i++)
            {
                for (int j = i + 1; j <= textList.Length - 1; j++)
                {
                    string swapBookmark = string.Empty;

                    if (_sortComparer.SwapValues(textList[i], textList[j]))
                    {
                        swapBookmark = textList[i];
                        textList[i] = textList[j];
                        textList[j] = swapBookmark;
                    }
                }
            }

            return textList;

        }

        private ComparerBase<string> GetSortComparer(TextualSortParams sortParams)
        {
            if (sortParams.Direction == SortParams<string>.SortDirection.Ascending)
            {
                return new AscendingOrderStringComparer(sortParams.CompareType);
            }
            else if (sortParams.Direction == SortParams<string>.SortDirection.Descending)
            {
               return new DescendingOrderStringComparer(sortParams.CompareType);
            }

            return null;
        }
            
    }

    public sealed class TextualSortParams : SortParams<string>
    {
        internal StringCompareType CompareType { get; set; }
    }

    public enum StringCompareType
    {
        Strict,
        Fuzzy
    }
}
