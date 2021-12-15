﻿using ConsoleTableExt;
using FlashcardsApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcardsApp
{
    internal static class MainMenu
    {
        //Give the user some instructions and display an options menu
        internal static void GetUserCommand()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nWelcome to StudyConsole\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 study flashcards");
            Console.WriteLine("\nType 2 to manage existing flashcards");

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
                    //Return to main menu
                    StudyHome.studyCards();
                    GetUserCommand();
                    break;
                case 2:
                    ManagerHome.managerMenu();
                    //Return to main menu
                    GetUserCommand();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to main menu
                    GetUserCommand();
                    break;
            }
        }
    }
}
