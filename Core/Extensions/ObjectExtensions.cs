using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        

        public static string ToJson(this object input)
        {
            var json = Utilities.Json.Serializ(input);
            return json;
        }

        
    }
}
