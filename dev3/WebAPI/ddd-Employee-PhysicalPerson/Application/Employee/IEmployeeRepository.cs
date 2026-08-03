using ddd_Employee_PhysicalPerson.Controllers;
using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IEmployeeRepository
{
    Task<bool> CreateEmployeeAsync(Employee employee);
}
