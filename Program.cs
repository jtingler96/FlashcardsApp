using System;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using flashcardsApp;
using flashcardsApp.Models;
using System.Linq;

namespace flashcardsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nInitializing database\n");
            Console.ForegroundColor = ConsoleColor.White;

            SqlHelper.createDb();
            
        }
    }
}