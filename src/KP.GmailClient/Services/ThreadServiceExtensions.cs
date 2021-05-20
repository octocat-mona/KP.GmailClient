using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services
{
    /// <summary>Extensions for <see cref="ThreadService"/>.</summary>
    public static class ThreadServiceExtensions
    {
        /// <summary>Get the number of estimated threads of the specified label.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>The number of threads</returns>
        public static async Task<uint> CountAsync(this ThreadService service, params string[] labelIds)
        {
            return (await service.ListIdsAsync(labelIds: labelIds)).ResultSizeEstimate;
        }

        /// <summary>Get the number of estimated threads in the user's inbox.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>The number of threads</returns>
        public static async Task<uint> CountAsync(this ThreadService service)
        {
            return await CountAsync(service, Label.Inbox);
        }

        /// <summary>Lists the thread IDs of the user's inbox.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A <see cref="ThreadList"/> instance</returns>
        public static async Task<ThreadList> ListIdsAsync(this ThreadService service)
        {
            return await service.ListIdsAsync(labelIds: Label.Inbox);
        }

        /// <summary>Lists the threads of the user's inbox.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Threads</returns>
        public static async Task<IList<MessageThread>> ListAsync(this ThreadService service)
        {
            return await ListAsync(service, null, labelIds: Label.Inbox);
        }

        /// <summary>Lists the threads filtered with a query.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>A list of threads</returns>
        public static async Task<IList<MessageThread>> ListAsync(this ThreadService service, string query, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            var threadList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelIds);

            var tasks = threadList.Threads.Select(id => service.GetAsync(id.Id));
            return (await Task.WhenAll(tasks)).ToList();
        }

        /// <summary>Lists the threads in the specified label.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelId">Only return threads with the specified label ID</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <returns>A list of threads</returns>
        public static async Task<IList<MessageThread>> ListByLabelAsync(this ThreadService service, string labelId, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var threadList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelId);

            var tasks = threadList.Threads.Select(id => service.GetAsync(id.Id));
            return (await Task.WhenAll(tasks)).ToList();
        }

        /// <summary>Lists the threads in the specified labels.</summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <returns>A list of threads</returns>
        public static async Task<IList<MessageThread>> ListByLabelsAsync(this ThreadService service, string[] labelIds, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var threadList = await service.ListIdsAsync(query, maxResults, includeSpamAndTrash, labelIds);

            var tasks = threadList.Threads.Select(id => service.GetAsync(id.Id));
            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}
