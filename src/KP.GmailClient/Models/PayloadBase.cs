using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// The parsed email structure in the message parts.
    /// </summary>
    public class PayloadBase
    {
        /// <summary>
        /// The parsed email structure in the message parts.
        /// </summary>
        public PayloadBase()
        {
            PartId = string.Empty;
            MimeType = string.Empty;
            Filename = string.Empty;
            Headers = new List<Header>(0);
            Body = new Attachment();
        }

        /// <summary>
        /// The immutable ID of the message part.
        /// </summary>
        [JsonProperty("partId")]
        public string PartId { get; set; }

        /// <summary>
        /// The MIME type of the message part.
        /// </summary>
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// The filename of the attachment. Only present if this message part represents an attachment.
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// List of headers on this message part. For the top-level message part, representing the entire message payload,
        /// it will contain the standard RFC 2822 email headers such as To, From, and Subject.
        /// </summary>
        [JsonProperty("headers")]
        public List<Header> Headers { get; set; }

        /// <summary>
        /// The message part body for this part, which may be empty for container MIME message parts.
        /// </summary>
        [JsonProperty("body")]
        public Attachment Body { get; set; }

        /// <summary>
        /// Get the extension ('X-') headers
        /// </summary>
        [JsonIgnore]
        public IEnumerable<Header> XHeaders
        {
            get { return Headers.Where(h => h.IsExtensionHeader); }
        }

        /// <summary>
        /// A string with the values of the properties from this <see cref="PayloadBase"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("PartID: ", PartId, ", MimeType: ", MimeType, ", Filename: ", Filename, ", # Headers: ", Headers.Count);
        }
    }
}