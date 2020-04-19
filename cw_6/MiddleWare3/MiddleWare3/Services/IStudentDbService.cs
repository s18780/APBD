using MiddleWare3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWare3.Services
{
    interface IStudentDbService
    {
        public Students GetStudent(string Index);
        public List<Students> GetStudents();
    }
}
