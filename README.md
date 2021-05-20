[![NuGet version (KP.GmailClient)](https://img.shields.io/nuget/v/KP.GmailClient.svg?style=flat-square)](https://www.nuget.org/packages/KP.GmailClient/)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/KP/gmail-api)

# KP.GmailClient
This is an alternative client for the auto generated [Google.Apis.Gmail.v1](https://www.nuget.org/packages/Google.Apis.Gmail.v1/) Client Library.

## Prerequisites
1. Create [a new project](https://console.cloud.google.com/projectcreate) in the Google Cloud Console ([Guide](https://developers.google.com/workspace/guides/create-project))
1. Create a [OAuth consent screen](https://console.cloud.google.com/apis/credentials/consent) ([Guide](https://developers.google.com/workspace/guides/create-credentials#configure_the_oauth_consent_screen))
    1. Publish the app or add any test users
1. Create [Desktop application credentials](https://console.cloud.google.com/apis/credentials/oauthclient) and download the client secret JSON file ([Guide](https://developers.google.com/workspace/guides/create-credentials#desktop))
1. Enable the [Gmail API](https://console.cloud.google.com/apis/library/gmail.googleapis.com)


## Setup
One-time setup which opens the browser to authenticate the user.

``` csharp
// Define the required scopes
const GmailScopes scopes = GmailScopes.Readonly | GmailScopes.Send;

var broker = new GmailAuthenticationBroker();
var token = await broker.AuthenticateAsync("oauth_client_credentials.json", scopes);

var tokenStore = new FileTokenStore("token.json");
await tokenStore.StoreTokenAsync(token);
```

## Usage examples
``` csharp
// Use the previously created files
var tokenClient = TokenClient.Create("oauth_client_credentials.json");
var tokenStore = new FileTokenStore("token.json");
using var client = new GmailClient(tokenClient, tokenStore);


// Send a plain text email
Message plainMessage = await client.Messages.SendAsync("example@gmail.com", "Subject", "Plain text body");

// Send a HTML email
Message htmlMessage = await client.Messages.SendAsync("example@gmail.com", "Subject", "<h1>HTML body</h1>", isBodyHtml: true);

// Get the users profile
Profile profile = await client.GetProfileAsync();

// Get inbox messages
IList<Message> messages = await client.Messages.ListAsync();

// Get starred messages
IList<Message> starredMessages = await client.Messages.ListByLabelAsync(Label.Starred);

// List all labels
IList<Label> labels = await client.Labels.ListAsync();

// List all drafts
IList<Draft> drafts = await client.Drafts.ListAsync();
```
