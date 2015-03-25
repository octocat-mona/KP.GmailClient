using GmailApi.Builders;

namespace GmailApi.Services
{
    public class GmailService
    {
        private readonly GmailClient _client;

        public MessageService Messages { get; private set; }
        public DraftService Drafts { get; private set; }
        public LabelService Labels { get; private set; }
        public ThreadService Threads { get; private set; }
        public HistoryService History { get; private set; }

        public GmailService(GmailClient client)
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
