using System;
using KP.GmailClient.Common;
using KP.GmailClient.Models;

namespace KP.GmailClient.Builders
{
    internal class OAuthQueryStringBuilder : QueryStringBuilder
    {
        private readonly OAuth2ClientCredentials _credentials;
        private readonly string _redirectUri;

        internal OAuthQueryStringBuilder(OAuth2ClientCredentials credentials, string redirectUri)
        {
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            _redirectUri = redirectUri ?? throw new ArgumentNullException(nameof(redirectUri));
        }

        internal OAuthQueryStringBuilder WithScopes(GmailScopes scopes)
        {
            SetParameter("scope", scopes.ToScopeString());
            return this;
        }

        internal OAuthQueryStringBuilder WithState(string state)
        {
            SetParameter("state", state);
            return this;
        }

        public override string Build()
        {
            SetParameter("access_type", "offline");
            SetParameter("response_type", "code");
            SetParameter("client_id", _credentials.ClientId);
            SetParameter("redirect_uri", _redirectUri);

            return base.Build();
        }
    }
}
