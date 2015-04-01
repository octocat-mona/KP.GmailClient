using System.Runtime.Serialization;

namespace GmailApi.Models
{
    /// <summary>
    /// Header according to the RFC 2822, RFC 2369, RFC 2919 Internet Message Format standard.
    /// </summary>
    public enum HeaderName
    {
        Unknown,
        Subject,
        Comments,
        References,
        From,
        To,
        Cc,
        Bcc,
        Keywords,
        Trace,
        ResentMsgId,
        Received,
        Sender,
        Date,
        Precedence,

        [EnumMember(Value = "Resent-date")]
        ResentDate,

        [EnumMember(Value = "Optional-field")]
        OptionalField,

        [EnumMember(Value = "Reply-to")]
        ReplyTo,

        [EnumMember(Value = "In-reply-to")]
        InReplyTo,

        [EnumMember(Value = "Message-id")]
        MessageId,

        [EnumMember(Value = "Resent-from")]
        ResentFrom,

        [EnumMember(Value = "Resent-sender")]
        ResentSender,

        [EnumMember(Value = "Resent-to")]
        ResentTo,

        [EnumMember(Value = "Resent-cc")]
        ResentCc,

        [EnumMember(Value = "Resent-bcc")]
        ResentBcc,

        [EnumMember(Value = "Orig-date")]
        OrigDate,

        [EnumMember(Value = "Delivered-to")]
        DeliveredTo,

        [EnumMember(Value = "Return-Path")]
        ReturnPath,

        [EnumMember(Value = "Received-SPF")]
        ReceivedSpf,

        [EnumMember(Value = "Authentication-Results")]
        AuthenticationResults,

        [EnumMember(Value = "Mime-Version")]
        MimeVersion,

        [EnumMember(Value = "Content-Type")]
        ContentType,

        [EnumMember(Value = "Content-Transfer-Encoding")]
        ContentTransferEncoding,

        [EnumMember(Value = "Auto-Submitted")]
        AutoSubmitted,

        [EnumMember(Value = "DKIM-Signature")]
        DkimSignature,

        //RFC 2369:
        [EnumMember(Value = "List-Help")]
        ListHelp,

        [EnumMember(Value = "List-Subscribe")]
        ListSubscribe,

        [EnumMember(Value = "List-Unsubscribe")]
        ListUnsubscribe,
        //End

        //RFC 2919
        [EnumMember(Value = "List-ID")]
        ListId,

        [EnumMember(Value = "List-Archive")]
        ListArchive,

        [EnumMember(Value = "List-Post")]
        ListPost,
    }
}