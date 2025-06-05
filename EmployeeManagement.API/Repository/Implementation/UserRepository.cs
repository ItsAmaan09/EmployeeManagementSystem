using Dapper;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
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
                var query = @"SELECT * FROM Users WHERE Username = @username";
                return await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
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
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("Username", dto.Username);
                parameters.Add("Password", dto.Password);
                parameters.Add("Role", dto.Role);

                var query = @"INSERT INTO Users (Username, Password, Role)
                VALUES (@Username, @Password, @Role)";

                var result = await connection.ExecuteAsync(query, parameters);

                return result > 0;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}