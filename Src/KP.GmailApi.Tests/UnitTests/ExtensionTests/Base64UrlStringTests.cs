using FluentAssertions;
using KP.GmailApi.Common;
using Xunit;

namespace KP.GmailApi.Tests.UnitTests.ExtensionTests
{
    public class Base64UrlStringTests
    {
        const string TestText = "1234567890-=~!@#$%^&*()_+'\\";

        [Fact]
        public void CanEncodeDecode()
        {
            // Act
            string encoded = TestText.ToBase64UrlString();
            string decoded = encoded.FromBase64UrlString();

            // Assert
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
