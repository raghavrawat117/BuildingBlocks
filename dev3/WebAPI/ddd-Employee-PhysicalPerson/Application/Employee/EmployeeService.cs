using ddd_Employee_PhysicalPerson.Controllers;
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

    public async Task<int> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
    {
        try
        {
            _logger.LogInformation($"Creating Employee with ID: {createEmployeeDTO.EmployeeId}");

            Employee employee = new Employee
            {
                EmployeeId = createEmployeeDTO.EmployeeId,
                PhysicalPersonId = createEmployeeDTO.PhysicalPersonId,
                FirstName = createEmployeeDTO.FirstName,
                LastName = createEmployeeDTO.LastName,
                Location = createEmployeeDTO.Location,
                Grade = createEmployeeDTO.Grade,
                Experience = createEmployeeDTO.Experience,
                PhoneNumber = createEmployeeDTO.PhoneNumber,
                WorkEmail = createEmployeeDTO.WorkEmail
            };

            bool result = await _employeeRepository.CreateEmployeeAsync(employee);

            if ( !result )
            {
                throw new Exception("Failed to create Employee");
            }

            _logger.LogInformation($"Successfully created Employee with ID: {createEmployeeDTO.EmployeeId}");

            return employee.EmployeeId;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error creating employee");
            throw;
        }
    }
}
