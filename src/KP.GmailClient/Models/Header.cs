using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>A header containing a name and a value. </summary>
    public class Header
    {
        /// <summary>The name of the header before the : separator. For example, To.</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>Returns wehter the header is an extension header, which starts with 'X-'.</summary>
        public bool IsExtensionHeader
        {
            get { return Name.StartsWith("X-", StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>The value of the header after the : separator. For example, someuser@example.com.</summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>Get the Internet Message Format (IMF) header or <see cref="HeaderName.Unknown"/> when invalid or unknown.</summary>
        [JsonIgnore]
        public HeaderName ImfHeader
        {
            get
            {
                try
                {
                    return JsonSerializer.Deserialize<HeaderName>($"\"{Name}\"");
                }
                catch (JsonException)
                {
                    return HeaderName.Unknown;
                }
            }
        }

        /// <summary>A string with the values of the properties from this <see cref="Header"/></summary>
        public override string ToString()
        {
            return string.Concat(Name, ": ", Value);
        }
    }
}