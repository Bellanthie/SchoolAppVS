using Microsoft.EntityFrameworkCore;
using SchoolAppVS.Models;

namespace SchoolAppVS
{
    public class EFQueries // EFQueries = Entity Framework Queries (EF Core fills in the constructors automatically from the database)
    {
        private SchoolContext context = new SchoolContext();

        public void TeachersPerDepartment()
        {
            var result = context.Staff
                .Where(s => s.Title == "Lärare") // filters to only staff with the title "lärare"
                .GroupBy(s => s.Department.Name) // groups staff by department name
                .Select(g => new // for each group, counts how many teachers are in it
                {
                    Department = g.Key,
                    NumberOfTeachers = g.Count()
                })
                .ToList();

            Console.WriteLine("=== Antal lärare per avdelning ===");
            Console.WriteLine();

            foreach (var row in result)
            {
                Console.WriteLine($"{row.Department}: {row.NumberOfTeachers} lärare");
            }
        }

        public void AllStudents()
        {
            var students = context.Students
                .Include(s => s.Class) // tells EF: when you fetch students, also fetch their class info.
                .ToList();
            Console.WriteLine("---Alla Elever___");
            Console.WriteLine();

            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} | " +
                                  $"Klass: {student.Class.Name} | " + // without .Include --> student.Class would be empty
                                  $"Född: {student.DateOfBirth:yyyy-MM-dd} | " +
                                  $"Kön: {student.Gender}");
            }
        }

        public void AllActiveCourses()
        {
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