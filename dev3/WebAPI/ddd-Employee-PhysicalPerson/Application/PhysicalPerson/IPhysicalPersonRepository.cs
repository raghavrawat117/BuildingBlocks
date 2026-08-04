using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IPhysicalPersonRepository
{
    Task<bool> CreatePhysicalPersonAsync(PhysicalPerson physicalPerson);
    Task<PhysicalPerson> GetPhysicalPersonAsync(int physicalPersonId);
}
