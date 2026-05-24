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
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int TitleId { get; set; }


        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentId { get; set; } // FK nyckeln = passnumret

        // navigation property Department: EF Core uses it to AUTO get the whole DEPTS object connected to DepartmentId
        //these two together tells EF "this staff member belongs to one department" (like references in SQL)
        public Department? Department { get; set; } //PASSET



    }
    // Staff has no constructor - Entity Framework (EF fills in all properties when it reads data from the database)
}
