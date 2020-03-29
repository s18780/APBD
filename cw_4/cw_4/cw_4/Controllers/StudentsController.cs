using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using cw_4.DAL;
using cw_4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw_4.Controllers
{

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudent()
        {
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True "))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester " +
                    "from Student inner join Enrollment on Student.IdEnrollment = Enrollment.IdEnrollment " +
                    "inner join Studies on Enrollment.IdStudy = Studies.IdStudy"; ;
                connection.Open();
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var student = new Student();
                    student.FirstName = dataReader["FirstName"].ToString();
                    student.LastName = dataReader["LastName"].ToString();
                    student.IndexNumber = dataReader["BirthDate"].ToString();
                    student.Name = dataReader["Name"].ToString();
                    student.Semester = dataReader["Semester"].ToString();

                    students.Add(student);
                }
                return Ok(students);
            }
        }

        
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 2000)}";

            return Ok(student);
        }



        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            string myId = "s1234";
            string SqlID = "'" + id + "'";
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select enrollment.IdEnrollment, semester, Enrollment.IdStudy, StartDate" +
                    " from studies inner join enrollment on studies.IdStudy=enrollment.IdStudy "
                    + "inner join Student on Enrollment.IdEnrollment=Student.IdEnrollment where IndexNumber = " + SqlID;
                com.Parameters.AddWithValue("id", myId);

                client.Open();
                var dr = com.ExecuteReader();
                String student = "";
                while (dr.Read())
                {
                    var st = new Student();
                    st.IdEnrollment = dr["IdEnrollment"].ToString();
                    st.Semester = dr["Semester"].ToString() + " semestr";
                    st.IdStudy = dr["IdStudy"].ToString();
                    st.StartDate = dr["StartDate"].ToString();
                    student += st.SecondToString();
                }
                return Ok(student);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id, Student student)
        {
            if (student.IdStudent == id)
            {
                student = null;
            }
            return Ok("Usuwanie");
        }


    }
}