using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_4.Models
{
    public class Student
    {
        public int IdStudent { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public string BirthDate { get; set; }
        public string IdStudy { get; set; }
        public string IdEnrollment { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string StartDate { get; set; }
       
        
      
        public DateTime DateOfInsert { get; set; }

        public String ToString()
        {
            return FirstName + " " + LastName + ", " + BirthDate + ". Studia: " + Name + ", " + Semester;
        }
        public String SecondToString()
        {
            return "Id:" + IdEnrollment + ", " + Semester + ", Id kierunku: " + IdStudy + ", " + StartDate;
        }
    }
}
