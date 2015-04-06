using System.Collections.Generic;
using System.Linq;
using GmailApi.Models;
using GmailApi.Services;

namespace GmailApi.ServiceExtensions
{
    public static class LabelServiceExtensions
    {
        /// <summary>
        /// Lists all labels in the user's mailbox of the given type.
        /// </summary>
        /// <returns></returns>
        public static List<Label> List(LabelService service, LabelType type)
        {
            return service.List()
                .Where(l => l.Type == type)
                .ToList();
        }

    }
}
