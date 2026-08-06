using EmployeeAPI.Models;
using IO.Ably;
using Microsoft.Extensions.Options;

namespace EmployeeAPI.Repositories;

public class AblyEventPublisher : IEventPublisher
{
    private readonly AblyRealtime _ably;
    private readonly string _channelName;
    private readonly ILogger<AblyEventPublisher> _logger;

    public AblyEventPublisher(
        IOptions<AblySettings> settings,
        ILogger<AblyEventPublisher> logger)
    {
        _ably = new AblyRealtime(settings.Value.ApiKey);
        _logger = logger;

        _channelName = settings.Value.ChannelName;
    }

    public async Task PublishCreatedEmployeeAsync(
        object payload)
    {
        try
        {
            var channel = _ably.Channels.Get(_channelName);

            await channel.PublishAsync(
                "EmployeeCreated",
                payload);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while publishing created employee event to Ably");
            throw;
        }
    }
}