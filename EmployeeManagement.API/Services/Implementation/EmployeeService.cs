using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Interfaces;
using EmployeeManagement.API.Services.Interfaces;

namespace EmployeeManagement.API.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Employee>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Employee> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<int> AddAsync(Employee employee) => _repository.AddAsync(employee);
        public Task<int> UpdateAsync(Employee employee) => _repository.UpdateAsync(employee);
        public Task<int> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
