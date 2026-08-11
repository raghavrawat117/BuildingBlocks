using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Contracts;
using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Validations;
using ddd_Employee_PhysicalPerson.Domain.Entities;
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
        _logger.LogInformation($"Creating PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

        // // 1.Request Validation
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createPhysicalPersonRequest);

        // if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors.Select(x => x.ErrorMessage)); }
        if (!validationResult.IsValid) { throw new CreatePhysicalPersonValidationException(createPhysicalPersonRequest.PhysicalPersonId , validationResult.Errors.Select(x => x.ErrorMessage)); }

        // // 2.Operation
        bool result = await _physicalPersonRepository.CreatePhysicalPersonAsync(createPhysicalPersonRequest.ToPhysicalPerson());

        if (!result) { throw new Exception("Failed to create Physical Person"); }

        _logger.LogInformation($"Successfully created PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

        return new CreatePhysicalPersonResponse($"Successfully created PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

    }

    public async Task<GetPhysicalPersonResponse> GetPhysicalPersonAsync(GetPhysicalPersonRequest getPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation($"Getting Physical Person with ID: {getPhysicalPersonRequest.physicalPersonId}");

            PhysicalPersonEntity physicalPerson = await _physicalPersonRepository.GetPhysicalPersonAsync(getPhysicalPersonRequest.physicalPersonId) ?? throw new Exception("Physical person not found");

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

    public async Task<CreatePhysicalPersonResponse> CreatePhysicalPersonAsyncV2(CreatePhysicalPersonRequest createPhysicalPersonRequest)
    {
        _logger.LogInformation($"Creating PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

        // // 1. Request Validation
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createPhysicalPersonRequest);

        if (!validationResult.IsValid) { throw new CreatePhysicalPersonValidationException(createPhysicalPersonRequest.PhysicalPersonId , validationResult.Errors.Select(x => x.ErrorMessage)); }

        // // 2. Request to Domain
        PhysicalPersonEntity physicalPerson = createPhysicalPersonRequest.ToPhysicalPerson();

        // // 3. Transform to canonical by applying envelope
        CanonicalEnvelope<PhysicalPersonEntity> physicalPersonCanonical = CanonicalEnvelopeFactory.Create(physicalPerson , "laminar" , 1);
        
        bool result = await _physicalPersonRepository.StoreCanonical(physicalPersonCanonical);
        
        if (!result) { throw new Exception("Failed to process Physical Person"); }

        _logger.LogInformation($"Successfully stored PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

        return new CreatePhysicalPersonResponse($"Successfully stored PhysicalPerson with ID: {createPhysicalPersonRequest.PhysicalPersonId}");

    }
}
