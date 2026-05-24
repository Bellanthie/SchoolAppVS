using Microsoft.Data.SqlClient;
using SchoolAppVS.Models;
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
            string sql = @"SELECT s.FirstName, s.LastName, t.Name AS Title, 
               d.Name AS Department, s.HireDate
               FROM Staff s 
               INNER JOIN Titles t ON s.TitleId = t.Id
               INNER JOIN Departments d ON s.DepartmentId = d.Id";
            using var command = new SqlCommand(sql, connection);

            // ExecuteReader() sends query + recieves a reader
            using var reader = command.ExecuteReader();


            Console.WriteLine("\n~~~ All Personal ~~~\n");
            while (reader.Read()) //moves to next row, returns FALSE when no more rows
            {

                int yearsWorked = DateTime.Now.Year - Convert.ToDateTime(reader["HireDate"]).Year;
                Console.WriteLine($"{reader["FirstName"],-10} " +
                                  $"{reader["LastName"],-10} | " +
                                  $"Titel: {reader["Title"],-15} | " +
                                  $"Avdelning: {reader["Department"],-35} | " +
                                  $"Anställd i: {yearsWorked} år");
                
            }
        }

        public void GradesPerStudent()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = @"SELECT st.FirstName + ' ' + st.LastName AS StudentName,
                        sub.Name AS Subject,
                        g.Grade,
                        g.GradeDate,
                        sf.FirstName + ' ' + sf.LastName AS Teacher
                        FROM Grades g
                        INNER JOIN Students st ON g.StudentId = st.Id
                        INNER JOIN Subjects sub ON g.SubjectId = sub.Id
                        INNER JOIN Staff sf ON g.StaffId = sf.Id
                        ORDER BY st.LastName";

            using var command = new SqlCommand(sql, connection);

            using var reader = command.ExecuteReader();

            Console.WriteLine("\n ~~~ Betyg per elev ~~~");
            while (reader.Read())
            {
                // testing padding within C# to give a clearer console output = 
                // leave 20 characters för that value -> fill with space if its shorter. "-" = adjustLeft
                Console.WriteLine($"{reader["StudentName"],-20} | " +
                                  $"Ämne: {reader["Subject"],-20} | " +
                                  $"Betyg: {reader["Grade"],-3} | " +
                                  $"Datum: {Convert.ToDateTime(reader["GradeDate"]):yyyy-MM-dd} | " +
                                  $"Lärare: {reader["Teacher"]}");
            }
            
        }

        public void SalaryPerDepartment()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = @"SELECT d.Name AS Department,
                         SUM(s.Salary) AS TotalSalary
                         FROM Staff s
                         INNER JOIN Departments d ON s.DepartmentId = d.Id
                         GROUP BY d.Name";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            Console.WriteLine("\n ~~~ Lön per avdelning ~~~");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Department"],-35} | " +
                                $"Total lön: {reader["TotalSalary"],-10} kr");
            }
        }


        public void AvgSalaryPerDepartment()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = @"SELECT d.Name AS Department,
                        AVG(s.Salary) AS AverageSalary
                        FROM Staff s
                        INNER JOIN Departments d ON s.DepartmentId = d.Id
                        GROUP BY d.Name";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            Console.WriteLine("\n ~~~ Medellön per avdelning ~~~");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Department"],-35} | " +
                    $"Medel lön: {Convert.ToDecimal(reader["AverageSalary"]):F2} kr");
            }
        }

    }
}

