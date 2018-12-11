using System;
using System.Collections.Generic;
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

        
    }
}
