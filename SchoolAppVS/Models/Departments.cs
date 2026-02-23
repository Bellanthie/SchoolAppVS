using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS.Models
{
    public class Department // properties to match Id, name, Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Staff> Staff { get; set; }

    }
}
