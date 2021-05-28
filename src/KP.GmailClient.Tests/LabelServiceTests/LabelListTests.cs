using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.LabelServiceTests
{
    public class LabelListTests
    {
        private readonly LabelService _service;

        public LabelListTests()
        {
            _service = new LabelService(SettingsManager.GmailProxy);
        }

        [Fact]
        public async Task CanList()
        {
            // Act
            IList<Label> labels = await _service.ListAsync();

            // Assert
            Assert.NotNull(labels);
        }
    }
}
