using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using FluentAssertions;

namespace server.unit.tests
{
    [TestClass]
    public class ConsoleTests
    {
        [TestMethod]
        public void Should_PromptUser_ToInsertAmount()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.Main(new string[] { });

            // Assert
            output.ToString().Should().Be($"Enter the amount to format: {Environment.NewLine}");
        }
    }
}