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
        private readonly ServiceItemHelper<Draft, Draft> _helper;

        public DraftCreateTests()
        {

            GmailClient client = SettingsManager.GetGmailClient();
            _service = new DraftService(client);

            Action<Draft> deleteAction = label => _service.Delete(label.Id);
            Func<Draft, Draft> createAction = input => _service.Create(input);
            _helper = new ServiceItemHelper<Draft, Draft>(createAction, deleteAction);
        }

        //[Fact]
        public void CanCreate()
        {
            Message message = new Message();
            message.Raw = "<div>bla</div>".ToBase64UrlString();
            message.Snippet = "snippet123";
            Draft draft = new Draft();
            draft.Message = message;

            Draft createdDraft = _helper.Create(draft);
        }

        public void Dispose()
        {
            _helper.Cleanup();
        }
    }
}
