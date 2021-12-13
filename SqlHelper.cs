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
        public static void InitializeDB()
        {
            //Open a connection to the database using the value of ConnectionString. If Mode=ReadWriteCreate is used (the default) the file is created, if it doesn't already exist.
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Create a sample database
            Console.Write("Dropping and creating database 'StacksDb' ... ");
            String sql = "DROP DATABASE IF EXISTS [StacksDb]; CREATE DATABASE [StacksDb]";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
            }

            // Create a Table and insert some sample data
           
        }
    }
}
