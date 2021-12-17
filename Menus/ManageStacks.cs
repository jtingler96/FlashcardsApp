using ConsoleTableExt;
using flashcardsApp;
using flashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp.Menus
{
    class ManageStacks
    {
        internal static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
        internal static void manageStacks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nManage stacks");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nType 0 to exit the program");
            Console.WriteLine("\nType 1 view stacks");
            Console.WriteLine("\nType 2 to create a stack");
            Console.WriteLine("\nType 3 to delete a stack");
            Console.WriteLine("\nType 4 to edit a stack name");
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
                    viewStacks();
                    //Return to manage menu
                    manageStacks();
                    break;
                case 2:
                    createStack();
                    //Return to manage menu
                    manageStacks();
                    break;
                case 3:
                    deleteStacks();
                    //Return to main menu
                    manageStacks();
                    break;
                case 4:
                    editStack();
                    //Return to main menu
                    manageStacks();
                    break;
                case 5:
                    //Return to main menu
                    ManagerHome.managerMenu();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n************   Please select an option from the menu   ************\n");
                    //Return to manage menu
                    manageStacks();
                    break;
            }
        }
        internal static void viewStacks()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT stackid, stackname FROM stacks";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                List<Stack> tableData = new List<Stack>();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new Stack
                        {
                            StackId = reader.GetInt32(0),
                            StackName = reader.GetString(1),
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }
                reader.Close();
                Console.WriteLine("\n\n");

                ConsoleTableBuilder
                    .From(tableData)
                    .ExportAndWriteLine();
                Console.WriteLine("\n\n");
            }
        }

        internal static void createStack()
        {
            //Insert new record into the Stacks table

            Console.WriteLine("\nEnter the stack name\n");
            string stackname = Console.ReadLine();

            //open connection to the database
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO stacks (stackname) values ('{stackname}')";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("stackname", SqlDbType.VarChar);
                cmd.Parameters["stackname"].Value = stackname;
                cmd.ExecuteNonQuery();

                Console.WriteLine($"\n\nYour stack was created!\n\n");

            }
        }

        internal static void editStack()
        {
            Console.WriteLine("\n\nType Id of the stack would like to edit. Type 0 to return to the menu.\n\n");

            string userInput = InputHelper.inputInt();

            int Id = Convert.ToInt32(userInput);

            if (Id == 0) manageStacks();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT stackid FROM stacks where stackid = {Id}";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {Id} doesn't exist.\n\n");
                    editStack();
                }

                Console.WriteLine("\nEnter the new stack name\n");
                string stackname = Console.ReadLine();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE stacks SET stackname = '{stackname}' WHERE stackid = {Id}";
                tableCmd.ExecuteNonQuery();

                Console.WriteLine($"\n\nThe new stack name is: {stackname}\n\n");
                connection.Close();
            }
        }
        internal static void deleteStacks()
        {
            Console.WriteLine("\n\nType the Id of the stack you want to remove. Type 0 to return to the menu.\n\n");

            int Id = Int32.Parse(InputHelper.inputInt());

            if (Id == 0) manageStacks();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE s FROM stacks s WHERE s.stackid = '{Id}' " +
                    $"DELETE f FROM flashcards f WHERE f.stackid = '{Id}'";
                int rowCount = tableCmd.ExecuteNonQuery();
                while (rowCount == 0)
                {
                    Console.WriteLine($"\n\nClass with Id {Id} doesn't exist. Try Again or type 0 to return to main menu. \n\n");
                    Id = Int32.Parse(InputHelper.inputInt());

                    if (Id == 0) manageStacks();

                    if (rowCount != 0) break;
                }
                Console.WriteLine("\n\n\nstack was removed\n\n");
            }
        }
    }
}
