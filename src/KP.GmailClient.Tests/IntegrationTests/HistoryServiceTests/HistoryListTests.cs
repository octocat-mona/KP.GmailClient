using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests.HistoryServiceTests
{
    public class HistoryListTests
    {
        private readonly HistoryService _service;
        private readonly MessageService _messageService;

        public HistoryListTests()
        {
            _service = new HistoryService(SettingsManager.GmailProxy);
            _messageService = new MessageService(SettingsManager.GmailProxy);
        }

        [Fact]
        public async Task CanList()
        {
            // Arrange
            var message = (await _messageService.ListAsync()).First();

            // Act
            var list = await _service.ListAsync(message.HistoryId);

            // Assert
            list.Histories.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task NonExistingId_ReturnsNotFound()
        {
            // Act
            Func<Task> action = async () => await _service.ListAsync(int.MaxValue);

            // Assert
            var ex = await Assert.ThrowsAsync<GmailApiException>(action);
            ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
