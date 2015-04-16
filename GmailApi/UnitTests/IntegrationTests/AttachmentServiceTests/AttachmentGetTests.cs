using System;
using FluentAssertions;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;

namespace UnitTests.IntegrationTests.AttachmentServiceTests
{
    public class AttachmentGetTests
    {
        private readonly AttachmentService _service;
        private readonly DraftService _draftService;

        public AttachmentGetTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();

            _service = new AttachmentService(client);
            _draftService = new DraftService(client);
        }

        //[Fact]
        public void NonExistingAttachment_ThrowsException()
        {
            // Arrange
            Draft draft = new Draft();// TODO: create draft with attachment
            Message message = _draftService.Create(draft).Message;
            string messageId = message.Id;

            // Act
            Action action = () => _service.Get(messageId, Guid.NewGuid().ToString("N"));

            // Assert
            action.ShouldThrow<GmailException>();
        }
    }
}
