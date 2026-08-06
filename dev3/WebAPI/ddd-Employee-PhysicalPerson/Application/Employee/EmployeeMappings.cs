using ddd_Employee_PhysicalPerson.Application.Employee.Contracts;
// This still collides since .Employee is being used
//using ddd_Employee_PhysicalPerson.Domain.Entities;
// better to alias it here
using EmployeeEntity = ddd_Employee_PhysicalPerson.Domain.Entities.Employee;


namespace ddd_Employee_PhysicalPerson.Application.Employee;

public static class EmployeeMappings
{
    public static EmployeeEntity ToEmployee(
        this CreateEmployeeRequest request)
    {
        return new EmployeeEntity
        {
            FirstName = request.FirstName,
            PhysicalPersonId = request.PhysicalPersonId,
            LastName = request.LastName,
            Location = request.Location,
            Salary = request.Salary,
            Grade = request.Grade,
            Experience = request.Experience,
            PhoneNumber = request.PhoneNumber,
            WorkEmail = request.WorkEmail
        };
    }

    public static GetEmployeeResponse ToResponse(
        this EmployeeEntity employee)
    {
        return new GetEmployeeResponse
        {
            EmployeeId = employee.EmployeeId,
            PhysicalPersonId = employee.PhysicalPersonId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Location = employee.Location,
            Grade = employee.Grade,
            Experience = employee.Experience,
            WorkEmail = employee.WorkEmail
        };
    }
}