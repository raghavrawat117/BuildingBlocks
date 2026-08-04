namespace ddd_Employee_PhysicalPerson.Application;

public class GetPhysicalPersonResponse
{
    public int PhysicalPersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
}