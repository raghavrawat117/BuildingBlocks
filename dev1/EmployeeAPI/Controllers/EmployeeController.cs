using EmployeeAPI.DTOs;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(
        IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeResponseDto>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponseDto>> GetById(string id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
        catch (NotFoundException notfoundException)
        {
            return NotFound(notfoundException.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeDto dto)
    {
        await _employeeService.CreateEmployeeAsync(dto);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    string id,
    [FromBody] UpdateEmployeeDto dto)
    {
        await _employeeService.UpdateEmployeeAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _employeeService.DeleteEmployeeAsync(id);

        return NoContent();
    }
}