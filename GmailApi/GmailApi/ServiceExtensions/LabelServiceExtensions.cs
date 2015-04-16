﻿using System.Collections.Generic;
using System.Linq;
using GmailApi.Models;
using GmailApi.Services;

namespace GmailApi.ServiceExtensions
{
    public static class LabelServiceExtensions
    {
        /// <summary>
        /// Lists all labels in the user's mailbox of the given type.
        /// </summary>
        /// <param name="service">Gmail API service instance</param>
        /// <param name="type"></param>
        /// <returns>A list of Labels</returns>
        public static List<Label> List(this LabelService service, LabelType type)
        {
            return service.List()
                .Where(l => l.Type == type)
                .ToList();
        }

    }
}
