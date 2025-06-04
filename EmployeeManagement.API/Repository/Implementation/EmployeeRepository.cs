using Dapper;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Interfaces;

namespace EmployeeManagement.API.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Employee employee)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var query = "INSERT INTO Employees (Name, Department, Email) VALUES (@Name, @Department, @Email)";
                return await connection.ExecuteAsync(query, employee);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM Employees WHERE Id = @Id";
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, new { Id = id });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var query = "SELECT * FROM Employees";
                return await connection.QueryAsync<Employee>(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var query = "SELECT * FROM Employees WHERE Id = @id";
                return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            try
            {
                using var connection = _context.CreateConnection();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", employee.Id);
                parameters.Add("Name", employee.Name);
                parameters.Add("Department", employee.Department);
                parameters.Add("Email", employee.Email);

                var query = "UPDATE Employees SET Name = @Name, Department = @Department, Email = @Email WHERE Id = @Id";
                return await connection.ExecuteAsync(query, parameters);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}