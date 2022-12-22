using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Infrastructure.Enums;

namespace TrafficLight.Infrastructure.Models
{
    public class TrafficLightModel
    {
        public Color TrafficLightColor { get; set; }
        public int RedLightTime { get; set; } = 120;
        public int YellowLightTime { get; set; } = 5;
        public int GreenLightTime { get; set; } = 120;
        public int GreenLightTimeMax { get; set; } = 360;
        public int PedestrianCrossingTime { get; set; } = 30;
    }
}
