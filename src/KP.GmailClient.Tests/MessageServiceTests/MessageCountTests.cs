using System.Threading.Tasks;
using AwesomeAssertions;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.MessageServiceTests;

public class MessageCountTests : IClassFixture<GlobalDelayFixture>
{
    private readonly MessageService _service;

    public MessageCountTests()
    {
        _service = new MessageService(SettingsManager.GmailProxy);
    }

    [Fact]
    public async Task Count_ReturnsSameAs_InboxCount()
    {
        // Arrange
        const string label = Label.Inbox;
        uint labelInboxCount = await _service.CountAsync(label);

        // Act
        uint inboxCount = await _service.CountAsync(label);

        // Assert
        inboxCount.Should().Be(labelInboxCount);
    }

    [Fact]
    public async Task NonExistingLabel_CountIsZero() // Apparently doesn't return NotFound
    {
        // Arrange
        const string id = "Label_1234567890123456789";

        // Act
        uint count = await _service.CountAsync(id);

        // Assert
        count.Should().Be(0);
    }
}