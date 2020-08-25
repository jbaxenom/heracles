using System;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputMessage = "Enter the amount to format: ";
            Console.WriteLine(inputMessage);
            decimal amount = new ConsoleAmountRetriever().GetAmount();

            string formattedMoney = new MoneyFormatter().Format(amount);

            var outputMessage = $"Your formatted amount is: {formattedMoney}";
            Console.WriteLine(outputMessage);
        }
    }
}
