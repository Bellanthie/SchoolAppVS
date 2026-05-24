using Microsoft.EntityFrameworkCore;
using SchoolAppVS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS
{
    public class SchoolContext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }

        //ADO lägga till ny tabell
        public DbSet<Department> Departments { get; set; }


        // Metod som talar om VAR databasen finns
        // Connection string:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Hogstadiet;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
