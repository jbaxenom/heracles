using System;

namespace server
{
    class ConsoleAmountRetriever
    {
        public virtual decimal GetAmount()
        {
            return decimal.Parse(Console.ReadLine());
        }
    }
}
