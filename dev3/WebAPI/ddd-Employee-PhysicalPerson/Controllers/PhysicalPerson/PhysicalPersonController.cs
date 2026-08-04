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
            int physicalPersonId = await _physicalPersonService.CreatePhysicalPersonAsync(createPhysicalPersonRequest);

            return Ok(CreatePhysicalPersonResponse.AcknowledgementMessage(physicalPersonId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating physical person.");
            return StatusCode(500, "Internal server error");
        }
    }
}
