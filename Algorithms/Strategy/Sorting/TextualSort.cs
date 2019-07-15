using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Algorithms.Strategy.Sorting
{
    public sealed class TextualSort : ITextualSort
    {
        private ComparerBase<string> _sortComparer;



        IEnumerable<string> ISort<IEnumerable<string>, TextualSortParams>.Execute(TextualSortParams p)
        {
            Initialize(p);

            string[] textList = p.ItemsToSort.ToArray<string>();
            string swapBookmark = string.Empty;


            for (int i = 0; i <= textList.Length - 1; i++)
            {
                for (int j = i + 1; j <= textList.Length - 1; j++)
                {
                    if(_sortComparer.SwapValues(textList[i],textList[j]))
                    {
                        swapBookmark = textList[i];
                        textList[i] = textList[j];
                        textList[j] = swapBookmark;
                        swapBookmark = string.Empty;
                    }                  
                }
            }

            return textList;

        }

        private void Initialize(TextualSortParams sortParams)
        {
            if(sortParams.Direction == SortParams<string>.SortDirection.Ascending)
            {
                _sortComparer = new AscendingOrderStringComparer(sortParams.CompareType);
            }
            else if(sortParams.Direction == SortParams<string>.SortDirection.Descending)
            {
                _sortComparer = new DescendingOrderStringComparer(sortParams.CompareType);
            }
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
