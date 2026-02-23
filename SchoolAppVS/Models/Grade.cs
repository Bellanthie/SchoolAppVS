using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppVS.Models
{
    // Id, StudentId, Student, StaffId, Staff, CourseId, Course, GradeValue, GradeDate
    public class Grade 
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string GradeValue { get; set; }
        public DateTime GradeDate { get; set; }
        // properties connect to 3 models: student, staff and course.
        //Grades table in SQL referenced 3 other tables
    }
}
