    using SchoolAppVS;

    using var context = new SchoolContext();

    var departments = context.Departments.ToList();

    foreach (var department in departments)
    {
    Console.WriteLine(department.Name);
    }

    // SQL SERVER DATABASE (SchoolAppVS) -> SchoolContext.cs (the bridge) -> Program.cs (my C# code)