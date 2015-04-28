using GmailApi.Models;

namespace UnitTests
{
    public class Samples
    {
        public static Draft DraftSample
        {
            get
            {
                return new Draft
                 {
                     Message = new Message
                     {
                         DecodedRaw = "Body content",
                         Snippet = "snippet123"
                     }
                 };
            }
        }

        /*var message = new Message
          {
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
          };*/
    }
}
