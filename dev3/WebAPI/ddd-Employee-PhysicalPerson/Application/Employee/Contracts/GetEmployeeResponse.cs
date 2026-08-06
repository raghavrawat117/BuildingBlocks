namespace ddd_Employee_PhysicalPerson.Application.Employee.Contracts;

public class GetEmployeeResponse
{
    public int EmployeeId { get; set; }
    public int PhysicalPersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Grade { get; set; }
    public int Experience { get; set; }
    public long PhoneNumber { get; set; }
    public string WorkEmail { get; set; } = string.Empty;
}
