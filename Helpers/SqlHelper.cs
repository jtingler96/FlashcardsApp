using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using flashcardsApp;

namespace flashcardsApp
{
    class SqlHelper
    {
        internal static void createDb()
        {
            //Open a connection to the database using the value of ConnectionString. If Mode=ReadWriteCreate is used (the default) the file is created, if it doesn't already exist.
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";
            //Data Source = (local); Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    // Open the connection to the server
                    connection.Open();

                    // Create a the database
                    var tableCmd = connection.CreateCommand();

                    //Create the database if it does not exist
                    tableCmd.CommandText =
                        $@"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'StudyDb') 
                        BEGIN 
                        CREATE DATABASE StudyDb;
                        END;
                        ";

                    //Execute the CommandText against the database
                    tableCmd.ExecuteNonQuery();

                    //Close the connection to the database. Open transactions are rolled back.
                    connection.Close();

                }
                CreateTables();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            MainMenu.GetUserCommand();
        }
        internal static void CreateTables()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=StudyDb; Integrated Security=true;";
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                //Create a new command associated with the connection.
                var tableCmd = connection.CreateCommand();

                //Create the tables
                tableCmd.CommandText =
                $@"IF NOT EXISTS(select * from sysobjects where name='stacks' and xtype='U') 
                BEGIN 
                CREATE TABLE stacks(
                stackid     INTEGER PRIMARY KEY IDENTITY,
                stackname   VARCHAR(150));
                END;

                IF NOT EXISTS(select * from sysobjects where name='flashcards' and xtype='U') 
                BEGIN 
                CREATE TABLE flashcards(
                id INTEGER  PRIMARY KEY IDENTITY,
                cardfront   VARCHAR(150),
                cardback    VARCHAR(150),
                stackid     INTEGER FOREIGN KEY REFERENCES stacks(stackid));
                END;
                ";

                //Execute the CommandText against the database
                tableCmd.ExecuteNonQuery();

                //Close the connection to the database. Open transactions are rolled back.
                connection.Close();
            }
        }
    }
}
