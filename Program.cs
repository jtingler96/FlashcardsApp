using System;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using FlashcardsApp;
using flashcardsApp.Models;
using System.Linq;

namespace flashcardsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to Flashcards!\n");
            Console.WriteLine("\nChecking for existing database...\n");

            //check for an existing database
            string databasePath = ConfigurationManager.AppSettings.Get("DatabasePath");
            bool dbPath = File.Exists(databasePath);

            //If the database does not exist, create one
            if (!dbPath)
            {
                Console.WriteLine("\n\nDatabase doesn't exist, creating one...\n\n");
                SqlHelper.InitializeDB();
            }
            //If a database exists continue to the controller
            else
            {
                Console.WriteLine("\nInitializing database..\n");
                FlashcardController.GetUserCommand();
            };
        }
    }
}