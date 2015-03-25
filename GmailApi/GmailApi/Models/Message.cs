using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Message
    {
        public Message()
        {
            Id = string.Empty;
            ThreadId = string.Empty;
            Snippet = string.Empty;
            LabelIds = new List<string>(0);
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The ID of the thread the message belongs to. To add a message or draft to a thread, the following criteria must be met:
        /// <list type="disc">
        /// <item><description>1. The requested threadId must be specified on the Message or Draft.Message you supply with your request.</description></item>
        /// <item><description>2. The References and In-Reply-To headers must be set in compliance with the RFC 2822 standard.</description></item>
        /// <item><description>3. The Subject headers must match.</description></item>
        /// </list>
        /// </summary>
        [JsonProperty("threadId")]
        public string ThreadId { get; set; }

        /// <summary>
        /// The ID of the last history record that modified this message.
        /// </summary>
        [JsonProperty("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>
        /// List of IDs of labels applied to this message.
        /// </summary>
        [JsonProperty("labelIds")]
        public List<string> LabelIds { get; set; }

        /// <summary>
        /// A short part of the message text.
        /// </summary>
        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        /// <summary>
        /// The parsed email structure in the message parts.
        /// </summary>
        [JsonProperty("payload")]
        public Payload Payload { get; set; }

        public string From
        {
            get
            {
                return Payload.Headers
                    .First(h => string.Equals("From", h.Name, StringComparison.OrdinalIgnoreCase))
                    .Value;
            }
        }

        public string To
        {
            get
            {
                return Payload.Headers
                    .First(h => string.Equals("To", h.Name, StringComparison.OrdinalIgnoreCase))
                    .Value;
            }
        }

        /// <summary>
        /// Estimated size in bytes of the message.
        /// </summary>
        [JsonProperty("sizeEstimate")]
        public int SizeEstimate { get; set; }

        /// <summary>
        /// The entire email message in an RFC 2822 formatted and base64url encoded string.
        /// Returned in messages.get and drafts.get responses when the format=RAW parameter is supplied.
        /// </summary>
        [JsonProperty("raw")]
        public byte[] Raw { get; set; }

        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", ~Size: ", SizeEstimate, " bytes, # LabelIds: ", LabelIds.Count, ", Snippet: ", Snippet);
        }
    }
}