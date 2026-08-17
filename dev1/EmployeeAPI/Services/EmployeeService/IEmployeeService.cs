using EmployeeAPI.DTOs;

namespace EmployeeAPI.Services.EmployeeService;
public interface IEmployeeService
{
    Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

    Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id);

    Task CreateEmployeeAsync(CreateEmployeeDto dto);

    Task UpdateEmployeeAsync(string id, UpdateEmployeeDto dto);

    Task DeleteEmployeeAsync(string id);
}