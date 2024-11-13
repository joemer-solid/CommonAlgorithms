using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Strategy.Search;
using Algorithms.Strategy.Sorting;
using CommonAlgorithms.Algorithms.Strategy.PredictiveAnalysis;
using CommonAlgorithms.ArraySegments;
using CommonAlgorithms.PMInvestmentWatcherUtilities;
using CommonAlgorithms.PMInvestmentWatcherUtilities.Extensions;

namespace Algorithms
{
    class Program
    {
        private static int _permutationsExpected;
        private static int _permutationItemNumber;

        public static async Task Main(string[] args)
        {
            //SortNumbersArray( new int[] { 8, 10, 5, 3, 2, 1, 0, 95,33, 41, 15, 98, 77, 4, 6 });

            //StrictSortTextArrayAscending(new string[] { "Xylophone", "Zephyr", "Union", "unicorn", "cutaneous", "Hedgerow", "Penultimate", "Apogee", "Nadir", "Cotillion", "histrionics", "cut", "Apoplectic", "Circadian" });

            // StrictSortTextArrayDescending(new string[] { "Union", "unicorn", "Hedgerow", "Penultimate", "Apogee", "Nadir", "Cotillion" });

            //SearchForNumber(new int[] { 8, 10, 5, 3, 2, 1, 0, 95, 33, 41, 15, 98, 77, 4, 6 }, 77);

            // SearchForNumber(new int[] { 8, 10, 5, 3, 2, 1, 0, 95, 33, 41, 15, 98, 77, 4, 6 }, 17);

            //HashSet<int[]> result = AllPermutationsOf(new int[] {1,2,3,4,5});

            //Console.WriteLine($"Permutation Total: {result.Count}");

            //foreach(int[] permutation in result)
            //{
            //    Console.WriteLine(String.Join(',', permutation));
            //}

            //AbraAlgTest();
            //  PermutationsBuilder("ABCD");

            //char[] testSequence = new string("ABCD").ToArray();
            //char[] testSequence = new string("AMON").ToArray();
            //_permutationsExpected = CalculateExpectedPermutations(testSequence);
            //Console.WriteLine($"Permutations Excpected for: {string.Join(string.Empty, testSequence)} - {_permutationsExpected}");
            //PermutationsBuilder(testSequence,testSequence.Length);

            // for(int i = 0; i < 100; i++) { BuildPMInvestmentWatcherLicenseKey(); }

            //ConvertUTCTimeStampToDateTime();
            //ConvertDateTimetoUTCTimeStamp();

            //await ArraySegmentsTutorialServiceFirstExample();

            // await PerformAsyncFileRead();

            TestToPreviousSpotPriceBusinessDateTime();

           // Console.WriteLine($"Calling HandleExceptionReturnValue: {HandleExceptionReturnValue()}");

            Console.ReadLine();

        }

        private static int HandleExceptionReturnValue()
        {
            int defaultRetVal = 0;

            try
            {
                ThrowAnException();
            }
            catch(ApplicationException e)
            {
                Console.WriteLine($"received this exception but just logging it: {e.Message}");
            }
            catch(Exception) { throw; }

            return defaultRetVal;
        }

        private static int ThrowAnException()
        {
            int retVal = 0;

            throw new ApplicationException("oh no joe");

            return retVal;
        }

        private static void PreviousSpotPriceBusinessDateTimeToConsole(DateTime originalDate, DateTime translatedDate) 
        {
            Console.WriteLine($"ToPreviousSpotPriceBusinessDateTime result - input: {originalDate}, output:{translatedDate}");
        }

        private static void TestToPreviousSpotPriceBusinessDateTime()
        {
            //DateTime sundayAt2AM = new DateTime(2024, 10, 13, 2, 15, 45);

            //DateTime sundayAt2AMToPrevSpotPrice = sundayAt2AM.ToPreviousSpotPriceBusinessDay();

            //PreviousSpotPriceBusinessDateTimeToConsole(sundayAt2AM, sundayAt2AMToPrevSpotPrice);

            DateTime tuesdayAt4PM = DateTime.Now;

            DateTime tuesdayAt4PMToPrevSpotPrice = tuesdayAt4PM.ToPreviousSpotPriceBusinessDay();

            PreviousSpotPriceBusinessDateTimeToConsole(tuesdayAt4PM, tuesdayAt4PMToPrevSpotPrice);
        }

        private static async Task PerformAsyncFileRead()
        {
            await ArraySegmentsTutorialService.FileReadAsync();
        }

        private static async Task ArraySegmentsTutorialServiceFirstExample()
        {
            await ArraySegmentsTutorialService.ArraySegmentIntro();
        }

        private static void ConvertDateTimetoUTCTimeStamp()
        {
            double utcTimeStamp = TimeStampConverter.ConvertToUTCTimeStamp(DateTime.Now);
            Console.WriteLine($"Converting: {DateTime.Now} to UTC: {utcTimeStamp}");
        }

        private static void ConvertUTCTimeStampToDateTime()
        {
            ////1725235199
            long utcTimeStamp = 1725531472;
            DateTime result = TimeStampConverter.ConvertFromUTCTimeStamp(utcTimeStamp);
            Console.WriteLine($"Converted UTC Time Stamp: {utcTimeStamp} to {result}");

        }

        private static void BuildPMInvestmentWatcherLicenseKey()
        {
            string licenseKey = LicenseKeyBuilder.BuildLicenseKey(new LicenseKeyBuilderParams("Joseph", "Merlino", "16116", "PA"));
            Console.WriteLine($"LicenseKeyResult: {licenseKey}");
        }


        private static int CalculateExpectedPermutations(char[] testSequence) 
        { 
            IList<int> possiblePerDimension = new List<int>();
            for(int i = testSequence.Length; i > 0; i--) 
            {
                possiblePerDimension.Add(i);
            }          

            return possiblePerDimension.ToList()
                .Aggregate(1, (currentResult, nextValue) => currentResult * nextValue);
        }  

        private static void AbraAlgTest()
        {
            AbraAlg abraAlg = new AbraAlg();
            abraAlg.Compute();

        }

        /// <summary>
        /// https://www.geeksforgeeks.org/heaps-algorithm-for-generating-permutations/
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputLengthBuffer"></param>
        /// <param name="actualInputLength"></param>
        private static void PermutationsBuilder(char[] input, int inputLengthBuffer)
        {
            if (inputLengthBuffer == 1)
            {
                _permutationItemNumber += 1;
                Console.WriteLine($"Permutation #: {_permutationItemNumber} result: {string.Join(string.Empty, input)}");
            }

            for (int i = 0; i < inputLengthBuffer; i++)
            {
                PermutationsBuilder(input, inputLengthBuffer - 1);

                // if size is odd, swap 0th i.e (first) and
                // (size-1)th i.e (last) element
                if (inputLengthBuffer % 2 == 1)
                {
                    (input[0], input[inputLengthBuffer - 1])
                        = (input[inputLengthBuffer - 1], input[0]);
                }
                // If size is even, swap ith and
                // (size-1)th i.e (last) element
                else
                {
                    (input[i], input[inputLengthBuffer - 1])
                        = (input[inputLengthBuffer - 1], input[i]);
                }
            }
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
