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

            var input = string.Join(Environment.NewLine, new[]
            {
                "100",
                Environment.NewLine
            });

            Console.SetIn(new StringReader(input));

            // Act
            Program.Main(new string[] { });

            // Assert
            var expectedMessage = $"Welcome to HERACLES!{Environment.NewLine}{Environment.NewLine}Enter the amount to format:{Environment.NewLine}{Environment.NewLine}Thanks! Your formatted amount is: '100.00'{Environment.NewLine}{Environment.NewLine}Press any key to exit Heracles...{Environment.NewLine}";
            output.ToString().Should().Be(expectedMessage);
        }
    }
}