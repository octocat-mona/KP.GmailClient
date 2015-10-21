using System;
using FluentAssertions;
using KP.GmailApi.Builders;
using KP.GmailApi.Common.Enums;
using KP.GmailApi.Models;
using Xunit;

namespace KP.GmailApi.UnitTests.UnitTests.BuilderTests
{
    public class LabelBuilderTests
    {
        private const string Path = "labels";

        [Fact]
        public void EmptyLabel_ThrowsError()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, string.Empty)
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void AllLabelFields_ReturnsNoValue()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetFields(LabelFields.All)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionCreate_CanSet()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Create)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionCreate_CannotSetWithId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Create, "id")
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void RequestActionList_CanSet()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.List)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionList_CannotSetWithId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.List, "id")
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void RequestActionGet_CanSet()
        {
            // Act
            const string id = "id";
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, id)
                .Build();

            // Assert
            queryString.Should().Be(Path + "/" + id);
        }

        [Fact]
        public void RequestActionGet_CannotSetWithoutId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get)
                .Build();

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
    }
}
