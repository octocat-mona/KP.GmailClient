using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Services;
using KP.GmailClient.Services.Extensions;

namespace KP.GmailClient.Tests.IntegrationTests.ThreadServiceTests
{
    public class ThreadListTests
    {
        private readonly ThreadService _service;

        public ThreadListTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new ThreadService(proxy);
        }

        //[Fact] TODO: implement
        public void CanListIds()
        {
            // Act
            Func<Task> action = async () => await _service.ListIdsAsync();

            // Assert
            action.ShouldNotThrow();
        }

        //[Fact] TODO: implement
        public void CanList()
        {
            // Act
            Func<Task> action = async () => await _service.ListAsync();

            // Assert
            action.ShouldNotThrow();
        }
    }
}
