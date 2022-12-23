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

        private TrafficLightClass TrafficLight = new TrafficLightClass();

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<TrafficLightClass>>> GetTrafficLihtConfiguration()
        { 
            var trafficLightConfiguration = await _trafficLightService.GetTrafficLightConfiguration();
            return Ok(trafficLightConfiguration);
        }
        [HttpPost]
        public async Task<ActionResult<TrafficLightClass>> GetTrafficLihtConfiguration(TrafficLightClass trafficLightConfiguration)
        {
            var response = await _trafficLightService.UpdateTrafficLightConfiguration(trafficLightConfiguration);
            return Ok(response);
        }
        [HttpPost]

        public async Task StartTrafficLight()
        {
           
            
        }

        private static Timer aTimer;
        private static void StartTimer() { 
            
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

    }
}