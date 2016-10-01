using KP.GmailApi.Builders;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Contains all services provided by Gmail.
    /// </summary>
    public class GmailClient
    {
        private readonly GmailProxy _proxy;

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
        public GmailClient(OAuth2TokenManager tokenManager)
        {
            _proxy = new GmailProxy(new AuthorizationDelegatingHandler(tokenManager));

            Messages = new MessageService(_proxy);
            Drafts = new DraftService(_proxy);
            Labels = new LabelService(_proxy);
            Threads = new ThreadService(_proxy);
            History = new HistoryService(_proxy);
        }

        /// <summary>
        /// Gets the current user's Gmail profile.
        /// </summary>
        /// <returns></returns>
        public Profile GetProfile()
        {
            string queryString = new UserQueryStringBuilder()
                 .Build();

            return _proxy.Get<Profile>(queryString);
        }
    }
}
