using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TrafficLight.Api.HubConfig;
using TrafficLight.Api.Models;
using TrafficLight.Api.Models.TrafficLightModels;
using TrafficLight.Api.Services;
using TrafficLight.Api.Services.Interfaces;

namespace TrafficLight.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLightController : ControllerBase
    {
        private readonly ITrafficLightService _trafficLightService;
        private readonly IHubContext<TrafficLightHub> _hub;
        private readonly ITrafficLightManager _timer;

        public TrafficLightController(ITrafficLightService trafficLightService, ITrafficLightManager timer)
        {
            _trafficLightService = trafficLightService;
            _timer = timer;
        }


        [HttpGet]
        public async Task<IActionResult> GetTrafficLihtConfiguration()
        { 
            var response = _trafficLightService.GetTrafficLightProperies();
            if (response.Success == false) {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTrafficLihtConfiguration(TrafficLightProperies trafficLightConfiguration)
        {
            var response = _trafficLightService.UpdateTrafficLightProperies(trafficLightConfiguration);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    
        [HttpGet("start")]
        public IActionResult StartTrafficLight()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(() => _trafficLightService.StartTrafficLight(_timer.Ticks));
            return Ok(new { Message = "Request Completed" });
        }
    }
}