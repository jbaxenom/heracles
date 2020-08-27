using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using heracles.console;

namespace heracles.unit.tests
{
    [TestClass]
    public class MoneyFormatterTests
    {
        [TestMethod]
        public void Should_ReturnString_WithTwoDecimalPoints()
        {
            // TC2.1, TC2.2
                
            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = "100";

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Split(new char[] { '\'' }).Length.Should().Be(2);
        }

        [TestMethod]
        public void Should_ApproximateDecimals_ToClosestTwoDecimalValues()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = "2310000.159897";
            string expectedMoney = "2 310 000";
            string expectedCents = "16";

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Should().Be($"'{expectedMoney}.{expectedCents}'");

        }

        [TestMethod]
        public void Should_AddTwoDecimals_IfNoneInInput()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = "1600";

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Split(new char[] { '\'' }).Length.Should().Be(2);
        }

        [DataTestMethod]
        [DataRow("231000", "'231 000.00'")]
        [DataRow("231000000", "'231 000 000.00'")]
        public void Should_SeparateGroups_WithSpace(string input, string expectedMoney)
        {
            // TC2.4

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Should().Be(expectedMoney);
        }

        [TestMethod]
        public void Should_AddSingleQuotesToOutput()
        {
            // TC2.5

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = "2310000.159897";
            string expectedResult = "'2 310 000.16'";

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Should().Be(expectedResult);
        }

        [DataTestMethod]
        [DataRow("-2310000.159897")]
        [DataRow("jashgdkjhasdhg")]
        [DataRow(" ")]
        [DataRow("823165476123581236523876531658136531265")]
        public void Should_ShowError_IfWrongInput(string badInput)
        {
            // TC4..7

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();

            // Act
            Action act = () => formatter.Format(badInput);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits");
        }

        [TestMethod]
        public void Should_ShowError_IfNoInput()
        {
            // TC8

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = Environment.NewLine;

            // Act
            Action act = () => formatter.Format(input);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits");
        }

        [TestMethod]
        public void Should_Work_IfInputVeryBigNumberBelow30Digits()
        {
            // TC7

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            string input = "8231654761235812365238765316";
            string expectedResult = "'8 231 654 761 235 812 365 238 765 316.00'";

            // Act
            var formattedMoney = formatter.Format(input);

            // Assert
            formattedMoney.Should().Be(expectedResult);
        }
    }
}
