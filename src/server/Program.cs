using System;
using System.IO;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var welcomeMessage = "Welcome to HERACLES!";
            Console.WriteLine(welcomeMessage);
            
            var inputMessage = "\nEnter the amount to format: ";
            Console.WriteLine(inputMessage);

            string outputMessage;
            try
            {
                string amount = new ConsoleAmountRetriever().GetAmount();
                string formattedMoney = new MoneyFormatter().Format(amount);
                outputMessage = $"\nThanks! Your formatted amount is: {formattedMoney}";
            }
            catch (ArgumentException e)
            {
                outputMessage = e.Message;
            }

            Console.WriteLine(outputMessage);

            string exitMessage = "\nPress any key to exit Heracles...";
            Console.WriteLine(exitMessage);
            Console.ReadKey();
        }
    }
}
