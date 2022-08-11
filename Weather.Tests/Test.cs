using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Weather.Models;

namespace Weather.Tests;


public class IntegrationTests
{
    private WebApplicationFactory<Program> application;
    private HttpClient client;
    private StaticMeasurementSource source = new StaticMeasurementSource();

    public IntegrationTests()
    {
        // To te¿ jest Arrange
        application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType ==
        typeof(DbContextOptions<ContextDb>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddDbContext<ContextDb>(options =>
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
                   // services.AddSingleton<IMeasurementSource>(source);

                });
            });
        client = application.CreateClient();
    }

    [Fact]
    public async void Get_ShouldReturnWeatherMeasurement()
    {
        // Wywo³anie konstruktora
        // Arrange - przygotowujemy sobie œrodowisko testowe
        source.Value = 100;
        // Act - dzia³amy tak jak klient naszej us³ugi
        var response = await client.GetAsync("/measurements/current");
        var measurement = await response.Content.ReadAsAsync<WeatherMeasurement>();
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(100, measurement.TemperatureC);
    }

    [Fact]
    public async void Should_return_empty_history_on_start()
    {
        // Act - dzia³amy tak jak klient naszej us³ugi
        var response = await client.GetAsync("/measurements");
        var measurements = await response.Content.ReadAsAsync<WeatherMeasurement[]>();
        // Assert
        Assert.NotNull(measurements);
        Assert.Equal(0, measurements.Length);
    }
}

public class UnitTests
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


}