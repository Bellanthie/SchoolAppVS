using Microsoft.EntityFrameworkCore;
using Högstadiet.Models;

namespace HöstadietAppVS
{
    public class EFQueries // EFQueries = Entity Framework Queries (EF Core fills in the constructors automatically from the database)
    {
        // hämta personal
        public void TeachersPerDepartment()
        {
            using var context = new SchoolContext() ;
            
                var result = context.Staff // starts with all staff members from the database.
                .Where(s => s.Title == "Lärare") // filters to only staff with the title "lärare"
                .GroupBy(s => s.Department.Name) // takes filtered teachers and groups staff by department name
                .Select(st => new // transforms each group into a new object
                {
                    Department = st.Key, // department name
                    NumberOfTeachers = st.Count() // # of teachers in that group
                })
                .ToList(); // fetches the requested data ABOVE from the database memory

                Console.WriteLine("=== Antal lärare per avdelning ===");
                Console.WriteLine();

                    foreach (var row in result)
                    {
                        Console.WriteLine($"{row.Department}: {row.NumberOfTeachers} lärare");
                    }
            
            // database connection(key to filing cabinet). every method inside this class will have access to this key whenever it needs it
        }


        public void AllStudentsSorted()
        {
            using var context = new SchoolContext();
            var sortChoice = context.Students
                .Include(s => s.Class) // tells EF: when you fetch students, also fetch their class info.
                .ToList();
            Console.WriteLine("---Alla Elever___");
            Console.WriteLine();

            foreach (var student in sortChoice)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} | " +
                                  $"Klass: {student.Class.Name} | " + // without .Include --> student.Class would be empty
                                  $"Född: {student.DateOfBirth:yyyy-MM-dd} | " +
                                  $"Kön: {student.Gender}");
            }
        }

        public void AllActiveCourses()
        {
            using var context = new SchoolContext();
            var courses = context.Courses
                .Where(c => c.IsActive == true) // filters out any courses where IsActive is false.
                .ToList();
            Console.WriteLine("--- Alla aktiva kurser ---");
            Console.WriteLine();

            foreach (var course in courses)
            {
                Console.WriteLine($"- {course.Name}");
            }
        }
    }
}