namespace Weather
{
    public interface IMeasurementSource
    {
        WeatherMeasurements GetCurrentMeasurement();
    }

    public class RandomMeasurementSource : IMeasurementSource
    {
        public string Name => "Tralalala";

        public WeatherMeasurements GetCurrentMeasurement()
        {
            Random rand = new Random();
            DateTime dateTime = DateTime.Now;
            WeatherMeasurements w = new WeatherMeasurements();
            w.date = dateTime;
            w.temperatureC = rand.Next(-20, 50);
            return w;
        }
    }

    public class NullMeasurementSource : IMeasurementSource
    {
        public WeatherMeasurements GetCurrentMeasurement()
        {
            WeatherMeasurements w = new WeatherMeasurements();
            w.temperatureC = 0;
            return w;
        }
    }


    // Starajmy się być konsekwentni w nazywaniu i stosujmy sie do sugestii nazewnictwa
    // https://docs.microsoft.com/pl-pl/dotnet/csharp/fundamentals/coding-style/coding-conventions
    public class WeatherMeasurements
    {
        public int Id { get; set; }
        public DateTime date { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);
    }
}
