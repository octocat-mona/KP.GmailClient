using FluentAssertions;
using KP.GmailClient.Common;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.ExtensionTests
{
    public class Base64UrlStringTests
    {
        private const string TestText = "1234567890-=~!@#$%^&*()_+'\\";

        [Fact]
        public void CanEncode()
        {
            // Act
            string encoded = TestText.ToBase64UrlString();

            // Assert
            string decoded = encoded.FromBase64UrlString();
            decoded.Should().Be(TestText);
        }

        [Fact]
        public void ToBase64UrlString_DoesNotContainIllegalChars()
        {
            // Act
            string encoded = TestText.ToBase64UrlString();

            // Assert
            encoded.Should().NotContain("+");
            encoded.Should().NotContain("/");
        }
    }
}
