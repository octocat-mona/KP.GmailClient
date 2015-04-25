using System;
using FluentAssertions;
using GmailApi;
using GmailApi.ServiceExtensions;
using GmailApi.Services;

namespace UnitTests.IntegrationTests.ThreadServiceTests
{
    public class ThreadListTests
    {
        private readonly ThreadService _service;

        public ThreadListTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new ThreadService(client);
        }

        public void CanListIds()
        {
            // Act
            Action action = () => _service.ListIds();

            // Assert
            action.ShouldNotThrow();
        }

        public void CanList()
        {
            // Act
            Action action = () => _service.List();

            // Assert
            action.ShouldNotThrow();
        }
    }
}
