using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.IntegrationTests.ThreadServiceTests;

public class ThreadListTests : IClassFixture<GlobalDelayFixture>
{
    private readonly ThreadService _service;

    public ThreadListTests()
    {
        _service = new ThreadService(SettingsManager.GmailProxy);
    }

    //[Fact] TODO: implement
    public void CanListIds()
    {
        // Act
        Func<Task> action = async () => await _service.ListIdsAsync();

        // Assert
        action.Should().NotThrowAsync();
    }

    //[Fact] TODO: implement
    public void CanList()
    {
        // Act
        Func<Task> action = async () => await _service.ListAsync();

        // Assert
        action.Should().NotThrowAsync();
    }
}