using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Weather.Tests;

public class Test
{
    [Fact]
    public void GetRandomMeasurement_ShouldReturnWeatherMeasurement()
    {
        //Arrange
        IMeasurementSource source = new RandomMeasurementSource();

        //Act
        var weatherMeasurement = source.GetCurrentMeasurement();

        //Assert
        Assert.NotNull(weatherMeasurement);
    }

    [Fact]
    public async void Get_ShouldReturnWeatherMeasurement()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/measurements/current");
        var data = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}