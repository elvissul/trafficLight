using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Infrastructure.Models;
using TrafficLight.Infrastructure.Services.Interfaces;

namespace TrafficLight.Infrastructure.Services
{
    public class TrafficLightService : ITrafficLightService
    {

        private TrafficLightClass TrafficLight = new TrafficLightClass();
        public async Task<TrafficLightClass> GetTrafficLightConfiguration() 
        {
            return TrafficLight;
        }

        public int SetGreenTime(int timeInSeconds)
        {
            throw new NotImplementedException();
        }

        public int SetRedTime(int timeInSeconds)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResponse<TrafficLightClass>> UpdateTrafficLightConfiguration(TrafficLightClass trafficLightCofiguration)
        {
            var result = new ServiceResponse<TrafficLightClass>
            {
                Data = new TrafficLightClass()
            };

            //var error = Validate(trafficLightCofiguration);

            //if (error != null)
            //{
            //    result.Success = false;
            //    result.Message = error;
            //    return result;
            //}
                 
            TrafficLight = trafficLightCofiguration;
            result.Data = TrafficLight;

            return result;
        }

        //private static string Validate(TrafficLightModel trafficLightCofiguration)
        //{
        //    if (trafficLightCofiguration.PedestrianCrossingTime < 0 || trafficLightCofiguration.PedestrianCrossingTime == null)
        //    {
        //        return "not valid PedestrianCrossingTime";
        //    }
        //    if (trafficLightCofiguration.RedLightTime < 0 || trafficLightCofiguration.RedLightTime == null)
        //    {
        //        return "not valid RedLightTime";
        //    }
        //    if (trafficLightCofiguration.GreenLightTime < 0 || trafficLightCofiguration.GreenLightTime == null || trafficLightCofiguration.GreenLightTime > trafficLightCofiguration.GreenLightTimeMax)
        //    {
        //        return "not valid GreenLightTime";
        //    }
        //    if (trafficLightCofiguration.GreenLightTimeMax < 0 || trafficLightCofiguration.GreenLightTimeMax == null || trafficLightCofiguration.GreenLightTimeMax < trafficLightCofiguration.GreenLightTime)
        //    {
        //        return "not valid GreenLightTimeMax";
        //    }
        //    return null;
        //}
    }
}
