using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// The parsed email structure in the message parts.
    /// </summary>
    public class Payload : PayloadBase
    {
        /// <summary>
        /// The parsed email structure in the message parts.
        /// </summary>
        public Payload()
        {
            Parts = new List<PayloadBase>(0);
        }

        /// <summary>
        /// The child MIME message parts of this part. This only applies to container MIME message parts, for example multipart/*.
        /// For non- container MIME message part types, such as text/plain, this field is empty. For more information, see RFC 1521.
        /// </summary>
        [JsonProperty("parts")]
        public List<PayloadBase> Parts { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="Payload"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat(base.ToString(), ", # Parts: ", Parts.Count);
        }
    }
}