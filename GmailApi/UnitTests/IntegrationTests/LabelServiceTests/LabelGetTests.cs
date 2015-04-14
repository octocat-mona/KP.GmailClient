using System;
using System.Collections.Generic;
using FluentAssertions;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.LabelServiceTests
{
    public class LabelGetTests
    {
        private readonly LabelService _service;

        public LabelGetTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new LabelService(client);
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
        public void NonExistingLabel_ThrowsException()
        {
            // Act
            Action action = () => _service.Get(Guid.NewGuid().ToString("N"));

            // Assert
            action.ShouldThrow<GmailException>();
        }
    }
}
