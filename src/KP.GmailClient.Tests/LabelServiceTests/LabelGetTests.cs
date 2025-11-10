using System;
using System.Net;
using System.Threading.Tasks;
using AwesomeAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.LabelServiceTests;

public class LabelGetTests
{
    private readonly LabelService _service;

    public LabelGetTests()
    {
        _service = new LabelService(SettingsManager.GmailProxy);
    }

    [Fact]
    public async Task CanGet()
    {
        // Act
        Label label = await _service.GetAsync(Label.Inbox);

        // Assert
        Assert.NotNull(label);
    }

    [Fact]
    public async Task NonExistingLabel_ReturnsNotFound()
    {
        // Act
        async Task Action() => await _service.GetAsync(Guid.NewGuid().ToString("N"));

        // Assert
        var ex = await Assert.ThrowsAsync<GmailApiException>(Action);
        ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}