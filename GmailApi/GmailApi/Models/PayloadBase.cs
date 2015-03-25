using System.Collections.Generic;
using ConsoleApplication1.Models;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class PayloadBase
    {
        public PayloadBase()
        {
            PartId = string.Empty;
            MimeType = string.Empty;
            Filename = string.Empty;
            Headers = new List<Header>(0);
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

        public MimeType GetMimeType()
        {
            switch (MimeType)
            {
                case "text/plain":
                    return Models.MimeType.TextPlain;
                case "text/html":
                    return Models.MimeType.TextHtml;
                default:
                    return Models.MimeType.Unknown;
            }
        }

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

        public override string ToString()
        {
            string hasBody = Body == null ? bool.FalseString : bool.TrueString;
            return string.Concat("PartID: ", PartId, ", MimeType: ", MimeType, ", Filename: ", Filename, ", has body: ", hasBody, ", # Headers: ", Headers.Count);
        }
    }
}