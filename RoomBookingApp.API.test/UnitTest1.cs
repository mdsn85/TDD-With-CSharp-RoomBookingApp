using Microsoft.Extensions.Logging;
using Moq;
using RoomBookingApp.API.Controllers;
using Shouldly;

namespace RoomBookingApp.API.test;

public class UnitTest1
{
    [Fact]
    public void Should_return_Forcast_Result()
    {
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();

        var controller = new WeatherForecastController(loggerMock.Object);

        var result = controller.Get();

        result.Count().ShouldBeGreaterThan(1);
        result.ShouldNotBeNull();

    }
}
