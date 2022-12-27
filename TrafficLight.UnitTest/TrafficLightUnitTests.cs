using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Api.Controllers;
using TrafficLight.Api.Models;
using TrafficLight.Api.Models.TrafficLightModels;
using TrafficLight.Api.Services.Interfaces;
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
            _mockTrafficLightService.Setup(x => x.GetTrafficLightProperies()).Returns(new ServiceResponse<TrafficLightProperies>());

            var actual = await _sut.GetTrafficLihtConfiguration();

            var result = actual as OkObjectResult;
            result.Value.Equals(new ServiceResponse<TrafficLightProperies>());
        }
        [Fact]
        public async void UpdateTrafficLightConfiguration_shouldPass()
        {
            _mockTrafficLightService.Setup(x => x.UpdateTrafficLightProperies(It.IsAny<TrafficLightProperies>())).Returns(new ServiceResponse<TrafficLightProperies>());

            var actual = await _sut.UpdateTrafficLihtConfiguration(new TrafficLightProperies());

            var result = actual as OkObjectResult;
            result.Value.Equals(new TrafficLightProperies());
        }
        [Fact]
        public async void UpdateTrafficLightConfiguration_shouldReturnErrorMessage()
        {
            var response = new ServiceResponse<TrafficLightProperies>();
            response.Message = "not valid GreenLightTime";
            response.Success = false;
            response.Data = null;
            _mockTrafficLightService.Setup(x => x.UpdateTrafficLightProperies(It.IsAny<TrafficLightProperies>())).Returns(response);

            var actual = await _sut.UpdateTrafficLihtConfiguration(It.IsAny<TrafficLightProperies>());

            var result = actual as BadRequestObjectResult;

            result.Value.Equals(response);
        }
    }
}
