using System;

namespace GmailApi.DTO
{
    [Flags]
    public enum ThreadFields
    {
        [StringValue("historyId")]
        HistoryId = 1,

        [StringValue("id")]
        Id = 2,

        [StringValue("snippet")]
        Snippet = 4,

        [StringValue("historyId")]
        MessageHistoryId = 1024,

        [StringValue("id")]
        MessageId = 2048,

        [StringValue("labelIds")]
        MessageLabelIds = 4096,

        [StringValue("payload")]
        MessagePayload = 8192,

        [StringValue("raw")]
        MessageRaw = 16384,

        [StringValue("sizeEstimate")]
        MessageSizeEstimate = 32768,

        [StringValue("snippet")]
        MessageSnippet = 65536,

        [StringValue("threadId ")]
        MessageThreadId = 131072,

        /// <summary>
        /// All the message fields (excluded: HistoryId, Id and Snippet)
        /// </summary>
        Messages = MessageHistoryId | MessageId | MessageLabelIds | MessagePayload | MessageRaw | MessageSizeEstimate | MessageSnippet | MessageThreadId,

        /// <summary>
        /// All values of Messages plus HistoryId, Id and Snippet
        /// </summary>
        All = int.MaxValue
    }
}