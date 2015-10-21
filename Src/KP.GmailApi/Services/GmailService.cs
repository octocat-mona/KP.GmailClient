using KP.GmailApi.Builders;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Contains all services provided by Gmail.
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

        /// <summary>
        /// Access to all Gmail services.
        /// </summary>
        /// <param name="tokenManager"></param>
        public GmailService(OAuth2TokenManager tokenManager)
        {
            _client = new GmailClient(tokenManager);

            Messages = new MessageService(_client);
            Drafts = new DraftService(_client);
            Labels = new LabelService(_client);
            Threads = new ThreadService(_client);
            History = new HistoryService(_client);
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
