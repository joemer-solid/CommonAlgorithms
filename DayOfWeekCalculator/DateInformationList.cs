using System;
using System.Collections.Generic;
using System.Linq;

namespace DayOfWeekCalculator
{
    internal class DateInformationList
    {
        public static readonly int MinYear = 2000;
        public static readonly int YearSpan = 2024 - MinYear;
        private static readonly int FirstWeekDayInMinYear = 6;


        static DateInformationList()
        {
            Items = Initialize();
        }

        internal static IList<DateInformation> Items { get; private set; }


        internal static string GetDayOfWeekForInputDate(int day, int month, int year)
        {
            const int MONTH = 0;
            const int DAYOFMONTH = 1;
            DateInformation dateInformationForYear =
                (DateInformation)(Items?.FirstOrDefault(dateInformation => dateInformation.Year == year));                

            int dayOfYear = 0;

            for (int i = 1; i <= dateInformationForYear.MonthDaysInMonth.GetLength(0); i++)
            {
                if (month == dateInformationForYear.MonthDaysInMonth[i - 1, MONTH])
                {                  
                    dayOfYear += day;
                    break;
                }
                else
                {                   
                    dayOfYear += dateInformationForYear.MonthDaysInMonth[i - 1, DAYOFMONTH];
                }
            }

            return dateInformationForYear.GetInputDateDayOfWeek(day, month, year, dayOfYear);
        }

        private static IList<DateInformation> Initialize()
        {      

            IList<DateInformation> items = new List<DateInformation>();

            int calcDowNumberBuffer = FirstWeekDayInMinYear - 1;

            for (int i = 0; i <= YearSpan; i++)
            {
                int year = i + MinYear;
                int daysInYear = GetDaysInYear(year);

                int[,] monthsDaysInMonth = new int[12, 2];

                for (int j = 1; j <= 12; j++)
                {
                    monthsDaysInMonth[j - 1, 0] = j;
                    monthsDaysInMonth[j - 1, 1] = GetDaysInMonth(j, year % 4 == 0);
                }

                int[,] dayOfYearWeekDay = new int[daysInYear, 2];

                // Iterating through days of current year
                for (int doy = 1; doy <= dayOfYearWeekDay.GetLength(0); doy++)
                {
                    dayOfYearWeekDay[doy - 1, 0] = doy;

                    calcDowNumberBuffer = calcDowNumberBuffer += 1;
                    calcDowNumberBuffer = calcDowNumberBuffer == 8 ?
                        calcDowNumberBuffer = 1 : calcDowNumberBuffer;

                    dayOfYearWeekDay[doy - 1, 1] = calcDowNumberBuffer;
                }

                items.Add(new DateInformation(year, daysInYear, monthsDaysInMonth, dayOfYearWeekDay));

            }

            return items;
        }

        private static int GetDaysInYear(int year)
           => year % 4 == 0 ? 366 : 365;

        private static int GetDaysInMonth(int month, bool leapYear)
        {            
            int[] ThirtyOneDays = new int[] { 1, 3, 5, 7, 8, 10, 12 };
            int[] ThirtyDays = new int[] { 4, 6, 9, 11 };
            int[] PossibleLeapYear = new int[] { 2 };

            int result = 0;

            if (ThirtyOneDays.Any(x => x == month))
            {
                result = 31;
            }
            else if (ThirtyDays.Any(x => x == month))
            {
                result = 30;
            }
            else if(PossibleLeapYear.Any(x => x == month))
            {
                result = leapYear ? 29 : 28;
            }

            return result;         
        }

        internal struct DateInformation
        {
            public DateInformation(int year, int daysInYear, int[,] monthDaysInMonth, int[,] dayOfYearWeekday)
            {
                MonthDaysInMonth = monthDaysInMonth;
                Year = year;
                DaysInYear = daysInYear;
                DayOfYearWeekDay = dayOfYearWeekday;
            }

            public int Year;
            public int DaysInYear;
            public int[,] MonthDaysInMonth;

            public int[,] DayOfYearWeekDay;

            public string GetInputDateDayOfWeek(int day, int month, int year, int dayInYear) => 
                string.Format("Calculated Day Of Week Name For Input {0}: {1}",
                $"{month}/{day}/{year}",
                GetDayOfWeekName(DayOfYearWeekDay[dayInYear - 1, 1]));

            private string GetDayOfWeekName(int dayOfWeekNumber)
            {
                string[] daysOfWeek = new string[]
                    {"Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday",
                    "Sunday"};

                return daysOfWeek[dayOfWeekNumber - 1];
            }

            public void Display()
            {
                Console.WriteLine(
                    $"Year: {Year}, DaysInYear: {DaysInYear}"
                );

                for (int i = 0; i < MonthDaysInMonth.GetLength(0); i++)
                {
                    Console.WriteLine(
                       $"Month: {MonthDaysInMonth[i, 0]}, DaysInMonth: {MonthDaysInMonth[i, 1]}"
                    );
                }
            }
        }
    }
}
