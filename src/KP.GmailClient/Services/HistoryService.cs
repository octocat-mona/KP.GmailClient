using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Models;

namespace KP.GmailClient.Services
{
    /// <summary>Service for getting the history of emails.</summary>
    public class HistoryService
    {
        private readonly GmailProxy _proxy;

        internal HistoryService(GmailProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>Lists the history of all changes to the given mailbox. History results are returned in chronological order (increasing historyId).</summary>
        /// <param name="startHistoryId">Returns history records after the specified startHistoryId</param>
        /// <param name="labelId">Only return messages with a label matching the ID</param>
        /// <param name="pageToken">Page token to retrieve a specific page of results in the list</param>
        /// <param name="maxResults">The maximum number of history records to return. Use zero ('0') to use the default.</param>
        /// <returns></returns>
        public async Task<HistoryList> ListAsync(ulong startHistoryId, string labelId = null, string pageToken = null, uint maxResults = 0)
        {
            string queryString = new HistoryQueryStringBuilder()
                .SetStartHistoryId(startHistoryId)
                .SetLabelId(labelId)
                .SetPageToken(pageToken)
                .SetMaxResults(maxResults)
                .Build();

            return await _proxy.Get<HistoryList>(queryString);
        }
    }
}