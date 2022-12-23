using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrafficLight.Infrastructure.Models;
using TrafficLight.Infrastructure.Services.Interfaces;

namespace TrafficLight.Infrastructure.Services
{
    public class TrafficLightService : ITrafficLightService
    {

        //private TrafficLightModel TrafficLight = new TrafficLightModel();
        private TrafficLightModel _trafficLight = new TrafficLightModel();
        public TrafficLightModel TrafficLight  // read-write instance property
        {
            get => _trafficLight;
            set => _trafficLight = value;
        }

        public async Task<TrafficLightModel> GetTrafficLightConfiguration() 
        {
            return _trafficLight;
        }

        public async Task<ServiceResponse<TrafficLightModel>> UpdateTrafficLightConfiguration(TrafficLightModel trafficLightCofiguration)
        {
            var result = new ServiceResponse<TrafficLightModel>
            {
                Data = new TrafficLightModel()
            };

            var error = Validate(trafficLightCofiguration);

            if (error != null)
            {
                result.Success = false;
                result.Message = error;
                return result;
            }

            _trafficLight = trafficLightCofiguration;
            result.Data = _trafficLight;

            return result;
        }

        private static string Validate(TrafficLightModel trafficLightCofiguration)
        {
            if (trafficLightCofiguration.PedestrianCrossingTime < 0 || trafficLightCofiguration.PedestrianCrossingTime == null)
            {
                return "not valid PedestrianCrossingTime";
            }
            if (trafficLightCofiguration.RedLightTime < 0 || trafficLightCofiguration.RedLightTime == null)
            {
                return "not valid RedLightTime";
            }
            if (trafficLightCofiguration.GreenLightTime < 0 || trafficLightCofiguration.GreenLightTime == null || trafficLightCofiguration.GreenLightTime > trafficLightCofiguration.GreenLightTimeMax)
            {
                return "not valid GreenLightTime";
            }
            if (trafficLightCofiguration.GreenLightTimeMax < 0 || trafficLightCofiguration.GreenLightTimeMax == null || trafficLightCofiguration.GreenLightTimeMax < trafficLightCofiguration.GreenLightTime)
            {
                return "not valid GreenLightTimeMax";
            }
            return null;
        }
    }
}
