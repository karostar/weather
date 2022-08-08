namespace Weather
{
    public interface IMeasurementSource
    {
        WeatherMeasurement GetCurrentMeasurement();
    }

    public class MeasurementSource : IMeasurementSource
    {
        public WeatherMeasurement GetCurrentMeasurement()
        {
            Random rand = new Random();
            DateTime dateTime = DateTime.Now;
            WeatherMeasurement w = new WeatherMeasurement();
            w.Date = dateTime;
            w.TemperatureC = rand.Next(-20, 50);
            w.WindStrength = rand.Next(0, 100);
            return w;
        }
    }

    public class RandomMeasurementSource : IMeasurementSource
    {
        public WeatherMeasurement GetCurrentMeasurement()
        {
            Random rand = new Random();
            int year = rand.Next(2005, 2022);
            int month = rand.Next(1, 12);
            int day = rand.Next(1, 30);
            DateTime dateTime = new DateTime(year, month, day);
            WeatherMeasurement w = new WeatherMeasurement();
            w.Date = dateTime;
            w.TemperatureC = rand.Next(-20, 50);
            return w;
        }
    }

    public class NullMeasurementSource : IMeasurementSource
    {
        public WeatherMeasurement GetCurrentMeasurement()
        {
            WeatherMeasurement w = new WeatherMeasurement();
            w.TemperatureC = 0;
            return w;
        }
    }
}
