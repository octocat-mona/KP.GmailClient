using System;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftCreateTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftCreateTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new DraftService(client);

            Action<Draft> deleteAction = label => _service.Delete(label.Id);
            Func<Draft, Draft> createAction = input => _service.Create(input);
            _helper = new CleanupHelper<Draft, Draft>(createAction, deleteAction);
        }

        [Fact]
        public void CanCreate()
        {
            // Arrange
            Draft draft = new Draft
            {
                Message = new Message
                {
                    DecodedRaw = "<div>text</div>",
                    Snippet = "snippet123"
                }
            };

            // Act
            _helper.Create(draft);
        }

        public void Dispose()
        {
            _helper.Cleanup();
        }
    }
}
