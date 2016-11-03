using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftGetTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly CleanupHelper<Draft, Draft> _helper;
        private readonly GmailProxy _proxy;

        public DraftGetTests()
        {
            _proxy = SettingsManager.GetGmailProxy();
            _service = new DraftService(_proxy);
            _helper = CleanupHelpers.GetDraftServiceCleanupHelper(_service);
        }

        [Fact(Skip = "Fails on Mono")]
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
            _helper?.Cleanup();
            _proxy?.Dispose();
        }
    }
}
