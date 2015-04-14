using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Draft
    {
        /// <summary>
        /// The immutable ID of the draft.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// The message content of the draft.
        /// </summary>
        [JsonProperty("message")]
        public Message Message { get; set; }

        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Message: ", Message);
        }
    }
}
