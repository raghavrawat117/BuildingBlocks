using ddd_Employee_PhysicalPerson.Domain;
using FluentValidation;

namespace ddd_Employee_PhysicalPerson.Application;

public class EmployeeService : IEmployeeService
{
    private readonly ILogger<PhysicalPersonService> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<CreateEmployeeRequest> _validator;
    public EmployeeService(
        ILogger<PhysicalPersonService> logger,
        IEmployeeRepository employeeRepository,
        IValidator<CreateEmployeeRequest> validator
    )
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
        _validator = validator;
    }

    public async Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest)
    {
        try
        {
            _logger.LogInformation($"Creating Employee with ID: {createEmployeeRequest.EmployeeId}");

            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(createEmployeeRequest);

            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors.Select(x => x.ErrorMessage)); }

            bool result = await _employeeRepository.CreateEmployeeAsync(createEmployeeRequest.ToEmployee());

            if (!result) { throw new Exception("Failed to create Employee"); }

            _logger.LogInformation($"Successfully created Employee with ID: {createEmployeeRequest.EmployeeId}");

            return new CreateEmployeeResponse(createEmployeeRequest.EmployeeId);

        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Validation has failed with erros:{string.Join(" , ", ex.Errors)}");
            return new CreateEmployeeResponse(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error creating employee");
            throw;
        }
    }

    public async Task<GetEmployeeResponse> GetEmployeeAsync(GetEmployeeRequest getEmployeeRequest)
    {
        try
        {
            _logger.LogInformation($"Getting Employee with ID: {getEmployeeRequest.employeeId}");

            Employee employee = await _employeeRepository.GetEmployeeAsync(getEmployeeRequest.employeeId) ?? throw new Exception("Employee not found");

            _logger.LogInformation($"Successfully retrieved Employee with ID: {getEmployeeRequest.employeeId}");

            GetEmployeeResponse getEmployeeResponse = employee.ToResponse();

            return getEmployeeResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error retrieving employee");
            throw;
        }
    }
}
