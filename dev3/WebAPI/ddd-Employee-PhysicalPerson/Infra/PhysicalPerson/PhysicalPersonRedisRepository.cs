using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Microsoft.Extensions.Options;
using ddd_Employee_PhysicalPerson.Application;
using ddd_Employee_PhysicalPerson.Domain;


namespace ddd_Employee_PhysicalPerson.Infra;

public class PhysicalPersonRedisRepository : IPhysicalPersonRepository
{
    private readonly ILogger<PhysicalPersonRedisRepository> _logger;
    private readonly IRedisRepository _redisRepository;

    public PhysicalPersonRedisRepository(
        ILogger<PhysicalPersonRedisRepository> logger,
        IRedisRepository redisRepository
        )
    {
        _redisRepository = redisRepository;
        _logger = logger;
    }
    
    public async Task<bool> CreatePhysicalPersonAsync(PhysicalPerson physicalPerson)
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
}