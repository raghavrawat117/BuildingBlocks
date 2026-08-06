using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Validations;

namespace ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Contracts;

public class CreatePhysicalPersonResponse
{
    public string StatusMessage { get; }

    public CreatePhysicalPersonResponse(string statusMessage)
    {
        StatusMessage = statusMessage;
    }
}

