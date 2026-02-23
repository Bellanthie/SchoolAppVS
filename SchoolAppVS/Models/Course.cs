using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS.Models
{
    public class Course // Id, Name, IsActive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        // IsActive is a bool (on/off) -- the same as BIT in SQL. true = active, false = not active
    }
}
