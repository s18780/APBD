using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWare3.Models
{
    public class Students
    {
        public int IdStudent { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IndexNumber { get; set; }

        public string BirthDate { get; set; }

        public string Studies { get; set; }

        public string Semester { get; set; }

        public override string ToString()
        {
            return IndexNumber + " " + FirstName + " " + LastName + " " + BirthDate + " " + Semester + " " + Studies;
        }
    }
}
