using ConsoleTableExt;
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
        //Open a connection to the Database
        static readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";

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
                    StudyMenu.studyCards();
                    GetUserCommand();
                    break;
                case 2:
                    ManageCardsMenu.manageCards();
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

        
        

        //internal static void addClass()
        //{
        //    //Insert new record into the classes table

        //    Console.WriteLine("\nEnter the class name\n");
        //    string name = Console.ReadLine();

        //    Console.WriteLine("\nEnter the amount of credits earned\n");
        //    string credits = inputInt();

        //    Console.WriteLine("\nEnter the class gpa\n");
        //    string gpa = inputFloat();
            
        //    //open connection to the database
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        //use the connection here
        //        var dBCommand = connection.CreateCommand();
        //        dBCommand.CommandText = $"insert into classes (name, credits, gpa) values ('{name}','{ credits}','{gpa}')";
        //        dBCommand.ExecuteNonQuery();
        //        connection.Close();
        //    }

        //    Console.WriteLine($"\n\nYour class was submitted: {name} | {credits} credits | {gpa} gpa.\n\n");
        //}

        //internal static void removeClass()
        //{
        //    Console.WriteLine("\n\nType the Id of the class you want to remove. Type 0 to return to main menu.\n\n");
       
        //    int Id = Int32.Parse(inputInt());

        //    if (Id == 0) GetUserCommand();

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        var tableCmd = connection.CreateCommand();
        //        tableCmd.CommandText = $"DELETE from classes WHERE Id = '{Id}'";
        //        int rowCount = tableCmd.ExecuteNonQuery();
        //        while (rowCount == 0)
        //        {
        //            Console.WriteLine($"\n\nClass with Id {Id} doesn't exist. Try Again or type 0 to return to main menu. \n\n");
        //            Id = Int32.Parse(inputInt());

        //            if (Id == 0) GetUserCommand();

        //            if (rowCount != 0) break;
        //        }
        //        Console.WriteLine("\n\n\nclass was removed\n\n");
        //    }
        //}

        //internal static void editClass()
        //{
        //    Console.WriteLine("\n\nType Id of the class would like to edit. Type 0 to return to main manu.\n\n");

        //    string userInput = inputInt();

        //    int Id = Convert.ToInt32(userInput);

        //    if (Id == 0) GetUserCommand();

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        var checkCmd = connection.CreateCommand();
        //        checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM classes WHERE Id = {Id})";
        //        int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

        //        if (checkQuery == 0)
        //        {
        //            Console.WriteLine($"\n\nRecord with Id {Id} doesn't exist.\n\n");
        //            editClass();
        //        }

        //        Console.WriteLine("\nEnter the class name\n");
        //        string name = Console.ReadLine();

        //        Console.WriteLine("\nEnter the amount of credits earned\n");
        //        string credits = inputInt();

        //        Console.WriteLine("\nEnter the class gpa\n");
        //        string gpa = inputFloat();

        //        var tableCmd = connection.CreateCommand();
        //        tableCmd.CommandText = $"UPDATE classes SET name = '{name}', credits = '{credits}', gpa = '{gpa}' WHERE Id = {Id}";
        //        tableCmd.ExecuteNonQuery();

        //        Console.WriteLine($"\n\nChanges to your class were successful: {name} | {credits} credits | {gpa} gpa.\n\n");
        //        connection.Close();
        //    }
        //}

    }
}
