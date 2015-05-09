using System.Collections.Generic;
using System.Linq;
using KP.GmailApi.Models;
using KP.GmailApi.Services;

namespace KP.GmailApi.ServiceExtensions
{
    /// <summary>
    /// Extensions for <see cref="ThreadService"/>.
    /// </summary>
    public static class ThreadServiceExtensions
    {
        /// <summary>
        /// Get the number of estimated threads of the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>The number of threads</returns>
        public static uint Count(this ThreadService service, params string[] labelIds)
        {
            return service.ListIds(labelIds: labelIds).ResultSizeEstimate;
        }

        /// <summary>
        /// Get the number of estimated threads in the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>The number of threads</returns>
        public static uint Count(this ThreadService service)
        {
            return Count(service, Label.Inbox);
        }

        /// <summary>
        /// Lists the thread IDs of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A <see cref="ThreadList"/> instance</returns>
        public static ThreadList ListIds(this ThreadService service)
        {
            return service.ListIds(labelIds: Label.Inbox);
        }

        /// <summary>
        /// Lists the threads of the user's inbox.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <returns>A list of Threads</returns>
        public static IEnumerable<Thread> List(this ThreadService service)
        {
            return List(service, null, labelIds: Label.Inbox);
        }

        /// <summary>
        /// Lists the threads filtered with a query.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>A list of threads</returns>
        public static IEnumerable<Thread> List(this ThreadService service, string query, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            var threadList = service.ListIds(query, maxResults, includeSpamAndTrash, labelIds);

            return threadList.Threads.Select(id => service.Get(id.Id));
        }

        /// <summary>
        /// Lists the threads in the specified label.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelId">Only return threads with the specified label ID</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <returns>A list of threads</returns>
        public static IEnumerable<Thread> ListByLabel(this ThreadService service, string labelId, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var threadList = service.ListIds(query, maxResults, includeSpamAndTrash, labelId);

            return threadList.Threads.Select(id => service.Get(id.Id));
        }

        /// <summary>
        /// Lists the threads in the specified labels.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <returns>A list of threads</returns>
        public static IEnumerable<Thread> ListByLabels(this ThreadService service, string[] labelIds, string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false)
        {
            var threadList = service.ListIds(query, maxResults, includeSpamAndTrash, labelIds);

            return threadList.Threads.Select(id => service.Get(id.Id));
        }
    }
}
