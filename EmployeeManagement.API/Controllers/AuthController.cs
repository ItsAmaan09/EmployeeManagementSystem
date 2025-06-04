using EmployeeManagement.API.Models;
using EmployeeManagement.API.Services.Interfaces;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _service;

        public AuthController(ITokenService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login.Username == "admin" && login.Password == "1234")
            {
                var token = _service.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized("Invalid crediantials");
        }
    }
}