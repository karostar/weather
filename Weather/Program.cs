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

string _address = "https://localhost:57826/temperature";
HttpClient client = new HttpClient();

async Task<Temperature> RunClient()
{
    HttpResponseMessage response = await client.GetAsync(_address);
    response.EnsureSuccessStatusCode();
    var temperature = await response.Content.ReadAsAsync<Temperature>();
    return temperature;
}

//returns an entry by id
app.MapGet("/measurements/{id}", async (ContextDb db, int id) =>
{
    var data = from ContextDb
               in db.WeatherMeasurements
               where ContextDb.WeatherMeasurementId == id
               select ContextDb;
    return data;
});

//returns historic entries starting with chosen date
app.MapGet("/measurements/from", async (ContextDb db, [FromQuery] DateTime? from) => 
{ 
    var data = from ContextDb
               in db.WeatherMeasurements 
               where !@from.HasValue || ContextDb.Date > @from
               select ContextDb;
    return data;
});

//returns historic entries between two dates
app.MapGet("/measurements/timeperiod", async (ContextDb db, [FromQuery] DateTime? from, 
    DateTime? to) =>
{
    var data = from ContextDb
               in db.WeatherMeasurements
               where !@from.HasValue || !to.HasValue 
               || ContextDb.Date > @from && ContextDb.Date < to
               select ContextDb;
    return data;
});

//returns historic entries before chosen date
app.MapGet("/measurements/to", async (ContextDb db, [FromQuery] DateTime? to) =>
{
    var data = from ContextDb
               in db.WeatherMeasurements
               where !to.HasValue || ContextDb.Date < to
               select ContextDb;
    return data;
});

//creates a new entry
app.MapGet("/measurements/current", async (IMeasurementSource source, ContextDb db) => 
{
    Temperature t = RunClient().Result;
    WeatherMeasurement w = new WeatherMeasurement();
    w.Date = t.Date;
    w.TemperatureC = t.TemperatureC;
    //var w = source.GetCurrentMeasurement();
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
db.Database.Migrate();

app.Run();
