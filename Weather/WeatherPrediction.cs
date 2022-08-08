namespace Weather
{
    public class WeatherPrediction
    {
        public int WeatherPredictionId { get; set; }
        public int TemperatureCPrediction { get; set; }
        public TimeSpan Time { get; set; }
        public int WeatherMeasurementId { get; set; }
    }
}
