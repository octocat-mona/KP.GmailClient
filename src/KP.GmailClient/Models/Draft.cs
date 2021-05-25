using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>A draft email in the user's mailbox.</summary>
    public class Draft
    {
        /// <summary>The immutable ID of the draft.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>The message content of the draft.</summary>
        [JsonPropertyName("message")]
        public Message Message { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="Draft"/></summary>
        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Message: ", Message);
        }
    }
}
