using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public static class EmployeeMappings
{
    public static Employee ToEmployee(
        this CreateEmployeeRequest request)
    {
        return new Employee
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
        this Employee employee)
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