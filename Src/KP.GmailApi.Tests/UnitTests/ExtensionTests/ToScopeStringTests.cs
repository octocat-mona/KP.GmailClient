using FluentAssertions;
using KP.GmailApi.Common;
using Xunit;

namespace KP.GmailApi.Tests.UnitTests.ExtensionTests
{
    public class ToScopeStringTests
    {
        [Fact]
        public void CanConvert()
        {
            // Arrange
            const GmailScopes scopes = GmailScopes.Readonly;
            string readOnlyScope = GmailHelper.GetGmailScopesField("ReadOnlyScope");

            // Act
            string scopeString = scopes.ToScopeString();

            // Assert
            scopeString.ShouldBeEquivalentTo(readOnlyScope);
        }
    }
}
