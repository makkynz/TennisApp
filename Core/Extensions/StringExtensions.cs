using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


    }
}
