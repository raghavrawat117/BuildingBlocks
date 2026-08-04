using ddd_Employee_PhysicalPerson.Application;
using Microsoft.AspNetCore.Mvc;

namespace ddd_Employee_PhysicalPerson.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
     private readonly IEmployeeService _employeeService;
    public EmployeeController(
         ILogger<EmployeeController> logger,
         IEmployeeService employeeService
    )
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
       try
        {
            _logger.LogInformation("Create Request for Employee received");
            int employeeId = await _employeeService.CreateEmployeeAsync(createEmployeeRequest);

            return Ok(CreateEmployeeResponse.AcknowledgementMessage(employeeId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating employee.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployee([FromQuery] GetEmployeeRequest getEmployeeRequest)
    {
       try
        {
            _logger.LogInformation("Get Request for Employee received");
            GetEmployeeResponse getEmployeeResponse = await _employeeService.GetEmployeeAsync(getEmployeeRequest);

            return Ok(getEmployeeResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving employee.");
            return StatusCode(500, "Internal server error");
        }
    }
}
