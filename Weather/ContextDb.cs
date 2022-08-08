using Microsoft.EntityFrameworkCore;

namespace Weather
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options) {}
        public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }
        public DbSet<WeatherPrediction> WeatherPredictions { get; set; }
    }
}
