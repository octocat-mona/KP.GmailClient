using System.Text.Json.Serialization;
using KP.GmailClient.Common;

namespace KP.GmailClient.Models
{
    /// <summary>The body of a single MIME message part.</summary>
    public class Attachment
    {
        /// <summary>
        /// When present, contains the ID of an external attachment that can be retrieved in a separate messages.attachments.get request.
        /// When not present, the entire content of the message part body is contained in the data field.
        /// </summary>
        [JsonPropertyName("attachmentId")]
        public string AttachmentId { get; set; }

        /// <summary>Total number of bytes in the body of the message part.</summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// The body data of a MIME message part. May be empty for MIME container types that have no message body or when the body data is sent as a separate attachment.
        /// An attachment ID is present if the body data is contained in a separate attachment.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>Get or set the <see cref="Data"/> Base64 URL decoded.</summary>
        [JsonIgnore]
        public string PlainData
        {
            get { return Data.FromBase64UrlString(); }
            set { Data = value.ToBase64UrlString(); }
        }

        /// <summary>
        /// A string with the values of the properties from this <see cref="Attachment"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("ID: ", AttachmentId, ", Size: ", Size, ", Data size: ", Data.Length);
        }
    }
}