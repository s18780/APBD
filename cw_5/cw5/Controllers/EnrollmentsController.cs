using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cw5.DTOs.Requests;
using cw5.DTOs.Responses;
using System.Data.SqlClient;

namespace cw5.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {


            
            var st = new Student();
            st.IndexNumber = request.IndexNumber;
            st.FirstName = request.FirstName;
            st.LastName = request.LastName;
            st.BirthDate = request.BirthDate;
            st.Studies = request.Studies;
           
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                var tran = client.BeginTransaction();

                  try
                  {

                      com.CommandText = "select IdStudies from studies where name=@name";
                      com.Parameters.AddWithValue("name", request.Studies);
                      var dr = com.ExecuteReader();
                      if (!dr.Read())
                      {
                          tran.Rollback();
                          return BadRequest("nie istneieje");
                      }
                      int idstudies = (int)dr["IdStudies"];

  
                      com.CommandText = "select * from Enrollment " +
                         "where StartDate = (select MAX(StartDate) from Enrollment where idStudy=" + idstudies + ") " +
                         "AND Semester=1";

                      var execread = com.ExecuteReader();
                      if (!execread.Read())
                      {
                          com.CommandText = "Insert into Enrollment values (" +
                              "(SELECT MAX(idEnrollment)+1 from Enrollment),1," + idstudies + ",GetDate()";
                      }
                      com.CommandText = "Select * from Student WHERE IndexNumber=@indexNumber";
                      com.Parameters.AddWithValue("indexNumber", request.IndexNumber);

                      var studentread = com.ExecuteReader();

                      if (!studentread.Read())
                      {
                          tran.Rollback();
                          return BadRequest("Index jest nieunikalny.");
                      }
                      com.CommandText = "Insert into Student values(" +request.IndexNumber + "," +  request.FirstName + "," + 
                          request.LastName + "," + request.BirthDate + "," + idstudies + ")";
                     
                        
                      com.ExecuteNonQuery();
                      


                      tran.Commit();
                  }
                  catch (SqlException exc) {
                      tran.Rollback();
                  }
                  


                var response = new EnrollStudentResponse();
                response.IndexNumber = st.IndexNumber;
                response.FirstName = st.FirstName;
                response.LastName = st.LastName;
                response.BirthDate = st.BirthDate;
                response.Studies = st.Studies;





                return Ok(response);
            }
        }
    }
}
    