using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using KP.GmailClient.Common;

namespace KP.GmailClient.Builders
{
    /// <summary>
    /// Message builder for RFC 2822 email messages.
    /// </summary>
    internal class EmailMessageBuilder
    {
        private readonly MailMessage _mailMessage;

        public EmailMessageBuilder()
            : this(null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailMessage"><see cref="MailMessage"/> used as base</param>
        public EmailMessageBuilder(MailMessage mailMessage)
        {
            _mailMessage = mailMessage ?? new MailMessage();
        }

        /// <summary>
        /// Add a list of emailaddresses.
        /// </summary>
        /// <param name="addresses">One or more email addresses separated with a comma character (",").</param>
        /// <returns></returns>
        public EmailMessageBuilder AddTo(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return this;

            _mailMessage.To.Add(addresses);
            return this;
        }

        /// <summary>
        /// Set a list of emailaddresses.
        /// </summary>
        /// <param name="addresses">One or more email addresses separated with a comma character (",").</param>
        /// <returns></returns>
        public EmailMessageBuilder SetTo(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return this;

            _mailMessage.To.Clear();
            _mailMessage.To.Add(addresses);
            return this;
        }

        public EmailMessageBuilder AddReplyTo(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return this;

            _mailMessage.ReplyToList.Add(addresses);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addresses">One or more email addresses separated with a comma character (",").</param>
        /// <returns></returns>
        public EmailMessageBuilder AddCc(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return this;

            _mailMessage.CC.Add(addresses);
            return this;
        }

        public EmailMessageBuilder AddBcc(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return this;

            _mailMessage.Bcc.Add(addresses);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="encoding">An <see cref="Encoding"/> or null to use default encoding</param>
        public EmailMessageBuilder SetSubject(string subject, Encoding encoding = null)
        {
            _mailMessage.Subject = subject;
            if (encoding != null)
            {
                _mailMessage.SubjectEncoding = encoding;
            }

            return this;
        }

        /// <summary>
        /// Set the email message body.
        /// </summary>
        /// <param name="body">The message body</param>
        /// <param name="isBodyHtml">True for HTML message ('text/html'). Defaults to 'text/plain'.</param>
        /// <param name="encoding">An <see cref="Encoding"/> or null to use default encoding</param>
        /// <returns></returns>
        public EmailMessageBuilder SetBody(string body, bool isBodyHtml, Encoding encoding = null)
        {
            _mailMessage.Body = body ?? "";
            _mailMessage.IsBodyHtml = isBodyHtml;
            if (encoding != null)
            {
                _mailMessage.BodyEncoding = encoding;
            }

            return this;
        }

        public string Build()
        {
            using (_mailMessage)
            {
                string mediaType = _mailMessage.IsBodyHtml ? MediaTypeNames.Text.Html : MediaTypeNames.Text.Plain;
                var view = AlternateView.CreateAlternateViewFromString(_mailMessage.Body ?? "", _mailMessage.BodyEncoding, mediaType);
                const string crlf = "\r\n";

                return new StringBuilder()
                    .Append("To: " + _mailMessage.To + crlf, _mailMessage.To.Any())
                    .Append("Reply-To: " + _mailMessage.ReplyToList + crlf, _mailMessage.ReplyToList.Any())
                    .Append("Cc: " + _mailMessage.CC + crlf, _mailMessage.CC.Any())
                    .Append("Bcc: " + _mailMessage.Bcc + crlf, _mailMessage.Bcc.Any())
                    .Append("Subject: " + _mailMessage.Subject + crlf)
                    .Append("Content-Type: " + view.ContentType + crlf)
                    //.Append("Transfer-Encoding: " + view.TransferEncoding + crlf)
                    .Append(crlf)
                    .Append(_mailMessage.Body)
                    .ToString();
            }
        }
    }
}
