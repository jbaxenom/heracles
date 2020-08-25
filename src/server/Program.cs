using System;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the amount to format: ");
            double amount = new ConsoleAmountRetriever().GetAmount();

            string formattedMoney = new MoneyFormatter().Format(amount);

            Console.WriteLine($"Your formatted amount is: {formattedMoney}");
        }
    }
}
