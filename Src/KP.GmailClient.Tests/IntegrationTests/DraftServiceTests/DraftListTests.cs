using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Services;
using KP.GmailClient.Services.Extensions;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests.DraftServiceTests
{
    public class DraftListTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly GmailProxy _proxy;

        public DraftListTests()
        {
            _proxy = SettingsManager.GetGmailProxy();
            _service = new DraftService(_proxy);
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

        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
