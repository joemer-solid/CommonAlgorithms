using System;
using System.Collections.Generic;
using System.Linq;


namespace Algorithms.Strategy.Sorting
{
    public sealed class TextualSort : ITextualSort
    {
      
        IEnumerable<string> ISort<IEnumerable<string>, TextualSortParams>.Execute(TextualSortParams p)
        {
            ComparerBase<string> sortComparer = GetSortComparer(p);
            
            string[] textList = p.ItemsToSort.ToArray<string>();

            for (int i = 0; i <= textList.Length - 1; i++)
            {
                for (int j = i + 1; j <= textList.Length - 1; j++)
                {
                    if (sortComparer.SwapValues(textList[i], textList[j]))
                    {
                        string swapBookmark = textList[i];
                        textList[i] = textList[j];
                        textList[j] = swapBookmark;
                    }
                }  

                if(OkToShortCircuit(sortComparer, textList))
                {
                    Console.WriteLine($"short circuiting on iteration: {i}");
                    break;
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

        private static bool OkToShortCircuit(ComparerBase<string> sortComparer, string[] currentSort)
        {           
            bool[] comparisonResultsPerElement = new bool[currentSort.Length];

            for (int i = 0; i < currentSort.Length - 1; i++)
               comparisonResultsPerElement[i] = sortComparer.SwapValues(currentSort[i], currentSort[i + 1]);  
           
            return comparisonResultsPerElement.All(x => x == false);
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
