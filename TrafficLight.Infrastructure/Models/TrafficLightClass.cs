using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLight.Infrastructure.Models
{
    public class TrafficLightClass
    {
        public TrafficLightProperies DurationProperties { get; set; }

        public List<ColorLight> CurrentLights { get; set; }

        public bool PedestrianRequest { get; set; }

        public void ChangeLight(List<ColorLight> newColorLights ) {
            this.CurrentLights = newColorLights;
        }

        public TrafficLightProperies UpdateTrafficLightProperies(TrafficLightProperies newDurationProperies)
        {
            var isValid = ValidateDurationProperties(newDurationProperies);
            if (isValid)
            {
                return newDurationProperies;
            }
            else {
                throw new Exception("Validation Error");
            }
        }


        private bool ValidateDurationProperties(TrafficLightProperies trafficLightProperies)
        {
            if (trafficLightProperies.PedestrianCrossingTime < 0 || trafficLightProperies.PedestrianCrossingTime == null)
            {
                return false;
            }
            if (trafficLightProperies.RedLightTime < 0 || trafficLightProperies.RedLightTime == null)
            {
                return false;
            }
            if (trafficLightProperies.GreenLightTime < 0 || trafficLightProperies.GreenLightTime == null || trafficLightProperies.GreenLightTime > trafficLightProperies.GreenLightTimeMax)
            {
                return false;
            }
            if (trafficLightProperies.GreenLightTimeMax < 0 || trafficLightProperies.GreenLightTimeMax == null || trafficLightProperies.GreenLightTimeMax < trafficLightProperies.GreenLightTime)
            {
                return false;
            }
            return true;
        }
    }
    public class TrafficLightProperies {
        public int RedLightTime { get; set; } = 120;
        public int YellowLightTime { get; set; } = 5;
        public int GreenLightTime { get; set; } = 120;
        public int GreenLightTimeMax { get; set; } = 360;
        public int PedestrianCrossingTime { get; set; } = 30;
    }
    public enum ColorLight
    {
        red,
        yellow,
        green,
    }
    
}
