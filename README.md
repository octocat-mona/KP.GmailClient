# Gmail-Api
An alternative library of the Gmail API NuGet package.

[![Build Status](https://travis-ci.org/kpstolk/Gmail-Api.svg)](https://travis-ci.org/kpstolk/Gmail-Api)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/kpstolk/gmail-api)


## Setup
``` csharp
// Client ID and secret of your project, see your Dev Console (https://console.developers.google.com/project)
TokenManager tokenManager = new TokenManager(clientId, clientSecret);

// Get a refresh token, launches a browser for user interaction:
string authCode = tokenManager.GetAuthorizationCode();
Oauth2Token oauth2Token = tokenManager.GetToken(authCode);

// First time required only
tokenManager.Setup(oauth2Token.RefreshToken, false);

// Setup
GmailClient client = new GmailClient(emailAddress, tokenManager);
GmailService service = new GmailService(client);
```

## Usage examples
    // Get inbox messages
    service.Messages.List();

    // Get starred messages
    service.Messages.ListByLabel(Label.Starred);

    // List all labels
    service.Labels.List();
