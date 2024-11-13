using System;


namespace CommonAlgorithms.PMInvestmentWatcherUtilities
{
    public class TimeStampConverter
    {
        private static readonly DateTime UTCEpochBase = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ConvertFromUTCTimeStamp(long utcTimeStamp)
        {
            
            double posixTimeUnixEpochElapsedSeconds = GetPosixTime(UTCEpochBase);

            //return UTCEpochBase.AddSeconds(posixTimeUnixEpochElapsedSeconds);

            return UTCEpochBase.AddSeconds(utcTimeStamp);
        }

        //The Unix epoch (or Unix time or POSIX time or Unix timestamp) is the number of seconds
        //that have elapsed since January 1, 1970 (midnight UTC/GMT), not counting leap seconds.
        private static double GetPosixTime(DateTime utcEpochBase)
            => (DateTime.UtcNow - utcEpochBase).TotalSeconds;


        public static int ConvertToUTCTimeStamp(DateTime dateTime)
            => Convert.ToInt32((dateTime - UTCEpochBase).TotalSeconds);
        
    }
}
