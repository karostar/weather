using Microsoft.EntityFrameworkCore;

namespace Weather
{
    public class WeatherMeasurementsDb :DbContext
    {
        public DbSet<WeatherMeasurements> weatherMeasurements { get; set; }
        public WeatherMeasurementsDb(DbContextOptions options) : base(options) { }
    }
}
