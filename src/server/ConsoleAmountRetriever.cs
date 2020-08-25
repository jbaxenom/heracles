using System;

namespace server
{
    class ConsoleAmountRetriever
    {
        public virtual string GetAmount()
        {
            return Console.ReadLine();
        }
    }
}
