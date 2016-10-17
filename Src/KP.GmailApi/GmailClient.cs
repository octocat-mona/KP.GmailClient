using System;
using System.Linq;
using System.Threading.Tasks;
using KP.GmailApi.Builders;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;

namespace KP.GmailApi
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
        /// <param name="keyFile">The Google Account Credentials JSON file</param>
        /// <param name="emailAddress">The emailaddress of the user to impersonate</param>
        /// <param name="scopes">The required Gmail scopes</param>
        public GmailClient(string keyFile, string emailAddress, GmailScopes scopes)
            : this(keyFile, emailAddress, ConvertToScopes(scopes)) { }

        private static string ConvertToScopes(GmailScopes gmailScopes)
        {
            GmailScopes[] scopes = gmailScopes.GetFlagEnumValues();
            string[] scopeValueStrings = new string[scopes.Length];
            for (int i = 0; i < scopes.Length; i++)
            {
                switch (scopes[i])
                {
                    case GmailScopes.Readonly:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.readonly";
                        break;
                    case GmailScopes.Modify:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.modify";
                        break;
                    case GmailScopes.Compose:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.compose";
                        break;
                    case GmailScopes.Insert:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.insert";
                        break;
                    case GmailScopes.Labels:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.labels";
                        break;
                    case GmailScopes.Send:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.send";
                        break;
                    case GmailScopes.ManageBasicSettings:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.settings.basic";
                        break;
                    case GmailScopes.ManageSensitiveSettings:
                        scopeValueStrings[i] = "https://www.googleapis.com/auth/gmail.settings.sharing";
                        break;
                    case GmailScopes.FullAccess:
                        scopeValueStrings[i] = "https://mail.google.com/";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(gmailScopes));
                }
            }

            return scopeValueStrings.Aggregate((scope1, scope2) => scope1 + " " + scope2);
        }

        /// <summary>
        /// Access to all Gmail services.
        /// </summary>
        /// <param name="keyFile">The Google Account Credentials JSON file</param>
        /// <param name="emailAddress">The emailaddress of the user to impersonate</param>
        /// <param name="scopes">The required Gmail scopes, separated by space</param>
        public GmailClient(string keyFile, string emailAddress, string scopes)
        {
            _proxy = new GmailProxy(new AuthorizationDelegatingHandler(keyFile, emailAddress, scopes));

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
    }
}
