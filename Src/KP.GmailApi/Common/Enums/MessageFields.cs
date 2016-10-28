using System;

namespace KP.GmailApi.Common.Enums
{
    [Flags]
    internal enum MessageFields
    {
        [StringValue("nextPageToken")]
        NextPageToken = 1,

        [StringValue("resultSizeEstimate")]
        ResultSizeEstimate = 2,

        [StringValue("historyId")]
        HistoryId = 1024,

        [StringValue("id")]
        Id = 2048,

        [StringValue("labelIds")]
        LabelIds = 4096,

        [StringValue("payload")]
        Payload = 8192,

        [StringValue("raw")]
        Raw = 16384,

        [StringValue("sizeEstimate")]
        SizeEstimate = 32768,

        [StringValue("snippet")]
        Snippet = 65536,

        [StringValue("threadId")]
        ThreadId = 131072,

        /// <summary>
        /// All the message fields (excluded: NextPageToken and ResultSizeEstimate).
        /// </summary>
        Messages = HistoryId | Id | LabelIds | Payload | Raw | SizeEstimate | Snippet | ThreadId,

        /// <summary>
        /// All values of Messages plus NextPageToken and ResultSizeEstimate.
        /// </summary>
        All = int.MaxValue
    }
}