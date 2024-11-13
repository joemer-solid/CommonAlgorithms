using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAlgorithms.ArraySegments
{
    public class ArraySegmentsTutorialService
    {

        public static async Task ArraySegmentIntro()
        {
            await TaskBasedAsyncOperationExample();
        }


        public static async Task FileReadAsync()
        {
            AsyncFileReader fileReader = new AsyncFileReader();
            string filePath = "C:\\Users\\jBenzy\\source\\repos\\CommonAlgorithms\\Essential Job Functions.txt";
            await fileReader.ProcessReadAsync(filePath);
        }

        private static async Task TaskBasedAsyncOperationExample()
        {
            List<Task> tasks = new List<Task>();
            const int SEGMENT_SIZE = 10;

            // Create array.
            int[] arr = new int[50];
            for (int ctr = 0; ctr <= arr.GetUpperBound(0); ctr++)
            {
                arr[ctr] = ctr + 1;
            }            

            // Handle array in segments of 10.
            for (int ctr = 1; ctr <= Math.Ceiling(((double)arr.Length) / SEGMENT_SIZE); ctr++)
            {
                int multiplier = ctr;

                int arrayChunkElementsAvailable = (multiplier - 1) * 10 + SEGMENT_SIZE;

                int actualElements = arrayChunkElementsAvailable > arr.Length ?
                                arrayChunkElementsAvailable : SEGMENT_SIZE;

                Console.WriteLine($"SegmentSize: {SEGMENT_SIZE} : AvailableElements: {arrayChunkElementsAvailable}");               

                ArraySegment<int> segment = BuildArraySegment(arr, (ctr - 1) * 10, actualElements);

                tasks.Add(Task.Run(() => {
                    IList<int> list = CreateTaskSegment(segment, multiplier);
                }));
            }

            try
            {
                // Creates a task that will complete when all of the supplied tasks have completed.
                await Task.WhenAll(tasks.ToArray());

                int elementsShown = 0;
                foreach (var value in arr)
                {
                    Console.Write("{0,3} ", value);
                    elementsShown++;
                    if (elementsShown % 10 == 0)
                        Console.WriteLine();
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Errors occurred when working with the array:");
                foreach (var inner in e.InnerExceptions)
                    Console.WriteLine("{0}: {1}", inner.GetType().Name, inner.Message);
            }
        }


        private static IList<int> CreateTaskSegment(IList<int> segment, int multiplier)
             => segment.Select(x =>  x * multiplier).ToList();  
        
        private static ArraySegment<int> BuildArraySegment(int[] baseArray, int offset, int elementCount)
        {
            const char DASH = '-';

            Console.WriteLine($"BuildingArraySegment: Offset: {offset} Element Count: {elementCount}");

            ArraySegment<int> segment = new ArraySegment<int>(baseArray, offset, elementCount);       
            
            Console.WriteLine($"Segment Result: {string.Join(DASH,segment)}");

            return segment;
        }
    }
}
