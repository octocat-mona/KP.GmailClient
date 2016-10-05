using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.LabelServiceTests
{
    public class LabelGetTests
    {
        private readonly LabelService _service;

        public LabelGetTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new LabelService(proxy);
        }

        [Fact]
        public async Task CanGet()
        {
            // Act
            Label label = await _service.GetAsync(Label.Inbox);

            // Assert
            Assert.NotNull(label);
        }

        [Fact]
        public async Task CanList()
        {
            // Act
            IList<Label> labels = await _service.ListAsync();

            // Assert
            Assert.NotNull(labels);
        }

        [Fact]
        public async Task NonExistingLabel_ReturnsNotFound()
        {
            // Act
            Func<Task> action = async () => await _service.GetAsync(Guid.NewGuid().ToString("N"));

            // Assert
            var ex = await Assert.ThrowsAsync<GmailException>(action);
            ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
