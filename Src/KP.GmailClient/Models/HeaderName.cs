using System.Runtime.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// Headers according to the RFC 2822, RFC 2369 and RFC 2919 Internet Message Format standard.
    /// </summary>
    public enum HeaderName
    {
        /// <summary>
        /// Header is not a known type.
        /// </summary>
        Unknown,
        /// <summary>
        /// The name of the RFC 2822 header that stores the subject.
        /// </summary>
        Subject,
        /// <summary>
        /// The name of the RFC 2822 header that stores human-readable comments.
        /// </summary>
        Comments,
        /// <summary>
        ///  The name of the RFC 2822 header that is used to identify the thread to which this message refers.
        /// </summary>
        References,
        /// <summary>
        ///  The name of the RFC 2822 header that stores the mail author(s).
        /// </summary>
        From,
        /// <summary>
        /// The name of the RFC 2822 header that stores the primary mail recipients.
        /// </summary>
        To,
        /// <summary>
        /// The name of the RFC 2822 header that stores the carbon copied mail recipients.
        /// </summary>
        Cc,
        /// <summary>
        /// The name of the RFC 2822 header that stores the blind carbon copied mail recipients.
        /// </summary>
        Bcc,
        /// <summary>
        /// The name of the RFC 2822 header that stores human-readable keywords.
        /// </summary>
        Keywords,
        /// <summary>
        /// The name of the RFC 2822 header that store additional tracing data.
        /// </summary>
        Received,
        /// <summary>
        /// The name of the RFC 2822 header that stores the actual mail transmission agent, if this differs from the author of the message.
        /// </summary>
        Sender,
        /// <summary>
        /// The name of the RFC 2822 header that stores the mail date.
        /// </summary>
        Date,
        /// <summary>
        /// The name of the RFC 2822 header that stores the date the message was resent.
        /// </summary>
        [EnumMember(Value = "Resent-date")]
        ResentDate,
        /// <summary>
        /// The name of the RFC 2822 header that stores the reply-to address.
        /// </summary>
        [EnumMember(Value = "Reply-to")]
        ReplyTo,
        /// <summary>
        /// The name of the RFC 2822 header that stores the message id of the message that to which this email is a reply.
        /// </summary>
        [EnumMember(Value = "In-reply-to")]
        InReplyTo,
        /// <summary>
        /// The name of the RFC 2822 header that stores the message id.
        /// </summary>
        [EnumMember(Value = "Message-id")]
        MessageId,
        /// <summary>
        /// The name of the RFC 2822 header that stores the originator of the resent message.
        /// </summary>
        [EnumMember(Value = "Resent-from")]
        ResentFrom,
        /// <summary>
        /// The name of the RFC 2822 header that stores the transmission agent of the resent message.
        /// </summary>
        [EnumMember(Value = "Resent-sender")]
        ResentSender,
        /// <summary>
        /// The name of the RFC 2822 header that stores the recipients of the resent message.
        /// </summary>
        [EnumMember(Value = "Resent-to")]
        ResentTo,
        /// <summary>
        /// The name of the RFC 2822 header that stores the carbon copied recipients of the resent message.
        /// </summary>
        [EnumMember(Value = "Resent-cc")]
        ResentCc,
        /// <summary>
        /// The name of the RFC 2822 header that stores the blind carbon copied recipients of the resent message.
        /// </summary>
        [EnumMember(Value = "Resent-bcc")]
        ResentBcc,
        /// <summary>
        /// The name of the RFC 2822 header that consists of the field name "Date" followed by a date-time specification.
        /// </summary>
        [EnumMember(Value = "Orig-date")]
        OrigDate,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "Delivered-to")]
        DeliveredTo,
        /// <summary>
        /// The name of the RFC 2822 header that store the tracing data for the return path.
        /// </summary>
        [EnumMember(Value = "Return-Path")]
        ReturnPath,
        /// <summary>
        /// The name of the RFC 2822 header that stores the MIME version.
        /// </summary>
        [EnumMember(Value = "Mime-Version")]
        MimeVersion,
        /// <summary>
        /// The name of the MIME header that stores the content type.
        /// </summary>
        [EnumMember(Value = "Content-Type")]
        ContentType,
        /// <summary>
        /// The name of the RFC 2822 header that stores a single token specifying the type of encoding.
        /// </summary>
        [EnumMember(Value = "Content-Transfer-Encoding")]
        ContentTransferEncoding,
        /// <summary>
        /// The name of the RFC 4871 header DomainKeys Identified Mail (DKIM).
        /// </summary>
        [EnumMember(Value = "DKIM-Signature")]
        DkimSignature,
        /// <summary>
        /// The name of the RFC 2369 header that is the most important of the RFC 2369 headers. It would be acceptable for a list
        /// manager to include only this field, since by definition it SHOULD direct the user to complete instructions for all other commands.
        /// Typically, the URL specified would request the help file, perhaps incorporating an HTML form for list commands, for the list, and
        /// alternatively provide access to an instructive website.
        /// </summary>
        [EnumMember(Value = "List-Help")]
        ListHelp,
        /// <summary>
        /// The name of the RFC 2369 header that describes the command (preferably using mail) to directly subscribe the user (request addition to the list).
        /// </summary>
        [EnumMember(Value = "List-Subscribe")]
        ListSubscribe,
        /// <summary>
        /// The name of the RFC 2369 header that describes the command (preferably using mail) to directly unsubscribe the user (removing them from the list).
        /// </summary>
        [EnumMember(Value = "List-Unsubscribe")]
        ListUnsubscribe,
        /// <summary>
        /// The name of the RFC 2369 header that describes the method for posting to the list.
        /// This is typically the address of the list, but MAY be a moderator, or
        /// potentially some other form of submission. For the special case of a
        /// list that does not allow posting (e.g., an announcements list), the
        /// List-Post field may contain the special value "NO".
        /// </summary>
        [EnumMember(Value = "List-Post")]
        ListPost,
        /// <summary>
        /// The name of the RFC 2369 header that identifies the path to contact a human administrator for the list.
        /// The URL MAY contain the address of a administrator for the list, the mail system administrator, or any
        /// other person who can handle user contact for the list. There is no
        /// need to specify List-Owner if it is the same person as the mail system administrator (postmaster).
        /// </summary>
        [EnumMember(Value = "List-Owner")]
        ListOwner,
        /// <summary>
        /// The name of the RFC 2369 header that describes how to access archives for the list.
        /// </summary>
        [EnumMember(Value = "List-Archive")]
        ListArchive,
        /// <summary>
        /// The name of the RFC 2919 header, see https://www.ietf.org/rfc/rfc2919.txt for more info.
        /// </summary>
        [EnumMember(Value = "List-ID")]
        ListId,

        //TODO: Received-SPF
        // extension to RFC2822 reserving the "Received-SPF:" header.
    }
}