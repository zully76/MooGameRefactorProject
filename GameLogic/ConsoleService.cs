using System;
using System.Linq;

namespace MooGameRefactorProject.Services
{
    public class ConsoleService
    {
        public string GetUserName()
        {
            Console.WriteLine("Enter your user name: \n");
            return Console.ReadLine();
        }

        public string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public bool IsValidInput(string input)
        {
            return input != null && input.Length == 4 && input.All(char.IsDigit);
        }
    }
}
