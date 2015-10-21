using System.Collections.Generic;
using System.Linq;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;

namespace KP.GmailApi.ServiceExtensions
{
    /// <summary>
    /// Extensions for <see cref="DraftService"/>.
    /// </summary>
    public static class DraftServiceExtensions
    {
        /// <summary>
        /// Create a draft.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="subject">The subject of the draft</param>
        /// <param name="body">The body of the draft</param>
        /// <returns></returns>
        public static Draft Create(this DraftService service, string subject, string body)
        {
            Draft draftInput = new Draft
            {
                Message = new Message
                {
                    Snippet = subject,
                    PlainRaw = body.ToBase64UrlString(),//TODO: HTML headers
                    Payload =
                    {
                        Headers = { new Header { Name = "Content-Type", Value = "text/html" } },
                        MimeType = "text/html"
                    }
                }
            };

            return service.Create(draftInput);
        }

        /// <summary>
        /// Lists the drafts in the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Drafts</returns>
        public static IEnumerable<Draft> List(this DraftService service)
        {
            DraftList draftIds = service.ListIds();

            return draftIds.Drafts.Select(draft => service.Get(draft.Id));
        }
    }
}
