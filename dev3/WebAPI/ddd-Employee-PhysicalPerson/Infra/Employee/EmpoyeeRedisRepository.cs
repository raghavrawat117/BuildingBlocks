using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Microsoft.Extensions.Options;
using ddd_Employee_PhysicalPerson.Application;
using ddd_Employee_PhysicalPerson.Domain;


namespace ddd_Employee_PhysicalPerson.Infra;

public class EmployeeRedisRepository : IEmployeeRepository
{
    private readonly ILogger<EmployeeRedisRepository> _logger;
    private readonly IRedisRepository _redisRepository;

    public EmployeeRedisRepository(
        ILogger<EmployeeRedisRepository> logger,
        IRedisRepository redisRepository
        )
    {
        _redisRepository = redisRepository;
        _logger = logger;
    }
    
    public async Task<bool> CreateEmployeeAsync(Employee employee)
    {
        try
        {
            string key = $"employee:{employee.EmployeeId}";
            bool result = _redisRepository.SetJson(key, employee);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating employee in Redis");
            return false;
        }
    }

    public async Task<Employee?> GetEmployeeAsync(int employeeId)
    {
        try
        {
            string key = $"employee:{employeeId}";
            Employee? employee = _redisRepository.GetJson<Employee>(key);
            return employee;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving employee from Redis");
            return null;
        }
    }
}