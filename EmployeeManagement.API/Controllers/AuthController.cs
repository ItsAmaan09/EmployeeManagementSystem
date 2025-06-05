using EmployeeManagement.API.DTOs;
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
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _userService.ValidateUser(login.Username, login.Password);
            if (user == null) return Unauthorized(new { IsSuccess = false, Message = "Invalid crediantials" });

            var token = _tokenService.GenerateToken(user.Username, user.Role);
            return Ok(new { IsSuccess = true, Token = token, Role = user.Role });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            var success = await _userService.RegisterUser(dto);
            if (!success) return BadRequest(new { IsSuccess = false, Message = "User not created." });

            return Ok(new { IsSuccess = true, Message = "User created successfully." });
        }
    }
}