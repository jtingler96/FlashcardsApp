using flashcardsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    class StudyHome
    {
        internal static void studyCards()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nStudy flashcards");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 choose a stack");
            Console.WriteLine("\nType 2 to study random flashcards");
            Console.WriteLine("\nType 3 to view study session history");
            Console.WriteLine("\nType 4 to return to the main menu");

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
                    //Return to study menu
                    studyCards();
                    break;
                case 2:
                    //Return to study menu
                    studyCards();
                    break;
                case 3:
                    //Return to study menu
                    studyCards();
                    break;
                case 4:
                    //Return to main menu
                    MainMenu.GetUserCommand();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to study menu
                    studyCards();
                    break;
            }
        }
    }
}
