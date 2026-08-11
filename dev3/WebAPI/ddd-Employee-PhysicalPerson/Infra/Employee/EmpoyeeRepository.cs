using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Microsoft.Extensions.Options;
using ddd_Employee_PhysicalPerson.Application;
using ddd_Employee_PhysicalPerson.Domain;
using ddd_Employee_PhysicalPerson.Domain.Entities;


namespace ddd_Employee_PhysicalPerson.Infra;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ILogger<EmployeeRepository> _logger;
    private readonly IRedisRepository _redisRepository;

    public EmployeeRepository(
        ILogger<EmployeeRepository> logger,
        IRedisRepository redisRepository
        )
    {
        _redisRepository = redisRepository;
        _logger = logger;
    }
    
    public async Task<bool> CreateEmployeeAsync(EmployeeEntity employee)
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

    public async Task<EmployeeEntity?> GetEmployeeAsync(int employeeId)
    {
        try
        {
            string key = $"employee:{employeeId}";
            EmployeeEntity? employee = _redisRepository.GetJson<EmployeeEntity>(key);
            return employee;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving employee from Redis");
            return null;
        }
    }

    public async Task<bool> StoreCanonical(CanonicalEnvelope<EmployeeEntity> employee)
    {
        try
        {
            string key = $"canonical:employee:{employee.BusinessKey}";
            bool result = _redisRepository.SetJson(key, employee);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while storing employee in Redis");
            return false;
        }
    }
}