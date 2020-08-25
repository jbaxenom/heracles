using System;
using System.IO;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputMessage = "Enter the amount to format: ";
            Console.WriteLine(inputMessage);

            string outputMessage;
            try
            {
                string amount = new ConsoleAmountRetriever().GetAmount();
                string formattedMoney = new MoneyFormatter().Format(amount);
                outputMessage = $"Your formatted amount is: {formattedMoney}";
            }
            catch (ArgumentException e)
            {
                outputMessage = e.Message;
            }

            Console.WriteLine(outputMessage);
        }
    }
}
