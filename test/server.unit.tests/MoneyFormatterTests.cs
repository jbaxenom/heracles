using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace server.unit.tests
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
            string moneyString = "100";
            decimal money = decimal.Parse(moneyString);

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Split(new char[] { '\'' }).Length.Should().Be(2);
        }

        [TestMethod]
        public void Should_ApproximateDecimals_ToClosestTwoDecimalValues()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            decimal money = 2310000.159897m;
            string expectedMoney = "2 310 000";
            string expectedCents = "16";

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Should().Be($"'{expectedMoney}.{expectedCents}'");

        }

        [TestMethod]
        public void Should_AddTwoDecimals_IfNoneInInput()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            decimal money = 1600;

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Split(new char[] { '\'' }).Length.Should().Be(2);
        }

        [DataTestMethod]
        [DataRow("231000", "'231 000.00'")]
        [DataRow("231000000", "'231 000 000.00'")]
        public void Should_SeparateGroups_WithSpace(string money, string expectedMoney)
        {
            // TC2.4

            // Arrange
            decimal moneyDec = decimal.Parse(money);
            MoneyFormatter formatter = new MoneyFormatter();

            // Act
            var formattedMoney = formatter.Format(moneyDec);

            // Assert
            formattedMoney.Should().Be(expectedMoney);
        }

        [TestMethod]
        public void Should_AddSingleQuotesToOutput()
        {
            // TC2.5

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            decimal money = 2310000.159897m;
            string expectedResult = "'2 310 000.16'";

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Should().Be(expectedResult);
        }
    }
}
