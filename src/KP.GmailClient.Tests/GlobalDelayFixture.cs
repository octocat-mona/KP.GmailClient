using System;
using System.Threading.Tasks;
using KP.GmailClient.IntegrationTests;
using Xunit;

[assembly: AssemblyFixture(typeof(GlobalDelayFixture))]

namespace KP.GmailClient.IntegrationTests;

public class GlobalDelayFixture : IAsyncDisposable
{
    private const string XunitDelayVariableName = "XUNIT_CLASS_DELAY_MS";

    private static readonly TimeSpan Delay = TimeSpan.FromSeconds(int.TryParse(Environment.GetEnvironmentVariable(XunitDelayVariableName), out var value) ? value : 1);

    public async ValueTask DisposeAsync()
    {
        await Task.Delay(Delay);
    }
}