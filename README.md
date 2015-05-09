# KP.GmailApi
An alternative library of the Gmail API NuGet package.

[![Build Status](https://travis-ci.org/kpstolk/KP.GmailApi.svg?branch=master)](https://travis-ci.org/kpstolk/KP.GmailApi)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/kpstolk/gmail-api)


## Setup
``` csharp
// Client ID and secret of your project,
// see the Dev Console (https://console.developers.google.com/project)
var tokenHelper = new TokenAccessHelper("clientId", "clientSecret");

// Get a refresh token, launches a browser for user interaction:
string authCode = tokenHelper.GetAuthorizationCode();
string refreshToken = tokenHelper.GetRefreshToken(authCode);

// First time required only
TokenManager tokenManager = new TokenManager("clientId", "clientSecret");
tokenManager.Setup(refreshToken, false);

// Setup
GmailClient client = new GmailClient("me", tokenManager);
GmailService service = new GmailService(client);
```

## Usage examples
``` csharp
// Get inbox messages
service.Messages.List();

// Get starred messages
service.Messages.ListByLabel(Label.Starred);

// List all labels
service.Labels.List();
```
