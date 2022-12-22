using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrafficLight.Infrastructure.Models;
using TrafficLight.Infrastructure.Services.Interfaces;

namespace TrafficLight.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLightController : ControllerBase
    {
        private readonly ITrafficLightService _trafficLightService;

        public TrafficLightController(ITrafficLightService trafficLightService)
        {
            _trafficLightService = trafficLightService;
        }
        //    private static readonly string[] Summaries = new[]
        // {
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //    private readonly ILogger<WeatherForecastController> _logger;

        //    public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //    {
        //        _logger = logger;
        //    }

        //    [HttpGet(Name = "GetWeatherForecast")]
        //    public IEnumerable<WeatherForecast> Get()
        //    {
        //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = Random.Shared.Next(-20, 55),
        //            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //    }

        private TrafficLightModel TrafficLight = new TrafficLightModel();
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<TrafficLightModel>>> GetTrafficLihtConfiguration()
        { 
            var trafficLightConfiguration = await _trafficLightService.GetTrafficLightConfiguration();
            return Ok(trafficLightConfiguration);
        }
        [HttpPost]
        public async Task<ActionResult<TrafficLightModel>> GetTrafficLihtConfiguration(TrafficLightModel trafficLightConfiguration)
        {
            var response = await _trafficLightService.UpdateTrafficLightConfiguration(trafficLightConfiguration);
            return Ok(response);
        }
    }
}