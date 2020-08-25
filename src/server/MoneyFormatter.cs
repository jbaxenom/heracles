using System.Globalization;

namespace server
{
    public class MoneyFormatter
    {
        public string Format(decimal moneyInput)
        {
            var format = "#,0.00";
            var customFormatter = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            customFormatter.NumberGroupSeparator = " ";            
            
            return moneyInput.ToString(format, customFormatter);
        }
    }
}

