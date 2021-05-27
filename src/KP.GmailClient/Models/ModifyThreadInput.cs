using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>The input to modify a thread.</summary>
    public class ModifyThreadInput
    {
        /// <summary>A list of IDs of labels to add to this thread.</summary>
        [JsonPropertyName("addLabelIds")]
        public List<string> AddLabelIds { get; set; } = new();

        /// <summary>A list of IDs of labels to remove from this thread.</summary>
        [JsonPropertyName("removeLabelIds")]
        public List<string> RemoveLabelIds { get; set; } = new();
    }
}
