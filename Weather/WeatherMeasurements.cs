namespace Weather
{
    public class WeatherMeasurements
    {
        public DateTime date { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);
    }
}
