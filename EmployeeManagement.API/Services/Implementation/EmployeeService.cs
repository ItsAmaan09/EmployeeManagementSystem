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
        public async Task<EmployeeAddResponseBO> AddAsync(Employee employee)
        {
            try
            {
                var employeeAddResponseBO = new EmployeeAddResponseBO();

                var employeeAddDO = await _repository.AddAsync(employee);

                if (employeeAddDO != null)
                {
                    return new EmployeeAddResponseBO
                    {
                        IsSuccess = true,
                        EmployeeId = employeeAddDO.EmployeeId,
                        Message = "Employee added successfully.",
                    };
                }
                return new EmployeeAddResponseBO
                {
                    IsSuccess = false,
                    EmployeeId = null,
                    Message = "Employee added failed"
                };
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<EmployeeUpdateResponseBO> UpdateAsync(Employee employee)
        {
            try
            {
                var employeeUpdateResponseBO = new EmployeeUpdateResponseBO();

                var employeeUpdateDO = await _repository.UpdateAsync(employee);
                if (employeeUpdateDO != null)
                {
                    employeeUpdateResponseBO.IsSuccess = true;
                    employeeUpdateResponseBO.EmployeeId = employeeUpdateDO.EmployeeId;
                    employeeUpdateResponseBO.Message = "Employee updated successfully.";
                }
                else
                {
                    employeeUpdateResponseBO.IsSuccess = false;
                    employeeUpdateResponseBO.EmployeeId = employeeUpdateDO?.EmployeeId;
                    employeeUpdateResponseBO.Message = "Employee updated failed.";
                }
                return employeeUpdateResponseBO;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public async Task<EmployeeDeleteResponseBO> DeleteAsync(int id)
        {
            var employeeDeleteResponseBO = new EmployeeDeleteResponseBO();

            var employeeDeleteDO = await _repository.DeleteAsync(id);

            if (employeeDeleteDO != null)
            {
                return new EmployeeDeleteResponseBO
                {
                    IsSuccess = true,
                    EmployeeId = employeeDeleteDO.EmployeeId,
                    Message = "Employee deleted successfully."
                };
            }
            return new EmployeeDeleteResponseBO
            {
                IsSuccess = false,
                EmployeeId = employeeDeleteDO.EmployeeId,
                Message = "Employee deleted successfully."
            };
        }
    }
}
