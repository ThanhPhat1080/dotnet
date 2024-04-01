using Microsoft.AspNetCore.Mvc;

// Models
using Employee.Models;
using Employee.Repositories;

namespace Employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IEmployeeRepository _employeeRepository) : ControllerBase
    {
        // Import Employee repository
        private readonly IEmployeeRepository employeeRepository = _employeeRepository;

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<EmployeeModel> Get()
        {
            return employeeRepository.GetAllEmployees();
            //IEnumerable<EmployeeModel> employees =
            //[
            //    new()
            //    {
            //        Id = 1,
            //        Name = "Phat",
            //        Department = "Role",
            //        Email = "Phat.truong@asnet.com.vn",
            //        Role="Admin"
            //    }
            //];
        }

        // GET api/<EmployeeController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<EmployeeController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EmployeeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmployeeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
