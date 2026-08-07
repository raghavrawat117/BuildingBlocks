using EmployeeAPI.Models;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(string id);

    Task CreateAsync(Employee employee);

    Task UpdateAsync(string id, Employee employee);

    Task DeleteAsync(string id);

    Task<bool> EmployeeExistsAsync(string email);
}