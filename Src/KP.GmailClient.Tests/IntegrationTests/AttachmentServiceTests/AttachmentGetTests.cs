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
    public class AttachmentGetTests : IDisposable
    {
        private readonly AttachmentService _service;
        private readonly DraftService _draftService;
        private readonly GmailProxy _proxy;

        public AttachmentGetTests()
        {
            _proxy = SettingsManager.GetGmailProxy();
            _service = new AttachmentService(_proxy);
            _draftService = new DraftService(_proxy);
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

        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
