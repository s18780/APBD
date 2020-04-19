using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddleWare3.Services;
using MiddleWare3.Models;


namespace MiddleWare3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentDbService _service;
        public StudentsController(IStudentDbService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            List<Students> list = _service.GetStudents();

            if (!list.Any()) return NotFound("not found");

            return Ok(list);
        }
}