using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Models;
using Xunit;
using KP.GmailClient.Common;
using KP.GmailClient.Services;

namespace KP.GmailClient.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftCreateTests : IDisposable
    {
        private readonly CleanupHelper<Draft, Draft> _helper;
        private readonly GmailProxy _proxy;

        public DraftCreateTests()
        {
            _proxy = SettingsManager.GetGmailProxy();
            var service = new DraftService(_proxy);
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
            action.ShouldNotThrow();
            createdDraft.Should().NotBeNull();
        }

        public void Dispose()
        {
            _helper?.Cleanup();
            _proxy?.Dispose();
        }
    }
}
