using EmployeeAPI.DTOs;

namespace EmployeeAPI.Services;
public interface IEmployeeService
{
    Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

    Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id);

    Task CreateEmployeeAsync(CreateEmployeeDto dto);
}