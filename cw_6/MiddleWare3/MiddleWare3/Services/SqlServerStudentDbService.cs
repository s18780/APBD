using MiddleWare3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiddleWare3.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public Students GetStudent(string Index)
        {
            Students student = null;
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();

                command.CommandText = "Select IndexNumber,FirstName,LastName,BirthDate,Semester,Name from Studies inner join enrollment on studies.idStudy=enrollment.idStudy"
                    + " inner join Student on enrollment.idEnrollment=student.idEnrollment where indexnumber=@index";
                command.Parameters.AddWithValue("index", Index);
                SqlDataReader sql = command.ExecuteReader();


                while (sql.Read())
                {
                    student = new Students();
                    student.IndexNumber = sql["IndexNumber"].ToString();
                    student.FirstName = sql["FirstName"].ToString();
                    student.LastName = sql["LastName"].ToString();
                    student.BirthDate = sql["BirthDate"].ToString();
                    student.Semester = sql["Semester"].ToString();
                    student.Studies = sql["Name"].ToString();
                    Console.Out.WriteLine(student.ToString());
                }
                sql.Close();





            }


            return student;
        }

        public List<Students> GetStudents()
        {
            List<Students> list = new List<Students>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "Select IndexNumber,FirstName,LastName,BirthDate,Semester,Name from Studies inner join enrollment on studies.idStudy=enrollment.idStudy" 
                   + " inner join Student on enrollment.idEnrollment=student.idEnrollment";
                client.Open();
                var sql = com.ExecuteReader();

                while (sql.Read())
                {
                    Students students = new Students();
                    students.IndexNumber = sql["IndexNumber"].ToString();
                    students.FirstName = sql["FirstName"].ToString();
                    students.LastName = sql["LastName"].ToString();
                    students.BirthDate = sql["BirthDate"].ToString();
                    students.Semester = sql["Semester"].ToString() +" semestr";
                    students.Studies = sql["Name"].ToString();
                    list.Add(students);


                }
            }
            return list;
        }
    }
}
