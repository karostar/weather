using Microsoft.EntityFrameworkCore;

namespace Weather
{
    public class WeatherMeasurementDb : DbContext
    {
        public WeatherMeasurementDb(DbContextOptions options) : base(options) {}
        public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }
    }
}
