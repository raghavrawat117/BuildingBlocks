using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using Microsoft.Extensions.Options;


namespace ddd_Employee_PhysicalPerson.Infra;

public class RedisRepository : IRedisRepository
{
    private readonly ILogger<RedisRepository> _logger;
    // This is option 1.
    //private readonly RedisSettings _redisSettings;
    private readonly IDatabase _db;
    private readonly IJsonCommands _jsonCommands;

    public RedisRepository(
        //IOptions<RedisSettings> redisSettings,
        ILogger<RedisRepository> logger,
        IConnectionMultiplexer connectionMultiplexer
        )
    {
        // This is option 1.
        //_redisSettings = redisSettings.Value;
        // IConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(
        //     new ConfigurationOptions
        //     {
        //         EndPoints = { { _redisSettings.Host, _redisSettings.Port } },
        //         User = _redisSettings.User,
        //         Password = _redisSettings.Password
        //     }
        // );
        _db = connectionMultiplexer.GetDatabase();
        _jsonCommands = _db.JSON();
        _logger = logger;
    }

    public bool SetJson(string key, object value)
    {
        try
        {
            _jsonCommands.Set(key, "$", value);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while setting JSON in Redis");
            return false;
        }
    }
}