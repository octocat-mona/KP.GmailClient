using System;
using System.Collections.Generic;
using System.Net;
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
            GmailProxy proxy = SettingsManager.GetGmailClient();
            _service = new LabelService(proxy);
        }

        [Fact]
        public void CanGet()
        {
            // Act
            Label label = _service.Get(Label.Inbox);

            // Assert
            Assert.NotNull(label);
        }

        [Fact]
        public void CanList()
        {
            // Act
            List<Label> labels = _service.List();

            // Assert
            Assert.NotNull(labels);
        }

        [Fact]
        public void NonExistingLabel_ReturnsNotFound()
        {
            // Act
            Action action = () => _service.Get(Guid.NewGuid().ToString("N"));

            // Assert
            action.ShouldThrow<GmailException>()
                .And.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
