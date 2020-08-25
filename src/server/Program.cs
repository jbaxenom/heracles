using System;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the amount to format: ");
            decimal amount = new ConsoleAmountRetriever().GetAmount();

            Console.WriteLine($"Your formatted amount is: {amount}");
        }
    }
}
