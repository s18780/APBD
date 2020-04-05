using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw5.DTOs.Requests;
using cw5.DTOs.Responses;
using cw5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
    [Route("api/enrollments/promotions")]
    [ApiController]
    public class EnrollmentPromoteController : ControllerBase
    {




        [HttpPost]
        public IActionResult Promotions(EnrollPromoteRequest request)
        {
          


            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18780;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                var tran = client.BeginTransaction();

                try
                {

                    com.CommandText = "select Studies,semester from enrollment where studies=@studies AND semester=@semester";
                    com.Parameters.AddWithValue("studies", request.StudiesName);
                    com.Parameters.AddWithValue("semester", request.Semester);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("nie istneieja");
                    }

                   

                    com.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }

            }


            string response = "Semestr: ";
            return Ok(response);
        }
    }
}