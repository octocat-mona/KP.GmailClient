# KP.GmailApi
This is an alternative client for the auto generated Google.Apis.Gmail.v1 Client Library.

- It's easy to use
- Has added extension methods to make common tasks even more easier
- Supports Async
- Did I mention easy?

[![Build Status](https://travis-ci.org/kpstolk/KP.GmailApi.svg?branch=master)](https://travis-ci.org/kpstolk/KP.GmailApi)
[![Build status](https://ci.appveyor.com/api/projects/status/tqv09fs3fo9a37t0?svg=true)](https://ci.appveyor.com/project/kpstolk/gmail-api)
[![Coverage Status](https://coveralls.io/repos/kpstolk/KP.GmailApi/badge.svg)](https://coveralls.io/r/kpstolk/KP.GmailApi)

## Prerequisites
1. Create a new project in the Google Developer Console -> https://console.developers.google.com/project
2. Create a service account for the project -> https://console.cloud.google.com/iam-admin/serviceaccounts/
3. Create and download a new key as JSON file.

## Setup
``` csharp
var client = new GmailClient(KeyFile, EmailAddress, GmailScopes.Readonly);
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
