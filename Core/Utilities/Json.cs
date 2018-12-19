using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Core.Utilities
{
    public class Json
    {
        public static string Serializ(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T Deserialise<T>(string json)
        {
            var returnJson = JsonConvert.DeserializeObject<T>(json);
            return returnJson;
        }
    }
}
