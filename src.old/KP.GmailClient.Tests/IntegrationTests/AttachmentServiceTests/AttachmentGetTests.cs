using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests.AttachmentServiceTests
{
    public class AttachmentGetTests
    {
        private readonly AttachmentService _service;
        private readonly DraftService _draftService;

        public AttachmentGetTests()
        {
            _service = new AttachmentService(SettingsManager.GmailProxy);
            _draftService = new DraftService(SettingsManager.GmailProxy);
        }

        //[Fact]
        public async Task NonExistingAttachment_ReturnsNotFound()
        {
            // Arrange
            Draft draft = new Draft();// TODO: create draft with attachment
            Message message = (await _draftService.CreateAsync(draft)).Message;
            string messageId = message.Id;

            // Act
            Func<Task> action = async () => await _service.GetAsync(messageId, Guid.NewGuid().ToString("N"));

            // Assert
            var ex = await Assert.ThrowsAsync<GmailException>(action);
            ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
