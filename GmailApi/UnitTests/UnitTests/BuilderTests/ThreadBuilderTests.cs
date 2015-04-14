using System;
using System.ComponentModel;
using System.Web;
using FluentAssertions;
using GmailApi.Builders;
using GmailApi.DTO;
using Xunit;

namespace UnitTests.UnitTests.BuilderTests
{
    public class ThreadBuilderTests
    {
        [Fact]
        public void CanBuild()
        {
            // Act
            string queryString = new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.List)
                .SetFormat(ThreadFormat.Minimal)
                .SetMetadataHeaders(new[] { "header1", "header2" })
                .Build();

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

            // Arrange
            queryString = queryString.Substring(queryString.IndexOf('?'));
            var collection = HttpUtility.ParseQueryString(queryString);
            string value = collection["format"];

            // Assert
            value.Should().BeEquivalentTo(ThreadFormat.Metadata.ToString());
        }

        [Fact]
        public void WithoutGetId_ThrowsException()
        {
            // Act
            Action action = () => new ThreadQueryStringBuilder()
                .SetRequestAction(ThreadRequestAction.Get)
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void InvalidRequestAction_ThrowsException()
        {
            // Act
            Action action = () => new ThreadQueryStringBuilder()
                .SetRequestAction((ThreadRequestAction)int.MaxValue)
                .Build();

            // Assert
            action.ShouldThrow<InvalidEnumArgumentException>();
        }
    }
}
