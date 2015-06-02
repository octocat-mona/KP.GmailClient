using System;
using System.Collections.Generic;
using KP.GmailApi.Models;

namespace KP.GmailApi.UnitTests
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
                         PlainRaw = "Body content",
                         Snippet = "snippet123"
                     }
                 };
            }
        }

        public static Message MessageSample
        {
            get
            {
                return new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    ThreadId = Guid.NewGuid().ToString(),
                    HistoryId = ulong.MaxValue,
                    LabelIds = { "id1", "id2" },
                    Snippet = "Snippet example containing special characters: ~`!@#$%^&*()_+=-",
                    PlainRaw = "Raw text",
                    SizeEstimate = int.MaxValue,
                    Payload = new Payload
                    {
                        PartId = "1",
                        Filename = "File 1",
                        MimeType = "text/html",
                        Body = new Attachment
                        {
                            AttachmentId = "1",
                            PlainData = "Data body",
                            Size = int.MaxValue
                        },
                        Headers = new List<Header>
                        {
                            new Header{Name = "header1",Value = "Value 1"},
                            new Header{Name = "header-2",Value = "Value-2"}
                        }
                    }
                };
            }
        }

        /*var message = new Message
          {
              PlainRaw = "test",
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
