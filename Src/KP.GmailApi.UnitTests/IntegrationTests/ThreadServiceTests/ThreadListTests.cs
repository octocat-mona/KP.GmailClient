using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;

namespace KP.GmailApi.UnitTests.IntegrationTests.ThreadServiceTests
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
