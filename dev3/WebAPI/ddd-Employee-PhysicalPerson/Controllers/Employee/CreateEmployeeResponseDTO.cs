namespace ddd_Employee_PhysicalPerson.Controllers;

public class CreateEmployeeResponseDTO
{
    public static string AcknowledgementMessage(int employeeId) => $"Employee {employeeId} created successfully";
}
