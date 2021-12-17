using ConsoleTableExt;
using flashcardsApp;
using flashcardsApp.Models;
using FlashcardsApp.Menus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp
{
    class ManagerHome
    {
        internal static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
        internal static void managerMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nManager menu");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 to manage stacks");
            Console.WriteLine("\nType 2 to manage flashcards");
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
                    ManageStacks.manageStacks();
                    //Return to manage menu
                    managerMenu();
                    break;
                case 2:
                    ManageCards.manageCards();
                    //Return to manage menu
                    managerMenu();
                    break;
                case 3:
                    //Return to main menu
                    MainMenu.GetUserCommand();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to manage menu
                    managerMenu();
                    break;
            }
        }
    }
}
