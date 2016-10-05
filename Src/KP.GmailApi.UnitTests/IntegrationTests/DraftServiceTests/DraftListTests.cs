using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailApi.Common;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;
using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftListTests
    {
        private readonly DraftService _service;

        public DraftListTests()
        {
            GmailProxy proxy = SettingsManager.GetGmailProxy();
            _service = new DraftService(proxy);
        }

        [Fact]
        public async Task CanListIds()
        {
            // Act
            var ids = await _service.ListIdsAsync();

            // Assert
            ids.Should().NotBeNull();
        }

        [Fact]
        public async Task CanList()
        {
            // Act
            var drafts = (await _service.ListAsync()).ToList();

            // Assert
            drafts.Should().NotBeNull();
        }
    }
}
