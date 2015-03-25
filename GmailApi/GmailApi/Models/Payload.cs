using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApplication1
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

        public override string ToString()
        {
            return string.Concat(base.ToString(), ", # Parts: ", Parts.Count);
        }
    }
}