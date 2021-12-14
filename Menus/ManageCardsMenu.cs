using flashcardsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    class ManageCardsMenu
    {
        internal static void manageCards()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nManage flashcards");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 create, update, or delete a stack of flashcards");
            Console.WriteLine("\nType 2 to create, update, or delete flashcards in an existing stack");
            Console.WriteLine("\nType 3 to return to the main menu");

            //assign user input to a string
            string userInput = InputHelper.inputInt();

            //convert user string input to an integer
            int command = Convert.ToInt32(userInput);

            //The switch statement selects a statement list to execute based on a pattern match with a match expression
            switch (command)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    //Return to manage menu
                    manageCards();
                    break;
                case 2:
                    //Return to manage menu
                    manageCards();
                    break;
                case 3:
                    //Return to main menu
                    MainMenu.GetUserCommand();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to manage menu
                    manageCards();
                    break;
            }
        }
    }
}
