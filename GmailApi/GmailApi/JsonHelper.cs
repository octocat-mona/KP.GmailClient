using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    internal class JsonHelper
    {
        internal static IList<T> ParseCollection<T>(string json, string propertyName)
        {
            var jObject = JObject.Parse(json);
            var jArray = (JArray)jObject[propertyName];

            return jArray.ToObject<IList<T>>();
        }
    }
}
