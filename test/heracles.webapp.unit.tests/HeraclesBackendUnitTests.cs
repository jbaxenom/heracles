using FluentAssertions;
using heracles.webapp.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace heracles.webapp.unit.tests
{
    [TestClass]
    public class HeraclesBackendUnitTests
    {
        [TestMethod]
        public void MoneyController_Should_ProvideValidResponseWithValidInput()
        {
            // Arrange
            string input = "1600";
            var loggerStub = new Mock<ILogger<MoneyController>>();

            // Act
            var money = new MoneyController(loggerStub.Object).GetFormattedMoney(input).Value;

            // Assert
            money.Should().Be("'1 600.00'");
        }

        [TestMethod]
        public void MoneyController_Should_ProvideValidErrorWithInValidInput()
        {
            // Arrange
            string input = "ABCD";
            var loggerStub = new Mock<ILogger<MoneyController>>();

            // Act
            var money = new MoneyController(loggerStub.Object).GetFormattedMoney(input).Value;

            // Assert
            money.Should().Be("You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits");
        }
    }
}
