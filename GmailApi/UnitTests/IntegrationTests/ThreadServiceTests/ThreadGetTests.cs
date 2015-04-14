using System;
using System.Linq;
using FluentAssertions;
using GmailApi;
using GmailApi.Models;
using GmailApi.ServiceExtensions;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.ThreadServiceTests
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
            Message message = _messageService.List(Label.Sent).First();
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
        public void NonExistingThread_ThrowsException()
        {
            // Act
            Action action = () => _service.Get(Guid.NewGuid().ToString("N"));

            // Assert
            action.ShouldThrow<GmailException>();
        }
    }
}
