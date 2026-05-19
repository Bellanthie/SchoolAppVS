  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppVS.Models
{
    // Id, StudentId, Student, StaffId, Staff, CourseId, Course, GradeValue, GradeDate
    public class Grade 
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public int SubjectId { get; set; }

        [Column("Grade")] // SQL->Grade, VS--> GradeValue. EF Core behöver annotation just här för att hitta kolumnen. DÄRMED en using, rad 6
        public string GradeValue { get; set; } = string.Empty;
        public DateTime GradeDate { get; set; }

        //public Staff Staff { get; set; }
        //public int CourseId { get; set; }
        //public string GradeValue { get; set; }
        // properties connect to 3 models: student, staff and course.
        //Grades table in SQL referenced 3 other tables
    }
}
