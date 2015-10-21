using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.ThreadServiceTests
{
    public class ThreadGetTests
    {
        private readonly ThreadService _service;
        private readonly MessageService _messageService;

        public ThreadGetTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();

            _service = new ThreadService(client);
            _messageService = new MessageService(client);
        }

        [Fact]
        public void CanGet()
        {
            // Arrange
            Message message = _messageService.ListByLabel(Label.Sent).First();
            string threadId = message.ThreadId;

            // Act
            Thread thread = _service.Get(threadId);

            // Assert
            thread.Should().NotBeNull();

            thread.Id.Should().NotBeNullOrWhiteSpace();
            //thread.Snippet.ShouldNotBeNullOrEmpty();
        }

        /*[Fact]
        public void CanList()
        {
            // Act
            List<Thread> threads = _service.List();

            // Assert
            Assert.NotNull(threads);
        }*/

        [Fact]
        public void NonExistingThreadId_ReturnsNotFound()
        {
            // Arrange
            const string id = "13c97ae7b72cb05e";

            // Act
            Action action = () => _service.Get(id);

            // Assert
            action.ShouldThrow<GmailException>()
                .And.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
