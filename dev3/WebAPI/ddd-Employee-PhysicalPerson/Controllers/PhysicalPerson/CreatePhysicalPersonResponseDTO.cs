namespace ddd_Employee_PhysicalPerson.Controllers;

public class CreatePhysicalPersonResponseDTO
{
    public static string AcknowledgementMessage(int physicalPersonId) => $"Physical person {physicalPersonId} created successfully";
}
