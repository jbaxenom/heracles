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
            double money = double.Parse(moneyString);

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Length.Should().Be(2);
        }

        [TestMethod]
        public void Should_ApproximateDecimals_ToClosestTwoDecimalValues()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            double money = 2310000.159897;
            string expectedMoney = "2310000";
            string expectedCents = "16";

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Should().Be($"{expectedMoney}.{expectedCents}");

        }

        [TestMethod]
        public void Should_AddTwoDecimals_IfNoneInInput()
        {
            // TC2.3, TC3

            // Arrange
            MoneyFormatter formatter = new MoneyFormatter();
            double money = 1600;

            // Act
            var formattedMoney = formatter.Format(money);

            // Assert
            formattedMoney.Split(new char[] { '.' })[1].Length.Should().Be(2);
        }
    }
}
