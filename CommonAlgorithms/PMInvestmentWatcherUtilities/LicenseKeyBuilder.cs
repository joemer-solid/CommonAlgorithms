using Algorithms.Strategy.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CommonAlgorithms.PMInvestmentWatcherUtilities
{
  //  INSERT INTO PMUser(FirstName, LastName, EmailAddress, Phone, PostalCode, CountryCode)
//VALUES('Joseph', 'Merlino', 'joemer@startmail.com', '412-555-1212','16116','US')
    public struct LicenseKeyBuilderParams
    {
        public LicenseKeyBuilderParams(string firstName, string lastName, string postalCode, string countryCode)
        {
            FirstName = firstName;
            LastName = lastName;
            PostalCode = postalCode;
            CountryCode = countryCode;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PostalCode { get; private set; }
        public string CountryCode { get; private set; }
    }
    public class LicenseKeyBuilder
    {
        public static string BuildLicenseKey(LicenseKeyBuilderParams licenseKeyBuilderParams)
        {
            
            int tokenFirstName = GetTokenFromString(licenseKeyBuilderParams.FirstName);
            int tokenLastName = GetTokenFromString(licenseKeyBuilderParams.LastName);
            int[] tokenGroupPostalCode = GetTokenGroupFromString(licenseKeyBuilderParams.PostalCode,4);

            StringBuilder sb = new StringBuilder();

            for(int i = 1; i <= 4; i++)
            {
                int randomNumber = new Random().Next(i, 50);
                if (i % 2 > 0)
                {
                    if (i > 1) { sb.Append("-"); }
                    sb.Append(ConvertLicenseKeySegment(new int[] {tokenFirstName + randomNumber, tokenLastName + randomNumber}));
                    sb.Append("-");
                }
                else
                {
                    sb.Append(ConvertLicenseKeySegment(tokenGroupPostalCode, randomNumber));
                }
            }

            return sb.ToString();
        }


        private static int GetTokenFromString(string sourceData) 
                => (int)Encoding.ASCII.GetBytes(sourceData)[0];

        private static int[] GetTokenGroupFromString(string sourceData, int elements)
            => Encoding.ASCII.GetBytes(sourceData[..elements]).Select(x => (int)x).ToArray();

        //int intValue = 182;
        //// Convert integer 182 as a hex in a string variable
        //string hexValue = intValue.ToString("X");
        //// Convert the hex string back to the number
        //int intAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        private static string ConvertLicenseKeySegment(int[] segment)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendJoin(string.Empty, segment.Select(x =>  x.ToString("X")));

            return sb.ToString()[..4];
        }

        private static string ConvertLicenseKeySegment(int[] segment, int salt)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendJoin(string.Empty, segment.Select(x => (x + salt).ToString()));

            return sb.ToString()[..8];
               
        }       
    }
}
