namespace Weather
{
    public class WeatherMeasurementsDTO
    {
        public int Id { get; set; }
        public DateTime date { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);

        public WeatherMeasurementsDTO() { }
        public WeatherMeasurementsDTO(WeatherMeasurements measurement) =>
        (Id, date, temperatureC) = (measurement.Id, measurement.date, measurement.temperatureC);
    }
}
