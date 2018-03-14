using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Services;
using KP.GmailClient.Services.Extensions;
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
        }

        [Fact(Skip = "Fails on Mono")]
        public async Task CanList()
        {
            // Act
            var drafts = (await _service.ListAsync()).ToList();

            // Assert
            drafts.Should().NotBeNull();
        }
    }
}
