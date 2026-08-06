using ddd_Employee_PhysicalPerson.Application.Employee;
using ddd_Employee_PhysicalPerson.Application.Employee.BusinessRules;
using ddd_Employee_PhysicalPerson.Application.Employee.Contracts;
using ddd_Employee_PhysicalPerson.Application.Employee.Validations;
using ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Contracts;
using ddd_Employee_PhysicalPerson.Domain.Entities;
using FluentValidation;

namespace ddd_Employee_PhysicalPerson.Application;

public class EmployeeService : IEmployeeService
{
    private readonly ILogger<PhysicalPersonService> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPhysicalPersonRepository _physicalPersonRepository;
    private readonly IValidator<CreateEmployeeRequest> _validator;
    public EmployeeService(
        ILogger<PhysicalPersonService> logger,
        IEmployeeRepository employeeRepository,
        IPhysicalPersonRepository physicalPersonRepository,
        IValidator<CreateEmployeeRequest> validator
    )
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
        _physicalPersonRepository = physicalPersonRepository;
        _validator = validator;
    }

    public async Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest)
    {

        _logger.LogInformation($"Creating Employee with ID: {createEmployeeRequest.EmployeeId}");

        // // 1. Request Validation
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createEmployeeRequest);
        if (!validationResult.IsValid) { throw new CreateEmployeeValidationException(createEmployeeRequest.EmployeeId, validationResult.Errors.Select(x => x.ErrorMessage)); }

        // // 2. Business Rule Validation
        bool exists = await _physicalPersonRepository.DoesPhysicalPersonExistAsync(createEmployeeRequest.PhysicalPersonId);
        if (!exists) { throw new PhysicalPersonNotFoundException(createEmployeeRequest.PhysicalPersonId); }

        // // 3. Operation
        bool result = await _employeeRepository.CreateEmployeeAsync(createEmployeeRequest.ToEmployee());

        if (!result) { throw new Exception("Failed to create Employee"); }

        _logger.LogInformation($"Successfully created Employee with ID: {createEmployeeRequest.EmployeeId}");

        return new CreateEmployeeResponse($"Successfully created Employee with ID: {createEmployeeRequest.EmployeeId}");
    }

    public async Task<GetEmployeeResponse> GetEmployeeAsync(GetEmployeeRequest getEmployeeRequest)
    {
        _logger.LogInformation($"Getting Employee with ID: {getEmployeeRequest.employeeId}");

        EmployeeEntity employee = await _employeeRepository.GetEmployeeAsync(getEmployeeRequest.employeeId) ?? throw new Exception("Employee not found");

        _logger.LogInformation($"Successfully retrieved Employee with ID: {getEmployeeRequest.employeeId}");

        GetEmployeeResponse getEmployeeResponse = employee.ToResponse();

        return getEmployeeResponse;
    }

    public async Task<CreateEmployeeResponse> CreateEmployeeAsyncV2(CreateEmployeeRequest createEmployeeRequest)
    {

        _logger.LogInformation($"Creating Employee with ID: {createEmployeeRequest.EmployeeId}");

        // // 1. Request Validation
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createEmployeeRequest);
        if (!validationResult.IsValid) { throw new CreateEmployeeValidationException(createEmployeeRequest.EmployeeId, validationResult.Errors.Select(x => x.ErrorMessage)); }

        // // 2. Business Rule Validation
        bool exists = await _physicalPersonRepository.DoesPhysicalPersonExistAsync(createEmployeeRequest.PhysicalPersonId);
        if (!exists) { throw new PhysicalPersonNotFoundException(createEmployeeRequest.PhysicalPersonId); }

        // // 3. Request to Domain
        EmployeeEntity employee = createEmployeeRequest.ToEmployee(); 
        
        // // 4. Transform to canonical by applying envelope
        CanonicalEnvelope<EmployeeEntity> employeeCanonical = CanonicalEnvelopeFactory.Create(employee , "laminar" , 1);

        bool result = await _employeeRepository.StoreCanonical(employeeCanonical);

        if (!result) { throw new Exception("Failed to process Employee"); }

        _logger.LogInformation($"Successfully stored Employee with ID: {createEmployeeRequest.EmployeeId}");

        return new CreateEmployeeResponse($"Successfully stored Employee with ID: {createEmployeeRequest.EmployeeId}");
    }
}
