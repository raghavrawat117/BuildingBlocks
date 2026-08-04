using EmployeeAPI.DTOs;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public EmployeeService(
        IEmployeeRepository repository,
        IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
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

        await _eventPublisher.PublishCreatedEmployeeAsync(employee);
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

    public async Task UpdateEmployeeAsync(
    string id,
    UpdateEmployeeDto dto)
{
    var employee =
        await _repository.GetByIdAsync(id);

    if (employee == null)
        throw new Exception($"Employee {id} not found");

    employee.Name = dto.Name;
    employee.Department = dto.Department;
    employee.Email = dto.Email;
    employee.Salary = dto.Salary;

    await _repository.UpdateAsync(id, employee);
}

public async Task DeleteEmployeeAsync(string id)
{
    var employee =
        await _repository.GetByIdAsync(id);

    if (employee == null)
        throw new Exception($"Employee {id} not found");

    await _repository.DeleteAsync(id);
}
}