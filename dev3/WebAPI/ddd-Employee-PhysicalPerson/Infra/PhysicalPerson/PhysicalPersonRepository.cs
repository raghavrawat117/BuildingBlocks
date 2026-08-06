using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Microsoft.Extensions.Options;
using ddd_Employee_PhysicalPerson.Application;
using ddd_Employee_PhysicalPerson.Domain;
using PhysicalPersonEntity = ddd_Employee_PhysicalPerson.Domain.Entities.PhysicalPerson;



namespace ddd_Employee_PhysicalPerson.Infra;

public class PhysicalPersonRepository : IPhysicalPersonRepository
{
    private readonly ILogger<PhysicalPersonRepository> _logger;
    private readonly IRedisRepository _redisRepository;

    public PhysicalPersonRepository(
        ILogger<PhysicalPersonRepository> logger,
        IRedisRepository redisRepository
        )
    {
        _redisRepository = redisRepository;
        _logger = logger;
    }
    
    public async Task<bool> CreatePhysicalPersonAsync(PhysicalPersonEntity physicalPerson)
    {
        try
        {
            string key = $"physicalperson:{physicalPerson.PhysicalPersonId}";
            bool result = _redisRepository.SetJson(key, physicalPerson);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating physical person in Redis");
            return false;
        }
    }

    public async Task<PhysicalPersonEntity?> GetPhysicalPersonAsync(int physicalPersonId)
    {
        try
        {
            string key = $"physicalperson:{physicalPersonId}";
            PhysicalPersonEntity? physicalPerson = _redisRepository.GetJson<PhysicalPersonEntity>(key);
            return physicalPerson;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving physical person from Redis");
            return null;
        }
    }
}