using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Contracts;
using ddd_Employee_PhysicalPerson.Controllers;

namespace ddd_Employee_PhysicalPerson.Application;

public interface IPhysicalPersonService
{
    Task<CreatePhysicalPersonResponse> CreatePhysicalPersonAsync(CreatePhysicalPersonRequest createPhysicalPersonRequest);
    Task<GetPhysicalPersonResponse> GetPhysicalPersonAsync(GetPhysicalPersonRequest getPhysicalPersonRequest);
}
