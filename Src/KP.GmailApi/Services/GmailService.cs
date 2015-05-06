using KP.GmailApi.Builders;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Contains all services provided by Gmail
    /// </summary>
    public class GmailService
    {
        private readonly GmailClient _client;

        /// <summary>
        /// Service to get, create, update and delete emails.
        /// </summary>
        public MessageService Messages { get; private set; }
        /// <summary>
        /// Service to get, create, update and delete email drafts.
        /// </summary>
        public DraftService Drafts { get; private set; }
        /// <summary>
        /// Service to get, create, update and delete email labels.
        /// </summary>
        public LabelService Labels { get; private set; }
        /// <summary>
        /// Service for getting email threads.
        /// </summary>
        public ThreadService Threads { get; private set; }
        /// <summary>
        /// Service for getting the history of emails.
        /// </summary>
        public HistoryService History { get; private set; }

        internal GmailService(GmailClient client)
        {
            _client = client;

            Messages = new MessageService(client);
            Drafts = new DraftService(client);
            Labels = new LabelService(client);
            Threads = new ThreadService(client);
            History = new HistoryService(client);
        }

        /// <summary>
        /// Gets the current user's Gmail profile.
        /// </summary>
        /// <returns></returns>
        public Profile GetProfile()
        {
            string queryString = new UserQueryStringBuilder()
                 .Build();

            return _client.Get<Profile>(queryString);
        }
    }
}
