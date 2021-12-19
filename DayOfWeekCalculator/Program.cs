using System;

namespace DayOfWeekCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Month Day Year.. then [Enter] to begin...");
            Console.WriteLine();

            string input = Console.ReadLine();
            string[] inputParams = input.Split(' ');

            int month = Convert.ToInt32(inputParams[0]);
            int day = Convert.ToInt32(inputParams[1]);
            int year = Convert.ToInt32(inputParams[2]);

            if(month > 12) { throw new ArgumentOutOfRangeException(nameof(month)); }
            if(year < DateInformationList.MinYear || year > DateInformationList.MinYear + DateInformationList.YearSpan) 
            { throw new ArgumentOutOfRangeException(nameof(year));  }

            //expected day, month, year
            Console.WriteLine(
                DateInformationList.GetDayOfWeekForInputDate(day,
                month,
                year));

            Console.Read();
        }
    }
}
