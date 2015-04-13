using System;
using System.Collections.Generic;
using System.Linq;
using GmailApi;
using GmailApi.Models;
using GmailApi.ServiceExtensions;
using GmailApi.Services;
using Shouldly;
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
            Message message = _messageService.ListMessages(Label.Sent).First();
            string threadId = message.ThreadId;

            // Act
            Thread thread = _service.Get(threadId);

            // Assert
            Assert.NotNull(thread);
            thread.Id.ShouldNotBeNullOrEmpty();
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
            Should.Throw<GmailException>(action);
        }
    }
}
