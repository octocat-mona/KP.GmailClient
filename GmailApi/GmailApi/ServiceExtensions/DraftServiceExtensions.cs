using System.Collections.Generic;
using System.Linq;
using GmailApi.Models;
using GmailApi.Services;

namespace GmailApi.ServiceExtensions
{
    public static class DraftServiceExtensions
    {
        /// <summary>
        /// Lists the drafts in the user's inbox
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IEnumerable<Draft> List(this DraftService service)
        {
            DraftList draftIds = service.ListIds();

            return draftIds.Drafts.Select(draft => service.Get(draft.Id));
        }
    }
}
