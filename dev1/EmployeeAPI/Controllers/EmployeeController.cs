using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> Get()
    {
        return await _service.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(string id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee is null)
            return NotFound();

        return employee;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        await _service.CreateAsync(employee);

        return Ok(employee);
    }
}