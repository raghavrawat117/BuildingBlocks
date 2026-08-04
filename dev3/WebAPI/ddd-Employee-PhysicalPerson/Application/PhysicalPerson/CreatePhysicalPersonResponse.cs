namespace ddd_Employee_PhysicalPerson.Application;

public class CreatePhysicalPersonResponse
{
    public static string AcknowledgementMessage(int physicalPersonId) => $"Physical person {physicalPersonId} created successfully";
}
