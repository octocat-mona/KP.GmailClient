using System.Linq;
using KP.GmailClient.Models;

namespace KP.GmailClient.Extensions
{
    /// <summary>
    /// Extensions for <see cref="PayloadBase"/> and <see cref="Payload"/>.
    /// </summary>
    internal static class PayloadExtensions
    {
        /// <summary>
        /// Get the MIME type of this payload.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>The MIME type or Unknown</returns>
        internal static MimeType GetMimeType(this PayloadBase payload)
        {
            return payload.MimeType switch
            {
                "text/plain" => MimeType.TextPlain,
                "text/html" => MimeType.TextHtml,
                _ => MimeType.Unknown
            };
        }

        /// <summary>
        /// Get the <see cref="Header"/> with a <see cref="HeaderName"/>
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="headerName"></param>
        /// <returns>A <see cref="Header"/> or null</returns>
        internal static Header GetHeader(this PayloadBase payload, HeaderName headerName)
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
        internal static string GetHeaderValue(this PayloadBase payload, HeaderName headerName)
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
        internal static string GetBodyData(this Payload payload, MimeType mimeType)
        {
            var part = payload.Parts
                .FirstOrDefault(p => p.GetMimeType() == mimeType);

            return part?.Body == null ? string.Empty : part.Body.Data;
        }
    }
}
