using System;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.DraftServiceTests
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
        public async Task CanCreate()
        {
            // Arrange
            Draft draft = CreateDraft();

            // Act
            Draft createdDraft = await _helper.CreateAsync(draft);

            // Assert
            createdDraft.Should().NotBeNull();
        }

        private static Draft CreateDraft()
        {
            return new()
            {
                Message = new Message
                {
                    PlainRaw = "Body content",
                    Snippet = "snippet123"
                }
            };
        }

        public void Dispose()
        {
            _helper?.Cleanup();
        }
    }
}
