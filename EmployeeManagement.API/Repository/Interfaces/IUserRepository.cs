using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Repository.Implementation
{
    public interface IUserRepository
    {
        Task<User> ValidateUser(string username, string password);
        Task<bool> RegisterUser(UserRegisterDTO dto);
    }
}