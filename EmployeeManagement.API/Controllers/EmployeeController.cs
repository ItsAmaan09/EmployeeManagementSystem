using EmployeeManagement.API.Models;
using EmployeeManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            var result = await _service.AddAsync(employee);
            return result.IsSuccess  ? Ok(result) : BadRequest(result);
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(Employee employee)
        {
            var result = await _service.UpdateAsync(employee);
            return result.IsSuccess ? Ok(result) : BadRequest(result); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result > 0 ? Ok("Success") : BadRequest("Failed");
        }


    }
}