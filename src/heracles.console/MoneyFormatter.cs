using System;
using System.Globalization;

namespace heracles.console
{
    public class MoneyFormatter
    {
        public string Format(string moneyInput)
        {
            if (!decimal.TryParse(moneyInput, out decimal money) || money < 0)
            {
                throw new ArgumentException("You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits");
            }

            var format = "#,0.00";
            var customFormatter = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            customFormatter.NumberGroupSeparator = " ";

            return $"'{money.ToString(format, customFormatter)}'";
        }
    }
}

