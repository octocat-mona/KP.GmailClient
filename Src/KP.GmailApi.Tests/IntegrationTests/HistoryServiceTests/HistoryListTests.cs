using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.Tests.IntegrationTests.HistoryServiceTests
{
    public class HistoryListTests
    {
        private readonly HistoryService _service;

        public HistoryListTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new HistoryService(proxy);
        }

        //[Fact]
        public void CanList()
        {
            // Act
            //_service.List(ulong.MaxValue);
        }

        //[Fact]
        public async Task NonExistingId_ReturnsNotFound()
        {
            // Act
            Func<Task> action = async () => await _service.ListAsync(ulong.MaxValue);

            // Assert
            var ex = await Assert.ThrowsAsync<GmailException>(action);
            ex.StatusCode.Should().Be(HttpStatusCode.NotFound);//TODO: currently returns 400
        }
    }
}
