using System;
using FluentAssertions;
using GmailApi;
using Xunit;

namespace UnitTests.UnitTests.ExtensionTests
{
    public class GetAttributeTests
    {
        public const string AttributeText = "Text";

        [Fact]
        public void CanGetGetAttribute()
        {
            // Arrange
            const TestEnum test = TestEnum.ValueWithAttribute;

            // Act
            TestAttribute attribute = test.GetAttribute<TestAttribute, TestEnum>();

            // Assert
            attribute.Text.Should().Be(AttributeText);
        }

        [Fact]
        public void NoAttribute_ReturnsNull()
        {
            // Arrange
            const TestEnum test = TestEnum.ValueWithoutAttribute;

            // Act
            TestAttribute attribute = test.GetAttribute<TestAttribute, TestEnum>();

            // Assert
            attribute.Should().BeNull();
        }
    }

    public enum TestEnum
    {
        [Test(GetAttributeTests.AttributeText)]
        ValueWithAttribute,

        ValueWithoutAttribute
    }

    public class TestAttribute : Attribute
    {
        public string Text { get; set; }

        public TestAttribute(string value)
        {
            Text = value;
        }
    }
}
