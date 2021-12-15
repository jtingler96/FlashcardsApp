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
                    //viewCards();
                    //Return to manage menu
                    manageCards();
                    break;
                case 2:
                    createCards();
                    //Return to manage menu
                    manageCards();
                    break;
                case 3:
                    //editCards();
                    //Return to main menu
                    manageCards();
                    break;
                case 4:
                    //deleteCards();
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

        //internal static void viewStacks()
        //{
        //    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=StudyDb;";
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        string sql = $"SELECT stackid, stackname FROM stacks";
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(sql, connection);

        //        List<Stack> tableData = new List<Stack>();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                tableData.Add(
        //                new Stack
        //                {
        //                    StackId = reader.GetInt32(0),
        //                    StackName = reader.GetString(1),
        //                });
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("No rows found");
        //        }
        //        reader.Close();
        //        Console.WriteLine("\n\n");

        //        ConsoleTableBuilder
        //            .From(tableData)
        //            .ExportAndWriteLine();
        //        Console.WriteLine("\n\n");
        //    }
        //}

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
                cmd.Parameters.Add("stackid", SqlDbType.VarChar);
                cmd.Parameters["stackid"].Value = stackId;
                cmd.ExecuteNonQuery();

                Console.WriteLine($"\n\nYour card was added to stack with ID: {stackId}\n\n");

            }
        }

        //internal static void editStack()
        //{
        //    Console.WriteLine("\n\nType Id of the stack would like to edit. Type 0 to return to the menu.\n\n");

        //    string userInput = InputHelper.inputInt();

        //    int Id = Convert.ToInt32(userInput);

        //    if (Id == 0) manageStacks();

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        var checkCmd = connection.CreateCommand();
        //        checkCmd.CommandText = $"SELECT stackid FROM stacks where stackid = {Id}";
        //        int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

        //        if (checkQuery == 0)
        //        {
        //            Console.WriteLine($"\n\nRecord with Id {Id} doesn't exist.\n\n");
        //            editStack();
        //        }

        //        Console.WriteLine("\nEnter the new stack name\n");
        //        string stackname = Console.ReadLine();

        //        var tableCmd = connection.CreateCommand();
        //        tableCmd.CommandText = $"UPDATE stacks SET stackname = '{stackname}' WHERE stackid = {Id}";
        //        tableCmd.ExecuteNonQuery();

        //        Console.WriteLine($"\n\nThe new stack name is: {stackname}\n\n");
        //        connection.Close();
        //    }
        //}
        //internal static void deleteStacks()
        //{
        //    Console.WriteLine("\n\nType the Id of the stack you want to remove. Type 0 to return to the menu.\n\n");

        //    int Id = Int32.Parse(InputHelper.inputInt());

        //    if (Id == 0) manageStacks();

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        var tableCmd = connection.CreateCommand();
        //        tableCmd.CommandText = $"DELETE from stacks WHERE stackid = '{Id}'";
        //        int rowCount = tableCmd.ExecuteNonQuery();
        //        while (rowCount == 0)
        //        {
        //            Console.WriteLine($"\n\nClass with Id {Id} doesn't exist. Try Again or type 0 to return to main menu. \n\n");
        //            Id = Int32.Parse(InputHelper.inputInt());

        //            if (Id == 0) manageStacks();

        //            if (rowCount != 0) break;
        //        }
        //        Console.WriteLine("\n\n\nstack was removed\n\n");
        //    }
        //}
    }
}
