using ddd_Employee_PhysicalPerson.Application.Employee.Contracts;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IEmployeeService
{
    Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest);
    Task<CreateEmployeeResponse> CreateEmployeeAsyncV2(CreateEmployeeRequest createEmployeeRequest);
    Task<GetEmployeeResponse> GetEmployeeAsync(GetEmployeeRequest getEmployeeRequest);
}
