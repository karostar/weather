using System.ComponentModel.DataAnnotations;

namespace Weather
{
    public class WeatherMeasurement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int WindStrength { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        //public List<WeatherForecast> WeatherForecasts { get; set; }
    }
}
