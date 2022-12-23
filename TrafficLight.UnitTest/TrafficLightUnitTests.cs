using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System.Linq.Expressions;
using TrafficLight.Api.Controllers;
using TrafficLight.Infrastructure.Models;
using TrafficLight.Infrastructure.Services;
using TrafficLight.Infrastructure.Services.Interfaces;
using Xunit;

namespace TrafficLight.UnitTest
{
    public class TrafficLightUnitTests
    {
        private readonly static Fixture _fixture = new();
        public Mock<ITrafficLightService> _mockTrafficLightService = new Mock<ITrafficLightService>();
        private readonly TrafficLightController _sut;
       
        public TrafficLightUnitTests()
        {
            _fixture.Customize(new AutoMoqCustomization());
            _mockTrafficLightService = _fixture.Freeze<Mock<ITrafficLightService>>();
            _sut = _fixture.Build<TrafficLightController>().OmitAutoProperties().Create();
        }

        [Fact]
        public async void GetTrafficLightConfiguration_shouldPass()
        {
            _mockTrafficLightService.Setup(x => x.GetTrafficLightConfiguration()).ReturnsAsync(new TrafficLightModel());

            var actual = await _sut.GetTrafficLihtConfiguration();

            var result = actual as OkObjectResult;
            result.Value.Equals(new TrafficLightModel());
        }
        [Fact]
        public async void UpdateTrafficLightConfiguration_shouldPass()
        {
            _mockTrafficLightService.Setup(x => x.UpdateTrafficLightConfiguration(It.IsAny<TrafficLightModel>())).ReturnsAsync(new ServiceResponse<TrafficLightModel>());

            var actual = await _sut.UpdateTrafficLihtConfiguration(new TrafficLightModel());

            var result = actual as OkObjectResult;
            result.Value.Equals(new TrafficLightModel());
        }
        [Fact]
        public async void UpdateTrafficLightConfiguration_shouldReturnErrorMessage()
        {
            var response = new ServiceResponse<TrafficLightModel>();
            response.Message = "not valid GreenLightTime";
            response.Success = false;
            response.Data = null;
            _mockTrafficLightService.Setup(x => x.UpdateTrafficLightConfiguration(It.IsAny<TrafficLightModel>())).ReturnsAsync(response);

            var actual = await _sut.UpdateTrafficLihtConfiguration(It.IsAny<TrafficLightModel>());

            var result = actual as OkObjectResult;
           

            result.Value.Equals(response);
        }
    }
}
