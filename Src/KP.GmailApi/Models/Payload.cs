using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    public class Payload : PayloadBase
    {
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
        /// Get the body from a part of a given MIME type
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public string GetBodyData(MimeType mimeType)
        {
            var part = Parts
                .FirstOrDefault(p => p.GetMimeType() == mimeType);

            return part == null || part.Body == null
                ? string.Empty
                : part.Body.Data;
        }

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