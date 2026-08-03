using ddd_Employee_PhysicalPerson.Controllers;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IPhysicalPersonService
{
    Task<int> CreatePhysicalPersonAsync(CreatePhysicalPersonDTO physicalPerson);
}
