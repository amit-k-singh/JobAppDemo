using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Infra.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employee;

        public EmployeeController(IEmployeeServices employee)
        {
            _employee = employee;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(UserLoginRequestModel userLoginRequestModel)
        {
            var result = await _employee.UserLoginAsync(userLoginRequestModel);
            return Ok(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> Get() 
        { 
            var employees = await _employee.GetEmployeesAsync();
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employee.GetEmployeeById(id);
            return Ok(employee);
        }

        [Authorize]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> Post([FromForm] EmployeeRequestModel employee)
        { 
            await _employee.AddEmployee(employee);
            return Ok("Employee added successfully...!!!");
        }

        [Authorize]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> Put([FromForm] EmployeeRequestModel model,int id)
        {
            await _employee.UpdateEmployee(model,id);
            return Ok(await _employee.GetEmployeesAsync());
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _employee.DeleteEmployee(id);
            return Ok(await _employee.GetEmployeesAsync());
        }
    }
}
