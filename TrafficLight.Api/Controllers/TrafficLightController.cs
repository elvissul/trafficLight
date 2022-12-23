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

        [HttpGet]
        public async Task<IActionResult> GetTrafficLihtConfiguration()
        { 
            var trafficLightConfiguration = await _trafficLightService.GetTrafficLightConfiguration();
            return Ok(trafficLightConfiguration);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTrafficLihtConfiguration(TrafficLightModel trafficLightConfiguration)
        {
            var response = await _trafficLightService.UpdateTrafficLightConfiguration(trafficLightConfiguration);
            return Ok(response);
        }
    }
}