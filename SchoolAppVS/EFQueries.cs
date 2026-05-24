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

        //creates resource & shuts down auto
        using var context = new SchoolContext();

        //IQueryable, query expression: recipe of anonymous type id+name
        var students = context.Students
            .Include(s => s.Class)
            .Select(s => new
            {
                s.FirstName,
                s.LastName,
                ClassName = s.Class.Name,
                s.DateOfBirth,
                s.Gender
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
            Console.WriteLine($"{student.FirstName} {student.LastName} | " +
                $"Klass: {student.ClassName} | " +
                $"Född: {student.DateOfBirth:yyyy-MM-dd} | " +
                $"Kön: {student.Gender}");
        }
    }

    public void FetchStudentsInSpecificClass()
    {
        //creates resource & shuts down auto
        using var context = new SchoolContext();

        //IQueryable, query expression: recipe of anonymous type id+name
        var classes = context.Classes
            .Select(c => new
            {
                c.Id,
                c.Name
            });

        Console.WriteLine("\n ~~~ Tillgängliga Klasser ~~~ ");

        //IQueryable NOW hits the database when enumerated
        // each c = 1 instance of classes to properties of Anonymous object
        foreach (var c in classes)
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }

        // choose a class
        Console.WriteLine("\n Välj klass (skriv siffran): ");
        if (!int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Ogiltigt val --> ange en siffra!");
            return; //returns to program.cs, user sees menu again-Stop here! dont cnt w.invalid data
        }
        ;

        // obtain students within a specified class--runs when inputs valid
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

        if (firstName.Any(char.IsDigit))
        {
            Console.WriteLine("Ogiltigt - förnman får inte innehålla siffror!");
            return; //return to beginning of the menu to start over
        }

        Console.WriteLine("Efternamn: ");
        string lastName = Console.ReadLine();

        if (lastName.Any(char.IsDigit))
        {
            Console.WriteLine("Ogiltigt - efternamn får inte innehålla siffror!");
        }
        Console.WriteLine("Välj titel (skriv siffran): ");

        // this code WONT run if user's input is wrong
        if (!int.TryParse(Console.ReadLine(), out int titleId))
        {
            Console.WriteLine("Ogiltigt val --> ange en siffra!");
            return; // stops the method here and goes back to start of line 118.
        }


        // create new object
        var newStaff = new Staff
        {
            FirstName = firstName,
            LastName = lastName,
            TitleId = titleId
        };


        // save to database
        context.Staff.Add(newStaff);
        context.SaveChanges();

        // confirm
        Console.WriteLine($"\n {firstName} {lastName} har lagts till");
    }

    public void ObtainAllStaff()
    {
        using var context = new SchoolContext();

        Console.WriteLine("Visa 1) All Personal, 2) Endast en titel");
        string val = Console.ReadLine();

        if (val == "1")
        {
            //get all staff
            var staff = context.Staff
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.TitleId
                });
            Console.WriteLine("\n ~~~ All Personal ~~~"); ;
            foreach (var s in staff)
            {
                Console.WriteLine($"{s.FirstName}. {s.LastName} - TitelId: {s.TitleId} ");
            }
        }
        else
        {
            // show the titles and allow the user to choose
            var titles = context.Titles
                .Select(t => new
                {
                    t.Id,
                    t.Name
                });
            Console.WriteLine("\n~~~ Tillgängliga Titlar ~~~");
            foreach (var title in titles)
            {
                Console.WriteLine($"{title.Id} {title.Name}");
            }

            Console.WriteLine("\n Välj titel (skriv siffran): ");
            if (!int.TryParse(Console.ReadLine(), out int titleId))
            {
                Console.WriteLine("Ogiltigt val --> ange en siffra!");
                return; // stops method and returns to where its called on (the menu)
            }

            var filteredStaff = context.Staff
                .Where(s => s.TitleId == titleId)
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName
                });
            Console.WriteLine("\n~~~Personal med vald titel ~~~\n ");
            foreach (var s in filteredStaff)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName}");
            }
        }
    }


    public void GradesLastMonth()
    {
        using var context = new SchoolContext();

        var oneMonthAgo = DateTime.Now.AddMonths(-1);

        var grades = context.Grades
            .Where(g => g.GradeDate >= oneMonthAgo)
            .Select(g => new
            {
                // obtain the student's name directly through Students table
                StudentName = context.Students
                .Where(s => s.Id == g.StudentId)
                .Select(s => s.FirstName + " " + s.LastName)
                .FirstOrDefault(),

                // obtain the subject name through subjects table
                SubjectName = context.Subjects
                .Where(s => s.Id == g.SubjectId)
                .Select(s => s.Name)
                .FirstOrDefault(),

                g.GradeValue,
                g.GradeDate
            });

        Console.WriteLine("\n ~~~Betyg satta senaste månaden ~~~");
        foreach (var g in grades)
        {
            Console.WriteLine($"Elev: {g.StudentName} | " +
                              $"Ämne: {g.SubjectName} | " +
                              $"Betyg: {g.GradeValue} | " +
                              $"Datum: {g.GradeDate:yyyy-MM-dd}");
        }
    }


    public void AverageGradePerSubject()
    {
        using var context = new SchoolContext();
        var subjects = context.Subjects
            .Select(sub => new
            {
                SubjectName = sub.Name,

                //transform grades to numbers for calculation
                Grades = context.Grades
                .Where(g => g.SubjectId == sub.Id)
                .Select(g => g.GradeValue)
                .ToList()
            })
            .ToList(); // borrow home SUBJECTS from 'library'

        Console.WriteLine("\n ~~~ Snittbetyg per ämne ~~~\n");
        foreach (var sub in subjects)
        {
            if (!sub.Grades.Any())
            {
                Console.WriteLine($"{sub.SubjectName}: Inga betyg");
                continue;
            }

            // transform a letter to a number
            var gradeNumbers = sub.Grades.Select(g => g switch
            {
                "A" => 5,
                "B" => 4,
                "C" => 3,
                "D" => 2,
                "E" => 1,
                _ => 0
            });

            double avg = gradeNumbers.Average();
            string highest = sub.Grades.OrderBy(g => g).First();
            string lowest = sub.Grades.OrderByDescending(g => g).First();

            Console.WriteLine($"{sub.SubjectName}: " +
                $"Snitt: {avg:F1} | " +
                $"Högsta: {highest} | " +
                $"Lägsta: {lowest}");
        }
    }

    public void AddNewStudent()
    {
        using var context = new SchoolContext();

        // Visa klasser så användaren kan välja
        var classes = context.Classes
            .Select(c => new { c.Id, c.Name });

        Console.WriteLine("\n==== Tillgängliga Klasser ====\n");
        foreach (var c in classes)
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }

        // Mata in uppgifter
        Console.Write("\nFörnamn: ");
        string firstName = Console.ReadLine();

        Console.Write("Efternamn: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("Födelsedatum (YYYY-MM-DD): ");
        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Kön (Man/Kvinna): ");
        string gender = Console.ReadLine();

        Console.WriteLine("Personnummer (YYYYMMDD-XXXX): ");
        string socialSecurityNo = Console.ReadLine();

        Console.WriteLine("Välj klass (skriv siffran): ");
        if (!int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Ogiltigt val --> ange en siffra!");
            return;
        }

        // Anropa stored procedure
        context.Database.ExecuteSqlRaw(
            "EXEC AddNewStudent @p0, @p1, @p2, @p3, @p4, @p5",
            firstName, lastName, dateOfBirth, gender, socialSecurityNo, classId);

        Console.WriteLine($"\n✅ {firstName} {lastName} har lagts till!");
    }




    // get teachers per dept.
    public void TeachersPerDepartment()
    {
        using var context = new SchoolContext();

        var result = context.Staff // starts with all staff members from the database.
        .Include(s => s.Department)
        .Where(s => s.TitleId == 2) // filters to only staff with the title "lärare"
        .GroupBy(s => s.Department.Name) // takes filtered teachers and groups staff by department name
        .Select(g => new // transforms each group into a new object
        {
            Department = g.Key, // department name
            NumberOfTeachers = g.Count() // # of teachers in that group
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

public void AllActiveCourses()
    {
        using var context = new SchoolContext();
        var courses = context.Subjects
            .Select(s => new
            {
                s.Id,
                s.Name
            })
            .ToList();

        Console.WriteLine("\n ~~~ Aktiva Kurser ~~~\n");
        foreach (var course in courses)
        {
            Console.WriteLine($"{course.Id}. {course.Name}");
        }
    }
}



//private bool MakeSureUserEntersLetter(string name, string fieldName)
//    {
//        if (name.Any(char.IsDigit))
//        {
//            Console.WriteLine($"Ogiltigt - {fieldName} får inte innehålla siffror!");
//            return false;
//        }
//        return true;
//    }
//}




