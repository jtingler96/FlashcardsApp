using flashcardsApp;
using flashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    studyByStack();
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
        internal static void studyByStack()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("\n");
                Console.WriteLine("Enter the ID of the stack of cards you want to view");

                string userInput = InputHelper.inputInt();

                int stackId = Convert.ToInt32(userInput);

                string sql = $"SELECT f.id, f.cardfront, f.cardback, s.stackname FROM flashcards f JOIN stacks s ON s.stackid = f.stackid AND s.stackid = {stackId}";

                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                List<string> tableData = new List<string>();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string stackName = reader.GetString(3);
                    Console.Clear();
                    Console.WriteLine($"You picked {stackName}! Great Choice.");
                    Console.WriteLine("Press any key to begin studying");
                    Console.ReadLine();
                    Console.Clear();

                    //Since the reader was already called, the while loop will skip over the first card if it is not called now
                    string cardFrontOne = reader.GetString(1);
                    Console.WriteLine($"{cardFrontOne}");
                    Console.WriteLine("\n\n\n\n\npress any key to view the answer");
                    Console.ReadLine();

                    string cardBackOne = reader.GetString(2);
                    Console.WriteLine($"\n\n\n\n\n{cardBackOne}");
                    Console.ReadLine();
                    Console.Clear();
                    while (reader.Read())
                    {
                        string cardFront = reader.GetString(1);
                        tableData.Add(cardFront);
                        Console.WriteLine($"{cardFront}");
                        Console.WriteLine("\n\n\n\n\npress any key to view the answer");
                        Console.ReadLine();

                        string cardBack = reader.GetString(2);
                        tableData.Add(cardBack);
                        Console.WriteLine($"\n\n\n\n\n{cardBack}");
                        Console.ReadLine();
                    }
                }
                else
                {
                  
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No rows found");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                reader.Close();



            }
        }
    }
}
