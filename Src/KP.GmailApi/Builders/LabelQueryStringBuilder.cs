using System;
using System.Linq;
using KP.GmailApi.DTO;
using KP.GmailApi.Models;

namespace KP.GmailApi.Builders
{
    internal class LabelQueryStringBuilder : QueryStringBuilder
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

                SetField("fields", fieldsValue);
            };

            return this;
        }

        /// <summary>
        /// Set action which doesn't require an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <returns>This builder</returns>
        public LabelQueryStringBuilder SetRequestAction(LabelRequestAction action)
        {
            base.SetRequestAction(action);
            return this;
        }

        /// <summary>
        /// Set action which requires an ID
        /// </summary>
        /// <param name="action">The action to set</param>
        /// <param name="id">Id of the message</param>
        /// <returns>This builder</returns>
        public LabelQueryStringBuilder SetRequestAction(LabelRequestAction action, string id)
        {
            base.SetRequestAction(action, id);
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