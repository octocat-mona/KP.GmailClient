[![Build Status](https://travis-ci.org/kpstolk/KP.GmailApi.svg?branch=master)](https://travis-ci.org/kpstolk/KP.GmailApi)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/kpstolk/gmail-api)

# KP.GmailApi
This is an alternative client for the auto generated Google.Apis.Gmail.v1 Client Library.

- It's easy to use
- Has added extension methods to make common tasks even more easier
- Supports Async
- Did I mention easy?

## Prerequisites
1. Create a new project in the Google Developer Console -> https://console.developers.google.com/project
2. Create a service account for the project -> https://console.cloud.google.com/iam-admin/serviceaccounts/
3. Create and download a new key as JSON file.
4. (only for Google Apps users) Go to the Google Apps Admin console and select the scopes, more on that here https://developers.google.com/identity/protocols/OAuth2ServiceAccount#delegatingauthority

## Setup
``` csharp
string privateKey = "<PKCS#8 private key from downloaded JSON file>";
string tokenUri = "https://accounts.google.com/o/oauth2/token";
string clientEmail = "<Client email from downloaded JSON file>";
string emailAddress = "<The associated Gmail address or domain email address>";
ServiceAccountCredential accountCredential = new ServiceAccountCredential
{
    PrivateKey = privateKey,
    TokenUri = tokenUri,
    ClientEmail = clientEmail
};
var client = new GmailClient(accountCredential, emailAddress, GmailScopes.Readonly);
```

## Usage examples
``` csharp
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
