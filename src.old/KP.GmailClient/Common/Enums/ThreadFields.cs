using System;

namespace KP.GmailClient.Common.Enums
{
    [Flags]
    internal enum ThreadFields
    {
        [StringValue("nextPageToken")]
        NextPageToken = 1,

        [StringValue("resultSizeEstimate")]
        ResultSizeEstimate = 2,

        [StringValue("historyId")]
        HistoryId = 4,

        [StringValue("id")]
        Id = 8,

        [StringValue("snippet")]
        Snippet = 16,

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
        /// All values of Messages plus NextPageToken, ResultSizeEstimate, HistoryId, Id and Snippet
        /// </summary>
        All = int.MaxValue
    }
}