using System.Collections.Generic;
using KP.GmailApi.Common;
using KP.GmailApi.Models.Extensions;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// An email message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// An email message.
        /// </summary>
        public Message()
        {
            ThreadId = string.Empty;
            Snippet = string.Empty;
            Raw = string.Empty;
            LabelIds = new List<string>(0);
            Payload = new Payload();
        }

        /// <summary>
        /// The immutable ID of the message.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// The ID of the thread the message belongs to. To add a message or draft to a thread, the following criteria must be met:
        /// <list type="disc">
        /// <item><description>1. The requested threadId must be specified on the Message or Draft.Message you supply with your request.</description></item>
        /// <item><description>2. The References and In-Reply-To headers must be set in compliance with the RFC 2822 standard.</description></item>
        /// <item><description>3. The Subject headers must match.</description></item>
        /// </list>
        /// </summary>
        [JsonProperty("threadId")]
        public string ThreadId { get; internal set; }

        /// <summary>
        /// The ID of the last history record that modified this message.
        /// </summary>
        [JsonProperty("historyId")]
        public ulong HistoryId { get; internal set; }

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
        public Payload Payload { get; internal set; }

        /// <summary>
        /// Estimated size in bytes of the message.
        /// </summary>
        [JsonProperty("sizeEstimate")]
        public int SizeEstimate { get; internal set; }

        /// <summary>
        /// The entire email message in an RFC 2822 formatted and base64url encoded string.
        /// Returned in <see cref="Services.MessageService.GetAsync"/> and <see cref="Services.DraftService.GetAsync"/> responses when the format=RAW parameter is supplied.
        /// </summary>
        [JsonProperty("raw")]
        public string Raw { get; private set; }

        /// <summary>
        /// Get/set the entire email message decoded from the <see cref="Raw"/> RFC 2822 formatted and base64url encoded string.
        /// Returned in messages.get and drafts.get responses when the format=RAW parameter is supplied.
        /// </summary>
        [JsonIgnore]
        public string PlainRaw
        {
            get { return Raw.FromBase64UrlString(); }
            set { Raw = value.ToBase64UrlString(); }
        }

        /// <summary>
        /// Get the 'Subject' header value.
        /// </summary>
        [JsonIgnore]
        public string Subject => Payload.GetHeaderValue(HeaderName.Subject);

        /// <summary>
        /// Get the 'From' header value.
        /// </summary>
        [JsonIgnore]
        public string From => Payload.GetHeaderValue(HeaderName.From);

        /// <summary>
        /// Get the 'To' header value.
        /// </summary>
        [JsonIgnore]
        public string To => Payload.GetHeaderValue(HeaderName.To);

        /// <summary>
        /// Get the body of the message in HTML
        /// </summary>
        [JsonIgnore]
        public string Html => Payload.GetBodyData(MimeType.TextHtml);

        /// <summary>
        /// Get the body of the message as plain text
        /// </summary>
        [JsonIgnore]
        public string PlainText => Payload.GetBodyData(MimeType.TextPlain);

        /// <summary>
        /// A string with the values of the properties from this <see cref="Message"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", ~Size: ", SizeEstimate, " bytes, # LabelIds: ", LabelIds.Count, ", Snippet: ", Snippet);
        }
    }
}