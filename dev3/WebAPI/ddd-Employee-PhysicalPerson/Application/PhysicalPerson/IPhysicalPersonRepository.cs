using ddd_Employee_PhysicalPerson.Domain.Entities;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IPhysicalPersonRepository
{
    Task<bool> CreatePhysicalPersonAsync(PhysicalPersonEntity physicalPerson);
    Task<PhysicalPersonEntity> GetPhysicalPersonAsync(int physicalPersonId);
    Task<bool> DoesPhysicalPersonExistAsync(int physicalPersonId);
    Task<bool> StoreCanonical(CanonicalEnvelope<PhysicalPersonEntity> physicalPerson);
}
