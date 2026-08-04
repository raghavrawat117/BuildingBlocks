using ddd_Employee_PhysicalPerson.Application;
using Microsoft.AspNetCore.Mvc;

namespace ddd_Employee_PhysicalPerson.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PhysicalPersonController : ControllerBase
{
    private readonly ILogger<PhysicalPersonController> _logger;
    private readonly IPhysicalPersonService _physicalPersonService;
    public PhysicalPersonController(
        ILogger<PhysicalPersonController> logger,
        IPhysicalPersonService physicalPersonService
    )
    {
        _logger = logger;
        _physicalPersonService = physicalPersonService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePhysicalPerson([FromBody] CreatePhysicalPersonRequest createPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation("Create Request for PhysicalPerson received");
            CreatePhysicalPersonResponse response = await _physicalPersonService.CreatePhysicalPersonAsync(createPhysicalPersonRequest);

            _logger.LogInformation(response.AcknowledgementMessage);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating physical person.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPhysicalPerson([FromQuery] GetPhysicalPersonRequest getPhysicalPersonRequest)
    {
        try
        {
            _logger.LogInformation("Get Request for PhysicalPerson received");
            GetPhysicalPersonResponse response = await _physicalPersonService.GetPhysicalPersonAsync(getPhysicalPersonRequest);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving physical person.");
            return StatusCode(500, "Internal server error");
        }
    }
}
