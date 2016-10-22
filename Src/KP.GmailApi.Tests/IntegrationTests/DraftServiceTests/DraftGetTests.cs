using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftGetTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftGetTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new DraftService(proxy);

            _helper = CleanupHelpers.GetDraftServiceCleanupHelper(_service);
        }

        [Fact]
        public async Task CanGet()
        {
            // Arrange
            Draft draft = Samples.DraftSample;
            Draft createdDraft = await _helper.CreateAsync(draft);
            Draft getDraft = null;

            // Act
            Func<Task> action = async () => getDraft = await _service.GetAsync(createdDraft.Id);

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
