using System.Collections.Generic;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;

namespace UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftGetTests
    {
        private readonly DraftService _service;

        public DraftGetTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new DraftService(client);
        }

        //[Fact]
        public void CanGet()
        {
            var message = new Message
            {
                Id = "1",
                DecodedRaw = "test",
                Payload = new Payload
                {
                    MimeType = "multipart/alternative",
                    Parts = new List<PayloadBase>
                    {
                        new Payload
                        {
                            Body = new Attachment { AttachmentId = "" ,Data = "test".ToBase64UrlString()},
                            MimeType = "text/html",
                            PartId = "0",
                            Headers = new List<Header>
                            {
                                new Header
                                {
                                    Name = "Content-Type",
                                    Value = "text/html; charset=UTF-8"
                                }
                            },
                        }
                    }
                }
            };

            _service.Create(new Draft
            {
                Id = "1",
                Message = message
            });
        }
    }
}
