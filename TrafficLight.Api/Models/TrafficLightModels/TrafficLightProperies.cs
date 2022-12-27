using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLight.Api.Models.TrafficLightModels
{
    public class TrafficLightProperies
    {
        public int RedLightTime { get; set; } = 10;
        public int YellowLightTime { get; set; } = 2;
        public int GreenLightTime { get; set; } = 10;
        public int GreenLightTimeMax { get; set; } = 60;
        public int PedestrianRequestGreenTrsitionTime{ get; set; } = 6;
    }
}
