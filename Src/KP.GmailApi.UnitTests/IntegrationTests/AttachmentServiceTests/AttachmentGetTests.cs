using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.AttachmentServiceTests
{
    public class AttachmentGetTests
    {
        private readonly AttachmentService _service;
        private readonly DraftService _draftService;

        public AttachmentGetTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();

            _service = new AttachmentService(proxy);
            _draftService = new DraftService(proxy);
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
