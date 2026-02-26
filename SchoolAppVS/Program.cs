using SchoolAppVS;

bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("---Skolans Adminverktyg---");
    Console.WriteLine();
    Console.WriteLine("1. Antal lärare per avdelning");
    Console.WriteLine("2. Information om alla elever");
    Console.WriteLine("3. Alla aktiva kurser");
    Console.WriteLine("0. Avsluta");
    Console.WriteLine();
    Console.Write("Välj ett alternativ: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            EFQueries ef = new EFQueries(); // SQL Database -> EF -> C# -> Console
            ef.TeachersPerDepartment();
            Console.ReadKey();
            break;
        case "2":
            ef = new EFQueries();
            ef.AllStudents();
            Console.ReadKey();
            break;
        case "3":
            ef = new EFQueries();
            ef.AllActiveCourses();
            Console.ReadKey();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Ogiltigt val, försök igen.");
            Console.ReadKey();
            break;
    }
}
