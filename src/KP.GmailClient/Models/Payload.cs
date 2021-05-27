using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>The parsed email structure in the message parts.</summary>
    public class Payload : PayloadBase
    {
        /// <summary>
        /// The child MIME message parts of this part. This only applies to container MIME message parts, for example multipart/*.
        /// For non- container MIME message part types, such as text/plain, this field is empty. For more information, see RFC 1521.
        /// </summary>
        [JsonPropertyName("parts")]
        public List<PayloadBase> Parts { get; set; } = new();

        /// <summary>A string with the values of the properties from this <see cref="Payload"/></summary>
        public override string ToString()
        {
            return string.Concat(base.ToString(), ", # Parts: ", Parts.Count);
        }
    }
}