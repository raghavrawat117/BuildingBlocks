using EmployeeEntity = ddd_Employee_PhysicalPerson.Domain.Entities.Employee;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IEmployeeRepository
{
    Task<bool> CreateEmployeeAsync(EmployeeEntity employee);
    Task<EmployeeEntity> GetEmployeeAsync(int employeeId);
}
