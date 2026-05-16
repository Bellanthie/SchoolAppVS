using Microsoft.EntityFrameworkCore;
using SchoolAppVS.Models;

namespace SchoolAppVS;

public class EFQueries // EFQueries = Entity Framework Queries (EF Core fills in the constructors automatically from the database)
{
    public void ObtainAllStudents()
    {
        Console.WriteLine("Sortera på: 1) Förnamn  2) Efternamn");
        string sortVal = Console.ReadLine();

        Console.WriteLine("Ordning: 1) Stigande   2) Fallande");
        string sortOrder = Console.ReadLine();

        using var context = new SchoolContext();
        var students = context.Students
            .Select(s => new
            {
                s.FirstName,
                s.LastName
            });

        if (sortVal == "1" && sortOrder == "1")
            students = students.OrderBy(s => s.FirstName);
        else if (sortVal == "1" && sortOrder == "2")
            students = students.OrderByDescending(s => s.FirstName);
        else if (sortVal == "2" && sortOrder == "1")
            students = students.OrderBy(s => s.LastName);
        else
            students = students.OrderByDescending(s => s.LastName);

        Console.WriteLine("\n ~~~ ALLA ELEVER ~~~");

        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
        }
    }

    public void FetchStudentsInSpecificClass()
    {
        using var context = new SchoolContext();

        var classes = context.Classes
            .Select(c => new
            {
                c.Id,
                c.Name
            });

        Console.WriteLine("\n ~~~ Tillgängliga Klasser ~~~ ");

        foreach (var c in classes)
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }

        // choose a class
        Console.WriteLine("\n Välj klass (skriv siffran): ");
        int classId = int.Parse(Console.ReadLine());

        // obtain students within a specified class
        var students = context.Students
            .Where(s => s.ClassId == classId)
            .Select(s => new
            {
                s.FirstName,
                s.LastName
            });

        Console.WriteLine("\n ~~~ Elever i vald klass ~~~ ");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
        }

    }

    public void AddStaff()
    {
        using var context = new SchoolContext();

        // Show titles first
        var titles = context.Titles
            .Select(t => new
            {
                t.Id,
                t.Name
            });
        Console.WriteLine("\n ~~~ Tillgängliga Titlar ~~~\n");
        foreach (var title in titles)
        {
            Console.WriteLine($"{title.Id}. {title.Name}");
        }

        Console.WriteLine("Lägg till ny personal: ");
        Console.WriteLine("\nFörnamn: ");
        string firstName = Console.ReadLine();

        Console.WriteLine("Efternamn: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Välj titel (skriv siffran): ");
        int titleId = int.Parse(Console.ReadLine());

        // create new object
        var newStaff = new Staf
        {
            FirstName = firstName,
            LastName = lastName,
            TitleId = titleId
        };

        var titles = context.Titles.ToList();
        
    }


    // hämta personal
    //public void TeachersPerDepartment()
    //{
    //    using var context = new SchoolContext() ;

    //        var result = context.Staff // starts with all staff members from the database.
    //        .Where(s => s.Title == "Lärare") // filters to only staff with the title "lärare"
    //        .GroupBy(s => s.Department.Name) // takes filtered teachers and groups staff by department name
    //        .Select(st => new // transforms each group into a new object
    //        {
    //            Department = st.Key, // department name
    //            NumberOfTeachers = st.Count() // # of teachers in that group
    //        })
    //        .ToList(); // fetches the requested data ABOVE from the database memory

    //        Console.WriteLine("=== Antal lärare per avdelning ===");
    //        Console.WriteLine();

    //            foreach (var row in result)
    //            {
    //                Console.WriteLine($"{row.Department}: {row.NumberOfTeachers} lärare");
    //            }

    //    // database connection(key to filing cabinet). every method inside this class will have access to this key whenever it needs it
    //}


    //public void AllStudentsSorted()
    //{
    //    using var context = new SchoolContext();
    //    var sortChoice = context.Students
    //        .Include(s => s.Class) // tells EF: when you fetch students, also fetch their class info.
    //        .ToList();
    //    Console.WriteLine("---Alla Elever___");
    //    Console.WriteLine();

    //    foreach (var student in sortChoice)
    //    {
    //        Console.WriteLine($"{student.FirstName} {student.LastName} | " +
    //                          $"Klass: {student.Class.Name} | " + // without .Include --> student.Class would be empty
    //                          $"Född: {student.DateOfBirth:yyyy-MM-dd} | " +
    //                          $"Kön: {student.Gender}");
    //    }
    //}

    //public void AllActiveCourses()
    //{
    //    using var context = new SchoolContext();
    //    var courses = context.Courses
    //        .Where(c => c.IsActive == true) // filters out any courses where IsActive is false.
    //        .ToList();
    //    Console.WriteLine("--- Alla aktiva kurser ---");
    //    Console.WriteLine();

    //    foreach (var course in courses)
    //    {
    //        Console.WriteLine($"- {course.Name}");
    //    }
    //}

    // ¨~~~~~~~~~~~~ LABB 3 ANROPA DATABASEN ORM ~~~~~~~~~~~~




}
