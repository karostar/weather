using Microsoft.EntityFrameworkCore;
using Weather.Models;

namespace Weather
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options) {}
        public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }
        public DbSet<WeatherPrediction> WeatherPredictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// konfiguracja relacji ...
            /// 
            modelBuilder.Entity<WeatherMeasurement>().OwnsMany(p => p.WeatherPredictions, a =>
            {
                a.WithOwner().HasForeignKey(p => p.ParentId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
