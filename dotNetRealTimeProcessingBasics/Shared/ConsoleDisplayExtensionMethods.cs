using System;

namespace dotNetRealTimeProcessingBasics.Shared
{
    public static class ConsoleDisplayExtensionMethods
    {
        public static void DisplayToConsole(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"{Environment.NewLine}{input}");
            }
        }

        public static void DisplayToConsolePosition(this string input, int cursorLeftPosition = 0, int cursorTopPosition = -1) 
        {           
            Console.SetCursorPosition(cursorLeftPosition, cursorTopPosition); 
            Console.WriteLine(input);
        }

        public static void DisplayToConsole(this byte[] input, string outputId)
        {
            IList<string> byteElements = [];
            const int maxLineDisplayElements = 50;

            Console.WriteLine($"Output For: {outputId}");

            if (input.Length > 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (byteElements.Count % maxLineDisplayElements == 0)
                    {
                        Console.WriteLine();
                        foreach (var item in byteElements.ToList())
                        {
                            Console.Write($"{item}:");
                        }
                        byteElements.Clear();
                    }

                    byteElements.Add(input[i].ToString());
                }
            }
        }
    }
}
