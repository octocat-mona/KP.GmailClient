using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    public class Draft
    {
        /// <summary>
        /// The immutable ID of the draft.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The message content of the draft.
        /// </summary>
        [JsonProperty("message")]
        public Message Message { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="Draft"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Message: ", Message);
        }
    }
}
