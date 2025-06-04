using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<EmployeeAddResponseBO> AddAsync(Employee employee);
        Task<EmployeeUpdateResponseBO> UpdateAsync(Employee employee);
        Task<int> DeleteAsync(int id);
    }
}
