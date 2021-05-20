using System.Threading.Tasks;
using KP.GmailClient.Authentication.TokenClients;
using KP.GmailClient.Authentication.TokenStores;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;

namespace KP.GmailClient
{
    /// <summary><inheritdoc cref="IGmailClient"/></summary>
    public class GmailClient : IGmailClient
    {
        private readonly GmailProxy _proxy;

        public MessageService Messages { get; }

        public DraftService Drafts { get; }

        public LabelService Labels { get; }

        public ThreadService Threads { get; }

        public HistoryService History { get; }

        /// <summary>Access to all Gmail services.</summary>
        /// <param name="tokenClient"></param>
        /// <param name="tokenStore"></param>
        public GmailClient(ITokenClient tokenClient, ITokenStore tokenStore)
        {
            _proxy = new GmailProxy(new AuthorizationDelegatingHandler(tokenClient, tokenStore));

            Messages = new MessageService(_proxy);
            Drafts = new DraftService(_proxy);
            Labels = new LabelService(_proxy);
            Threads = new ThreadService(_proxy);
            History = new HistoryService(_proxy);
        }

        public async Task<Profile> GetProfileAsync()
        {
            string queryString = new UserQueryStringBuilder()
                 .Build();

            return await _proxy.Get<Profile>(queryString);
        }

        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
