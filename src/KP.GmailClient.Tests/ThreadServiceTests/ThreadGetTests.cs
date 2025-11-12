using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AwesomeAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.ThreadServiceTests;

public class ThreadGetTests : IClassFixture<GlobalDelayFixture>
{
    private readonly ThreadService _service;
    private readonly MessageService _messageService;

    public ThreadGetTests()
    {
        _service = new ThreadService(SettingsManager.GmailProxy);
        _messageService = new MessageService(SettingsManager.GmailProxy);
    }

    [Fact]
    public async Task CanGet()
    {
        // Arrange
        Message message = (await _messageService.ListByLabelAsync(Label.Sent, maxResults: 1)).First();
        string threadId = message.ThreadId;

        // Act
        MessageThread thread = await _service.GetAsync(threadId);

        // Assert
        thread.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task NonExistingThreadId_ReturnsNotFound()
    {
        // Arrange
        const string id = "13c97ae7b72cb05e";

        // Act
        async Task Action() => await _service.GetAsync(id);

        // Assert
        var ex = await Assert.ThrowsAsync<GmailApiException>(Action);
        ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}