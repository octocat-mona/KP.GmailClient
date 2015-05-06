using System;
using FluentAssertions;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.MessageServiceTests
{
    public class MessageCountTests
    {
        private readonly MessageService _service;

        public MessageCountTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new MessageService(client);
        }

        [Fact]
        public void CanCount()
        {
            // Act
            Action action = () => _service.Count();

            // Assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void Count_ReturnsSameAs_InboxCount()
        {
            // Act
            uint inboxCount = _service.Count();// parameterless ctor should count the user's Inbox
            uint labelInboxCount = _service.Count(Label.Inbox);

            // Assert
            inboxCount.Should().Be(labelInboxCount);
        }

        [Fact]
        public void NonExistingLabel_CountIsZero()// Apparently doesn't return NotFound
        {
            // Act
            const string id = "Label_1234567890123456789";
            uint count = _service.Count(id);

            // Assert
            count.Should().Be(0);
        }
    }
}
