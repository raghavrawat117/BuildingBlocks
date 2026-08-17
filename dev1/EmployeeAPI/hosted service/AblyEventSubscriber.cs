using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Template_Repository;
using EmployeeAPI.Services.TransformationService;
using IO.Ably;
using Microsoft.Extensions.Options;

namespace EmployeeAPI.HostedService;

public class AblyEventSubscriber : BackgroundService
{
    private readonly AblySettings _ablySettings;
    private readonly ILogger<AblyEventSubscriber> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AblyEventSubscriber(IOptions<AblySettings> ablySettings, ILogger<AblyEventSubscriber> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _ablySettings = ablySettings.Value;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var ably = new AblyRealtime(_ablySettings.ApiKey);

            var channel = ably.Channels.Get(_ablySettings.ChannelName);

            _logger.LogInformation("Subscribing to channel {Channel}", _ablySettings.ChannelName);

            channel.Subscribe("EmployeeCreated",
                async void (message) =>
                {
                    _logger.LogInformation("EmployeeCreated event received");

                    _logger.LogInformation("Payload: {Payload}", message.Data?.ToString());

                    using var scope = _serviceScopeFactory.CreateScope();

                    var templateRepository = scope.ServiceProvider.GetRequiredService<ITemplateRepository>();
                    var transformationService = scope.ServiceProvider.GetRequiredService<ITransformationService>();


                    var templateContent = await templateRepository.GetTemplateByNameAsync("EmployeeJson");

                    if (templateContent == null)
                    {
                        throw new NotFoundException("Template not found for EmployeeCreatedTemplate");
                    }

                    var transformedEmployeeData = await transformationService.TransformEmployeeDataBasedOnTemplateAsync(templateContent.TemplateContent, message.Data);
                    _logger.LogInformation("Transformed Employee Data: {TransformedData}", transformedEmployeeData);
                });
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {

            _logger.LogInformation(

            "Ably subscriber is shutting down.");

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