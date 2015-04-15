using Newtonsoft.Json;

namespace GmailApi.Models
{
    /// <summary>
    /// The body of a single MIME message part.
    /// </summary>
    public class Attachment
    {
        private string _data;

        public Attachment()
        {
            AttachmentId = string.Empty;
            _data = string.Empty;
        }

        /// <summary>
        /// When present, contains the ID of an external attachment that can be retrieved in a separate messages.attachments.get request.
        /// When not present, the entire content of the message part body is contained in the data field.
        /// </summary>
        [JsonProperty("attachmentId")]
        public string AttachmentId { get; set; }

        /// <summary>
        /// Total number of bytes in the body of the message part.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// The body data of a MIME message part. May be empty for MIME container types that have no message body or when the body data is sent as a separate attachment.
        /// An attachment ID is present if the body data is contained in a separate attachment.
        /// </summary>
        [JsonProperty("data")]
        public string Data
        {
            get { return _data.FromBase64UrlString(); }
            set { _data = value; }
        }

        public override string ToString()
        {
            return string.Concat("ID: ", AttachmentId, ", Size: ", Size, ", Data size: ", Data.Length);
        }
    }
}