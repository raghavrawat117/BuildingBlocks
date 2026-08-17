using EmployeeAPI.DTOs;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Employee_Repository;
using EmployeeAPI.Repositories.Event_Publisher_Repository;
using MongoDB.Bson;

namespace EmployeeAPI.Services.EmployeeService;

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
        var existingEmployee = await _repository.EmployeeExistsAsync(dto.Email);
        if (existingEmployee)
        {
            throw new DuplicateEmployeeException($"Employee with email {dto.Email} already exists.");
        }
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
        if (!ObjectId.TryParse(id, out _))
        {
            throw new InvalidIdException("Invalid employee id format.");
        }
        var employee =
            await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Employee {id} not found");

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
        if (!ObjectId.TryParse(id, out _))
        {
            throw new InvalidIdException("Invalid employee id format.");
        }
        var employee =
            await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Employee {id} not found");

        employee.Name = dto.Name;
        employee.Department = dto.Department;
        employee.Email = dto.Email;
        employee.Salary = dto.Salary;

        await _repository.UpdateAsync(id, employee);
    }

    public async Task DeleteEmployeeAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            throw new InvalidIdException("Invalid employee id format.");
        }
        _ = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Employee {id} not found");
        await _repository.DeleteAsync(id);
    }
}