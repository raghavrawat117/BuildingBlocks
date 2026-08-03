using EmployeeAPI.Models;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(string id);

    Task CreateAsync(Employee employee);
}