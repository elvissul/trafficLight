using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Infrastructure.Models;

namespace TrafficLight.Infrastructure.Services.Interfaces
{
    public interface ITrafficLightService
    {
        public TrafficLightModel TrafficLight { get; set; }
        public Task<TrafficLightModel> GetTrafficLightConfiguration();
        public Task<ServiceResponse<TrafficLightModel>> UpdateTrafficLightConfiguration(TrafficLightModel trafficLightCofiguration);

    }
}
