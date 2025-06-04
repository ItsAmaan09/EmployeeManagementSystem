using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<EmployeeAddDO> AddAsync(Employee employee);
        Task<EmployeeUpdateDO> UpdateAsync(Employee employee);
        Task<EmployeeDeleteDO> DeleteAsync(int id);
    }
}