using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> ValidateUser(string username, string password);
    }
}