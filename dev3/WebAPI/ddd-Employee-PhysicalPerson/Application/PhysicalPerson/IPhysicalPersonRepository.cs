using PhysicalPersonEntity = ddd_Employee_PhysicalPerson.Domain.Entities.PhysicalPerson;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IPhysicalPersonRepository
{
    Task<bool> CreatePhysicalPersonAsync(PhysicalPersonEntity physicalPerson);
    Task<PhysicalPersonEntity> GetPhysicalPersonAsync(int physicalPersonId);
}
