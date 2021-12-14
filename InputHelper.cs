using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    class InputHelper
    {
        public static string inputInt()
        {
            string userInput = Console.ReadLine();

            while (string.IsNullOrEmpty(userInput) || !int.TryParse(userInput, out _))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please type a numeric value");
                Console.ForegroundColor = ConsoleColor.White;
                userInput = Console.ReadLine();
            }

            return userInput;

        }

        public static string inputFloat()
        {
            string userInput = Console.ReadLine();

            while (string.IsNullOrEmpty(userInput) || !float.TryParse(userInput, out _))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please type a numeric value");
                Console.ForegroundColor = ConsoleColor.White;
                userInput = Console.ReadLine();
            }
            return userInput;
        }
    }
}
