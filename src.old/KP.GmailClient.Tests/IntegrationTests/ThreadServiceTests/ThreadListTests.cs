using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Services;
using KP.GmailClient.Services.Extensions;

namespace KP.GmailClient.Tests.IntegrationTests.ThreadServiceTests
{
    public class ThreadListTests
    {
        private readonly ThreadService _service;

        public ThreadListTests()
        {
            _service = new ThreadService(SettingsManager.GmailProxy);
        }

        //[Fact] TODO: implement
        public void CanListIds()
        {
            // Act
            Func<Task> action = async () => await _service.ListIdsAsync();

            // Assert
            action.Should().NotThrow();
        }

        //[Fact] TODO: implement
        public void CanList()
        {
            // Act
            Func<Task> action = async () => await _service.ListAsync();

            // Assert
            action.Should().NotThrow();
        }
    }
}
