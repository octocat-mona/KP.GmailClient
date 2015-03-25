using System;
using System.Collections.Generic;
using System.Linq;
using GmailApi.Builders;
using GmailApi.DTO;

namespace GmailApi.Services
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

        public override string Build()
        {
            if (_fieldsAction != null)
                _fieldsAction();

            return base.Build();
        }
    }
}