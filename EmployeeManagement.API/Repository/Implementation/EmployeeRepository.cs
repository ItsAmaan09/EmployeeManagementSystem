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

        public async Task<EmployeeAddDO?> AddAsync(Employee employee)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("Name", employee.Name);
                parameters.Add("Department", employee.Department);
                parameters.Add("Email", employee.Email);

                var query = @"INSERT INTO Employees (Name, Department, Email) 
                                        VALUES (@Name, @Department, @Email)
                                        SELECT CAST(SCOPE_IDENTITY() as INT);";

                var insertedId = await connection.ExecuteScalarAsync<int>(query, parameters);
                return insertedId > 0 ? new EmployeeAddDO { EmployeeId = insertedId } : null;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<EmployeeDeleteDO> DeleteAsync(int id)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                var query = "DELETE FROM Employees WHERE Id = @Id";
                var rowsAffected = await connection.ExecuteAsync(query, parameters);

                return rowsAffected > 0 ? new EmployeeDeleteDO { EmployeeId = id } : null;
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

        public async Task<EmployeeUpdateDO?> UpdateAsync(Employee employee)
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
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                return rowsAffected > 0 ? new EmployeeUpdateDO { EmployeeId = employee.Id } : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}