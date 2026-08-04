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

    public async Task<int> CreatePhysicalPersonAsync(CreatePhysicalPersonRequest createPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation($"Creating PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

            PhysicalPerson physicalPerson = new PhysicalPerson
            {
                PhysicalPersonId = createPhysicalPersonRequest.PhysicalPersonId,
                Name = createPhysicalPersonRequest.Name,
                Address = createPhysicalPersonRequest.Address,
                DateOfBirth = createPhysicalPersonRequest.DateOfBirth,
                PhoneNumber = createPhysicalPersonRequest.PhoneNumber,
                Ssno = createPhysicalPersonRequest.Ssno
            };

            bool result = await _physicalPersonRepository.CreatePhysicalPersonAsync(physicalPerson);

            if ( !result )
            {
                throw new Exception("Failed to create Physical Person");
            }

            _logger.LogInformation($"Successfully created PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

            return physicalPerson.PhysicalPersonId;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error creating physical person");
            throw;
        }
    }
}
