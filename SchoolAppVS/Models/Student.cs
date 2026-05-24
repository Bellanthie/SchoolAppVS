using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SchoolAppVS.Models
{
    public class Student // Id, FirstName, LastName, DateOfBirth, Gender, ClassId, Class
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string SocialSecurityNo { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public Class? Class { get; set; }

    }
}
