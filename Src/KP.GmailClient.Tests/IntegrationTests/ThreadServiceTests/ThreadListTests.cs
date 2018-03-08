using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Services;
using KP.GmailClient.Services.Extensions;

namespace KP.GmailClient.Tests.IntegrationTests.ThreadServiceTests
{
    public class ThreadListTests : IDisposable
    {
        private readonly ThreadService _service;
        private readonly GmailProxy _proxy;

        public ThreadListTests()
        {
            _proxy = SettingsManager.GetGmailProxy();
            _service = new ThreadService(_proxy);
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

        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
