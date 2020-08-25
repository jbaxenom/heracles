using System;

namespace server
{
    class ConsoleAmountRetriever
    {
        public virtual double GetAmount()
        {
            return double.Parse(Console.ReadLine());
        }
    }
}
