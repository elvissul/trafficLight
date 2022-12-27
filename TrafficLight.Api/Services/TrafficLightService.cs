using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Api.Models.Enums;
using TrafficLight.Api.Models;
using TrafficLight.Api.Models.TrafficLightModels;
using TrafficLight.Api.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using TrafficLight.Api.HubConfig;

namespace TrafficLight.Api.Services
{
    public class TrafficLightService : ITrafficLightService
    {
        private readonly IHubContext<TrafficLightHub> _hub;
        private readonly ITrafficLightManager _trafficLightManager;

        public TrafficLightService(IHubContext<TrafficLightHub> hub, ITrafficLightManager trafficLightManager)
        {
            _hub = hub;
            _trafficLightManager = trafficLightManager;
            this.TLProperties = new TrafficLightProperies();
            this.PedestrianRequestTrasitionToRedCounter = 0;
        }

        public TrafficLightProperies? TLProperties { get; set; }

        public List<LightColors> CurrentLights { get; set; } = new List<LightColors> { LightColors.Red };

        public bool PedestrianRequest { get; set; }

        public void ChangeLight(List<LightColors> newColorLights)
        {
            this.CurrentLights = newColorLights;
        }
        public List<LightColors> GetCurrentActiveLights() {
            return this.CurrentLights;
        }

        public ServiceResponse<TrafficLightProperies> GetTrafficLightProperies()
        {
            ServiceResponse<TrafficLightProperies> response = new ServiceResponse<TrafficLightProperies>();
            response.Data = this.TLProperties;
            if (this.TLProperties == null)
            {
                response.Success = false;
                response.Message = "Duration Properties are empty";
            }
            return response;
        }

        public ServiceResponse<TrafficLightProperies> UpdateTrafficLightProperies(TrafficLightProperies newDurationProperies)
        {
            var isValid = ValidateDurationProperties(newDurationProperies);
            ServiceResponse<TrafficLightProperies> response = new ServiceResponse<TrafficLightProperies>();
            if (isValid)
            {
                this.TLProperties = newDurationProperies;
                response.Data = this.TLProperties;
            }
            else
            {
                response.Success = false;
                response.Message = "Duration Properties are not valid";
            }
            return response;
        }
        private int PedestrianRequestTrasitionToRedCounter { get; set; }
        private bool ValidateDurationProperties(TrafficLightProperies trafficLightProperies)
        {
            if (trafficLightProperies.PedestrianRequestGreenTrsitionTime < 0 || trafficLightProperies.PedestrianRequestGreenTrsitionTime == null)
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

        public void StartTrafficLight(int ticks)
        {
            // pedestrianRequest is false,
            // pedestrianRequest is true but the minimum greenLight time is not passed yet
            // pedestrianRequest is true but for (PedestrianRequestGreenTrsitionTime) seconds the traffic light will be red
            if (this.PedestrianRequest == false ||
                  (this.PedestrianRequest == true && (ticks < (this.TLProperties.RedLightTime + this.TLProperties.GreenLightTime))) ||
                  (this.PedestrianRequest == true && (ticks > (this.TLProperties.RedLightTime + (this.TLProperties.GreenLightTimeMax - this.TLProperties.PedestrianRequestGreenTrsitionTime)))))
            {
                this.TrafficLightCycle(ticks);
            }
            //pedestrianRequest is true, minimum greenLightTime is passed and
            else
            {
                _hub.Clients.All.SendAsync("TransferData", this.GetCurrentActiveLights());
                if (this.PedestrianRequest == true && (this.PedestrianRequestTrasitionToRedCounter <= this.TLProperties.PedestrianRequestGreenTrsitionTime)
                    && (this.PedestrianRequestTrasitionToRedCounter >= (this.TLProperties.PedestrianRequestGreenTrsitionTime - this.TLProperties.YellowLightTime)))
                {
                    this.CurrentLights = new List<LightColors> { LightColors.Yellow };
                }
                else if (this.PedestrianRequest == true && this.CurrentLights.Contains(LightColors.Yellow) && this.PedestrianRequestTrasitionToRedCounter >= this.TLProperties.PedestrianRequestGreenTrsitionTime)
                {
                    this.CurrentLights = new List<LightColors> { LightColors.Red };
                    _trafficLightManager.Reset();
                    this.PedestrianRequest = false;
                    this.PedestrianRequestTrasitionToRedCounter = 0;

                }
                else { }

                this.PedestrianRequestTrasitionToRedCounter++;
            }

        }
        public void StopTrafficLight()
        {
            _trafficLightManager.Stop();
        }

        public void ResumeTrafficLight()
        {
            _trafficLightManager.Resume();
        }
        private void TrafficLightCycle(int ticks) {

            _hub.Clients.All.SendAsync("TransferData", this.GetCurrentActiveLights());
            if (ticks > (this.TLProperties.RedLightTime + this.TLProperties.GreenLightTimeMax + this.TLProperties.YellowLightTime))
            {
                this.CurrentLights = new List<LightColors> { LightColors.Red };
                _trafficLightManager.Reset();
                
            }
            //Traffic light : red
            if (ticks <= this.TLProperties.RedLightTime) {
                this.CurrentLights = new List<LightColors> { LightColors.Red };
            }

            //Traffic light : yellow to green
            if (ticks > (this.TLProperties.RedLightTime - this.TLProperties.YellowLightTime) 
                && (ticks <= this.TLProperties.RedLightTime))
            {
                this.CurrentLights = new List<LightColors> { LightColors.Red, LightColors.Yellow };
            }

            //Traffic light : green
            if ((ticks > this.TLProperties.RedLightTime)
                && (ticks <= (this.TLProperties.RedLightTime + this.TLProperties.GreenLightTimeMax)))
            {
                this.CurrentLights = new List<LightColors> { LightColors.Green};
            }

            //Traffic light : yellow to Red
            if (ticks > this.TLProperties.RedLightTime + this.TLProperties.GreenLightTimeMax) 
            {
                this.CurrentLights = new List<LightColors> { LightColors.Yellow };
            }
        }

    }
}
