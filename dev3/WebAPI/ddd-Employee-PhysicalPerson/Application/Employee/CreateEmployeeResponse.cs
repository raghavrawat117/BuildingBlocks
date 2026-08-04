namespace ddd_Employee_PhysicalPerson.Application;

public class CreateEmployeeResponse
{
    public static string AcknowledgementMessage(int employeeId) => $"Employee {employeeId} created successfully";
}
