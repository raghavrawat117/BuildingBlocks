namespace ddd_Employee_PhysicalPerson.Application.Employee.BusinessRules;

public class PhysicalPersonNotFoundException : Exception
{
    public string ErrorMessage { get; }
    public int FailedId { get; }

    public PhysicalPersonNotFoundException(int physicalPersonId)
    {
        FailedId = physicalPersonId;
        ErrorMessage = $"Physical Person with Id:{physicalPersonId}, doesn't exist in the database.";
    }
}