namespace Weather
{
    public interface IMeasurementSource
    {
        WeatherMeasurement GetCurrentMeasurement();
    }


    public class MeasurementSource : IMeasurementSource
    {
        async Task<Temperature> RunClient()
        {
            string _address = "https://localhost:57826/temperature";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var temperature = await response.Content.ReadAsAsync<Temperature>();
            return temperature;
        }
        public WeatherMeasurement GetCurrentMeasurement()
        {
            Temperature t = RunClient().Result;
            WeatherMeasurement w = new WeatherMeasurement();
            w.Date = t.Date;
            w.TemperatureC = t.TemperatureC;
            Random rand = new Random();

            for (int i = 1; i < 4; i++)
            {
                WeatherPrediction p = new WeatherPrediction();
                p.TemperatureCPrediction = rand.Next(-20, 50);
                var hour = (w.Date.Hour+i) % 24;
                p.Time = new TimeSpan(hour, 0, 0);
                w.WeatherPredictions.Add(p);

            }

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
