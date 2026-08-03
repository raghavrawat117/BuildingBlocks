using EmployeeAPI.Models;

namespace EmployeeAPI.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAsync();
    Task<Employee?> GetByIdAsync(string id);
    Task CreateAsync(Employee employee);
}