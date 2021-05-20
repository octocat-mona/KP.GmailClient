using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services
{
    /// <summary>Service for getting email threads.</summary>
    public class ThreadService
    {
        private readonly GmailProxy _proxy;

        internal ThreadService(GmailProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>Gets the specified thread.</summary>
        /// <param name="id">The ID of the thread to retrieve.</param>
        /// <returns></returns>
        public async Task<MessageThread> GetAsync(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Get, id)
                .Build();

            return await _proxy.Get<MessageThread>(queryString);
        }

        /// <summary>Lists the thread IDs.</summary>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>A <see cref="ThreadList"/> containing the thread IDs</returns>
        public async Task<ThreadList> ListIdsAsync(string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.List)
                .SetFields(ThreadFields.Id | ThreadFields.ResultSizeEstimate | ThreadFields.NextPageToken)
                .SetQuery(query)
                .SetLabelIds(labelIds)
                .SetMaxResults(maxResults)
                .SetIncludeSpamAndTrash(includeSpamAndTrash)
                .Build();

            return await _proxy.Get<ThreadList>(queryString);
        }

        /// <summary>
        /// Immediately and permanently deletes the specified thread.
        /// WARNING: This operation cannot be undone. Prefer threads.trash instead.
        /// </summary>
        /// <param name="id">ID of the Thread to delete.</param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Delete, id)
                .Build();

            await _proxy.Delete(queryString);
        }

        /// <summary>Modifies the labels applied to the thread. This applies to all messages in the thread.</summary>
        /// <param name="id">The ID of the thread to modify</param>
        /// <param name="input">The input to modify a thread</param>
        /// <returns></returns>
        public async Task<MessageThread> ModifyAsync(string id, ModifyThreadInput input)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Modify, id)
                .Build();

            return await _proxy.Post<MessageThread>(queryString, input);
        }

        /// <summary>Moves the specified thread to the trash.</summary>
        /// <param name="id">The ID of the thread to Trash</param>
        /// <returns></returns>
        public async Task<MessageThread> TrashAsync(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Trash, id)
                .Build();

            return await _proxy.Post<MessageThread>(queryString);
        }

        /// <summary>Removes the specified thread from the trash.</summary>
        /// <param name="id">The ID of the thread to remove from Trash</param>
        /// <returns></returns>
        public async Task<MessageThread> UntrashAsync(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Untrash, id)
                .Build();

            return await _proxy.Post<MessageThread>(queryString);
        }
    }
}
