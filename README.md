# KP.GmailApi
An alternative library of the Gmail API NuGet package.

[![Build Status](https://travis-ci.org/kpstolk/KP.GmailApi.svg?branch=master)](https://travis-ci.org/kpstolk/KP.GmailApi)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/kpstolk/gmail-api)
[![Coverage Status](https://coveralls.io/repos/kpstolk/KP.GmailApi/badge.svg)](https://coveralls.io/r/kpstolk/KP.GmailApi)

## Setup
``` csharp
var tokenManager = new OAuth2TokenManager(clientId, clientSecret);
// Provide a refresh token (required once)
if (!tokenManager.HasTokenSetup())
{
    // Client ID and secret of your project,
    // see the Dev Console (https://console.developers.google.com/project)
    var tokenHelper = new TokenAccessHelper(clientId, clientSecret);

    // Get a refresh token, launches a browser for user interaction:
    string authCode = tokenHelper.GetAuthorizationCode();
    string refreshToken = tokenHelper.GetRefreshToken(authCode);

    // First time required only
    tokenManager.Setup(refreshToken, false);
}

var service = new GmailService(tokenManager);
```

## Usage examples
``` csharp
// Get the users profile
service.GetProfile();

// Get inbox messages
service.Messages.List();

// Get starred messages
service.Messages.ListByLabel(Label.Starred);

// List all labels
service.Labels.List();
```
