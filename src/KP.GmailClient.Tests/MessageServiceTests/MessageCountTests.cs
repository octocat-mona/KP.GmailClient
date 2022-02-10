using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.MessageServiceTests
{
    public class MessageCountTests
    {
        private readonly MessageService _service;

        public MessageCountTests()
        {
            _service = new MessageService(SettingsManager.GmailProxy);
        }

        [Fact]
        public void CanCount()
        {
            // Act
            Func<Task> action = async () => await _service.CountAsync();

            // Assert
            action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Count_ReturnsSameAs_InboxCount()
        {
            // Arrange
            uint labelInboxCount = await _service.CountAsync(Label.Inbox);

            // Act
            uint inboxCount = await _service.CountAsync(); // parameterless ctor should count the user's Inbox

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
}
