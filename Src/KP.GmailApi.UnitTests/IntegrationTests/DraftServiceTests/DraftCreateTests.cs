using System;
using FluentAssertions;
using GmailApi.Models;
using Xunit;

namespace UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftCreateTests : IDisposable
    {
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftCreateTests()
        {
            _helper = CleanupHelpers.GetDraftServiceCleanupHelper();
        }

        [Fact]
        public void CanCreate()
        {
            // Arrange
            Draft draft = Samples.DraftSample;
            Draft createdDraft = null;

            // Act
            Action action = () => createdDraft = _helper.Create(draft);

            // Assert
            action.ShouldNotThrow();
            createdDraft.Should().NotBeNull();
        }

        public void Dispose()
        {
            _helper.Cleanup();
        }
    }
}
