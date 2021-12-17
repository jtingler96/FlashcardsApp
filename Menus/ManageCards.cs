using ConsoleTableExt;
using flashcardsApp.Models;
using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp.Menus
{
    class ManageCards
    {
        internal static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
        internal static void manageCards()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nManage flashcards");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 view flashcards in a stack");
            Console.WriteLine("\nType 2 to create flashcards");
            Console.WriteLine("\nType 3 to edit flashcards");
            Console.WriteLine("\nType 4 to delete flashcards");
            Console.WriteLine("\nType 5 to return to the manager menu");

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
                    viewCards();
                    //Return to manage menu
                    manageCards();
                    break;
                case 2:
                    createCards();
                    //Return to manage menu
                    manageCards();
                    break;
                case 3:
                    editCards();
                    //Return to main menu
                    manageCards();
                    break;
                case 4:
                    deleteCards();
                    //Return to main menu
                    manageCards();
                    break;
                case 5:
                    //Return to main menu
                    ManagerHome.managerMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to manage menu
                    manageCards();
                    break;
            }
        }

        internal static void viewCards()
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

                List<StackOfFlashcards> tableData = new List<StackOfFlashcards>();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new StackOfFlashcards
                        {
                            Id = reader.GetInt32(0),
                            CardFront = reader.GetString(1),
                            CardBack = reader.GetString(2),
                            StackName = reader.GetString(3),
                        });
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

                Console.ForegroundColor = ConsoleColor.Yellow;
                ConsoleTableBuilder
                    .From(tableData)
                    .ExportAndWriteLine();
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void createCards()
        {
            //Insert new record into the Stacks table

            Console.WriteLine("\nEnter the ID of the stack you would like to add to.\n");
            string stackId = Console.ReadLine();

            Console.WriteLine("\nWhat would you like the front of the card to say?\n");
            string cardFront = Console.ReadLine();

            Console.WriteLine("\nWhat would you like the back of the card to say?\n");
            string cardBack = Console.ReadLine();

            //open connection to the database
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO flashcards (cardfront, cardback, stackid) values ('{cardFront}','{cardBack}','{stackId}')";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("cardfront", SqlDbType.VarChar);
                cmd.Parameters["cardfront"].Value = cardFront;
                cmd.Parameters.Add("cardback", SqlDbType.VarChar);
                cmd.Parameters["cardback"].Value = cardBack;
                cmd.Parameters.Add("stackid", SqlDbType.Int);
                cmd.Parameters["stackid"].Value = stackId;
                cmd.ExecuteNonQuery();

                Console.WriteLine($"\n\nYour card was added to stack with ID: {stackId},\n the front of the card says: {cardFront},\n and the back of the card says: {cardBack}\n\n");

            }
        }

        internal static void editCards()
        {
            Console.WriteLine("\n\nType Id of the flashcard would like to edit. Type 0 to return to the menu.\n\n");

            string userInput = InputHelper.inputInt();

            int Id = Convert.ToInt32(userInput);

            if (Id == 0) manageCards();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT id, cardfront, cardback, stackid FROM flashcards where id = {Id}";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {Id} doesn't exist.\n\n");
                    editCards();
                }

                Console.WriteLine("\nEnter the ID of the stack you would like to add to.\n");
                string stackId = Console.ReadLine();

                Console.WriteLine("\nWhat would you like the front of the flashcard to say?\n");
                string cardFront = Console.ReadLine();

                Console.WriteLine("\nWhat would you like the back of the flashcard to say?\n");
                string cardBack = Console.ReadLine();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE flashcards SET cardfront = '{cardFront}', cardback = '{cardBack}', stackid = '{stackId}' WHERE id = '{Id}'";
                tableCmd.ExecuteNonQuery();

                Console.WriteLine($"\n\nYour flashcard was updated and added to the stack with an ID of: {stackId},\n the front of the card says: {cardFront},\n and the back of the card says: {cardBack}\n\n");
                connection.Close();
            }
        }
        internal static void deleteCards()
        {
            Console.WriteLine("\n\nType the Id of the flashcard you want to remove. Type 0 to return to the menu.\n\n");

            int Id = Int32.Parse(InputHelper.inputInt());

            if (Id == 0) manageCards();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from flashcards WHERE id = '{Id}'";
                int rowCount = tableCmd.ExecuteNonQuery();
                while (rowCount == 0)
                {
                    Console.WriteLine($"\n\nClass with Id {Id} doesn't exist. Try Again or type 0 to return to main menu. \n\n");
                    Id = Int32.Parse(InputHelper.inputInt());

                    if (Id == 0) deleteCards();

                    if (rowCount != 0) break;
                }
                Console.WriteLine($"\n\nThe flashcard with an ID of: {Id} was removed\n\n");
            }
        }
    }
}
