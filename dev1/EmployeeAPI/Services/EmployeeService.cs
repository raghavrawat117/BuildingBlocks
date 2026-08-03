using EmployeeAPI.DTOs;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(
        IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeResponseDto>>
        GetAllEmployeesAsync()
    {
        var employees =
            await _repository.GetAllAsync();

        return employees.Select(e =>
            new EmployeeResponseDto
            {
                Id = e.Id!,
                Name = e.Name,
                Department = e.Department,
                Email = e.Email
            }).ToList();
    }

    public async Task CreateEmployeeAsync(
        CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Department = dto.Department,
            Email = dto.Email,
            Salary = dto.Salary
        };

        await _repository.CreateAsync(employee);
    }

    public async Task<EmployeeResponseDto?>
        GetEmployeeByIdAsync(string id)
    {
        var employee =
            await _repository.GetByIdAsync(id);

        if (employee == null)
            return null;

        return new EmployeeResponseDto
        {
            Id = employee.Id!,
            Name = employee.Name,
            Department = employee.Department,
            Email = employee.Email
        };
    }
}