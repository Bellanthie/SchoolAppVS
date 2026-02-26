using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS.Models // (Models = data-shape blueprints)
{
    public class Staff // properties to match Staff: id, FirstName, Lastname, Title, Salary, StartDate, DepartmentId, Department
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
        public Department Department { get; set; } 
        // these two together tells EF "this staff member belongs to one department" (like references in SQL)

    }
    // Staff has no constructor - Entity Framework (EF fills in all properties when it reads data from the database)
}
