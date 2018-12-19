using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Utilities;

namespace Core.Extensions
{
    public  static class StringExtensions
    {
        public static DateTime? ToDateTime(this string str)
        {
            if (DateTime.TryParse(str, out var date))
            {
                return date;
            }
            return null;
        }

        public static string GetNumbers(this string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        public static int ToInt(this string input)
        {
            return Convert.ToInt32(input);
        }

        public static T ToObject<T>(this string input)
        {
            return Json.Deserialise<T>(input);
        }
    }
}
