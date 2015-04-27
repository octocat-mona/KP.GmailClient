using System;
using GmailApi.Builders;
using GmailApi.DTO;
using GmailApi.Models;

namespace GmailApi.Services
{
    public class ThreadService
    {
        private readonly GmailClient _client;

        public ThreadService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The ID of the thread to retrieve.</param>
        /// <returns></returns>
        public Thread Get(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Get, id)
                .Build();

            return _client.Get<Thread>(queryString);
        }

        /// <summary>
        /// Lists the thread IDs.
        /// </summary>
        /// <param name="query">Only return threads matching the specified query.
        /// Supports the same query format as the Gmail search box. For example, "from:someuser@example.com rfc822msgid: is:unread".</param>
        /// <param name="maxResults">Maximum number of threads to return</param>
        /// <param name="includeSpamAndTrash">Include threads from SPAM and TRASH in the results.</param>
        /// <param name="labelIds">Only return threads with labels that match all of the specified label IDs</param>
        /// <returns>A <see cref="ThreadList"/> containing the thread IDs</returns>
        public ThreadList ListIds(string query = null, ushort maxResults = 0, bool includeSpamAndTrash = false, params string[] labelIds)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.List)
                .SetFields(ThreadFields.Id | ThreadFields.ResultSizeEstimate | ThreadFields.NextPageToken)
                .SetQuery(query)
                .SetLabelIds(labelIds)
                .SetMaxResults(maxResults)
                .SetIncludeSpamAndTrash(includeSpamAndTrash)
                .Build();

            return _client.Get<ThreadList>(queryString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID of the Thread to delete.</param>
        /// <returns></returns>
        public void Delete(string id)
        {
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Delete, id)
                .Build();

            _client.Delete(queryString);
        }

        public Thread Modify()
        {
            throw new NotImplementedException();
        }

        public Thread Trash()
        {
            throw new NotImplementedException();
        }

        public Thread Untrash()
        {
            throw new NotImplementedException();
        }
    }
}
