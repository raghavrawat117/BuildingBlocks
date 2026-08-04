using ddd_Employee_PhysicalPerson.Domain;

namespace ddd_Employee_PhysicalPerson.Application;

public class EmployeeService : IEmployeeService
{
    private readonly ILogger<PhysicalPersonService> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(
        ILogger<PhysicalPersonService> logger,
        IEmployeeRepository employeeRepository
    )
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    public async Task<int> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest)
    {
        try
        {
            _logger.LogInformation($"Creating Employee with ID: {createEmployeeRequest.EmployeeId}");
            
            bool result = await _employeeRepository.CreateEmployeeAsync(createEmployeeRequest.ToEmployee());

            if (!result)
            {
                throw new Exception("Failed to create Employee");
            }

            _logger.LogInformation($"Successfully created Employee with ID: {createEmployeeRequest.EmployeeId}");

            return createEmployeeRequest.EmployeeId;

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

            Employee employee = await _employeeRepository.GetEmployeeAsync(getEmployeeRequest.employeeId);

            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

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
