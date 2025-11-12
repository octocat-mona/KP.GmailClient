using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AwesomeAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.HistoryServiceTests;

public class HistoryListTests : IClassFixture<GlobalDelayFixture>
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
        var message = (await _messageService.ListAsync("", maxResults: 1)).First();

        // Act
        var list = await _service.ListAsync(message.HistoryId);

        // Assert
        list.Histories.Should().HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task NonExistingId_ReturnsNotFound()
    {
        // Act
        async Task Action() => await _service.ListAsync(int.MaxValue.ToString());

        // Assert
        var ex = await Assert.ThrowsAsync<GmailApiException>(Action);
        ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}