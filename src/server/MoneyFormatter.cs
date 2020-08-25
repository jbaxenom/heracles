using System;

namespace server
{
    public class MoneyFormatter
    {
        public string Format(double moneyInput)
        {
            return moneyInput.ToString("F2");
        }
    }
}

