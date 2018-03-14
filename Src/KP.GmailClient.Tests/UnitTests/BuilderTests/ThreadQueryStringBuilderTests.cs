using System;
using System.ComponentModel;
using System.Web;
using FluentAssertions;
using KP.GmailClient.Builders;
using KP.GmailClient.Common.Enums;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.BuilderTests
{
    public class ThreadQueryStringBuilderTests
    {
        [Fact]
        public void CanBuild()
        {
            // Arrange
            var builder = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.List)
                .SetFormat(ThreadFormat.Minimal)
                .SetMetadataHeaders(new[] { "header1", "header2" });

            // Act
            string queryString = builder.Build();

            // Assert
            queryString.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetHeaders_OverwritesFormat()
        {
            // Act
            string queryString = new ThreadQueryStringBuilder()
                .SetFormat(ThreadFormat.Minimal)
                .SetMetadataHeaders(new[] { "header1" })
                .Build();

            // Assert
            queryString = queryString.Substring(queryString.IndexOf('?'));
            var collection = HttpUtility.ParseQueryString(queryString);
            string value = collection["format"];

            value.Should().Be(ThreadFormat.Metadata.ToString());
        }

        [Fact]
        public void WithoutGetId_ThrowsException()
        {
            // Act
            Action action = () => new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Get)
                .Build();

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void InvalidRequestAction_ThrowsException()
        {
            // Act
            Action action = () => new ThreadQueryStringBuilder()
                .SetRequestAction((ThreadRequestAction)int.MaxValue)
                .Build();

            // Assert
            action.Should().Throw<InvalidEnumArgumentException>();
        }
    }
}
