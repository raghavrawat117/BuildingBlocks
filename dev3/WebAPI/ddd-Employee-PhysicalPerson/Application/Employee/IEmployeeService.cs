using ddd_Employee_PhysicalPerson.Controllers;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(CreateEmployeeDTO employee);
}
