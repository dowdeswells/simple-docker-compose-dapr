using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace VSDockerWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DaprClient _daprClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var items = await GetItems();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Items = items
            })
            .ToArray();
        }

        private async Task<IEnumerable<SupportItem>> GetItems()
        {
            var items = await _daprClient.InvokeMethodAsync<IEnumerable<SupportItem>>(HttpMethod.Get,
                "supportapi", "support");
            return items;
        }


    }

    public class SupportItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}