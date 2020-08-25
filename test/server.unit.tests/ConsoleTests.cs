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
        public void Should_PromptUser_ToInsertAmountAndCaptureIt()
        {
            // TC1

            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("100");
            Console.SetIn(input);

            // Act
            Program.Main(new string[] { });

            // Assert
            output.ToString().Should().Be($"Enter the amount to format: {Environment.NewLine}Your formatted amount is: '100.00'{Environment.NewLine}");
        }
    }
}