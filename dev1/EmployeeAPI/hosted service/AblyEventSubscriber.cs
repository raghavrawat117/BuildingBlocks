using EmployeeAPI.Models;
using IO.Ably;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EmployeeAPI.HostedService;

public class AblyEventSubscriber : BackgroundService
{
    private readonly AblySettings _ablySettings;
    private readonly ILogger<AblyEventSubscriber> _logger;

    public AblyEventSubscriber(IOptions<AblySettings> ablySettings,ILogger<AblyEventSubscriber> logger)
    {
        _ablySettings = ablySettings.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var ably = new AblyRealtime(_ablySettings.ApiKey);

            var channel = ably.Channels.Get(_ablySettings.ChannelName);

            _logger.LogInformation("Subscribing to channel {Channel}",_ablySettings.ChannelName);

            channel.Subscribe("EmployeeCreated",
                message =>
                {
                    _logger.LogInformation("EmployeeCreated event received");

                    _logger.LogInformation("Payload: {Payload}",message.Data?.ToString());
                });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1),stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred in Ably subscriber");

            throw;
        }
    }
}