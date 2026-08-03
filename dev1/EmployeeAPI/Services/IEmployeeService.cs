using EmployeeAPI.Models;

namespace EmployeeAPI.Services;

public interface IEmployeeService
{
    List<Employee> GetEmployees();
}