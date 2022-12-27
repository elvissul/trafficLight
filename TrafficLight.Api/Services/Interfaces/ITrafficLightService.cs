using TrafficLight.Api.Models.Enums;
using TrafficLight.Api.Models;
using TrafficLight.Api.Models.TrafficLightModels;

namespace TrafficLight.Api.Services.Interfaces
{
    public interface ITrafficLightService
    {
        //prop
        TrafficLightProperies TLProperties { get; set; }
        List<LightColors> CurrentLights { get; set; }
        bool PedestrianRequest { get; set; }

        //methods
        void StartTrafficLight(int ticks);
        void StopTrafficLight();
        void ResumeTrafficLight();
        void ChangeLight(List<LightColors> newColorLights);
        List<LightColors> GetCurrentActiveLights();
        ServiceResponse<TrafficLightProperies> UpdateTrafficLightProperies(TrafficLightProperies newDurationProperies);
        ServiceResponse<TrafficLightProperies> GetTrafficLightProperies();

    }
}
