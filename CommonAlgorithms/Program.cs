using System;
using System.Collections.Generic;
using Algorithms.Strategy.Search;
using Algorithms.Strategy.Sorting;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //SortNumbersArray( new int[] { 8, 10, 5, 3, 2, 1, 0, 95,33, 41, 15, 98, 77, 4, 6 });

            StrictSortTextArrayAscending(new string[] { "Xylophone", "Zephyr", "Union", "unicorn", "cutaneous", "Hedgerow", "Penultimate", "Apogee", "Nadir", "Cotillion", "histrionics", "cut", "Apoplectic", "Circadian" });

            StrictSortTextArrayDescending(new string[] { "Union", "unicorn", "Hedgerow", "Penultimate", "Apogee", "Nadir", "Cotillion" });

            //SearchForNumber(new int[] { 8, 10, 5, 3, 2, 1, 0, 95, 33, 41, 15, 98, 77, 4, 6 }, 77);

           // SearchForNumber(new int[] { 8, 10, 5, 3, 2, 1, 0, 95, 33, 41, 15, 98, 77, 4, 6 }, 17);

            Console.ReadLine();

        }

        private static void StrictSortTextArrayDescending(string[] itemsToSort)
        {
            ITextualSort textualSort = new TextualSort();

            IEnumerable<string> results = textualSort.Execute(new TextualSortParams()
            {
                CompareType = StringCompareType.Strict,
                Direction = SortParams<string>.SortDirection.Descending,
                ItemsToSort = itemsToSort
            });

            PrintResultsToScreen<string>("Descending Strict Evaluation Text Sort", results);
        }

        private static void StrictSortTextArrayAscending(string[] itemsToSort)
        {
            ITextualSort textualSort = new TextualSort();           

            IEnumerable<string> results = textualSort.Execute(new TextualSortParams()
            {
                 CompareType = StringCompareType.Strict,
                 Direction = SortParams<string>.SortDirection.Ascending,
                 ItemsToSort = itemsToSort
            });

            PrintResultsToScreen<string>("Ascending Strict Evaluation Text Sort", results);
        }

        private static void SearchForNumber(int[] numbersToSearch, int searchItem)
        {
            INumericSearch numericSearch = new PerformNumericSearch();

            int? searchResult = numericSearch.Execute(new NumericSearchParams()
            {
                Items = numbersToSearch,
                SearchItem = searchItem
            });

            if(searchResult != null)
            {
                Console.WriteLine(string.Format("Found the item: {0} in the numbers to search", searchItem.ToString()));
                PrintResultsToScreen<int>("Numeric Search", numbersToSearch);
            }
            else
            {
                Console.WriteLine(string.Format("Did not find the item: {0} in the numbers to search", searchItem.ToString()));
                PrintResultsToScreen<int>("Numeric Search", numbersToSearch);
            }
        }


        private static void SortNumbersArray(int[] numbersToSort)
        {
            INumericSort numericSort = new NumericSort();
            
            IEnumerable<int> result = numericSort.Execute(numbersToSort);

            PrintResultsToScreen<int>("Ascending Numeric Sort", result);

        }


        private static void PrintResultsToScreen<T>(string typeOfSort , IEnumerable<T> results)
        {
            Console.WriteLine(string.Format("************{0}************{1}", typeOfSort, Environment.NewLine));

            Action<T> writeLine;
            if (typeof(T) != typeof(string))
            {
               writeLine = (x) => { Console.WriteLine(x.ToString()); };
            }
            else
            {
                writeLine = (x) => { Console.WriteLine(x); };
            }               

            foreach (T item in results)
            {
                writeLine(item);
            }

            Console.WriteLine(Environment.NewLine);
        }
    }



}
