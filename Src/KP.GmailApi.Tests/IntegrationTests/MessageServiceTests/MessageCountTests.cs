using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using KP.GmailApi.Services.Extensions;
using Xunit;

namespace KP.GmailApi.Tests.IntegrationTests.MessageServiceTests
{
    public class MessageCountTests
    {
        private readonly MessageService _service;

        public MessageCountTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new MessageService(proxy);
        }

        [Fact]
        public void CanCount()
        {
            // Act
            Action action = () => _service.CountAsync().Wait();

            // Assert
            action.ShouldNotThrow();
        }

        [Fact]
        public async Task Count_ReturnsSameAs_InboxCount()
        {
            // Act
            uint inboxCount = await _service.CountAsync();// parameterless ctor should count the user's Inbox
            uint labelInboxCount = await _service.CountAsync(Label.Inbox);

            // Assert
            inboxCount.Should().Be(labelInboxCount);
        }

        [Fact]
        public async Task NonExistingLabel_CountIsZero()// Apparently doesn't return NotFound
        {
            // Act
            const string id = "Label_1234567890123456789";
            uint count = await _service.CountAsync(id);

            // Assert
            count.Should().Be(0);
        }
    }
}
