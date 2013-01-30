using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProspectRealEstate.Web.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Format and return price range as string
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="message">The message to return if both prices are not provided</param>
        public static string ParsePriceRange(long? min, long? max, string message = "Price on application")
        {
            if (min.HasValue && !max.HasValue)
            {
                return min.Value.ToString("C", CultureInfo.CurrentCulture);
            }

            if (max.HasValue && !min.HasValue)
            {
                return max.Value.ToString("C", CultureInfo.CurrentCulture);
            }

            if (min.HasValue && max.HasValue)
            {
                return String.Join(" - ",
                    min.Value.ToString("C", CultureInfo.CurrentCulture),
                    max.Value.ToString("C", CultureInfo.CurrentCulture));
            }

            return message;
        }

        public static string GetCultureInvarientRelativeUrl(string url)
        {
            if (url.ToLower() == "/en-au" || url.ToLower() == "/zh-cn")
            {
                return "";
            }

            if (url.Length > 1 && url.StartsWith("/")
                && !url.StartsWith("//") && !url.StartsWith("/\\")
                && Regex.IsMatch(url, @"^/[A-Za-z][A-Za-z]-[A-Za-z][A-Za-z]/"))
            {
                return url.Substring("/en-AU/".Length);
            }
            return url;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        // Verify a hash against a string. 
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}