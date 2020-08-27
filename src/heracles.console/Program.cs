using System;

namespace heracles.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var welcomeMessage = "Welcome to HERACLES!";
            Console.WriteLine(welcomeMessage);
            
            var inputMessage = $"{Environment.NewLine}Enter the amount to format:";
            Console.WriteLine(inputMessage);

            string outputMessage;
            try
            {
                string amount = Console.ReadLine();
                string formattedMoney = new MoneyFormatter().Format(amount);
                outputMessage = $"{Environment.NewLine}Thanks! Your formatted amount is: {formattedMoney}";
            }
            catch (ArgumentException e)
            {
                outputMessage = e.Message;
            }

            Console.WriteLine(outputMessage);

            string exitMessage = $"{Environment.NewLine}Press any key to exit Heracles...";
            Console.WriteLine(exitMessage);
            Console.ReadLine();
        }
    }
}
