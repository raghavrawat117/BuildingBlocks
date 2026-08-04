using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public static class PhysicalPersonMappings
{
    public static PhysicalPerson ToPhysicalPerson(
        this CreatePhysicalPersonRequest request)
    {
        return new PhysicalPerson
        {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Ssno = request.Ssno
        };
    }

    public static GetPhysicalPersonResponse ToGetPhysicalPersonResponse(
        this PhysicalPerson physicalPerson)
    {
        return new GetPhysicalPersonResponse
        {
            PhysicalPersonId = physicalPerson.PhysicalPersonId,
            Name = physicalPerson.Name,
            DateOfBirth = physicalPerson.DateOfBirth,
            Address = physicalPerson.Address,
            PhoneNumber = physicalPerson.PhoneNumber,
        };
    }
}