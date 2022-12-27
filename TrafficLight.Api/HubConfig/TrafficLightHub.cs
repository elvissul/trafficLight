using Microsoft.AspNetCore.SignalR;
using TrafficLight.Api.Services;
using TrafficLight.Api.Services.Interfaces;

namespace TrafficLight.Api.HubConfig
{
    public class TrafficLightHub : Hub
    {
        private readonly ITrafficLightService _trafficLightService;

        public TrafficLightHub(ITrafficLightService trafficLightService)
        {
            _trafficLightService = trafficLightService;
        }

        public void PedestrianRequestToCross()
        {
            _trafficLightService.PedestrianRequest = true;
        }
        public void StopTrafficLight()
        {
            _trafficLightService.StopTrafficLight();
        }
        
        public void ResumeTrafficLight()
        {
            _trafficLightService.ResumeTrafficLight();
        }
    }
}
