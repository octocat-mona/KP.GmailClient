using Newtonsoft.Json;

namespace GmailApi.Models
{
    /// <summary>
    /// The body of a single MIME message part.
    /// </summary>
    public class Attachment
    {
        public Attachment()
        {
            AttachmentId = string.Empty;
            DataBase64Url = string.Empty;
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
        /// <see cref="Data"/>
        /// </summary>
        [JsonProperty("data")]
        internal string DataBase64Url { get; set; }

        /// <summary>
        /// The body data of a MIME message part. May be empty for MIME container types that have no message body or when the body data is sent as a separate attachment.
        /// An attachment ID is present if the body data is contained in a separate attachment.
        /// </summary>
        public string Data
        {
            get { return DataBase64Url.DecodeBase64UrlString(); }
        }

        public override string ToString()
        {
            return string.Concat("ID: ", AttachmentId, ", Size: ", Size, ", Data size: ", Data.Length);
        }
    }
}