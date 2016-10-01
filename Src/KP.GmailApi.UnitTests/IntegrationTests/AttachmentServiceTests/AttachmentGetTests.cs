using System;
using System.Net;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;

namespace KP.GmailApi.UnitTests.IntegrationTests.AttachmentServiceTests
{
    public class AttachmentGetTests
    {
        private readonly AttachmentService _service;
        private readonly DraftService _draftService;

        public AttachmentGetTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailClient();

            _service = new AttachmentService(proxy);
            _draftService = new DraftService(proxy);
        }

        //[Fact]
        public void NonExistingAttachment_ReturnsNotFound()
        {
            // Arrange
            Draft draft = new Draft();// TODO: create draft with attachment
            Message message = _draftService.Create(draft).Message;
            string messageId = message.Id;

            // Act
            Action action = () => _service.Get(messageId, Guid.NewGuid().ToString("N"));

            // Assert
            action.ShouldThrow<GmailException>()
                .And.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
