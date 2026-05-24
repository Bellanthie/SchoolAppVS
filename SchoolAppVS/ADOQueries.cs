using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS
{
    internal class ADOQueries
    {
        private string connectionString = "Server=.;Database=Hogstadiet;Trusted_Connection=True;TrustServerCertificate=True;";

        public void ObtainAllStaff()
        {
            // SqlConnection = the connectionObject that keeps pipeline to dtb open
            // Using = when method is done -> connection AUTO closes ELSE manual close w/ risk of memory leak
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Sql query through connection (string + Sqlcommand)
            string sql = "SELECT s.FirstName, s.LastName, t.Name AS TITLE FROM Staff s INNER JOIN Titles t ON s.TitleId = t.Id";
            using var command = new SqlCommand(sql, connection);

            // ExecuteReader() sends query + recieves a reader
            using var reader = command.ExecuteReader();

            Console.WriteLine("\n ~~~ All Personal ~~~\n");
            while (reader.Read()) //moves to next row, returns FALSE when no more rows
            {

                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]} - {reader["Title"]}");
            }
        }
    }
}

