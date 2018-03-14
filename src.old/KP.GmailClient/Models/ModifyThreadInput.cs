using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// The input to modify a thread.
    /// </summary>
    public class ModifyThreadInput
    {
        /// <summary>
        /// The input to modify a thread.
        /// </summary>
        public ModifyThreadInput()
        {
            AddLabelIds = new List<string>(0);
            RemoveLabelIds = new List<string>(0);
        }

        /// <summary>
        /// A list of IDs of labels to add to this thread.
        /// </summary>
        [JsonProperty("addLabelIds")]
        public List<string> AddLabelIds { get; set; }

        /// <summary>
        /// A list of IDs of labels to remove from this thread.
        /// </summary>
        [JsonProperty("removeLabelIds")]
        public List<string> RemoveLabelIds { get; set; }
    }
}
