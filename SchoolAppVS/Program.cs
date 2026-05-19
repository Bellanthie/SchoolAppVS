using Microsoft.EntityFrameworkCore;
using SchoolAppVS.Models;
using SchoolAppVS;


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
            queries.ObtainAllStaff();
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
        case "0":
            running = false; // exit the program
            break;
        default:
            Console.WriteLine("Ogiltigt val, försök igen."); // användaren måste välja en siffra
            Console.ReadKey();
            break;
    }
}