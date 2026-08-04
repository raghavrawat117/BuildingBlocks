using EmployeeAPI.Models;
using IO.Ably;
using Microsoft.Extensions.Options;

namespace EmployeeAPI.Services;

public class AblyEventPublisher : IEventPublisher
{
    private readonly AblyRealtime _ably;
    private readonly string _channelName;

    public AblyEventPublisher(
        IOptions<AblySettings> settings)
    {
        _ably = new AblyRealtime(settings.Value.ApiKey);

        _channelName = settings.Value.ChannelName;
    }

    public async Task PublishCreatedEmployeeAsync(
        object payload)
    {
        var channel = _ably.Channels.Get(_channelName);

        await channel.PublishAsync(
            "EmployeeCreated",
            payload);
    }
}