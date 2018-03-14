using System;
using System.Threading.Tasks;
using KP.GmailClient.Builders;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;

namespace KP.GmailClient
{
    /// <summary>
    /// Contains all services provided by Gmail.
    /// </summary>
    public class GmailClient : IDisposable
    {
        private readonly GmailProxy _proxy;

        /// <summary>
        /// Service to get, create, update and delete emails.
        /// </summary>
        public MessageService Messages { get; }
        /// <summary>
        /// Service to get, create, update and delete email drafts.
        /// </summary>
        public DraftService Drafts { get; }
        /// <summary>
        /// Service to get, create, update and delete email labels.
        /// </summary>
        public LabelService Labels { get; }
        /// <summary>
        /// Service for getting email threads.
        /// </summary>
        public ThreadService Threads { get; }
        /// <summary>
        /// Service for getting the history of emails.
        /// </summary>
        public HistoryService History { get; }

        /// <summary>
        /// Access to all Gmail services.
        /// </summary>
        /// <param name="initializer">A <see cref="GmailClientInitializer"/> instance</param>
        /// <param name="emailAddress">The emailaddress of the user to impersonate</param>
        public GmailClient(GmailClientInitializer initializer, string emailAddress)
            : this(initializer.AccountCredential, emailAddress, initializer.Scopes) { }

        /// <summary>
        /// Access to all Gmail services.
        /// </summary>
        /// <param name="accountCredential">The Google Account Credentials</param>
        /// <param name="emailAddress">The emailaddress of the user to impersonate</param>
        /// <param name="scopes">The Gmail scopes required to access the users data</param>
        public GmailClient(ServiceAccountCredential accountCredential, string emailAddress, GmailScopes scopes)
            : this(accountCredential, emailAddress, scopes.ToScopeString()) { }

        /// <summary>
        /// Access to all Gmail services.
        /// </summary>
        /// <param name="accountCredential">The Google Account Credentials</param>
        /// <param name="emailAddress">The emailaddress of the user to impersonate</param>
        /// <param name="scopes">The required Gmail scopes, separated by space</param>
        public GmailClient(ServiceAccountCredential accountCredential, string emailAddress, string scopes)
        {
            _proxy = new GmailProxy(new AuthorizationDelegatingHandler(accountCredential, emailAddress, scopes));

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
