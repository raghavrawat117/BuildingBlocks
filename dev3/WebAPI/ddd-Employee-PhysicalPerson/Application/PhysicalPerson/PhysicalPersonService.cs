using ddd_Employee_PhysicalPerson.Controllers;
using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public class PhysicalPersonService : IPhysicalPersonService
{
    private readonly ILogger<PhysicalPersonService> _logger;
    private readonly IPhysicalPersonRepository _physicalPersonRepository;
    public PhysicalPersonService(
        ILogger<PhysicalPersonService> logger,
        IPhysicalPersonRepository physicalPersonRepository
    )
    {
        _logger = logger;
        _physicalPersonRepository = physicalPersonRepository;
    }

    public async Task<int> CreatePhysicalPersonAsync(CreatePhysicalPersonDTO createPhysicalPersonDTO)
    {
        try
        {
            _logger.LogInformation($"Creating PhysicalPerson with ID: {createPhysicalPersonDTO.PhysicalPersonId}");

            PhysicalPerson physicalPerson = new PhysicalPerson
            {
                PhysicalPersonId = createPhysicalPersonDTO.PhysicalPersonId,
                Name = createPhysicalPersonDTO.Name,
                Address = createPhysicalPersonDTO.Address,
                DateOfBirth = createPhysicalPersonDTO.DateOfBirth,
                PhoneNumber = createPhysicalPersonDTO.PhoneNumber,
                Ssno = createPhysicalPersonDTO.Ssno
            };

            bool result = await _physicalPersonRepository.CreatePhysicalPersonAsync(physicalPerson);

            if ( !result )
            {
                throw new Exception("Failed to create Physical Person");
            }

            _logger.LogInformation($"Successfully created PhysicalPerson with ID: {createPhysicalPersonDTO.PhysicalPersonId}");

            return physicalPerson.PhysicalPersonId;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error creating physical person");
            throw;
        }
    }
}
