using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftListTests
    {
        private readonly DraftService _service;

        public DraftListTests()
        {
            _service = new DraftService(SettingsManager.GmailProxy);
        }

        [Fact]
        public async Task CanListIds()
        {
            // Act
            var ids = await _service.ListIdsAsync();

            // Assert
            ids.Should().NotBeNull();
            ids.Drafts.Should().NotBeNull();
        }

        [Fact]
        public async Task CanList()
        {
            // Act
            var drafts = await _service.ListAsync();

            // Assert
            drafts.Should().NotBeNull();
        }
    }
}
