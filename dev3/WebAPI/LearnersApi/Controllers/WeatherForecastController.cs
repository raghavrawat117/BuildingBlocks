using Microsoft.AspNetCore.Mvc;

namespace LearnersApi.Controllers;

// The route should go to the Action, if we use 
// [Route("[controller]")]
// Then API call will be till /WeatherForecast
// other added endpoints will cause conflict in the controller at runtime.
[ApiController]
[Route("[controller]/[action]")] 
public class WeatherForecastController : ControllerBase
{
    // https://share.google/aimode/rk8jiD42Hy6qt8EcH
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    /*
    Instead of this syntax the used one is more readable
     [HttpGet(Name = "GetWeatherForecast")]
     public IEnumerable<WeatherForecast> Get()
    */
    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherForecast2()
    {
        int a=0;
        a++;
        return Ok("It's Working");
    }
}
