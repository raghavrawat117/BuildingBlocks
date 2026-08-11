using ddd_Employee_PhysicalPerson.Domain.Entities;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IEmployeeRepository
{
    Task<bool> CreateEmployeeAsync(EmployeeEntity employee);
    Task<EmployeeEntity> GetEmployeeAsync(int employeeId);
    Task<bool> StoreCanonical(CanonicalEnvelope<EmployeeEntity> employee);
}
