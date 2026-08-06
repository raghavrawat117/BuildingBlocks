using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Validations;

namespace ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Contracts;

public class CreatePhysicalPersonResponse
{
    public CreatePhysicalPersonResponse(int physicalPersonId)
    {
        AcknowledgementMessage = $"Physical person {physicalPersonId} created successfully";
    }
    public CreatePhysicalPersonResponse(CreatePhysicalPersonValidationException ex)
    {
        AcknowledgementMessage = $"Validation for PhysicalPerson with Id:{ex.FailedId} has failed with erros:{ex.ErrorMessage}";
    }
    public string AcknowledgementMessage { get; }

}

