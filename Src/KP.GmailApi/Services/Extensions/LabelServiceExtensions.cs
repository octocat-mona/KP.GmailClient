using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services.Extensions
{
    /// <summary>
    /// Extensions for <see cref="LabelService"/>.
    /// </summary>
    public static class LabelServiceExtensions
    {
        /// <summary>
        /// Lists all labels in the user's mailbox of the given type.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="type">The label type</param>
        /// <returns>A list of Labels</returns>
        public static async Task<IList<Label>> ListAsync(this LabelService service, LabelType type)
        {
            return (await service.ListAsync())
                .Where(l => l.Type == type)
                .ToList();
        }

        /// <summary>
        /// Lists all labels in the user's mailbox of the given type.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="name">The name of the label defined by the user</param>
        /// <returns>A list of Labels</returns>
        public static async Task<Label> GetByNameAsync(this LabelService service, string name)
        {
            return (await ListAsync(service, LabelType.User))
                .FirstOrDefault(label => string.Equals(label.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
