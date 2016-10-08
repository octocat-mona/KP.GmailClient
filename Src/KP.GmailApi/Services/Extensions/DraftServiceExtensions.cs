using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Common;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services.Extensions
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
        public static async Task<Draft> CreateAsync(this DraftService service, string subject, string body)
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

            return await service.CreateAsync(draftInput);
        }

        /// <summary>
        /// Lists the drafts in the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Drafts</returns>
        public static async Task<IList<Draft>> ListAsync(this DraftService service)
        {
            DraftList draftIds = await service.ListIdsAsync();

            var tasks = draftIds.Drafts.Select(async draft => (await service.GetAsync(draft.Id)));
            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}
