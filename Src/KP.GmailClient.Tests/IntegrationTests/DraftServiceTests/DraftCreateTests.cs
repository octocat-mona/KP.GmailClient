using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Models;
using Xunit;
using KP.GmailClient.Services;

namespace KP.GmailClient.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftCreateTests : IDisposable
    {
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftCreateTests()
        {
            var service = new DraftService(SettingsManager.GmailProxy);
            _helper = CleanupHelpers.GetDraftServiceCleanupHelper(service);
        }

        [Fact]
        public void CanCreate()
        {
            // Arrange
            Draft draft = Samples.DraftSample;
            Draft createdDraft = null;

            // Act
            Func<Task> action = async () => createdDraft = await _helper.CreateAsync(draft);

            // Assert
            action.Should().NotThrow();
            createdDraft.Should().NotBeNull();
        }

        public void Dispose()
        {
            _helper?.Cleanup();
        }
    }
}
