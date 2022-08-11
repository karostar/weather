namespace Weather.Tests;

public class Test
{
    [Fact]
    public void GetCurrentMeasurement_ShouldReturnWeatherMeasurement()
    {
        //Arrange
        IMeasurementSource source = new RandomMeasurementSource();

        //Act
        var weatherMeasurement = source.GetCurrentMeasurement();

        //Assert
        Assert.NotNull(weatherMeasurement);
    }

    [Fact]
    public void GetMeasurementById_ShouldReturnWeatherMeasurement()
    {
        //Arrange

        //Act
        
        //Assert
        
    }
}