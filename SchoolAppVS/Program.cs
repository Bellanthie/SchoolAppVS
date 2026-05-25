using Microsoft.EntityFrameworkCore;
using SchoolAppVS.Models;
using SchoolAppVS;

ADOQueries adoQueries = new ADOQueries();
EFQueries queries = new EFQueries();
bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("---Skolans Administrations System---");
    Console.WriteLine();
    Console.WriteLine("1. Hämta alla elever");
    Console.WriteLine("2. Hämta alla elever i en viss klass");
    Console.WriteLine("3. Lägg till ny personal");
    Console.WriteLine("4. Hämta personal");
    Console.WriteLine("5. Betyg satta senaste månaden");
    Console.WriteLine("6. Snittbetyg per ämne");
    Console.WriteLine("7. Lägg till ny elev");
    Console.WriteLine("8. Lärare per avdelning");
    Console.WriteLine("9. Visa aktiva Kurser");
    Console.WriteLine("10. Betyg per elev");
    Console.WriteLine("11. Lön per avdelning");
    Console.WriteLine("12. Medellön per avdelning");
    Console.WriteLine("13. Hämta elev med Id");
    Console.WriteLine("0. Avsluta");
    Console.WriteLine();
    Console.Write("Välj ett alternativ: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            queries.ObtainAllStudents();
            Console.ReadKey();
            break;
        case "2":
            queries.FetchStudentsInSpecificClass();
            Console.ReadKey();
            break;
        case "3":
            queries.AddStaff();
            Console.ReadKey();
            break;
        case "4":
            adoQueries.ObtainAllStaff();
            Console.ReadKey();
            break;
        case "5":
            queries.GradesLastMonth();
            Console.ReadKey();
            break;
        case "6":
            queries.AverageGradePerSubject();
            Console.ReadKey();
            break;
        case "7":
            queries.AddNewStudent();
            Console.ReadKey();
            break;
        case "8":
            queries.TeachersPerDepartment();
            Console.ReadKey();
            break;
        case "9":
            queries.AllActiveCourses();
            Console.ReadKey();
            break;
        case "10":
            adoQueries.GradesPerStudent();
            Console.ReadKey();
            break;
        case "11":
            adoQueries.SalaryPerDepartment();
            Console.ReadKey();
            break;
        case "12":
            adoQueries.AvgSalaryPerDepartment();
            Console.ReadKey();
            break;
        case "13":
            adoQueries.GetStudentById();
            Console.ReadKey();
            break;
        case "0":
            running = false; // exit the program
            break;
        default:
            Console.WriteLine("Ogiltigt val, försök igen."); // användaren måste välja en siffra
            Console.ReadKey();
            break;
    }
}