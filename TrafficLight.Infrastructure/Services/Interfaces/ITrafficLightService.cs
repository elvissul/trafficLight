﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Infrastructure.Enums;
using TrafficLight.Infrastructure.Models;

namespace TrafficLight.Infrastructure.Services.Interfaces
{
    public interface ITrafficLightService
    {
        public int SetRedTime(int timeInSeconds);
        public int SetGreenTime(int timeInSeconds);
        public Task<TrafficLightModel> GetTrafficLightConfiguration();
        public Task<ServiceResponse<TrafficLightModel>> UpdateTrafficLightConfiguration(TrafficLightModel trafficLightCofiguration);

    }
}
