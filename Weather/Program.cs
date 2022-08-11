using System.Text.Json;
using System.Linq;
using Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

//configuration
var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<ContextDb>(options => 
options.UseSqlServer("Server=.\\SQLEXPRESS;Database=WeatherDatabase2;Trusted_Connection=True;"));
builder.Services.AddSingleton<IMeasurementSource, MeasurementSource>();

var app = builder.Build();
Random rand = new Random();

//returns an entry by id
app.MapGet("/measurements/{id}", async (ContextDb db, int id) =>
{
    var data = from ContextDb
               in db.WeatherMeasurements
               where ContextDb.WeatherMeasurementId == id
               select ContextDb;
    return data;
});

//returns historic entries between two dates
app.MapGet("/measurements", async (ContextDb db, [FromQuery] DateTime? from, 
    DateTime? to) =>
{
    var data = from measurment in db.WeatherMeasurements
               where !@from.HasValue || measurment.Date > @from.Value
               where !to.HasValue || measurment.Date < to.Value
               select measurment;
    return data;
});



//creates a new entry
app.MapGet("/measurements/current", async (IMeasurementSource source, ContextDb db) => 
{
    var w = source.GetCurrentMeasurement();
    await db.WeatherMeasurements.AddAsync(w);
    await db.SaveChangesAsync();
    return Results.Created($"/weather/{w.WeatherMeasurementId}", w);
});

//returns weather predictions for chosen measurement
app.MapGet("measurements/{id}/predictions", async (ContextDb db, int id) =>
{
    var predictions = db.WeatherMeasurements
        .Include(predictions => predictions.WeatherPredictions)
        .Where(predictions => predictions.WeatherMeasurementId == id)
        .ToList()
        .SelectMany(w => w.WeatherPredictions)
        .ToList();
    return predictions;
}); 

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ContextDb>();
if (db.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
{
    db.Database.Migrate();
}

app.Run();

public partial class Program { }
