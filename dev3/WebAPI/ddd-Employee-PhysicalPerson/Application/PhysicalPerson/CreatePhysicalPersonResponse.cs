namespace ddd_Employee_PhysicalPerson.Application;

public class CreatePhysicalPersonResponse
{
    public CreatePhysicalPersonResponse(int physicalPersonId)
    {
        AcknowledgementMessage = $"Physical person {physicalPersonId} created successfully";
    }
    public CreatePhysicalPersonResponse(ValidationException ex)
    {
        AcknowledgementMessage = $"Validation has failed with erros:{string.Join(" , ", ex.Errors)}";
    }
    public string AcknowledgementMessage { get; }

}

