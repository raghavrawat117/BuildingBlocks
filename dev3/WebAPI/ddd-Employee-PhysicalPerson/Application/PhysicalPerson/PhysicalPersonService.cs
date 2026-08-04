using ddd_Employee_PhysicalPerson.Domain;
using FluentValidation;

namespace ddd_Employee_PhysicalPerson.Application;

public class PhysicalPersonService : IPhysicalPersonService
{
    private readonly ILogger<PhysicalPersonService> _logger;
    private readonly IPhysicalPersonRepository _physicalPersonRepository;
    private readonly IValidator<CreatePhysicalPersonRequest> _validator;
    public PhysicalPersonService(
        ILogger<PhysicalPersonService> logger,
        IPhysicalPersonRepository physicalPersonRepository,
        IValidator<CreatePhysicalPersonRequest> validator
    )
    {
        _logger = logger;
        _physicalPersonRepository = physicalPersonRepository;
        _validator = validator;
    }

    public async Task<CreatePhysicalPersonResponse> CreatePhysicalPersonAsync(CreatePhysicalPersonRequest createPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation($"Creating PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createPhysicalPersonRequest);

            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors.Select(x => x.ErrorMessage)); }

            bool result = await _physicalPersonRepository.CreatePhysicalPersonAsync(createPhysicalPersonRequest.ToPhysicalPerson());

            if (!result) { throw new Exception("Failed to create Physical Person"); }

            _logger.LogInformation($"Successfully created PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

            return new CreatePhysicalPersonResponse(createPhysicalPersonRequest.PhysicalPersonId);

        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Validation has failed with erros:{string.Join(" , ", ex.Errors)}");
            return new CreatePhysicalPersonResponse(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error creating physical person");
            throw;
        }
    }

    public async Task<GetPhysicalPersonResponse> GetPhysicalPersonAsync(GetPhysicalPersonRequest getPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation($"Getting Physical Person with ID: {getPhysicalPersonRequest.physicalPersonId}");

            PhysicalPerson physicalPerson = await _physicalPersonRepository.GetPhysicalPersonAsync(getPhysicalPersonRequest.physicalPersonId) ?? throw new Exception("Physical person not found");

            _logger.LogInformation($"Successfully retrieved Physical Person with ID: {getPhysicalPersonRequest.physicalPersonId}");

            GetPhysicalPersonResponse getPhysicalPersonResponse = physicalPerson.ToGetPhysicalPersonResponse();

            return getPhysicalPersonResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error retrieving physical person");
            throw;
        }
    }
}
