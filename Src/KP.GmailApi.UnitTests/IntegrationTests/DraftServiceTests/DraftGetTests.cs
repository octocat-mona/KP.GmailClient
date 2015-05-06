﻿using System;
using FluentAssertions;
using GmailApi;
using GmailApi.Models;
using GmailApi.Services;
using Xunit;

namespace UnitTests.IntegrationTests.DraftServiceTests
{
    public class DraftGetTests : IDisposable
    {
        private readonly DraftService _service;
        private readonly CleanupHelper<Draft, Draft> _helper;

        public DraftGetTests()
        {
            GmailClient client = SettingsManager.GetGmailClient();
            _service = new DraftService(client);

            _helper = CleanupHelpers.GetDraftServiceCleanupHelper(_service);
        }

        [Fact]
        public void CanGet()
        {
            // Arrange
            Draft draft = Samples.DraftSample;
            Draft createdDraft = _helper.Create(draft);
            Draft getDraft = null;

            // Act
            Action action = () => getDraft = _service.Get(createdDraft.Id);

            // Assert
            action.ShouldNotThrow();
            getDraft.Id.Should().Be(createdDraft.Id);
        }

        public void Dispose()
        {
            _helper.Cleanup();
        }
    }
}