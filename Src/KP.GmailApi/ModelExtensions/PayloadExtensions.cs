using System.Linq;
using KP.GmailApi.Models;

namespace KP.GmailApi.ModelExtensions
{
    /// <summary>
    /// Extensions for <see cref="PayloadBase"/> and <see cref="Payload"/>.
    /// </summary>
    public static class PayloadExtensions
    {
        /// <summary>
        /// Get the MIME type of this payload.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>The MIME type or Unknown</returns>
        public static MimeType GetMimeType(this PayloadBase payload)
        {
            switch (payload.MimeType)
            {
                case "text/plain":
                    return MimeType.TextPlain;
                case "text/html":
                    return MimeType.TextHtml;
                default:
                    return MimeType.Unknown;
            }
        }

        /// <summary>
        /// Get the <see cref="Header"/> with a <see cref="HeaderName"/>
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="headerName"></param>
        /// <returns>A <see cref="Header"/> or null</returns>
        public static Header GetHeader(this PayloadBase payload, HeaderName headerName)
        {
            return payload.Headers
                .Except(payload.XHeaders)
                .FirstOrDefault(h => h.ImfHeader == headerName);
        }

        /// <summary>
        /// Get the value of an <see cref="Header"/> with a <see cref="HeaderName"/>
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="headerName"></param>
        /// <returns>The value of the header or an empty string when not found</returns>
        public static string GetHeaderValue(this PayloadBase payload, HeaderName headerName)
        {
            var header = GetHeader(payload, headerName);
            return header == null ? string.Empty : header.Value;
        }

        /// <summary>
        /// Get the body from a part of a given MIME type
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static string GetBodyData(this Payload payload, MimeType mimeType)
        {
            var part = payload.Parts
                .FirstOrDefault(p => p.GetMimeType() == mimeType);

            return part == null || part.Body == null ? string.Empty : part.Body.Data;
        }
    }
}
