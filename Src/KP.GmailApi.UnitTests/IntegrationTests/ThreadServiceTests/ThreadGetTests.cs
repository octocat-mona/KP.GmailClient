using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            GmailProxy proxy = SettingsManager.GetGmailProxy();

            _service = new ThreadService(proxy);
            _messageService = new MessageService(proxy);
        }

        [Fact]
        public async Task CanGet()
        {
            // Arrange
            Message message = (await _messageService.ListByLabelAsync(Label.Sent)).First();
            string threadId = message.ThreadId;

            // Act
            MessageThread thread = await _service.GetAsync(threadId);

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
        public async Task NonExistingThreadId_ReturnsNotFound()
        {
            // Arrange
            const string id = "13c97ae7b72cb05e";

            // Act
            Func<Task> action = async () => await _service.GetAsync(id);

            // Assert
            var ex = await Assert.ThrowsAsync<GmailException>(action);
            ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
