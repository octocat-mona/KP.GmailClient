using System;
using System.Collections.Generic;
using KP.GmailClient.Models;

namespace KP.GmailClient.Tests
{
    public class Samples
    {
        public static Draft DraftSample
        {
            get
            {
                return new()
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
                return new()
                {
                    Id = Guid.NewGuid().ToString(),
                    ThreadId = Guid.NewGuid().ToString(),
                    HistoryId = int.MaxValue.ToString(),
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
                            new() {Name = "header1",Value = "Value 1"},
                            new() {Name = "header-2",Value = "Value-2"}
                        }
                    }
                };
            }
        }
    }
}
