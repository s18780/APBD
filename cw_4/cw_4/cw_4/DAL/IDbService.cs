using cw_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_4.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();


    }
}
