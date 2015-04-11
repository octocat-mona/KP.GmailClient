using System;
using System.Collections.Generic;
using System.Linq;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.LabelServiceTests
{
    public class LabelUpdateTests : IDisposable
    {
        private const string TestLabel = "Testing/";
        private readonly LabelService _service;
        private readonly List<Label> _labels = new List<Label>();

        public LabelUpdateTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new LabelService(client);
        }

        [Fact]
        public void CanUpdate()
        {
            // Arrange
            var random = new Random();
            Label createdLabel = _service.Create(new CreateLabelInput(TestLabel + random.Next()));
            _labels.Add(createdLabel);
            string newName = TestLabel + random.Next();

            // Act
            Label label = _service.Update(new UpdateLabelInput(createdLabel.Id) { Name = newName });

            // Assert
            Assert.NotNull(label);
            Assert.Equal(createdLabel.Id, label.Id);
            Assert.Equal(newName, label.Name);
        }

        public void Dispose()
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (var label in _labels)
            {
                try
                {
                    _service.Delete(label.Id);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                string errorMessages = string.Join(",", exceptions.Select(e => e.Message));
                throw new Exception("Could not cleanup Labels: " + errorMessages);
            }
        }
    }
}
