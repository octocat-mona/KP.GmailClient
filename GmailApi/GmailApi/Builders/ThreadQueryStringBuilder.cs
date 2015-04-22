﻿using System;
using System.Linq;
using GmailApi.DTO;

namespace GmailApi.Builders
{
    internal class ThreadQueryStringBuilder : QueryStringBuilder
    {
        public ThreadQueryStringBuilder()
        {
            Path = "threads";
        }

        public ThreadQueryStringBuilder SetFormat(ThreadFormat format)
        {
            base.SetFormat(format);
            return this;
        }

        /// <summary>
        /// Set action which doesn't require an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <returns>This builder</returns>
        public ThreadQueryStringBuilder SetRequestAction(ThreadRequestAction action)
        {
            base.SetRequestAction(action);
            return this;
        }

        public ThreadQueryStringBuilder SetFields(ThreadFields fields)
        {
            throw new NotImplementedException();//TODO:
            return this;
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <param name="id">Id of the message</param>
        /// <returns>This builder</returns>
        public ThreadQueryStringBuilder SetRequestAction(ThreadRequestAction action, string id)
        {
            base.SetRequestAction(action, id);

            return this;
        }

        public ThreadQueryStringBuilder SetMetadataHeaders(string[] headers)
        {
            if (!headers.Any())
                throw new ArgumentException("Collection can't be empty", "headers");

            SetFormat(ThreadFormat.Metadata);

            SetField("metadataHeaders", headers);
            return this;
        }
    }
}