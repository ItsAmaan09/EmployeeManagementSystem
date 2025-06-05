using Dapper;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Services.Interfaces;

namespace EmployeeManagement.API.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DapperContext _context;
        public UserService(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> ValidateUser(string username, string password)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("Username", username);
                parameters.Add("Password", password);
                var query = @"SELECT * FROM Users WHERE Username = @username AND
                Password = @password";

                return await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}