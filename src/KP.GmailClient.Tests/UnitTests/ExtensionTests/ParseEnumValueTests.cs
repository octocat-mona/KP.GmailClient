using System;
using System.ComponentModel;
using FluentAssertions;
using KP.GmailClient.Common;
using KP.GmailClient.Common.Enums;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.ExtensionTests
{
    public class ParseEnumValueTests
    {
        [Fact]
        public void CanParse()
        {
            // Arrange
            Enum actionEnum = RequestAction.Create;

            // Act
            var action = actionEnum.ParseEnumValue<RequestAction>();

            // Assert
            action.Should().BeEquivalentTo(RequestAction.Create);
        }

        [Fact]
        public void InvalidValue_ThrowsException()
        {
            // Arrange
            Enum actionEnum = (RequestAction)int.MaxValue;

            // Act
            Action action = () => actionEnum.ParseEnumValue<RequestAction>();

            // Assert
            action.Should().ThrowExactly<InvalidEnumArgumentException>();
        }

        /// <summary>
        /// Should match the name of the enum value, not the integer value.
        /// </summary>
        [Fact]
        public void WithSameValueName_ReturnsRequestedTypeValue()
        {
            // Arrange
            const LabelRequestAction labelAction = LabelRequestAction.Create; // nr. 0
            const RequestAction requestAction = RequestAction.Create; // nr. != 0

            // Act
            var action = labelAction.ParseEnumValue<RequestAction>();

            // Assert
            action.Should().BeEquivalentTo(requestAction);
        }
    }
}
