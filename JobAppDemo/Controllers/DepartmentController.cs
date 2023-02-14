using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Infra.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _department;

        public DepartmentController(IDepartmentServices department)
        {
            _department = department;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var departments = await _department.GetDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var department = await _department.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DepartmentRequestModel department) 
        {
            try
            {
                await _department.AddDepartmentAsync(department);
                return Created("getDepartment", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(DepartmentRequestModel department,int Id)
        {
            try
            {
                await _department.UpdateDepartmentAsync(department,Id);
                return Ok(await _department.GetDepartmentsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
