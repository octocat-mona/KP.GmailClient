using FluentAssertions;
using GmailApi;
using GmailApi.ServiceExtensions;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftListTests
    {
        private readonly DraftService _service;

        public DraftListTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new DraftService(client);
        }

        [Fact]
        public void CanListIds()
        {
            // Act
            var ids = _service.ListIds();

            // Assert
            ids.Should().NotBeNull();
            ids.Drafts.Should().NotBeNull();
        }

        [Fact]
        public void CanList()
        {
            // Act
            var drafts = _service.List();

            // Assert
            drafts.Should().NotBeNull();
        }
    }
}
