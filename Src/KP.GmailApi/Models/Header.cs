using System;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// A header containing a name and a value. 
    /// </summary>
    public class Header
    {
        /// <summary>
        /// A header containing a name and a value. 
        /// </summary>
        public Header()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// The name of the header before the : separator. For example, To.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Returns wehter the header is an extension header, which starts with 'X-'.
        /// </summary>
        public bool IsExtensionHeader
        {
            get { return Name.StartsWith("X-", StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// The value of the header after the : separator. For example, someuser@example.com.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Get the Internet Message Format (IMF) header if valid.
        /// </summary>
        public HeaderName ImfHeader
        {
            get
            {
                return JsonConvert.DeserializeObject<HeaderName>(string.Concat("\"", Name, "\""));
            }
        }

        /// <summary>
        /// A string with the values of the properties from this <see cref="Header"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat(Name, ": ", Value);
        }
    }
}