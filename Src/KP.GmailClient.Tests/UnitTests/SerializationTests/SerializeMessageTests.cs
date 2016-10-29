using System;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace KP.GmailClient.Tests.UnitTests.SerializationTests
{
    public class SerializeMessageTests
    {
        [Fact]
        public void CanSerialize()
        {
            // Arrange
            var msg = Samples.MessageSample;

            // Act
            Action action = () => JsonConvert.SerializeObject(msg);

            // Assert
            action.ShouldNotThrow();
        }
    }
}
