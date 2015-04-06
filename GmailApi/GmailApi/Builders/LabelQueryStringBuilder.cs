using System;
using System.Collections.Generic;
using System.Linq;
using GmailApi.DTO;
using GmailApi.Models;

namespace GmailApi.Builders
{
    public class LabelQueryStringBuilder : QueryStringBuilder
    {
        private Action _fieldsAction;

        public LabelQueryStringBuilder()
        {
            Path = "labels";
        }

        public LabelQueryStringBuilder SetFields(LabelFields fields)
        {
            _fieldsAction = () =>
            {
                if (fields.HasFlag(LabelFields.All))// All fields are default
                    return;

                var labelFields = fields.GetFlagEnumValues()
                    .Select(f => f.GetAttribute<StringValueAttribute, LabelFields>())
                     .Where(att => att != null)
                    .Select(att => att.Text)
                    .ToList();

                string fieldsValue = string.Concat("labels(", string.Join(",", labelFields), ")");

                Dictionary["fields"] = new List<string>(new[] { fieldsValue });
            };

            return this;
        }

        public LabelQueryStringBuilder SetRequestAction(LabelRequestAction action, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID is required", "id");

            switch (action)
            {
                //default: Create, List
                case LabelRequestAction.Patch:
                case LabelRequestAction.Get:
                case LabelRequestAction.Delete:
                case LabelRequestAction.Update:
                    Path += "/" + id;
                    break;
            }

            return this;
        }

        public override string Build()
        {
            if (_fieldsAction != null)
                _fieldsAction();

            return base.Build();
        }
    }
}