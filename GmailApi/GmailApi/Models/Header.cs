using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class Header
    {
        public Header()
        {
            //Name = string.Empty;
            Value = string.Empty;
        }

        /// <summary>
        /// The name of the header before the : separator. For example, To.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the header after the : separator. For example, someuser@example.com.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Concat(Name, ": ", Value);
        }
    }
}