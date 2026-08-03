using EmployeeAPI.Models;

namespace EmployeeAPI.Services;

public class EmployeeService : IEmployeeService
{
    private static List<Employee> employees =
    [
        new Employee
        {
            Id = 1,
            Name = "Anibrata",
            Department = "Consulting"
        },
        new Employee
        {
            Id = 2,
            Name = "John",
            Department = "IT"
        }
    ];

    public List<Employee> GetEmployees()
    {
        return employees;
    }
}