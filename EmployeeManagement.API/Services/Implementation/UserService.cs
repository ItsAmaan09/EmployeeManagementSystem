using Dapper;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Implementation;
using EmployeeManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DapperContext _context;
        private readonly IUserRepository _userRepository;
        public UserService(DapperContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<User> ValidateUser(string username, string password)
        {
            try
            {
                return await _userRepository.ValidateUser(username, password);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> RegisterUser(UserRegisterDTO dto)
        {
            try
            {
                return await _userRepository.RegisterUser(dto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}