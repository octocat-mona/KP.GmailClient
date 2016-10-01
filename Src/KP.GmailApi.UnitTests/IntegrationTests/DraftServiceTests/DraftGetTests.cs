using System;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftGetTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftGetTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailClient();
            _service = new DraftService(proxy);

            _helper = CleanupHelpers.GetDraftServiceCleanupHelper(_service);
        }

        [Fact]
        public void CanGet()
        {
            // Arrange
            Draft draft = Samples.DraftSample;
            Draft createdDraft = _helper.Create(draft);
            Draft getDraft = null;

            // Act
            Action action = () => getDraft = _service.Get(createdDraft.Id);

            // Assert
            action.ShouldNotThrow();
            getDraft.Id.Should().Be(createdDraft.Id);
        }

        public void Dispose()
        {
            _helper.Cleanup();
        }
    }
}
